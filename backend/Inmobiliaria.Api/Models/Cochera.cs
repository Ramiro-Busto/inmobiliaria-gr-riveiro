namespace Inmobiliaria.Api.Models;

public class Cochera : Propiedad
{
    public decimal? SuperficieTotal { get; set; }

    // Valores esperados: "Simple" | "Doble" | "Múltiple"
    public string? TipoCochera { get; set; }
    // Valores esperados: "Planta baja" | "Subsuelo 1" | "Subsuelo 2" | "Subsuelo 3 o más"
    public string? Ubicacion { get; set; }

    public bool? Techada { get; set; }
    public bool? PortonAutomatico { get; set; }

    // Listas (texto separado por comas)
    public string? Servicios { get; set; }
}
