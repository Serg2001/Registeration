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
    public class OtherLecturerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;

        public OtherLecturerService(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Fetch countries from the external CountryStateCity API.
        /// </summary>
        public async Task<List<Country>> GetAllCountriesAsync(CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient("CountryStateCity");
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", "S01GMlRPakZCMHVwUU9JTG51WVc4bG5kMEFWNFQ5aXB1SEx4SlRhVQ==");

            try
            {
                var response = await client.GetAsync("countries", cancellationToken);
                response.EnsureSuccessStatusCode();

                var countries = await response.Content.ReadFromJsonAsync<List<CountryStateCityCountry>>(cancellationToken: cancellationToken);
                if (countries == null)
                {
                    Console.WriteLine("⚠️ CountryStateCity API returned null or empty response for countries.");
                    return new List<Country>();
                }

                return countries
                    .Select(c => new Country
                    {
                        Name = new Name { Common = c.Name ?? string.Empty },
                        Cca2 = c.Iso2 ?? string.Empty
                    })
                    .OrderBy(c => c.Name.Common)
                    .ToList();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ HTTP error fetching countries: {ex.Message}");
                return new List<Country>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching countries: {ex.Message}");
                return new List<Country>();
            }
        }

        /// <summary>
        /// Fetch cities for a given country code from the external CountryStateCity API.
        /// </summary>
        public async Task<List<City>> GetCitiesByCountryAsync(string countryCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                Console.WriteLine("⚠️ Country code is null or empty.");
                return new List<City>();
            }

            var client = _httpClientFactory.CreateClient("CountryStateCity");
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", "S01GMlRPakZCMHVwUU9JTG51WVc4bG5kMEFWNFQ5aXB1SEx4SlRhVQ==");

            try
            {
                var response = await client.GetAsync($"countries/{countryCode}/cities", cancellationToken);
                response.EnsureSuccessStatusCode();

                var cities = await response.Content.ReadFromJsonAsync<List<CountryStateCityCity>>(cancellationToken: cancellationToken);
                if (cities == null)
                {
                    Console.WriteLine($"⚠️ CountryStateCity API returned null or empty response for cities in country {countryCode}.");
                    return new List<City>();
                }

                return cities
                    .Where(c => !string.IsNullOrEmpty(c.Name))
                    .Select(c => new City { Name = c.Name })
                    .OrderBy(c => c.Name)
                    .GroupBy(c => c.Name)
                    .Select(g => g.First())
                    .ToList();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ HTTP error fetching cities for country {countryCode}: {ex.Message}");
                return new List<City>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching cities for country {countryCode}: {ex.Message}");
                return new List<City>();
            }
        }


        /// <summary>
        /// Save a registration to the local DB.
        /// </summary>
        public async Task SaveAsync(OtherLecturer other, CancellationToken cancellationToken = default)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            try
            {
                _context.OtherLecturers.Add(other);
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"✅ Successfully saved registration with ID: {other.Id}");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"❌ Database error saving registration: {ex.Message}");
                if (ex.InnerException?.Message.Contains("duplicate") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    throw new InvalidOperationException("Դուք արդեն գրանցված եք։");
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving registration: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get a registration by ID.
        /// </summary>
        public async Task<OtherLecturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.OtherLecturers.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
                if (result == null)
                {
                    Console.WriteLine($"⚠️ Registration with ID {id} not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching registration with ID {id}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Delete a registration by ID.
        /// </summary>
        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var other = await _context.OtherLecturers.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
                if (other == null)
                {
                    Console.WriteLine($"⚠️ Registration with ID {id} not found for deletion.");
                    return false;
                }

                _context.OtherLecturers.Remove(other);
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"✅ Successfully deleted registration with ID: {id}");
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"❌ Database error deleting registration with ID {id}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error deleting registration with ID {id}: {ex.Message}");
                throw;
            }
        }


    }
}
