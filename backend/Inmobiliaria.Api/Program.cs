using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Models;
using Inmobiliaria.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Los enums (Operacion, Estado, etc.) viajan como texto ("Venta") en vez de número.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// En el hosting de producción (Render) la base y las fotos viven en un disco persistente
// aparte, configurado con "Storage:DataDir". En desarrollo esto no está seteado, así que
// todo sigue funcionando exactamente como antes (base en la carpeta del proyecto, fotos en
// wwwroot/uploads).
var dataDir = builder.Configuration["Storage:DataDir"];
if (dataDir is not null)
{
    // PhysicalFileProvider (más abajo) exige que la carpeta ya exista al arrancar; si no,
    // tira una excepción sin manejar en pleno startup y el proceso aborta de mala manera.
    Directory.CreateDirectory(Path.Combine(dataDir, "uploads", "propiedades"));
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = dataDir is null
        ? builder.Configuration.GetConnectionString("Default")
        : $"Data Source={Path.Combine(dataDir, "inmobiliaria.db")}";
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<JwtService>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Falta configurar Jwt:Key en appsettings.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();

// Límites por IP para los dos endpoints públicos más expuestos a abuso: login (fuerza
// bruta de contraseña) y consultas (spam, que además dispara un mail por cada envío).
// No afecta al resto de la API ni a un uso normal del sitio.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("login", httpContext => RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "sin-ip",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
        }));

    options.AddPolicy("consultas", httpContext => RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "sin-ip",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(10),
            QueueLimit = 0,
        }));
});

// Qué orígenes puede llamar a esta API. En producción se configura con la variable
// "Cors:AllowedOrigins" (separados por coma: la URL de Netlify, el dominio propio, etc.).
const string FrontendCors = "Frontend";
var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"]
    ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? ["http://localhost:4200"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCors, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Render (y cualquier hosting con proxy delante) termina el HTTPS antes de llegar a la app;
// esto le permite a ASP.NET Core enterarse de que el pedido original sí era HTTPS.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
});

app.UseHttpsRedirection();

// Sirve los archivos de wwwroot/uploads (las fotos de las propiedades) como URLs públicas.
app.UseStaticFiles();

// En producción, con "Storage:DataDir" configurado, las fotos viven fuera de wwwroot
// (en el disco persistente), así que necesitan su propio mapeo de archivos estáticos.
if (dataDir is not null)
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(dataDir, "uploads")),
        RequestPath = "/uploads",
    });
}

app.UseCors(FrontendCors);

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await AplicarMigracionesAsync(app);
await SeedAdminAsync(app);

app.Run();

// Aplica las migraciones pendientes al arrancar. En un hosting como Render no hay forma
// cómoda de correr "dotnet ef database update" a mano, así que la base se pone al día sola.
static async Task AplicarMigracionesAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Si todavía no existe ningún administrador, crea uno con los datos de appsettings
// (sección "AdminSeed"). Así arranca la app sin tener que insertar el usuario a mano.
static async Task SeedAdminAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (await db.AdminUsers.AnyAsync()) return;

    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var email = config["AdminSeed:Email"];
    var password = config["AdminSeed:Password"];
    var nombre = config["AdminSeed:Nombre"] ?? "Administradora";

    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
    {
        app.Logger.LogWarning(
            "No hay usuarios administradores y falta configurar AdminSeed:Email / AdminSeed:Password en appsettings. " +
            "No se pudo crear el usuario inicial.");
        return;
    }

    db.AdminUsers.Add(new AdminUser
    {
        Email = email,
        Nombre = nombre,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
    });

    await db.SaveChangesAsync();
    app.Logger.LogInformation("Usuario administrador inicial creado para {Email}.", email);
}
