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
        return Ok(new { Message = "Saved successfully" });
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<RegistrationFormDTO>> GetByEmail(string email)
    {
        var registration = await _service.GetByEmailAsync(email);
        if (registration == null)
            return NotFound(new { Message = "Registration not found" });

        var dto = new RegistrationFormDTO
        {
            RegionName = registration.RegionName,
            SchoolName = registration.SchoolName,
            Address = registration.Address,
            FullName = registration.FullName,
            Email = registration.Email
        };

        return Ok(dto);
    }

    [HttpDelete("by-email/{email}")]
    public async Task<IActionResult> DeleteByEmail(string email)
    {
        var success = await _service.DeleteByEmailAsync(email);
        if (!success)
            return NotFound(new { Message = "No records found to delete." });

        return Ok(new { Message = "Deleted successfully." });
    }


}
