using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inmobiliaria.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Inmobiliaria.Api.Services;

public class JwtService(IConfiguration config)
{
    public string GenerarToken(AdminUser usuario)
    {
        var claves = config["Jwt:Key"]
            ?? throw new InvalidOperationException("Falta configurar Jwt:Key en appsettings.");

        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claves)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nombre),
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
