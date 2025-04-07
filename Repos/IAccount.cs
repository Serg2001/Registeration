using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Repos
{
    public interface IAccount
    {
        Task<bool> SocNumberAndPassportExistsAsync(string socNumber, string passport);
        Task<bool> UsernameExistsAsync(string username);
        Task<RegisterationResponse> RegisterAsync(RegisterDTO model, MailDTO mail, UserDTO user);
    }
}
