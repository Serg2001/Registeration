// Registeration/Controllers/OtherController.cs
using Microsoft.AspNetCore.Mvc;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Services;

[ApiController]
[Route("api/other")]
public class OtherController : ControllerBase
{
    private readonly OtherService _service;

    public OtherController(OtherService service)
    {
        _service = service;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveOther([FromBody] OtherDTO dto)
    {
        var model = new Other
        {
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
            Purpose = dto.Purpose
        };

        await _service.SaveAsync(model);
        return Ok(new { Message = "Saved successfully" });
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
            Purpose = other.Purpose
        };

        return Ok(dto);
    }

    [HttpDelete("by-id/{id}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var result = await _service.DeleteByIdAsync(id);
        if (!result)
            return NotFound(new { Message = "Other not found" });

        return Ok(new { Message = "Deleted successfully" });
    }
}