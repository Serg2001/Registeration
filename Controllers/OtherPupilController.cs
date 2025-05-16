using Microsoft.AspNetCore.Http;
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

        public OtherPupilController(OtherPupilService service)
        {
            _service = service;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherPupilDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Region == null || dto.School == null ||
                dto.Region.Id == Guid.Empty || dto.School.Id == Guid.Empty)
                return BadRequest("Valid Region and School IDs must be provided.");

            var model = new OtherPupil
            {
                Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                IsArmenianCitizen = dto.IsArmenianCitizen,

                RegionId = dto.Region.Id,
                Region = new Region
                {
                    Id = dto.Region.Id,
                    Name = dto.Region.Name
                },

                SchoolId = dto.School.Id,
                School = new School
                {
                    Id = dto.School.Id,
                    Name = dto.School.Name,
                    Address = dto.School.Address,
                    RegionId = dto.School.RegionId
                },
                Grade = dto.Grade,
                FullName = dto.FullName,
                SocNumber = dto.SocNumber,
                ParentsEmail = dto.ParentsEmail
            };

            try
            {
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
