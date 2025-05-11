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
}
