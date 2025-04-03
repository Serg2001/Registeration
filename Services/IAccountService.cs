using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Services
{
    public interface IAccountService
    {
        Task<bool> SocNumberExistsAsync(string socNumber);
        Task<RegisterationResponse> RegisterAsync(RegisterDTO model, MailDTO mail, UserDTO user);
    }
}
