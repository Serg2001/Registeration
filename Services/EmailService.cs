using System.Net.Mail;
using System.Net;

namespace Registeration.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"]),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:Username"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            message.To.Add(to);
            await smtpClient.SendMailAsync(message);
        }
    }
}
