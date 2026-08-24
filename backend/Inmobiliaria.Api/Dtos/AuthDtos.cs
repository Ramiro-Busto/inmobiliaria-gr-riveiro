namespace Inmobiliaria.Api.Dtos;

public record LoginRequest(string Email, string Password);

public record LoginResponse(string Token, string Nombre, string Email);

public record CambiarPasswordRequest(string PasswordActual, string PasswordNueva);
