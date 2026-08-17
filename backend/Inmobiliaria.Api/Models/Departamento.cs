namespace Inmobiliaria.Api.Models;

public class Departamento : Propiedad
{
    // Valores esperados: "Duplex" | "Triplex" | "Departamento" | "Penthouse" | "Piso" | "Semipiso" | "Loft"
    public string? Edificacion { get; set; }

    // Superficies
    public decimal? SuperficieTotal { get; set; }
    public decimal? SuperficieCubierta { get; set; }
    public decimal? SuperficieDescubierta { get; set; }
    public decimal? FondoLibre { get; set; }

    // Detalle del depto
    public string? Plantas { get; set; }
    public string? Orientacion { get; set; }
    // Valores esperados: "Al frente" | "Contrafrente" | "Interno" | "Lateral"
    public string? Disposicion { get; set; }
    public string? Luminosidad { get; set; }
    // Valores esperados: "Francés" | "Corrido" | "Terraza"
    public string? TipoBalcon { get; set; }
    public string? TipoPiso { get; set; }
    public string? AguaCaliente { get; set; }
    public string? Calefaccion { get; set; }
    public string? TipoVigilancia { get; set; }

    // Datos del edificio
    // Valores esperados: "Entre medianeras" | "Torre" | "Tipo block" | "Esquina" | "Antiguo" | "Inteligente" | "1era Categoría" | "Estandar"
    public string? TipoEdificio { get; set; }
    // Valores esperados: "Excelente" | "Bueno" | "Muy bueno" | "A Refaccionar" | "Regular" | "A Estrenar"
    public string? CategoriaEdificio { get; set; }
    public int? CantidadPisosEdificio { get; set; }
    public int? DeptosPorPiso { get; set; }
    public int? AscensoresPrincipales { get; set; }
    public int? AscensoresServicio { get; set; }

    // Cocheras y ambientes
    public bool? CocheraOptativa { get; set; }
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
