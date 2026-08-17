using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inmobiliaria.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Operacion = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Moneda = table.Column<string>(type: "TEXT", nullable: true),
                    NoPublicarPrecio = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpensasMonto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    ExpensasTipo = table.Column<string>(type: "TEXT", nullable: true),
                    AptoProfesional = table.Column<bool>(type: "INTEGER", nullable: false),
                    AceptaMascotas = table.Column<bool>(type: "INTEGER", nullable: false),
                    PropiedadOcupada = table.Column<bool>(type: "INTEGER", nullable: true),
                    EsBarrioCerradoOrCountry = table.Column<bool>(type: "INTEGER", nullable: false),
                    ZonaGeografica = table.Column<string>(type: "TEXT", nullable: false),
                    PartidoLocalidad = table.Column<string>(type: "TEXT", nullable: false),
                    BarrioCiudad = table.Column<string>(type: "TEXT", nullable: false),
                    Calle = table.Column<string>(type: "TEXT", nullable: false),
                    NroCalle = table.Column<string>(type: "TEXT", nullable: true),
                    Piso = table.Column<string>(type: "TEXT", nullable: true),
                    Depto = table.Column<string>(type: "TEXT", nullable: true),
                    VisibilidadDireccion = table.Column<string>(type: "TEXT", nullable: false),
                    EntreCalle1 = table.Column<string>(type: "TEXT", nullable: true),
                    EntreCalle2 = table.Column<string>(type: "TEXT", nullable: true),
                    CercaDe = table.Column<string>(type: "TEXT", nullable: true),
                    Latitud = table.Column<double>(type: "REAL", nullable: true),
                    Longitud = table.Column<double>(type: "REAL", nullable: true),
                    EstadoPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    AntiguedadAnos = table.Column<int>(type: "INTEGER", nullable: true),
                    EsAEstrenar = table.Column<bool>(type: "INTEGER", nullable: true),
                    TipoCosta = table.Column<string>(type: "TEXT", nullable: true),
                    TipoVista = table.Column<string>(type: "TEXT", nullable: true),
                    TipoPendiente = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Campo_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    UnidadSuperficieTotal = table.Column<string>(type: "TEXT", nullable: true),
                    Campo_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    PrecioHectareaMonto = table.Column<decimal>(type: "TEXT", nullable: true),
                    PrecioHectareaMoneda = table.Column<string>(type: "TEXT", nullable: true),
                    TipoCampo = table.Column<string>(type: "TEXT", nullable: true),
                    Forestacion = table.Column<string>(type: "TEXT", nullable: true),
                    DistanciaPavimentoKm = table.Column<decimal>(type: "TEXT", nullable: true),
                    FormaTerreno = table.Column<string>(type: "TEXT", nullable: true),
                    DetalleAcceso = table.Column<string>(type: "TEXT", nullable: true),
                    Campo_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Campo_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadHabitaciones = table.Column<int>(type: "INTEGER", nullable: true),
                    Campo_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Campo_Servicios = table.Column<string>(type: "TEXT", nullable: true),
                    Campo_Instalaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_Edificacion = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_SuperficieTerreno = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_FondoLibre = table.Column<decimal>(type: "TEXT", nullable: true),
                    Casa_Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_AguaCaliente = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_Calefaccion = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_TipoVigilancia = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_TipoPiso = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_TipoTecho = table.Column<string>(type: "TEXT", nullable: true),
                    Casa_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Casa_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Casa_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Casa_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Casa_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Servicios = table.Column<string>(type: "TEXT", nullable: true),
                    Instalaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Cochera_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Cochera_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Cochera_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Cochera_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Cochera_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    TipoCochera = table.Column<string>(type: "TEXT", nullable: true),
                    TipoGarage = table.Column<string>(type: "TEXT", nullable: true),
                    TamanoVehiculo = table.Column<string>(type: "TEXT", nullable: true),
                    NivelPlanta = table.Column<string>(type: "TEXT", nullable: true),
                    MedioAcceso = table.Column<string>(type: "TEXT", nullable: true),
                    Cochera_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Cochera_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Cochera_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Cochera_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Cochera_ServiciosEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Cochera_AmenitiesEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_Edificacion = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Departamento_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Departamento_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Departamento_FondoLibre = table.Column<decimal>(type: "TEXT", nullable: true),
                    Departamento_Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_Disposicion = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_TipoBalcon = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_TipoPiso = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_AguaCaliente = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_Calefaccion = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_TipoVigilancia = table.Column<string>(type: "TEXT", nullable: true),
                    TipoEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriaEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    CantidadPisosEdificio = table.Column<int>(type: "INTEGER", nullable: true),
                    DeptosPorPiso = table.Column<int>(type: "INTEGER", nullable: true),
                    AscensoresPrincipales = table.Column<int>(type: "INTEGER", nullable: true),
                    AscensoresServicio = table.Column<int>(type: "INTEGER", nullable: true),
                    CocheraOptativa = table.Column<bool>(type: "INTEGER", nullable: true),
                    Departamento_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Departamento_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Departamento_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Departamento_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Departamento_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Departamento_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_ServiciosEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Departamento_AmenitiesEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    LeyendaComercial = table.Column<string>(type: "TEXT", nullable: true),
                    EtapaObra = table.Column<string>(type: "TEXT", nullable: true),
                    PaginaWeb = table.Column<string>(type: "TEXT", nullable: true),
                    FechaEntrega = table.Column<string>(type: "TEXT", nullable: true),
                    ConFinanciacion = table.Column<bool>(type: "INTEGER", nullable: true),
                    PorcentajeAnticipo = table.Column<decimal>(type: "TEXT", nullable: true),
                    CantidadCuotas = table.Column<int>(type: "INTEGER", nullable: true),
                    Financia = table.Column<string>(type: "TEXT", nullable: true),
                    DescripcionFinanciacion = table.Column<string>(type: "TEXT", nullable: true),
                    DepartamentosPorPiso = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadPisos = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadDepartamentos = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadAscensores = table.Column<int>(type: "INTEGER", nullable: true),
                    Emprendimiento_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Emprendimiento_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    LogoEmprendimiento = table.Column<string>(type: "TEXT", nullable: true),
                    PlanosUrls = table.Column<string>(type: "TEXT", nullable: true),
                    FirmaConstruye = table.Column<string>(type: "TEXT", nullable: true),
                    FirmaComercializa = table.Column<string>(type: "TEXT", nullable: true),
                    FirmaDirige = table.Column<string>(type: "TEXT", nullable: true),
                    FirmaAdministra = table.Column<string>(type: "TEXT", nullable: true),
                    FondoDeComercio_Edificacion = table.Column<string>(type: "TEXT", nullable: true),
                    FondoDeComercio_SuperficieTerreno = table.Column<decimal>(type: "TEXT", nullable: true),
                    FondoDeComercio_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    FondoDeComercio_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    FondoDeComercio_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    FondoDeComercio_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    FondoDeComercio_Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    FondoDeComercio_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    FondoDeComercio_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    FondoDeComercio_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    FondoDeComercio_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    FondoDeComercio_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    FondoDeComercio_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    FondoDeComercio_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Galpon_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_SuperficieTerreno = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_FondoLibre = table.Column<decimal>(type: "TEXT", nullable: true),
                    AreaOficinas = table.Column<decimal>(type: "TEXT", nullable: true),
                    Galpon_AreaDeposito = table.Column<decimal>(type: "TEXT", nullable: true),
                    Fos = table.Column<decimal>(type: "TEXT", nullable: true),
                    Fot = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieConstruible = table.Column<decimal>(type: "TEXT", nullable: true),
                    CodigoHabilitacion = table.Column<string>(type: "TEXT", nullable: true),
                    AlturaEntrada = table.Column<decimal>(type: "TEXT", nullable: true),
                    AlturaTecho = table.Column<decimal>(type: "TEXT", nullable: true),
                    AnchoEntrada = table.Column<decimal>(type: "TEXT", nullable: true),
                    CantidadColumnas = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadNaves = table.Column<int>(type: "INTEGER", nullable: true),
                    EspacioEstacionamiento = table.Column<int>(type: "INTEGER", nullable: true),
                    Cocheras = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoGalpon = table.Column<string>(type: "TEXT", nullable: true),
                    Galpon_TipoTecho = table.Column<string>(type: "TEXT", nullable: true),
                    TipoTechoIndustrial = table.Column<string>(type: "TEXT", nullable: true),
                    TipoPorton = table.Column<string>(type: "TEXT", nullable: true),
                    TipoGas = table.Column<string>(type: "TEXT", nullable: true),
                    Galpon_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Galpon_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Galpon_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Galpon_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Galpon_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Local_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Local_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Local_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Local_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Local_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieLocal = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieEntrepiso = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieSubsuelo = table.Column<decimal>(type: "TEXT", nullable: true),
                    AlturaInterior = table.Column<decimal>(type: "TEXT", nullable: true),
                    Local_Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    Situado = table.Column<string>(type: "TEXT", nullable: true),
                    UltimoDestino = table.Column<string>(type: "TEXT", nullable: true),
                    Local_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Local_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Local_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Local_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Local_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Local_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Local_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Local_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Local_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Local_ServiciosEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Local_AmenitiesEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Oficina_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Oficina_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    AreaDeposito = table.Column<decimal>(type: "TEXT", nullable: true),
                    Oficina_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_Disposicion = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_AguaCaliente = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_TipoVigilancia = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_TipoPiso = table.Column<string>(type: "TEXT", nullable: true),
                    TipoBalcon = table.Column<string>(type: "TEXT", nullable: true),
                    TipoAscensor = table.Column<string>(type: "TEXT", nullable: true),
                    TipoBano = table.Column<string>(type: "TEXT", nullable: true),
                    OficinasPorPiso = table.Column<string>(type: "TEXT", nullable: true),
                    BanosPorPiso = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Oficina_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Oficina_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadOficinas = table.Column<int>(type: "INTEGER", nullable: true),
                    Oficina_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Oficina_ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_ServiciosEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Oficina_AmenitiesEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Edificacion = table.Column<string>(type: "TEXT", nullable: true),
                    MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieTerreno = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    FondoLibre = table.Column<decimal>(type: "TEXT", nullable: true),
                    Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Disposicion = table.Column<string>(type: "TEXT", nullable: true),
                    Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    AguaCaliente = table.Column<string>(type: "TEXT", nullable: true),
                    Calefaccion = table.Column<string>(type: "TEXT", nullable: true),
                    TipoVigilancia = table.Column<string>(type: "TEXT", nullable: true),
                    TipoPiso = table.Column<string>(type: "TEXT", nullable: true),
                    TipoTecho = table.Column<string>(type: "TEXT", nullable: true),
                    CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiciosPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    InstalacionesPropiedad = table.Column<string>(type: "TEXT", nullable: true),
                    ServiciosEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    AmenitiesEdificio = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Edificacion = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_SuperficieTerreno = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_UnidadSuperficieTotal = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_SuperficieDescubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_FondoLibre = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_DistanciaPavimentoKm = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quinta_FormaTerreno = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_DetalleAcceso = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Plantas = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Orientacion = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_AguaCaliente = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Calefaccion = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Luminosidad = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_TipoPiso = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_TipoTecho = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_CocherasCubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Quinta_CocherasDescubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Quinta_CocherasSemicubiertas = table.Column<int>(type: "INTEGER", nullable: true),
                    Quinta_CantidadDormitorios = table.Column<int>(type: "INTEGER", nullable: true),
                    Quinta_CantidadBanos = table.Column<int>(type: "INTEGER", nullable: true),
                    Quinta_Servicios = table.Column<string>(type: "TEXT", nullable: true),
                    Quinta_Instalaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_MedidasTerrenoAncho = table.Column<decimal>(type: "TEXT", nullable: true),
                    Terreno_MedidasTerrenoLargo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Terreno_SuperficieTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Terreno_UnidadSuperficieTotal = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_SuperficieCubierta = table.Column<decimal>(type: "TEXT", nullable: true),
                    TipoLote = table.Column<string>(type: "TEXT", nullable: true),
                    FosPercent = table.Column<decimal>(type: "TEXT", nullable: true),
                    FotPercent = table.Column<decimal>(type: "TEXT", nullable: true),
                    SuperficieConstruibleMetros = table.Column<decimal>(type: "TEXT", nullable: true),
                    TipoZona = table.Column<string>(type: "TEXT", nullable: true),
                    TipoUsoTerreno = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_FormaTerreno = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_DetalleAcceso = table.Column<string>(type: "TEXT", nullable: true),
                    TipoEstructuraTerreno = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_Servicios = table.Column<string>(type: "TEXT", nullable: true),
                    Terreno_Instalaciones = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropiedadId = table.Column<int>(type: "INTEGER", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Mensaje = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Leida = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PropiedadId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Orden = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagenes_Propiedades_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Email",
                table: "AdminUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PropiedadId",
                table: "Consultas",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagenes_PropiedadId",
                table: "Imagenes",
                column: "PropiedadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Imagenes");

            migrationBuilder.DropTable(
                name: "Propiedades");
        }
    }
}
