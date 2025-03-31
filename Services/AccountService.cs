using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Registeration.DTOs;
using Registeration.Responses;
using System.Net.Http.Json;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Services
{
    public class AccountService : IAccountService
    {

        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomResponses.RegisterationResponse> RegisterAsync(RegisterDTO model)
        {
            var response = await httpClient.PostAsJsonAsync("/api/account/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegisterationResponse>();
            return result;
        }
    }
}
