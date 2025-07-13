using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.DTOs.OtherPhysicalPersonDTOs;
using Registeration.Models;
using Registeration.Services.OtherPhysicalPersonServices;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/otherphysicalperson")]
    public class OtherPhysicalPersonController : ControllerBase
    {
        private readonly OtherPhysicalPersonService _service;
        private readonly AppDbContext _context;

        public OtherPhysicalPersonController(
            OtherPhysicalPersonService service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Save Other with provided details.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherPhysicalPersonDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var model = new OtherPhysicalPerson
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    Country = dto.Country,
                    City = dto.City,
                    Profession = dto.Profession,
                    Name = dto.Name,
                    SurName = dto.SurName,
                    Age = dto.Age,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Purpose = dto.Purpose
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
        public async Task<ActionResult<OtherPhysicalPersonDTO>> GetById(Guid id)
        {
            var other = await _service.GetByIdAsync(id);

            if (other == null)
                return NotFound(new { Message = "Other not found" });

            var dto = new OtherPhysicalPersonDTO
            {
                Id = other.Id,
                Country = other.Country,
                City = other.City,
                Profession = other.Profession,
                Name = other.Name,
                SurName= other.SurName,
                Age = other.Age,
                Email = other.Email,
                Phone = other.Phone,
                Purpose = other.Purpose

                //Login = other.Login,
                //Password = other.Password,
                //AccessCode = other.AccessCode
            };

            return Ok(dto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var other = await _context.OtherPhysicalPersons.FindAsync(id);
            if (other == null)
                return NotFound();

            _context.OtherPhysicalPersons.Remove(other);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }

        //[HttpGet("identity-string/{id}")]
        //public async Task<IActionResult> GetIdentityString(Guid id)
        //{
        //    var other = await _service.GetByIdAsync(id);
        //    if (other == null)
        //        return NotFound(new { message = "Other not found" });

        //    // Create a unique identity string (e.g., Email + NameSurname)
        //    var identityString = $"{other.Email ?? ""}{other.NameSurname ?? ""}";
        //    if (string.IsNullOrWhiteSpace(identityString))
        //        return BadRequest(new { Message = "Insufficient data to generate identity string" });

        //    return Ok(new { identity = identityString });
        //}

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(Guid id, [FromBody] ConfirmCredentialsDTO dto)
        {
            var other = await _context.OtherPhysicalPersons.FindAsync(id);
            if (other == null)
                return NotFound();

            other.Login = other.Email;
            other.Password = dto.Password;
            other.Role = dto.Role;
            other.UserType = dto.UserType;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
