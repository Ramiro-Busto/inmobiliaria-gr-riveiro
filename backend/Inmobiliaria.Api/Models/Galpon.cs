namespace Inmobiliaria.Api.Models;

public class Galpon : Propiedad
{
    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }
    public decimal? SuperficieTerreno { get; set; }
    public decimal? SuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? FondoLibre { get; set; }
    public decimal? AreaOficinas { get; set; }
    public decimal? AreaDeposito { get; set; }

    // Constructivos y normativos
    public decimal? Fos { get; set; }
    public decimal? Fot { get; set; }
    public decimal? SuperficieConstruible { get; set; }
    public string? CodigoHabilitacion { get; set; }
    public decimal? AlturaEntrada { get; set; }
    public decimal? AlturaTecho { get; set; }
    public decimal? AnchoEntrada { get; set; }
    public int? CantidadColumnas { get; set; }
    public int? CantidadNaves { get; set; }
    public int? EspacioEstacionamiento { get; set; }
    public int? Cocheras { get; set; }

    // Tipos y clasificaciones
    // Valores esperados: "Almacén / depósito" | "Bodega comercial" | "Nave industrial"
    public string? TipoGalpon { get; set; }
    public string? TipoTecho { get; set; }
    // Valores esperados: "Cercha" | "Fibrocemento" | "Parabólica" | "A dos aguas" | "Bóveda de cañón" | "Chapa galvanizada" |
    // "Losa de hormigón" | "Diente de sierra" | "Zinc" | "Chapa acanalada" | "Losa" | "Estructura Astori"
    public string? TipoTechoIndustrial { get; set; }
    // Valores esperados: "Levadizo" | "Corredizo"
    public string? TipoPorton { get; set; }
    // Valores esperados: "De Red" | "Estacionario" | "De Cilindros"
    public string? TipoGas { get; set; }
    public string? Luminosidad { get; set; }

    // Ambientes
    public int? CantidadDormitorios { get; set; }
    public int? CantidadBanos { get; set; }

    // Listas (texto separado por comas)
    public string? ServiciosPropiedad { get; set; }
    public string? InstalacionesPropiedad { get; set; }
}
