using System.Net;
using System.Net.Mail;

namespace Inmobiliaria.Api.Services;

// Envía el mail de aviso usando un servidor SMTP configurado en appsettings (sección "Smtp").
// Si todavía no se configuró el SMTP (por ejemplo, en desarrollo), solo lo registra en el log
// y no rompe la creación de la consulta.
public class EmailService(IConfiguration config, ILogger<EmailService> logger) : IEmailService
{
    public async Task EnviarAvisoConsultaAsync(string asunto, string cuerpo)
    {
        var host = config["Smtp:Host"];
        var destinatario = config["Admin:NotifyEmail"];

        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(destinatario))
        {
            logger.LogWarning("SMTP no configurado: se omite el envío del mail '{Asunto}'.", asunto);
            return;
        }

        var puerto = int.Parse(config["Smtp:Port"] ?? "587");
        var remitente = config["Smtp:From"] ?? destinatario;

        using var mensaje = new MailMessage(remitente, destinatario, asunto, cuerpo);
        using var cliente = new SmtpClient(host, puerto)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(config["Smtp:User"], config["Smtp:Password"]),
        };

        await cliente.SendMailAsync(mensaje);
    }
}
