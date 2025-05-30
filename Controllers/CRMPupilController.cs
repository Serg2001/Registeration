using Microsoft.AspNetCore.Mvc;
using Registeration.Models;
using Registeration.Services;
using System.Net.Http.Json;

[ApiController]
[Route("api/[controller]")]
public class PupilsController : ControllerBase
{
    private readonly CRMPupilService _pupilService;
    private readonly IHttpClientFactory _httpClientFactory;

    public PupilsController(CRMPupilService pupilService, IHttpClientFactory httpClientFactory)
    {
        _pupilService = pupilService;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> SavePupils([FromBody] List<CRMPupil> pupils)
    {
        if (pupils == null || !pupils.Any())
            return BadRequest("Pupil list is empty.");

        await _pupilService.SavePupilsAsync(pupils);
        return Ok("Pupils saved successfully.");
    }

    [HttpGet("fetch-from-crm")]
    public async Task<IActionResult> FetchAndSavePupilsFromCRM()
    {
        // Hard-coded schoolId
        Guid schoolId = Guid.Parse("B1080242-4002-46F1-5F9B-08D49E8401FF");

        try
        {
            var client = _httpClientFactory.CreateClient("CrmApi");
            var url = $"api/Platform/GetPupilsBySchool?schoolId={schoolId}";
            var crmPupils = await client.GetFromJsonAsync<List<CRMPupil>>(url);

            if (crmPupils == null || !crmPupils.Any())
                return NotFound("No pupils found in CRM.");

            // Map to internal model
            var pupilsToSave = crmPupils.Select(p => new CRMPupil
            {
                Id = p.Id,  // Add Id mapping here
                Name = p.Name ?? string.Empty,
                Surname = p.Surname ?? string.Empty
            }).ToList();

            await _pupilService.SavePupilsAsync(pupilsToSave);

            return Ok($"{pupilsToSave.Count} pupils fetched and saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching pupils: {ex.Message}");
        }
    }
}
