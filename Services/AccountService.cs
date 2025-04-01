using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.Models;
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

        public async Task<bool> SocNumberExistsAsync(string socNumber)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<bool>($"/api/account/exists/{socNumber}");
            }
            catch
            {
                // You can log or handle error here if needed
                return false;
            }
        }
    }
}
