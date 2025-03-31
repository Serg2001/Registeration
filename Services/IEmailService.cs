namespace Registeration.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody);
    }
}
