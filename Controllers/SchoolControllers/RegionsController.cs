using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.DTOs.SchoolDTOs;
using Registeration.DTOs.CrmDTOs;
using Registeration.Models.SchoolModels;
using Registeration.Services.SchoolServices;
using Registeration.Services.CrmServices;

namespace Registeration.Controllers.SchoolControllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionRegistrationController : ControllerBase
    {
        private readonly RegistrationService _service;
        private readonly CrmService _crmService;
        private readonly AppDbContext _context;

        public RegionRegistrationController(
            RegistrationService service,
            CrmService crmService,
            AppDbContext context)
        {
            _service = service;
            _crmService = crmService;
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] RegistrationFormDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Region?.Name) || string.IsNullOrWhiteSpace(dto.School?.Name))
                return BadRequest("Region and School names must be provided.");

            try
            {
                var region = await _crmService.SaveRegionIfNotExistsAsync(dto.Region.Name);

                var school = await _crmService.SaveSchoolIfNotExistsAsync(
                    dto.School.Id,
                    dto.School.Name,
                    dto.School.Address ?? "Not Provided",
                    region.Id);

                var model = new Registration
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    SchoolId = school.Id,
                    Address = dto.Address,
                    DirectorName = dto.DirectorName,
                    DirectorSurName = dto.DirectorSurName,
                    Email = dto.Email
                };

                await _service.SaveAsync(model);
                return Ok(new { message = "Registration saved successfully", model.Id });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Foreign key violation: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }



        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<RegistrationFormDTO>> GetById(Guid id)
        {
            var registration = await _context.Registrations
                .Include(r => r.School)
                .ThenInclude(s => s.Region)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
                return NotFound(new { Message = "Registration not found" });

            var dto = new RegistrationFormDTO
            {
                Id = registration.Id,
                Address = registration.Address,
                DirectorName = registration.DirectorName,
                DirectorSurName = registration.DirectorSurName,
                Email = registration.Email,
                Login = registration.Login,
                Password = registration.Password,
                AccessCode = registration.AccessCode,
                School = new SchoolDTO
                {
                    Id = registration.School.Id,
                    Name = registration.School.Name,
                    Address = registration.School.Address,
                    RegionId = registration.School.RegionId
                },
                Region = new RegionDTO
                {
                    Id = registration.School.Region.Id,
                    Name = registration.School.Region.Name
                }
            };

            return Ok(dto);
        }

        [HttpDelete("by-id/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
                return NotFound();

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(Guid id, [FromBody] ConfirmCredentialsDTO dto)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
                return NotFound();

            registration.Login = registration.Email;
            registration.Password = dto.Password;
            registration.AccessCode = dto.AccessCode;

            await _context.SaveChangesAsync();
            return Ok();
        }


        //Updating IsRegistered = true
        [HttpPut("school/{id}")]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] SchoolDTO schoolDto)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return NotFound();

            // Update properties
            school.Name = schoolDto.Name;
            school.Address = schoolDto.Address;
            school.RegionId = schoolDto.RegionId;
            school.IsRegistered = schoolDto.IsRegistered;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
