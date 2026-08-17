namespace Inmobiliaria.Api.Models;

public class Local : Propiedad
{
    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }
    public decimal? SuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? SuperficieLocal { get; set; }
    public decimal? SuperficieEntrepiso { get; set; }
    public decimal? SuperficieSubsuelo { get; set; }
    public decimal? AlturaInterior { get; set; }

    // Atributos de la unidad
    public string? Plantas { get; set; }
    // Valores esperados: "Vía pública" | "Shopping" | "Galería"
    public string? Situado { get; set; }
    public string? UltimoDestino { get; set; }
    public string? Orientacion { get; set; }
    public string? Luminosidad { get; set; }

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
