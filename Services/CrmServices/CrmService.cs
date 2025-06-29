using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.DTOs.CrmDTOs;
using Registeration.DTOs.SchoolDTOs;
using Registeration.Models;
using Registeration.Models.CrmModels;

namespace Registeration.Services.CrmServices
{
    /// <summary>
    /// CRM Service handles Region and School operations 
    /// including fetching from external CRM API and local DB save/check.
    /// </summary>
    public class CrmService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;

        public CrmService(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        // ================================================
        // REGION METHODS
        // ================================================

        /// <summary>
        /// Fetch all regions from external CRM API.
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
        /// Save a region to the local DB if it does not already exist.
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

        // ================================================
        // SCHOOL METHODS
        // ================================================

        /// <summary>
        /// Fetch schools from external CRM API by Region Id.
        /// </summary>
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
                Console.WriteLine("❌ Error fetching schools: " + ex.Message);
                return new List<SchoolDTO>();
            }
        }

        /// <summary>
        /// Save a school to the local DB if it does not already exist. (WITHOUT external ID)
        /// </summary>
        public async Task<SchoolDTO> SaveSchoolIfNotExistsAsync(string name, string address, Guid regionId)
        {
            var existing = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == name);

            if (existing != null)
                return MapSchoolToDTO(existing);

            var school = new School
            {
                Name = name,
                Address = address,
                RegionId = regionId
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return MapSchoolToDTO(school);
        }

        /// <summary>
        /// Save a school to the local DB if it does not already exist. (WITH external SchoolId)
        /// </summary>
        public async Task<SchoolDTO> SaveSchoolIfNotExistsAsync(Guid schoolId, string name, string address, Guid regionId)
        {
            var existing = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == name && s.RegionId == regionId);

            if (existing != null)
                return MapSchoolToDTO(existing);

            var school = new School
            {
                Id = schoolId,
                Name = name,
                Address = address,
                RegionId = regionId
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return MapSchoolToDTO(school);
        }

        /// <summary>
        /// Get school by name from local DB.
        /// </summary>
        public async Task<SchoolDTO?> GetSchoolByNameAsync(string name)
        {
            var existing = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == name);

            return existing != null ? MapSchoolToDTO(existing) : null;
        }

        /// <summary>
        /// Check if school is registered (Exists and IsRegistered == true).
        /// </summary>
        public async Task<bool> IsSchoolRegisteredAsync(string name)
        {
            var existing = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == name);

            return existing != null && existing.IsRegistered;
        }

        // ================================================
        // PRIVATE HELPERS
        // ================================================

        /// <summary>
        /// Map School entity to DTO.
        /// </summary>
        private static SchoolDTO MapSchoolToDTO(School school)
        {
            return new SchoolDTO
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                RegionId = school.RegionId,
                IsRegistered = school.IsRegistered
            };
        }
    }
}
