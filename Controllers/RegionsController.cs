using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Services;
using System.Text.Json;

[ApiController]
[Route("api/regions")]
public class RegionRegistrationController : ControllerBase
{
    private readonly RegistrationService _service;

    public RegionRegistrationController(RegistrationService service)
    {
        _service = service;
    }

    //Save Method for RegistrationDTO
    //[HttpPost("save")]
    //public async Task<IActionResult> SaveRegistration([FromBody] RegistrationFormDTO dto)
    //{
    //    var model = new Registration
    //    {
    //        RegionName = dto.RegionName,
    //        SchoolName = dto.SchoolName,
    //        Address = dto.Address,
    //        FullName = dto.FullName,
    //        Email = dto.Email
    //    };

    //    await _service.SaveAsync(model);
    //    return Ok(new { Message = "Saved successfully", model.Id });
    //}


    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] JsonElement json)
    {
        try
        {
            if (!json.TryGetProperty("formType", out var formTypeProp))
                return BadRequest("Missing formType");

            var formType = formTypeProp.GetString();

            if (formType == "Registration")
            {
                var dto = JsonSerializer.Deserialize<RegistrationFormDTO>(json.GetRawText());
                if (dto == null) return BadRequest("Invalid Registration data");

                var reg = new Registration
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    RegionName = dto.RegionName,
                    SchoolName = dto.SchoolName,
                    Address = dto.Address,
                    FullName = dto.FullName,
                    Email = dto.Email
                };

                await _service.SaveAsync(reg);
                return Ok(new { id = reg.Id, message = "Registration saved" });
            }
            else if (formType == "OtherPupil")
            {
                var dto = JsonSerializer.Deserialize<OtherPupilDTO>(json.GetRawText());
                if (dto == null) return BadRequest("Invalid OtherPupil data");

                if (dto.Region == null || dto.School == null)
                    return BadRequest("Region or School information is missing.");

                var pupil = new OtherPupil
                {
                    Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                    IsArmenianCitizen = dto.IsArmenianCitizen,
                    RegionId = dto.Region.Id,
                    SchoolId = dto.School.Id,
                    Grade = dto.Grade,
                    FullName = dto.FullName,
                    SocNumber = dto.SocNumber,
                    ParentsEmail = dto.ParentsEmail
                };

                await _service.SaveAsync(pupil);
                return Ok(new { id = pupil.Id, message = "Pupil saved" });
            }

            return BadRequest("Unknown form type.");
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
    public async Task<ActionResult<RegistrationFormDTO>> GetById(Guid id)
    {
        var registration = await _service.GetByIdAsync(id);
        if (registration == null)
            return NotFound(new { Message = "Registration not found" });

        var dto = new RegistrationFormDTO
        {
            Id = registration.Id,
            RegionName = registration.RegionName,
            SchoolName = registration.SchoolName,
            Address = registration.Address,
            FullName = registration.FullName,
            Email = registration.Email
        };

        return Ok(dto);
    }

    [HttpDelete("by-id/{id}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var result = await _service.DeleteByIdAsync(id);
        if (!result)
            return NotFound(new { Message = "Registration not found" });

        return Ok(new { Message = "Deleted successfully" });
    }
}
