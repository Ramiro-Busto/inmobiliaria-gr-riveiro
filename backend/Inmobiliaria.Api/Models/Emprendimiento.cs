namespace Inmobiliaria.Api.Models;

// A diferencia del resto, no usa Operacion/Monto/Moneda de la base (se vende por unidad,
// con precio variable). Esos campos quedan sin usar (null) para las filas de este tipo.
public class Emprendimiento : Propiedad
{
    // Datos básicos específicos
    public string? Nombre { get; set; }
    public string? LeyendaComercial { get; set; }
    // Valores esperados: "En Pozo" | "En construcción" | "Terminado"
    public string? EtapaObra { get; set; }
    public string? PaginaWeb { get; set; }
    // Formato YYYY-MM-DD o MM/YYYY
    public string? FechaEntrega { get; set; }

    // Financiación
    public bool? ConFinanciacion { get; set; }
    public decimal? PorcentajeAnticipo { get; set; }
    public int? CantidadCuotas { get; set; }
    public string? Financia { get; set; }
    public string? DescripcionFinanciacion { get; set; }

    // Características del emprendimiento
    public int? DepartamentosPorPiso { get; set; }
    public int? CantidadPisos { get; set; }
    public int? CantidadDepartamentos { get; set; }
    public int? CantidadAscensores { get; set; }

    // Listas (texto separado por comas)
    public string? ServiciosPropiedad { get; set; }
    public string? InstalacionesPropiedad { get; set; }

    // Archivos adicionales
    public string? LogoEmprendimiento { get; set; }
    // URLs de planos, separadas por comas
    public string? PlanosUrls { get; set; }

    // Firmas responsables
    public string? FirmaConstruye { get; set; }
    public string? FirmaComercializa { get; set; }
    public string? FirmaDirige { get; set; }
    public string? FirmaAdministra { get; set; }
}
