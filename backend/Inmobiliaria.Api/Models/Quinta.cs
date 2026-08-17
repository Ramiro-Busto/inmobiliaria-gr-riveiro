namespace Inmobiliaria.Api.Models;

public class Quinta : Propiedad
{
    // Valores esperados: "Casa" | "Chalet"
    public string? Edificacion { get; set; }

    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }
    public decimal? SuperficieTerreno { get; set; }
    public decimal? SuperficieTotal { get; set; }
    // Valores esperados: "m²" | "ha"
    public string? UnidadSuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? FondoLibre { get; set; }

    // Acceso y lote
    public decimal? DistanciaPavimentoKm { get; set; }
    public string? FormaTerreno { get; set; }
    public string? DetalleAcceso { get; set; }

    // Detalles constructivos
    public string? Plantas { get; set; }
    public string? Orientacion { get; set; }
    public string? AguaCaliente { get; set; }
    public string? Calefaccion { get; set; }
    public string? Luminosidad { get; set; }
    public string? TipoPiso { get; set; }
    public string? TipoTecho { get; set; }

    // Ambientes y cocheras
    public int? CocherasCubiertas { get; set; }
    public int? CocherasDescubiertas { get; set; }
    public int? CocherasSemicubiertas { get; set; }
    public int? CantidadDormitorios { get; set; }
    public int? CantidadBanos { get; set; }

    // Listas (texto separado por comas)
    public string? Servicios { get; set; }
    public string? Instalaciones { get; set; }
}
