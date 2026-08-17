using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inmobiliaria.Api.Migrations
{
    /// <inheritdoc />
    public partial class QuitarTiposNoUsados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanosPorPiso",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_CantidadBanos",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_CantidadDormitorios",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_Instalaciones",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_Orientacion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_Servicios",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_SuperficieCubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Campo_SuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "CantidadHabitaciones",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "CantidadOficinas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_AmenitiesEdificio",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_CantidadBanos",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_CantidadDormitorios",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_InstalacionesPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_MedidasTerrenoAncho",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_MedidasTerrenoLargo",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_ServiciosEdificio",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_ServiciosPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_SuperficieCubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_SuperficieDescubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_SuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Departamento_TipoBalcon",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_CantidadBanos",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_CantidadDormitorios",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_CocherasCubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_CocherasDescubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_CocherasSemicubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_Edificacion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_InstalacionesPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_Orientacion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_Plantas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_ServiciosPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_SuperficieCubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_SuperficieDescubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_SuperficieTerreno",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "FondoDeComercio_SuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Forestacion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Galpon_AreaDeposito",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "MedioAcceso",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "NivelPlanta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_AguaCaliente",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_AmenitiesEdificio",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_CantidadBanos",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_CocherasCubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_CocherasDescubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_CocherasSemicubiertas",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_Disposicion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_InstalacionesPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_Luminosidad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_Orientacion",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_ServiciosEdificio",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_ServiciosPropiedad",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_SuperficieCubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_SuperficieDescubierta",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_SuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_TipoPiso",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Oficina_TipoVigilancia",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "OficinasPorPiso",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "PrecioHectareaMoneda",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "PrecioHectareaMonto",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Quinta_DetalleAcceso",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Quinta_DistanciaPavimentoKm",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Quinta_FormaTerreno",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Quinta_UnidadSuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TamanoVehiculo",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoAscensor",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoBano",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoCampo",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoCochera",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoGarage",
                table: "Propiedades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanosPorPiso",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Campo_CantidadBanos",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Campo_CantidadDormitorios",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Campo_Instalaciones",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Campo_Orientacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Campo_Servicios",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Campo_SuperficieCubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Campo_SuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadHabitaciones",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadOficinas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cochera_AmenitiesEdificio",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cochera_CantidadBanos",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cochera_CantidadDormitorios",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cochera_InstalacionesPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_MedidasTerrenoAncho",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_MedidasTerrenoLargo",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cochera_ServiciosEdificio",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cochera_ServiciosPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_SuperficieCubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_SuperficieDescubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_SuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento_TipoBalcon",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FondoDeComercio_CantidadBanos",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FondoDeComercio_CantidadDormitorios",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FondoDeComercio_CocherasCubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FondoDeComercio_CocherasDescubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FondoDeComercio_CocherasSemicubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FondoDeComercio_Edificacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FondoDeComercio_InstalacionesPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FondoDeComercio_Orientacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FondoDeComercio_Plantas",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FondoDeComercio_ServiciosPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FondoDeComercio_SuperficieCubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FondoDeComercio_SuperficieDescubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FondoDeComercio_SuperficieTerreno",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FondoDeComercio_SuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Forestacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Galpon_AreaDeposito",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedioAcceso",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NivelPlanta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_AguaCaliente",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_AmenitiesEdificio",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Oficina_CantidadBanos",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Oficina_CocherasCubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Oficina_CocherasDescubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Oficina_CocherasSemicubiertas",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_Disposicion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_InstalacionesPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_Luminosidad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_Orientacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_ServiciosEdificio",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_ServiciosPropiedad",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Oficina_SuperficieCubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Oficina_SuperficieDescubierta",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Oficina_SuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_TipoPiso",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oficina_TipoVigilancia",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OficinasPorPiso",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrecioHectareaMoneda",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioHectareaMonto",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quinta_DetalleAcceso",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quinta_DistanciaPavimentoKm",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quinta_FormaTerreno",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quinta_UnidadSuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TamanoVehiculo",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoAscensor",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoBano",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCampo",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCochera",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoGarage",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);
        }
    }
}
