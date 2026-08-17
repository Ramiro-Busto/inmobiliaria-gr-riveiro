namespace Inmobiliaria.Api.Services;

public interface IEmailService
{
    Task EnviarAvisoConsultaAsync(string asunto, string cuerpo);
}
