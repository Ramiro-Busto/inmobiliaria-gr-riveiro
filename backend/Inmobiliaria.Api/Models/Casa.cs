namespace Inmobiliaria.Api.Models;

public class Casa : Propiedad
{
    // Valores esperados: "Casa" | "Chalet" | "Duplex" | "Triplex" | "Departamento" | "PH"
    public string? Edificacion { get; set; }

    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }
    public decimal? SuperficieTerreno { get; set; }
    public decimal? SuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? FondoLibre { get; set; }

    // Estructura y equipamiento
    // Valores esperados: "Sin especificar" o 1-10
    public string? Plantas { get; set; }
    // Valores esperados: "Este" | "Oeste" | "Norte" | "Sur" | "Sudeste" | "Sudoeste" | "Noreste" | "Noroeste"
    public string? Orientacion { get; set; }
    public string? AguaCaliente { get; set; }
    public string? Calefaccion { get; set; }
    // Valores esperados: "Muy luminoso" | "Luminoso" | "Poco luminoso"
    public string? Luminosidad { get; set; }
    // Valores esperados: "24 horas" | "Diurno" | "Nocturno" | "Virtual"
    public string? TipoVigilancia { get; set; }
    public string? TipoPiso { get; set; }
    // Valores esperados: "Teja" | "Losa" | "Chapa" | "Pizarra" | "Teja colonial" | "Teja francesa"
    public string? TipoTecho { get; set; }

    // Cocheras y ambientes
    public int? CocherasCubiertas { get; set; }
    public int? CocherasDescubiertas { get; set; }
    public int? CocherasSemicubiertas { get; set; }
    public int? CantidadDormitorios { get; set; }
    public int? CantidadBanos { get; set; }

    // Listas guardadas como texto separado por comas (ej. "Piscina,Parrilla,Seguridad")
    public string? Servicios { get; set; }
    public string? Instalaciones { get; set; }
}
