using System.Security.Claims;
using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Dtos;
using Inmobiliaria.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, JwtService jwtService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var usuario = await db.AdminUsers.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
        {
            return Unauthorized("Email o contraseña incorrectos.");
        }

        var token = jwtService.GenerarToken(usuario);
        return new LoginResponse(token, usuario.Nombre, usuario.Email);
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> CambiarPassword(CambiarPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PasswordNueva) || request.PasswordNueva.Length < 6)
        {
            return BadRequest("La nueva contraseña tiene que tener al menos 6 caracteres.");
        }

        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (idClaim is null || !int.TryParse(idClaim, out var id))
        {
            return Unauthorized();
        }

        var usuario = await db.AdminUsers.FindAsync(id);
        if (usuario is null) return Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(request.PasswordActual, usuario.PasswordHash))
        {
            return BadRequest("La contraseña actual no es correcta.");
        }

        usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordNueva);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
