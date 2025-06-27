using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registeration.DTOs;
using Registeration.Services.CrmServices;

namespace Registeration.Services.Selection
{
    public class SchoolSelection
    {
        private readonly CrmService _crmService;
        private List<SchoolDTO> _schoolList = new();
        private Guid _currentRegionId = Guid.Empty;

        public SchoolSelection(CrmService crmService)
        {
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
                .GroupBy(s => s.Name)
                .Select(g => g.First())
                .ToList();

            _currentRegionId = regionId;
        }

        /// <summary>
        /// Provides search suggestions for schools within the current region.
        /// </summary>
        public Task<IEnumerable<string>> SearchSchoolsAsync(string input, Guid regionId)
        {
            if (regionId == Guid.Empty || _currentRegionId != regionId)
                return Task.FromResult(Enumerable.Empty<string>());

            var result = string.IsNullOrWhiteSpace(input)
                ? _schoolList.Select(s => s.Name)
                : _schoolList
                    .Where(s => s.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                    .Select(s => s.Name);

            return Task.FromResult(result);
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
                return (false, null, "Այս դպրոցն արդեն գրանցված է։ Խնդրում ենք ընտրել մեկ այլ դպրոց։");
            }

            return (true, selected, null);
        }
    }
}
