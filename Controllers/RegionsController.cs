using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/regions/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRegionNames()
        {
            var regionNames = await _context.Regions
                .Select(r => r.Name)
                .ToListAsync();

            return Ok(regionNames);
        }

        // GET: api/regions/{regionId}/schools
        [HttpGet("{regionId}/schools")]
        public async Task<ActionResult<IEnumerable<string>>> GetSchoolsByRegion(int regionId)
        {
            var regionExists = await _context.Regions.AnyAsync(r => r.Id == regionId);
            if (!regionExists)
            {
                return NotFound($"Region with ID {regionId} not found.");
            }

            var schoolNames = await _context.Schools
                .Where(s => s.RegionId == regionId)
                .Select(s => s.Name)
                .ToListAsync();

            return Ok(schoolNames);
        }

        // GET: api/regions/{regionId}/schools-full
        [HttpGet("{regionId}/schools-full")]
        public async Task<ActionResult<IEnumerable<SchoolDTO>>> GetFullSchoolsByRegion(int regionId)
        {
            var regionExists = await _context.Regions.AnyAsync(r => r.Id == regionId);
            if (!regionExists)
            {
                return NotFound($"Region with ID {regionId} not found.");
            }

            var schools = await _context.Schools
                .Where(s => s.RegionId == regionId)
                .Select(s => new SchoolDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address
                })
                .ToListAsync();

            return Ok(schools);
        }

        // GET: api/regions/all-full
        [HttpGet("all-full")]
        public async Task<ActionResult<IEnumerable<RegionDTO>>> GetAllRegionsFull()
        {
            var regions = await _context.Regions
                .Select(r => new RegionDTO
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();

            return Ok(regions);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveForm([FromBody] RegistrationFormDTO dto)
        {
            if (dto == null)
                return BadRequest("Form data is missing.");

            if (string.IsNullOrWhiteSpace(dto.RegionName) ||
                string.IsNullOrWhiteSpace(dto.SchoolName) ||
                string.IsNullOrWhiteSpace(dto.Address))
            {
                return BadRequest("Region, School, and Address fields are required.");
            }

            var region = await _context.Regions
                .FirstOrDefaultAsync(r => r.Name == dto.RegionName);

            if (region == null)
                return NotFound($"Region '{dto.RegionName}' not found.");

            var school = await _context.Schools
                .FirstOrDefaultAsync(s =>
                    s.Name == dto.SchoolName &&
                    s.Address == dto.Address &&
                    s.RegionId == region.Id);

            if (school == null)
                return NotFound($"School '{dto.SchoolName}' with address '{dto.Address}' not found in region '{dto.RegionName}'.");

            var registration = new Registration
            {
                FullName = dto.FullName,
                Email = dto.Email,
                RegionId = region.Id,
                SchoolId = school.Id,
                SelectedAddress = dto.Address
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SaveForm), new { id = registration.Id }, new
            {
                message = "Registration saved successfully.",
                registration.Id
            });
        }



        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<RegistrationFormDTO>> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required.");

            var reg = await _context.Registrations
                .Include(r => r.Region)
                .Include(r => r.School)
                .FirstOrDefaultAsync(r => r.Email.ToLower() == email.ToLower());

            if (reg == null)
                return NotFound("Registration not found for the given email.");

            var dto = new RegistrationFormDTO
            {
                RegionName = reg.Region.Name,
                SchoolName = reg.School.Name,
                Address = reg.SelectedAddress,
                FullName = reg.FullName,
                Email = reg.Email
            };

            return Ok(dto);
        }

    }
}