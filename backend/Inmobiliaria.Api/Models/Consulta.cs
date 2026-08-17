namespace Inmobiliaria.Api.Models;

// Mensajes recibidos desde los formularios de Contacto y Tasaciones.
// Al crearse, además de guardarse acá, se envía un mail de aviso al administrador.
public class Consulta
{
    public int Id { get; set; }

    // Nula cuando es una consulta general de Contacto (no ligada a una propiedad puntual).
    public int? PropiedadId { get; set; }
    public Propiedad? Propiedad { get; set; }

    public required string Nombre { get; set; }
    public string? Apellido { get; set; }
    public required string Email { get; set; }
    public string? Telefono { get; set; }
    public string? Mensaje { get; set; }

    // Solo se completa en el formulario de Tasaciones (combo "Tipo de propiedad").
    public TipoPropiedad? TipoPropiedad { get; set; }

    public TipoConsulta Tipo { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public bool Leida { get; set; }
}
