using System.Text.Json.Serialization;

namespace Inmobiliaria.Api.Models;

public class ImagenPropiedad
{
    public int Id { get; set; }

    public int PropiedadId { get; set; }

    // Nullable a propósito: si no, ASP.NET Core exige este campo en el JSON de entrada
    // (por más que esté ignorado), ya que infiere "requerido" de cualquier propiedad no-nullable.
    // La relación en sí sigue siendo obligatoria a nivel de base de datos por PropiedadId.
    [JsonIgnore]
    public Propiedad? Propiedad { get; set; }

    public required string Url { get; set; }

    // Define el orden de la galería; 0 = foto de portada.
    public int Orden { get; set; }
}
