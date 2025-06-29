using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Registeration.DTOs;

namespace Registeration.Services.MailServices
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailConfig;
        public EmailService(IOptions<MailSettings> mailConfig)
        {
            _mailConfig = mailConfig.Value;
        }

        public async Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_mailConfig.FromEmail);
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = HTMLBody;
            smtp.Port = _mailConfig.Port;
            smtp.Host = _mailConfig.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailConfig.FromEmail, _mailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}
