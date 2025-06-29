namespace Registeration.Services.MailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody);
    }
}
