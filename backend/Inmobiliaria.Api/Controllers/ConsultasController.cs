using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Dtos;
using Inmobiliaria.Api.Models;
using Inmobiliaria.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/consultas")]
public class ConsultasController(AppDbContext db, IEmailService emailService) : ControllerBase
{
    // Público: se usa desde los formularios de Contacto y Tasaciones. Máximo 5 envíos
    // cada 10 minutos por IP: frena el spam (cada consulta manda un mail) sin afectar
    // a alguien que manda una consulta real.
    [EnableRateLimiting("consultas")]
    [HttpPost]
    public async Task<ActionResult<Consulta>> Create(ConsultaCreateDto dto)
    {
        var consulta = new Consulta
        {
            PropiedadId = dto.PropiedadId,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Telefono = dto.Telefono,
            Mensaje = dto.Mensaje,
            TipoPropiedad = dto.TipoPropiedad,
            Tipo = dto.Tipo,
        };

        db.Consultas.Add(consulta);
        await db.SaveChangesAsync();

        var nombreCompleto = string.IsNullOrWhiteSpace(dto.Apellido) ? dto.Nombre : $"{dto.Nombre} {dto.Apellido}";

        var asunto = dto.Tipo == TipoConsulta.Tasacion
            ? $"Nueva solicitud de tasación de {nombreCompleto}"
            : $"Nuevo mensaje de contacto de {nombreCompleto}";

        var cuerpo = $"""
            Nombre: {nombreCompleto}
            Email: {dto.Email}
            Teléfono: {dto.Telefono ?? "(no informado)"}
            {(dto.TipoPropiedad is not null ? $"Tipo de propiedad: {dto.TipoPropiedad}" : "")}

            Mensaje:
            {dto.Mensaje ?? "(sin mensaje)"}
            """;

        // Si falla el envío del mail, la consulta ya quedó guardada igual: no se pierde el mensaje.
        await emailService.EnviarAvisoConsultaAsync(asunto, cuerpo);

        return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
    }

    // El resto de los endpoints son solo para la administradora, desde el panel.
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Consulta>>> GetAll()
    {
        return await db.Consultas
            .Include(c => c.Propiedad)
            .OrderByDescending(c => c.FechaCreacion)
            .ToListAsync();
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Consulta>> GetById(int id)
    {
        var consulta = await db.Consultas.Include(c => c.Propiedad).FirstOrDefaultAsync(c => c.Id == id);
        return consulta is null ? NotFound() : consulta;
    }

    [Authorize]
    [HttpPatch("{id:int}/leida")]
    public async Task<IActionResult> MarcarLeida(int id)
    {
        var consulta = await db.Consultas.FindAsync(id);
        if (consulta is null) return NotFound();

        consulta.Leida = true;
        await db.SaveChangesAsync();
        return NoContent();
    }
}
