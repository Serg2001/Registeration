using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Responses;
using System.Net.Http.Json;
using System.Text.Json;
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
        //public async Task<CustomResponses.RegisterationResponse> RegisterAsync(RegisterDTO model)
        //{
        //    var response = await httpClient.PostAsJsonAsync("/api/account/register", model);
        //    var result = await response.Content.ReadFromJsonAsync<RegisterationResponse>();
        //    return result;
        //}


        public async Task<RegisterationResponse> RegisterAsync(RegisterDTO model, MailDTO mail)
        {

            var payload = new RegisterWithMailDTO
            {
                Register = model,
                Mail = mail
            };

            var response = await httpClient.PostAsJsonAsync("/api/account/register", payload);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // You can log or throw a more meaningful error
                throw new Exception($"API error: {response.StatusCode}, Content: {content}");
            }

            try
            {
                var result = JsonSerializer.Deserialize<RegisterationResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result!;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to parse JSON: {content}", ex);
            }
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
