namespace Inmobiliaria.Api.Models;

// Corresponde al tipo "Tipo casa PH" de la lista original.
public class PH : Propiedad
{
    // Valores esperados: "Casa" | "Chalet" | "Duplex" | "Triplex" | "PH"
    public string? Edificacion { get; set; }

    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }
    public decimal? SuperficieTerreno { get; set; }
    public decimal? SuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? FondoLibre { get; set; }

    // Detalles de la unidad
    public string? Plantas { get; set; }
    public string? Orientacion { get; set; }
    public string? Disposicion { get; set; }
    public string? Luminosidad { get; set; }
    public string? AguaCaliente { get; set; }
    public string? Calefaccion { get; set; }
    public string? TipoVigilancia { get; set; }
    public string? TipoPiso { get; set; }
    public string? TipoTecho { get; set; }

    // Cocheras y ambientes
    public int? CocherasCubiertas { get; set; }
    public int? CocherasDescubiertas { get; set; }
    public int? CocherasSemicubiertas { get; set; }
    public int? CantidadDormitorios { get; set; }
    public int? CantidadBanos { get; set; }

    // Listas (texto separado por comas)
    public string? ServiciosPropiedad { get; set; }
    public string? InstalacionesPropiedad { get; set; }
    public string? ServiciosEdificio { get; set; }
    public string? AmenitiesEdificio { get; set; }
}
