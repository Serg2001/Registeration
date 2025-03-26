using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Services
{
    public interface IAccountService
    {
        Task<RegisterationResponse> RegisterAsync(RegisterDTO model);
    }
}
