using System.Text;
using System.Text.Json.Serialization;
using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Models;
using Inmobiliaria.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

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

// Permite que el frontend de Angular (en desarrollo) llame a esta API.
const string AngularDevCors = "AngularDev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularDevCors, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
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

app.UseHttpsRedirection();

// Sirve los archivos de wwwroot/uploads (las fotos de las propiedades) como URLs públicas.
app.UseStaticFiles();

app.UseCors(AngularDevCors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await SeedAdminAsync(app);

app.Run();

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
