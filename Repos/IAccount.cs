using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Repos
{
    public interface IAccount
    {
        Task<RegisterationResponse> RegisterAsync(RegisterDTO model);
    }
}
