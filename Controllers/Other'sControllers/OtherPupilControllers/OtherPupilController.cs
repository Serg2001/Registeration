using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Data;
using Registeration.DTOs.OtherPupilDTOs;
using Registeration.Services.OtherPupilServices;
using Registeration.Services.CrmServices;
using Registeration.DTOs.CrmDTOs;
using Registeration.DTOs.SchoolDTOs;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/otherpupil")]
    public class OtherPupilController : ControllerBase
    {
        private readonly OtherPupilService _service;
        private readonly CrmService _crmService;
        private readonly AppDbContext _context;

        public OtherPupilController(
            OtherPupilService service,
            CrmService crmService,
            AppDbContext context)
        {
            _service = service;
            _crmService = crmService;
            _context = context;
        }

        /// <summary>
        /// Save OtherPupil with region and school info.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherPupilDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Region?.Name) || string.IsNullOrWhiteSpace(dto.School?.Name))
                return BadRequest("Region and School names must be provided.");

            try
            {
                var region = await _crmService.SaveRegionIfNotExistsAsync(dto.Region.Name);

                var school = await _crmService.SaveSchoolIfNotExistsAsync(
                    dto.School.Name,
                    dto.School.Address ?? "Not Provided",
                    region.Id);

                var model = new OtherPupil
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    SchoolId = school.Id,
                    Grade = dto.Grade,
                    Name = dto.Name,
                    SurName = dto.SurName,
                    SocNumber = dto.SocNumber ?? "", // ensure not null
                    ParentsEmail = dto.ParentsEmail ?? ""
                };

                await _service.SaveAsync(model);
                return Ok(new { message = "OtherPupil saved successfully", model.Id });
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
        public async Task<ActionResult<OtherPupilDTO>> GetById(Guid id)
        {
            var pupil = await _service.GetByIdAsync(id);

            if (pupil == null)
                return NotFound(new { Message = "OtherPupil not found" });

            var dto = new OtherPupilDTO
            {
                Id = pupil.Id,
                Region = new RegionDTO
                {
                    Id = pupil.School.Region.Id,
                    Name = pupil.School.Region.Name
                },
                School = new SchoolDTO
                {
                    Id = pupil.School.Id,
                    Name = pupil.School.Name,
                    Address = pupil.School.Address,
                    RegionId = pupil.School.RegionId
                },
                Grade = pupil.Grade,
                Name = pupil.Name,
                SurName = pupil.SurName,
                SocNumber = pupil.SocNumber,
                ParentsEmail = pupil.ParentsEmail
            };

            return Ok(dto);
        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pupil = await _context.OtherPupil.FindAsync(id);
            if (pupil == null)
                return NotFound();

            _context.OtherPupil.Remove(pupil);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }


        [HttpGet("identity-string/{id}")]
        public async Task<IActionResult> GetIdentityString(Guid id)
        {
            var result = await _service.GetConcatenatedIdentityAsync(id);

            if (string.IsNullOrWhiteSpace(result))
                return NotFound(new { message = "OtherPupil not found." });

            return Ok(new { identity = result });
        }


        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(Guid id, [FromBody] ConfirmCredentialsDTO dto)
        {
            var pupil = await _context.OtherPupil.FindAsync(id);
            if (pupil == null)
                return NotFound();

            pupil.Login = pupil.ParentsEmail;
            pupil.Password = dto.Password;
            pupil.AccessCode = dto.AccessCode;

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
