using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Registeration.DTOs;

namespace Registeration.Services
{
    public class CrmSchoolService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CrmSchoolService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<SchoolDTO>> GetSchoolsByRegionAsync(Guid regionId)
        {
            var client = _httpClientFactory.CreateClient("CrmApi");

            try
            {
                var schools = await client.GetFromJsonAsync<List<SchoolDTO>>(
                    $"api/Platform/GetSchoolsByRegion?regionid={regionId}");

                return schools ?? new List<SchoolDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Failed to load schools: " + ex.Message);
                return new List<SchoolDTO>();
            }
        }
    }
}
