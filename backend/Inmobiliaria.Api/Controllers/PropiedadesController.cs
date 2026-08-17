using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/propiedades")]
public class PropiedadesController(AppDbContext db) : ControllerBase
{
    // Estados que se muestran en el sitio público. El resto (Borrador, Vendido, etc.)
    // solo los ve la administradora desde el panel.
    private static readonly EstadoPublicacion[] EstadosPublicos =
        [EstadoPublicacion.Vigente, EstadoPublicacion.Reservado];

    // GET api/propiedades?tipo=Casa&operacion=Venta&moneda=Dolares&ubicacion=Quilmes&precioMin=10000&precioMax=50000
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Propiedad>>> GetAll(
        [FromQuery] TipoPropiedad? tipo,
        [FromQuery] Operacion? operacion,
        [FromQuery] Moneda? moneda,
        [FromQuery] string? ubicacion,
        [FromQuery] decimal? precioMin,
        [FromQuery] decimal? precioMax)
    {
        var query = db.Propiedades
            .Include(p => p.Imagenes)
            .Where(p => EstadosPublicos.Contains(p.Estado))
            .AsQueryable();

        if (tipo is not null)
        {
            var tipoTexto = tipo.Value.ToString();
            query = query.Where(p => EF.Property<string>(p, "Tipo") == tipoTexto);
        }

        if (operacion is not null)
        {
            query = query.Where(p => p.Operacion == operacion);
        }

        if (moneda is not null)
        {
            query = query.Where(p => p.Moneda == moneda);
        }

        if (!string.IsNullOrWhiteSpace(ubicacion))
        {
            query = query.Where(p => p.PartidoLocalidad.Contains(ubicacion) || p.BarrioCiudad.Contains(ubicacion));
        }

        if (precioMin is not null)
        {
            query = query.Where(p => p.Monto >= precioMin);
        }

        if (precioMax is not null)
        {
            query = query.Where(p => p.Monto <= precioMax);
        }

        return await query.ToListAsync();
    }

    // Ve todas las propiedades sin importar el estado (incluye Borrador, Vendido, etc).
    // La usa el panel de administración.
    [Authorize]
    [HttpGet("admin")]
    public async Task<ActionResult<IEnumerable<Propiedad>>> GetAllParaAdmin()
    {
        return await db.Propiedades.Include(p => p.Imagenes).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Propiedad>> GetById(int id)
    {
        var propiedad = await db.Propiedades
            .Include(p => p.Imagenes)
            .FirstOrDefaultAsync(p => p.Id == id && EstadosPublicos.Contains(p.Estado));

        return propiedad is null ? NotFound() : propiedad;
    }

    // Igual que GetById, pero sin filtrar por estado: la usa el panel para poder
    // editar propiedades en Borrador, Suspendido, etc. que no se ven en el sitio público.
    [Authorize]
    [HttpGet("admin/{id:int}")]
    public async Task<ActionResult<Propiedad>> GetByIdParaAdmin(int id)
    {
        var propiedad = await db.Propiedades.Include(p => p.Imagenes).FirstOrDefaultAsync(p => p.Id == id);
        return propiedad is null ? NotFound() : propiedad;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Propiedad>> Create(Propiedad propiedad)
    {
        db.Propiedades.Add(propiedad);
        await db.SaveChangesAsync();

        var resultado = CreatedAtAction(nameof(GetById), new { id = propiedad.Id }, propiedad);
        // Sin esto, el JSON de respuesta se serializa según el tipo concreto (ej. Casa)
        // y no incluye el campo "tipo": lo forzamos a serializar como la clase base
        // Propiedad para que la respuesta sea consistente con la del resto de endpoints.
        resultado.DeclaredType = typeof(Propiedad);
        return resultado;
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Propiedad propiedad)
    {
        if (id != propiedad.Id) return BadRequest("El id de la ruta no coincide con el del cuerpo.");

        var existente = await db.Propiedades.Include(p => p.Imagenes).FirstOrDefaultAsync(p => p.Id == id);
        if (existente is null) return NotFound();

        // Copia los campos simples (título, precio, características, etc.) del objeto recibido
        // sobre el que ya está siendo trackeado por EF Core.
        db.Entry(existente).CurrentValues.SetValues(propiedad);

        // Las imágenes son una tabla aparte: hay que sincronizarlas a mano (sacar las que ya
        // no vienen, agregar las nuevas, actualizar el orden de las que se mantienen).
        foreach (var imagenActual in existente.Imagenes.ToList())
        {
            if (propiedad.Imagenes.All(i => i.Url != imagenActual.Url))
            {
                db.Imagenes.Remove(imagenActual);
            }
        }

        foreach (var imagenNueva in propiedad.Imagenes)
        {
            var imagenExistente = existente.Imagenes.FirstOrDefault(i => i.Url == imagenNueva.Url);
            if (imagenExistente is null)
            {
                existente.Imagenes.Add(new ImagenPropiedad { Url = imagenNueva.Url, Orden = imagenNueva.Orden });
            }
            else
            {
                imagenExistente.Orden = imagenNueva.Orden;
            }
        }

        await db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var propiedad = await db.Propiedades.FindAsync(id);
        if (propiedad is null) return NotFound();

        db.Propiedades.Remove(propiedad);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
