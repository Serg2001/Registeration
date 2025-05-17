using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Services;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/otherpupil")]
    public class OtherPupilController : ControllerBase
    {
        private readonly OtherPupilService _service;
        private readonly CrmRegionService _regionService;
        private readonly CrmSchoolService _schoolService;

        public OtherPupilController(
            OtherPupilService service,
            CrmRegionService regionService,
            CrmSchoolService schoolService)
        {
            _service = service;
            _regionService = regionService;
            _schoolService = schoolService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherPupilDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Region?.Name) || string.IsNullOrWhiteSpace(dto.School?.Name))
                return BadRequest("Region and School names must be provided.");

            try
            {
                // Save region if not exists
                var region = await _regionService.SaveRegionIfNotExistsAsync(dto.Region.Name);

                // Save school if not exists
                var school = await _schoolService.SaveSchoolIfNotExistsAsync(
                    dto.School.Name,
                    dto.School.Address ?? "Not Provided",
                    region.Id);

                // Build and save OtherPupil
                var model = new OtherPupil
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    IsArmenianCitizen = dto.IsArmenianCitizen,
                    SchoolId = school.Id,
                    Grade = dto.Grade,
                    FullName = dto.FullName,
                    SocNumber = dto.SocNumber,
                    ParentsEmail = dto.ParentsEmail
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
    }
}
