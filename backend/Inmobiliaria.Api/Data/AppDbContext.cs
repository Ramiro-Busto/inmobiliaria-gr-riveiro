using Inmobiliaria.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Propiedad> Propiedades => Set<Propiedad>();
    public DbSet<ImagenPropiedad> Imagenes => Set<ImagenPropiedad>();
    public DbSet<Consulta> Consultas => Set<Consulta>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Todos los tipos de propiedad se guardan en una sola tabla ("Propiedades"),
        // con la columna "Tipo" indicando de qué subtipo es cada fila (TPH).
        modelBuilder.Entity<Propiedad>(entity =>
        {
            // El discriminador se guarda como texto ("Casa", "Terreno", etc.) y no como
            // número: si se guardara como número, sacar o reordenar un tipo del enum
            // TipoPropiedad correría los números y rompería los datos ya guardados.
            entity.HasDiscriminator<string>("Tipo")
                .HasValue<Casa>(nameof(TipoPropiedad.Casa))
                .HasValue<Departamento>(nameof(TipoPropiedad.Departamento))
                .HasValue<Galpon>(nameof(TipoPropiedad.Galpon))
                .HasValue<Local>(nameof(TipoPropiedad.Local))
                .HasValue<Quinta>(nameof(TipoPropiedad.Quinta))
                .HasValue<Terreno>(nameof(TipoPropiedad.Terreno))
                .HasValue<PH>(nameof(TipoPropiedad.PH))
                .HasValue<Emprendimiento>(nameof(TipoPropiedad.Emprendimiento));

            // Los enums se guardan como texto (ej. "Venta" en vez de 0) para que la
            // base de datos sea legible a simple vista.
            entity.Property(p => p.Operacion).HasConversion<string>();
            entity.Property(p => p.Estado).HasConversion<string>();
            entity.Property(p => p.Moneda).HasConversion<string>();
            entity.Property(p => p.VisibilidadDireccion).HasConversion<string>();
            entity.Property(p => p.EstadoPropiedad).HasConversion<string>();

            entity.Property(p => p.Monto).HasPrecision(18, 2);
            entity.Property(p => p.ExpensasMonto).HasPrecision(18, 2);

            entity.HasMany(p => p.Imagenes)
                .WithOne(i => i.Propiedad)
                .HasForeignKey(i => i.PropiedadId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.Property(c => c.Tipo).HasConversion<string>();
            entity.Property(c => c.TipoPropiedad).HasConversion<string>();

            entity.HasOne(c => c.Propiedad)
                .WithMany()
                .HasForeignKey(c => c.PropiedadId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<AdminUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
