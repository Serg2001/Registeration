using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Registeration.DTOs;

namespace Registeration.Services
{
    public class CrmRegionService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CrmRegionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<RegionDTO>> GetAllRegionsAsync()
        {
            var client = _httpClientFactory.CreateClient("CrmPlatformApi");

            try
            {
                var regions = await client.GetFromJsonAsync<List<RegionDTO>>("api/Platform/GetAllRegions");
                return regions ?? new List<RegionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error fetching CRM regions: " + ex.Message);
                return new List<RegionDTO>();
            }
        }
    }
}
