using Inmobiliaria.Api.Data;
using Inmobiliaria.Api.Dtos;
using Inmobiliaria.Api.Services;
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
}
