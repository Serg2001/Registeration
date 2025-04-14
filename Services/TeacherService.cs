using Registeration.DTOs;

namespace Registeration.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient _client;

        public TeacherService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("CRM");
        }

        public async Task<TeacherDto?> GetTeacherBySocNumber(string socNumber)
        {
            var response = await _client.GetFromJsonAsync<TeacherDto>(
                $"/api/Platform/GetTeacherBySocNumber?socnumber={socNumber}");

            return response;
        }
    }
}
