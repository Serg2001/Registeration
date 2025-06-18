using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Registeration.DTOs;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;
using Registeration.Models;
using System.Text.Json.Serialization;


namespace Registeration.Services
{
    public class CrmSchoolService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;



        public CrmSchoolService(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
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
                Console.WriteLine("Failed to load schools: " + ex.Message);
                return new List<SchoolDTO>();
            }
        }


        // WITHOUT ID
        public async Task<SchoolDTO> SaveSchoolIfNotExistsAsync(string name, string address, Guid regionId)
        {
            var existing = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == name);

            if (existing != null)
                return new SchoolDTO
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    Address = existing.Address,
                    RegionId = existing.RegionId,
                    IsRegistered = existing.IsRegistered
                };

            var school = new School
            {
                Name = name,
                Address = address,
                RegionId = regionId
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return new SchoolDTO
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                RegionId = school.RegionId
            };
        }


        //School Save function with SchoolId
        public async Task<SchoolDTO> SaveSchoolIfNotExistsAsync(Guid schoolId, string name, string address, Guid regionId)
        {
            var existing = await _context.Schools.FirstOrDefaultAsync(s => s.Name == name && s.RegionId == regionId);
            if (existing != null)
                return new SchoolDTO
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    Address = existing.Address,
                    RegionId = existing.RegionId,
                    IsRegistered = existing.IsRegistered
                };

            var school = new School
            {
                Id = schoolId,
                Name = name,
                Address = address,
                RegionId = regionId
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return new SchoolDTO { Id = school.Id, Name = school.Name, Address = school.Address, RegionId = school.RegionId };
        }

        public async Task<SchoolDTO?> GetSchoolByNameAsync(string name)
        {
            var existing = await _context.Schools.FirstOrDefaultAsync(s => s.Name == name);
            return existing != null
                ? new SchoolDTO { Id = existing.Id, Name = existing.Name, Address = existing.Address, RegionId = existing.RegionId, IsRegistered = existing.IsRegistered }
                : null;
        }


    }
}
