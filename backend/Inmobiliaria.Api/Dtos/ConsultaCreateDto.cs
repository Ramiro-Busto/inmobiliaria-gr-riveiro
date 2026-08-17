using Inmobiliaria.Api.Models;

namespace Inmobiliaria.Api.Dtos;

// Lo que puede mandar el público desde los formularios de Contacto/Tasaciones.
// A propósito no incluye Id, FechaCreacion ni Leida: esos los controla el servidor.
public record ConsultaCreateDto(
    int? PropiedadId,
    string Nombre,
    string? Apellido,
    string Email,
    string? Telefono,
    string? Mensaje,
    TipoPropiedad? TipoPropiedad,
    TipoConsulta Tipo);
