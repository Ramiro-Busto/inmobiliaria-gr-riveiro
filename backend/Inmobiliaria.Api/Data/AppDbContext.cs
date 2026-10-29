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
                .HasValue<Emprendimiento>(nameof(TipoPropiedad.Emprendimiento))
                .HasValue<Cochera>(nameof(TipoPropiedad.Cochera));

            // Varios tipos declaran una propiedad con el mismo nombre (ej. "Servicios" en
            // Casa/Quinta/Terreno/Cochera, "SuperficieTotal" en casi todos). Por defecto, EF
            // Core decide sola a qué tipo le toca la columna "sin prefijo" y a cuáles les
            // agrega "Tipo_" adelante — y esa decisión puede cambiar sola cuando se agrega
            // o saca un tipo con el mismo nombre de propiedad, aunque ese tipo nuevo no
            // tenga nada que ver (así se descubrió este problema: al agregar Cochera con
            // su propio "Servicios", EF le sacó a Casa su columna "Servicios" de toda la
            // vida y le iba a asignar una columna nueva vacía "Casa_Servicios", perdiendo
            // de vista los datos ya cargados). Para que esto no vuelva a pasar, acá se fija
            // a mano el nombre de columna real de cada propiedad repetida, tal cual está
            // hoy en la base — así una migración futura nunca puede reasignar ninguna.
            modelBuilder.Entity<Casa>(t =>
            {
                t.Property(x => x.AguaCaliente).HasColumnName("Casa_AguaCaliente");
                t.Property(x => x.Calefaccion).HasColumnName("Casa_Calefaccion");
                t.Property(x => x.CantidadBanos).HasColumnName("Casa_CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("Casa_CantidadDormitorios");
                t.Property(x => x.CocherasCubiertas).HasColumnName("Casa_CocherasCubiertas");
                t.Property(x => x.CocherasDescubiertas).HasColumnName("Casa_CocherasDescubiertas");
                t.Property(x => x.CocherasSemicubiertas).HasColumnName("Casa_CocherasSemicubiertas");
                t.Property(x => x.Edificacion).HasColumnName("Casa_Edificacion");
                t.Property(x => x.FondoLibre).HasColumnName("Casa_FondoLibre");
                t.Property(x => x.Instalaciones).HasColumnName("Instalaciones");
                t.Property(x => x.Luminosidad).HasColumnName("Casa_Luminosidad");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("Casa_MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("Casa_MedidasTerrenoLargo");
                t.Property(x => x.Orientacion).HasColumnName("Casa_Orientacion");
                t.Property(x => x.Plantas).HasColumnName("Casa_Plantas");
                t.Property(x => x.Servicios).HasColumnName("Servicios");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Casa_SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("Casa_SuperficieDescubierta");
                t.Property(x => x.SuperficieTerreno).HasColumnName("Casa_SuperficieTerreno");
                t.Property(x => x.SuperficieTotal).HasColumnName("Casa_SuperficieTotal");
                t.Property(x => x.TipoPiso).HasColumnName("Casa_TipoPiso");
                t.Property(x => x.TipoTecho).HasColumnName("Casa_TipoTecho");
                t.Property(x => x.TipoVigilancia).HasColumnName("Casa_TipoVigilancia");
            });

            modelBuilder.Entity<Departamento>(t =>
            {
                t.Property(x => x.AguaCaliente).HasColumnName("Departamento_AguaCaliente");
                t.Property(x => x.AmenitiesEdificio).HasColumnName("Departamento_AmenitiesEdificio");
                t.Property(x => x.Calefaccion).HasColumnName("Departamento_Calefaccion");
                t.Property(x => x.CantidadBanos).HasColumnName("Departamento_CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("Departamento_CantidadDormitorios");
                t.Property(x => x.CocherasCubiertas).HasColumnName("Departamento_CocherasCubiertas");
                t.Property(x => x.CocherasDescubiertas).HasColumnName("Departamento_CocherasDescubiertas");
                t.Property(x => x.CocherasSemicubiertas).HasColumnName("Departamento_CocherasSemicubiertas");
                t.Property(x => x.Disposicion).HasColumnName("Departamento_Disposicion");
                t.Property(x => x.Edificacion).HasColumnName("Departamento_Edificacion");
                t.Property(x => x.FondoLibre).HasColumnName("Departamento_FondoLibre");
                t.Property(x => x.InstalacionesPropiedad).HasColumnName("Departamento_InstalacionesPropiedad");
                t.Property(x => x.Luminosidad).HasColumnName("Departamento_Luminosidad");
                t.Property(x => x.Orientacion).HasColumnName("Departamento_Orientacion");
                t.Property(x => x.Plantas).HasColumnName("Departamento_Plantas");
                t.Property(x => x.ServiciosEdificio).HasColumnName("Departamento_ServiciosEdificio");
                t.Property(x => x.ServiciosPropiedad).HasColumnName("Departamento_ServiciosPropiedad");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Departamento_SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("Departamento_SuperficieDescubierta");
                t.Property(x => x.SuperficieTotal).HasColumnName("Departamento_SuperficieTotal");
                t.Property(x => x.TipoPiso).HasColumnName("Departamento_TipoPiso");
                t.Property(x => x.TipoVigilancia).HasColumnName("Departamento_TipoVigilancia");
            });

            modelBuilder.Entity<Emprendimiento>(t =>
            {
                t.Property(x => x.InstalacionesPropiedad).HasColumnName("Emprendimiento_InstalacionesPropiedad");
                t.Property(x => x.ServiciosPropiedad).HasColumnName("Emprendimiento_ServiciosPropiedad");
            });

            modelBuilder.Entity<Galpon>(t =>
            {
                t.Property(x => x.CantidadBanos).HasColumnName("Galpon_CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("Galpon_CantidadDormitorios");
                t.Property(x => x.FondoLibre).HasColumnName("Galpon_FondoLibre");
                t.Property(x => x.InstalacionesPropiedad).HasColumnName("Galpon_InstalacionesPropiedad");
                t.Property(x => x.Luminosidad).HasColumnName("Galpon_Luminosidad");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("Galpon_MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("Galpon_MedidasTerrenoLargo");
                t.Property(x => x.ServiciosPropiedad).HasColumnName("Galpon_ServiciosPropiedad");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Galpon_SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("Galpon_SuperficieDescubierta");
                t.Property(x => x.SuperficieTerreno).HasColumnName("Galpon_SuperficieTerreno");
                t.Property(x => x.SuperficieTotal).HasColumnName("Galpon_SuperficieTotal");
                t.Property(x => x.TipoTecho).HasColumnName("Galpon_TipoTecho");
            });

            modelBuilder.Entity<Local>(t =>
            {
                t.Property(x => x.AmenitiesEdificio).HasColumnName("Local_AmenitiesEdificio");
                t.Property(x => x.CantidadBanos).HasColumnName("Local_CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("Local_CantidadDormitorios");
                t.Property(x => x.CocherasCubiertas).HasColumnName("Local_CocherasCubiertas");
                t.Property(x => x.CocherasDescubiertas).HasColumnName("Local_CocherasDescubiertas");
                t.Property(x => x.CocherasSemicubiertas).HasColumnName("Local_CocherasSemicubiertas");
                t.Property(x => x.InstalacionesPropiedad).HasColumnName("Local_InstalacionesPropiedad");
                t.Property(x => x.Luminosidad).HasColumnName("Local_Luminosidad");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("Local_MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("Local_MedidasTerrenoLargo");
                t.Property(x => x.Orientacion).HasColumnName("Local_Orientacion");
                t.Property(x => x.Plantas).HasColumnName("Local_Plantas");
                t.Property(x => x.ServiciosEdificio).HasColumnName("Local_ServiciosEdificio");
                t.Property(x => x.ServiciosPropiedad).HasColumnName("Local_ServiciosPropiedad");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Local_SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("Local_SuperficieDescubierta");
                t.Property(x => x.SuperficieTotal).HasColumnName("Local_SuperficieTotal");
            });

            modelBuilder.Entity<PH>(t =>
            {
                t.Property(x => x.AguaCaliente).HasColumnName("AguaCaliente");
                t.Property(x => x.AmenitiesEdificio).HasColumnName("AmenitiesEdificio");
                t.Property(x => x.Calefaccion).HasColumnName("Calefaccion");
                t.Property(x => x.CantidadBanos).HasColumnName("CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("CantidadDormitorios");
                t.Property(x => x.CocherasCubiertas).HasColumnName("CocherasCubiertas");
                t.Property(x => x.CocherasDescubiertas).HasColumnName("CocherasDescubiertas");
                t.Property(x => x.CocherasSemicubiertas).HasColumnName("CocherasSemicubiertas");
                t.Property(x => x.Disposicion).HasColumnName("Disposicion");
                t.Property(x => x.Edificacion).HasColumnName("Edificacion");
                t.Property(x => x.FondoLibre).HasColumnName("FondoLibre");
                t.Property(x => x.InstalacionesPropiedad).HasColumnName("InstalacionesPropiedad");
                t.Property(x => x.Luminosidad).HasColumnName("Luminosidad");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("MedidasTerrenoLargo");
                t.Property(x => x.Orientacion).HasColumnName("Orientacion");
                t.Property(x => x.Plantas).HasColumnName("Plantas");
                t.Property(x => x.ServiciosEdificio).HasColumnName("ServiciosEdificio");
                t.Property(x => x.ServiciosPropiedad).HasColumnName("ServiciosPropiedad");
                t.Property(x => x.SuperficieCubierta).HasColumnName("SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("SuperficieDescubierta");
                t.Property(x => x.SuperficieTerreno).HasColumnName("SuperficieTerreno");
                t.Property(x => x.SuperficieTotal).HasColumnName("SuperficieTotal");
                t.Property(x => x.TipoPiso).HasColumnName("TipoPiso");
                t.Property(x => x.TipoTecho).HasColumnName("TipoTecho");
                t.Property(x => x.TipoVigilancia).HasColumnName("TipoVigilancia");
            });

            modelBuilder.Entity<Quinta>(t =>
            {
                t.Property(x => x.AguaCaliente).HasColumnName("Quinta_AguaCaliente");
                t.Property(x => x.Calefaccion).HasColumnName("Quinta_Calefaccion");
                t.Property(x => x.CantidadBanos).HasColumnName("Quinta_CantidadBanos");
                t.Property(x => x.CantidadDormitorios).HasColumnName("Quinta_CantidadDormitorios");
                t.Property(x => x.CocherasCubiertas).HasColumnName("Quinta_CocherasCubiertas");
                t.Property(x => x.CocherasDescubiertas).HasColumnName("Quinta_CocherasDescubiertas");
                t.Property(x => x.CocherasSemicubiertas).HasColumnName("Quinta_CocherasSemicubiertas");
                t.Property(x => x.DetalleAcceso).HasColumnName("DetalleAcceso");
                t.Property(x => x.Edificacion).HasColumnName("Quinta_Edificacion");
                t.Property(x => x.FondoLibre).HasColumnName("Quinta_FondoLibre");
                t.Property(x => x.FormaTerreno).HasColumnName("FormaTerreno");
                t.Property(x => x.Instalaciones).HasColumnName("Quinta_Instalaciones");
                t.Property(x => x.Luminosidad).HasColumnName("Quinta_Luminosidad");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("Quinta_MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("Quinta_MedidasTerrenoLargo");
                t.Property(x => x.Orientacion).HasColumnName("Quinta_Orientacion");
                t.Property(x => x.Plantas).HasColumnName("Quinta_Plantas");
                t.Property(x => x.Servicios).HasColumnName("Quinta_Servicios");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Quinta_SuperficieCubierta");
                t.Property(x => x.SuperficieDescubierta).HasColumnName("Quinta_SuperficieDescubierta");
                t.Property(x => x.SuperficieTerreno).HasColumnName("Quinta_SuperficieTerreno");
                t.Property(x => x.SuperficieTotal).HasColumnName("Quinta_SuperficieTotal");
                t.Property(x => x.TipoPiso).HasColumnName("Quinta_TipoPiso");
                t.Property(x => x.TipoTecho).HasColumnName("Quinta_TipoTecho");
                t.Property(x => x.UnidadSuperficieTotal).HasColumnName("UnidadSuperficieTotal");
            });

            modelBuilder.Entity<Terreno>(t =>
            {
                t.Property(x => x.DetalleAcceso).HasColumnName("Terreno_DetalleAcceso");
                t.Property(x => x.FormaTerreno).HasColumnName("Terreno_FormaTerreno");
                t.Property(x => x.Instalaciones).HasColumnName("Terreno_Instalaciones");
                t.Property(x => x.MedidasTerrenoAncho).HasColumnName("Terreno_MedidasTerrenoAncho");
                t.Property(x => x.MedidasTerrenoLargo).HasColumnName("Terreno_MedidasTerrenoLargo");
                t.Property(x => x.Servicios).HasColumnName("Terreno_Servicios");
                t.Property(x => x.SuperficieCubierta).HasColumnName("Terreno_SuperficieCubierta");
                t.Property(x => x.SuperficieTotal).HasColumnName("Terreno_SuperficieTotal");
                t.Property(x => x.UnidadSuperficieTotal).HasColumnName("Terreno_UnidadSuperficieTotal");
            });

            // Cochera es tipo nuevo: sus columnas no colisionan con datos existentes, pero se
            // fijan igual para que el día de mañana (si se agrega un tipo 10) tampoco la pueda
            // mover a ella un cambio ajeno.
            modelBuilder.Entity<Cochera>(t =>
            {
                t.Property(x => x.Servicios).HasColumnName("Cochera_Servicios");
                t.Property(x => x.SuperficieTotal).HasColumnName("Cochera_SuperficieTotal");
            });

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
