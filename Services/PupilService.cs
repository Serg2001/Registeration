using System.Net;
using System.Text.Json;
using Registeration.DTOs;

namespace Registeration.Services
{
    public class PupilService
    {
        //private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;

        public PupilService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("CRM");
        }

        public async Task<PupilInfo?> GetPupilBySocNumber(string SocNumber)
        {
         
            var response = await _client.GetStringAsync($"/api/Platform/GetPupilsBySocNumber?socnumber={SocNumber}");

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    return await response.Content.ReadFromJsonAsync<PupilInfo>();
            //}

            return null; // or throw an exception, or return a default object depending on your use case
        }
    }
}
