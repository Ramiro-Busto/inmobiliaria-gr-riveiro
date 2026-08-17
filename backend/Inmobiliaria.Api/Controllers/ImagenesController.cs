using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers;

// Sube archivos de imagen al propio servidor (carpeta wwwroot/uploads/propiedades),
// que después se sirven como archivos estáticos. Solo lo usa la administradora
// desde el panel al cargar/editar una propiedad.
[ApiController]
[Route("api/imagenes")]
public class ImagenesController(IWebHostEnvironment env) : ControllerBase
{
    private static readonly string[] ExtensionesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];
    private const long TamanioMaximoBytes = 8 * 1024 * 1024; // 8 MB

    [Authorize]
    [HttpPost("upload")]
    public async Task<ActionResult<object>> Upload(IFormFile archivo)
    {
        if (archivo.Length == 0)
        {
            return BadRequest("El archivo está vacío.");
        }

        if (archivo.Length > TamanioMaximoBytes)
        {
            return BadRequest("La imagen no puede pesar más de 8 MB.");
        }

        var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
        if (!ExtensionesPermitidas.Contains(extension))
        {
            return BadRequest("Formato no permitido. Usá JPG, PNG o WEBP.");
        }

        var carpeta = Path.Combine(env.WebRootPath, "uploads", "propiedades");
        Directory.CreateDirectory(carpeta);

        // Nombre generado (no el original) para evitar colisiones y problemas de seguridad.
        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
        var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

        await using (var destino = System.IO.File.Create(rutaCompleta))
        {
            await archivo.CopyToAsync(destino);
        }

        return new { url = $"/uploads/propiedades/{nombreArchivo}" };
    }
}
