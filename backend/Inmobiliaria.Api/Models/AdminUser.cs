namespace Inmobiliaria.Api.Models;

// Un único usuario administrador (tu mamá). No hay registro público ni roles.
public class AdminUser
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Nombre { get; set; }
}
