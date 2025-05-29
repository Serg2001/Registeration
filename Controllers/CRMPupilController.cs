using Microsoft.AspNetCore.Mvc;
using Registeration.Models;
using Registeration.Services;

[ApiController]
[Route("api/[controller]")]
public class PupilsController : ControllerBase
{
    private readonly CRMPupilService _pupilService;

    public PupilsController(CRMPupilService pupilService)
    {
        _pupilService = pupilService;
    }

    [HttpPost]
    public async Task<IActionResult> SavePupils([FromBody] List<CRMPupil> pupils)
    {
        if (pupils == null || !pupils.Any())
            return BadRequest("Pupil list is empty.");

        await _pupilService.SavePupilsAsync(pupils);
        return Ok("Pupils saved successfully.");
    }
}
