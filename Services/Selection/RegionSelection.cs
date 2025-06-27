using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.Services.CrmServices;

namespace Registeration.Services.Selection
{
    public class RegionSelection
    {
        private readonly CrmService _crmService;
        private readonly List<RegionDTO> _regionList;

        public RegionSelection(CrmService crmService)
        {
            _crmService = crmService;
            _regionList = StaticRegions.Regions;
        }

        /// <summary>
        /// Search regions by name for autocomplete.
        /// </summary>
        public Task<IEnumerable<string>> SearchRegionsAsync(string input)
        {
            var results = string.IsNullOrWhiteSpace(input)
                ? _regionList.Select(r => r.Name)
                : _regionList
                    .Where(r => r.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                    .Select(r => r.Name);

            return Task.FromResult(results);
        }

        /// <summary>
        /// On region changed, save to local DB if needed and return RegionDTO.
        /// </summary>
        public Task<RegionDTO?> OnRegionChangedAsync(string selectedRegionName)
        {
            if (string.IsNullOrWhiteSpace(selectedRegionName))
                return Task.FromResult<RegionDTO?>(null);

            var selected = _regionList.FirstOrDefault(r =>
                string.Equals(r.Name?.Trim(), selectedRegionName?.Trim(), StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(selected);
        }

        /// <summary>
        /// Get selected region by name without DB operations.
        /// </summary>
        public RegionDTO? GetSelectedRegion(string selectedRegionName)
        {
            return _regionList.FirstOrDefault(r =>
                string.Equals(r.Name?.Trim(), selectedRegionName?.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
