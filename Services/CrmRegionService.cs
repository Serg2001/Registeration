using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Data;

namespace Registeration.Services
{
    public class CrmRegionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;

        public CrmRegionService(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        /// <summary>
        /// Fetch regions from external CRM API.
        /// </summary>
        public async Task<List<RegionDTO>> GetAllRegionsAsync()
        {
            var client = _httpClientFactory.CreateClient("CrmApi");

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

        /// <summary>
        /// Save a region to the local DB if it doesn't already exist.
        /// </summary>
        public async Task<RegionDTO> SaveRegionIfNotExistsAsync(string name)
        {
            var existing = await _context.Regions.FirstOrDefaultAsync(r => r.Name == name);
            if (existing != null)
                return new RegionDTO { Id = existing.Id, Name = existing.Name };

            var region = new Region { Name = name };
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return new RegionDTO { Id = region.Id, Name = region.Name };
        }
    }
}
