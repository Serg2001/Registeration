using Microsoft.AspNetCore.Mvc;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Services;

[ApiController]
[Route("api/regions")]
public class RegionRegistrationController : ControllerBase
{
    private readonly RegistrationService _service;

    public RegionRegistrationController(RegistrationService service)
    {
        _service = service;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveRegistration([FromBody] RegistrationFormDTO dto)
    {
        var model = new Registration
        {
            RegionName = dto.RegionName,
            SchoolName = dto.SchoolName,
            Address = dto.Address,
            FullName = dto.FullName,
            Email = dto.Email
        };

        await _service.SaveAsync(model);
        return Ok(new { Message = "Saved successfully", model.Id });
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
