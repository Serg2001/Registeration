using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Data;
using Registeration.DTOs.OtherCompanyDTOs;
using Registeration.Services.OtherCompanyServices;

namespace Registeration.Controllers
{
    [ApiController]
    [Route("api/othercompany")]
    public class OtherCompanyController : ControllerBase
    {
        private readonly OtherCompanyService _service;
        private readonly AppDbContext _context;

        public OtherCompanyController(
            OtherCompanyService service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Save Other with provided details.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OtherCompanyDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var model = new OtherCompany
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    Country = dto.Country,
                    City = dto.City,
                    OrganizationName = dto.OrganizationName,
                    FieldOfActivity = dto.FieldOfActivity,
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
        public async Task<ActionResult<OtherCompanyDTO>> GetById(Guid id)
        {
            var other = await _service.GetByIdAsync(id);

            if (other == null)
                return NotFound(new { Message = "Other not found" });

            var dto = new OtherDTO
            {
                Id = other.Id,
                Country = other.Country,
                City = other.City,
                OrganizationName = other.OrganizationName,
                FieldOfActivity = other.FieldOfActivity,
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
            var other = await _context.OtherCompanies.FindAsync(id);
            if (other == null)
                return NotFound();

            _context.OtherCompanies.Remove(other);
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
            var other = await _context.OtherCompanies.FindAsync(id);
            if (other == null)
                return NotFound();

            other.Login = other.Email;
            other.Password = dto.Password;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}