using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registeration.Data;
using Registeration.DTOs.SchoolDTOs;
using Registeration.Services.CrmServices;

namespace Registeration.Services.Selection
{
    public class SchoolSelection
    {
        private readonly CrmService _crmService;
        private List<SchoolDTO> _schoolList = new();
        private Guid _currentRegionId = Guid.Empty;
        private readonly AppDbContext _dbContext;

        public SchoolSelection(CrmService crmService, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _crmService = crmService;
        }

        /// <summary>
        /// Loads schools for a specific region into local memory cache.
        /// </summary>
        public async Task LoadSchoolsByRegionAsync(Guid regionId)
        {
            if (regionId == Guid.Empty)
            {
                _schoolList = new();
                _currentRegionId = Guid.Empty;
                return;
            }

            if (_currentRegionId == regionId && _schoolList.Any())
                return; // Already loaded for this region, skip loading

            var schools = await _crmService.GetSchoolsByRegionAsync(regionId);

            _schoolList = (schools ?? new List<SchoolDTO>())
                .ToList();

            _currentRegionId = regionId;
        }

        /// <summary>
        /// Provides search suggestions for schools within the current region.
        /// </summary>
        public Task<IEnumerable<SchoolDTO>> SearchSchoolsAsync(string input, Guid regionId)
        {
            if (regionId == Guid.Empty || _currentRegionId != regionId)
                return Task.FromResult(Enumerable.Empty<SchoolDTO>());

            var result = string.IsNullOrWhiteSpace(input)
                ? _schoolList
                : _schoolList.Where(s =>
                    (s.Name != null && s.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                );

            return Task.FromResult(result.AsEnumerable());
        }


        /// <summary>
        /// Provides search suggestions for schools within the current region.
        /// Returns list of SchoolDTO instead of string to avoid duplicate names issue.
        /// </summary>
        public Task<IEnumerable<SchoolDTO>> SearchSchoolsAsyncWithAddresses(string input, Guid regionId)
        {
            if (regionId == Guid.Empty || _currentRegionId != regionId)
                return Task.FromResult(Enumerable.Empty<SchoolDTO>());

            var result = string.IsNullOrWhiteSpace(input)
                ? _schoolList
                : _schoolList
                    .Where(s => s.Name.Contains(input, StringComparison.OrdinalIgnoreCase)
                             || s.Address.Contains(input, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Handles school selection logic:
        /// - Checks if school exists in current region
        /// - Checks if school is not already registered
        /// </summary>
        public async Task<(bool Success, SchoolDTO? School, string? ErrorMessage)> OnSchoolChangedAsync(
            string selectedSchoolName,
            string regionName,
            Guid regionId)
        {
            if (string.IsNullOrWhiteSpace(selectedSchoolName) || regionId == Guid.Empty)
                return (false, null, "Դպրոցը չի գտնվել։ Խնդրում ենք ընտրել դպրոց։");

            await LoadSchoolsByRegionAsync(regionId);

            var selected = _schoolList.FirstOrDefault(s =>
                string.Equals(s.Name?.Trim(), selectedSchoolName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (selected == null)
                return (false, null, "Դպրոցը չի գտնվել։ Խնդրում ենք ընտրել դպրոց ցանկից։");

            var existingSchool = await _crmService.GetSchoolByNameAsync(selected.Name);
            if (existingSchool != null && existingSchool.IsRegistered)
            {
                return (false, null, "Այս դպրոցն արդեն գրանցված է։");
            }

            return (true, selected, null);
        }

        public List<SchoolDTO> GetCurrentSchools()
        {
            return _schoolList;
        }
    }
}
