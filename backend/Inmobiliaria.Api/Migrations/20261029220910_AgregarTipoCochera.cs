using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inmobiliaria.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTipoCochera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cochera_Servicios",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cochera_SuperficieTotal",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PortonAutomatico",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Techada",
                table: "Propiedades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCochera",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Propiedades",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cochera_Servicios",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Cochera_SuperficieTotal",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "PortonAutomatico",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Techada",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoCochera",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Propiedades");
        }
    }
}
