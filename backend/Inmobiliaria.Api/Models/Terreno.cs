namespace Inmobiliaria.Api.Models;

public class Terreno : Propiedad
{
    // Medidas y superficies
    public decimal? MedidasTerrenoAncho { get; set; }
    public decimal? MedidasTerrenoLargo { get; set; }

    // Requeridos conceptualmente para Terreno (validar en la API al crear/editar).
    public decimal? SuperficieTotal { get; set; }
    // Valores esperados: "m²" | "ha"
    public string? UnidadSuperficieTotal { get; set; }

    public decimal? SuperficieCubierta { get; set; }

    // Normativa y lote
    // Valores esperados: "Perimetral" | "Interno" | "Al golf" | "A la laguna" | "Al río" | "Otro"
    public string? TipoLote { get; set; }
    public decimal? FosPercent { get; set; }
    public decimal? FotPercent { get; set; }
    public decimal? SuperficieConstruibleMetros { get; set; }
    public string? TipoZona { get; set; }
    // Valores esperados: "Residencial" | "Comercial" | "Industrial"
    public string? TipoUsoTerreno { get; set; }
    public string? FormaTerreno { get; set; }
    public string? DetalleAcceso { get; set; }
    // Valores esperados: "Terreno completo" | "Fracción de manzana" | "Fracción rural"
    public string? TipoEstructuraTerreno { get; set; }

    // Listas (texto separado por comas)
    public string? Servicios { get; set; }
    public string? Instalaciones { get; set; }
}
