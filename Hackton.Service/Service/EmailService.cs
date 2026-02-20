using System.Net;
using System.Net.Mail;

namespace Hackton.Service.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(string destinatario, string link)
        {
            var smtp = new SmtpClient(_configuration["Email:Smtp"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["Email:User"],
                    _configuration["Email:Password"]
                ),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_configuration["Email:User"]),
                Subject = "Envio de Documentos - Matrícula",
                Body = $"Olá! Clique no link para completar seu cadastro:\n\n{link}",
                IsBodyHtml = false
            };

            mail.To.Add(destinatario);

            await smtp.SendMailAsync(mail);
        }
    }
}
