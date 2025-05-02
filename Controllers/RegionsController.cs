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
    }
}
