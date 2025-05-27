using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Services;
using Registeration.Data;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/other")]
    public class OtherController : ControllerBase
    {
        private readonly OtherService _service;
        private readonly AppDbContext _context;

        public OtherController(
            OtherService service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Save Other with provided details.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var model = new Other
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    Country = dto.Country,
                    City = dto.City,
                    NameSurname = dto.NameSurname,
                    Profession = dto.Profession,
                    CustomProfession = dto.CustomProfession,
                    Age = dto.Age,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    TeachingPlace = dto.TeachingPlace,
                    TeachingSubject = dto.TeachingSubject,
                    Purpose = dto.Purpose,
                    StudyPlace = dto.StudyPlace
                    //Login = dto.Login,
                    //Password = dto.Password,
                    //AccessCode = dto.AccessCode
                };

                await _service.SaveAsync(model);
                return Ok(new { message = "Other saved successfully", model.Id });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<OtherDTO>> GetById(Guid id)
        {
            var other = await _service.GetByIdAsync(id);

            if (other == null)
                return NotFound(new { Message = "Other not found" });

            var dto = new OtherDTO
            {
                Id = other.Id,
                Country = other.Country,
                City = other.City,
                NameSurname = other.NameSurname,
                Profession = other.Profession,
                CustomProfession = other.CustomProfession,
                Age = other.Age,
                Email = other.Email,
                Phone = other.Phone,
                TeachingPlace = other.TeachingPlace,
                TeachingSubject = other.TeachingSubject,
                Purpose = other.Purpose,
                StudyPlace = other.StudyPlace
                //IsConfirmed = other.IsConfirmed,
                //Login = other.Login,
                //Password = other.Password,
                //AccessCode = other.AccessCode
            };

            return Ok(dto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var other = await _context.Others.FindAsync(id);
            if (other == null)
                return NotFound();

            _context.Others.Remove(other);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }

        [HttpGet("identity-string/{id}")]
        public async Task<IActionResult> GetIdentityString(Guid id)
        {
            var other = await _service.GetByIdAsync(id);
            if (other == null)
                return NotFound(new { message = "Other not found" });

            // Create a unique identity string (e.g., Email + NameSurname)
            var identityString = $"{other.Email ?? ""}{other.NameSurname ?? ""}";
            if (string.IsNullOrWhiteSpace(identityString))
                return BadRequest(new { Message = "Insufficient data to generate identity string" });

            return Ok(new { identity = identityString });
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(Guid id, [FromBody] ConfirmCredentialsDTO dto)
        {
            var other = await _context.Others.FindAsync(id);
            if (other == null)
                return NotFound();

            other.Login = other.Email;
            other.Password = dto.Password;
            other.AccessCode = dto.AccessCode;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}