using Microsoft.AspNetCore.Mvc;
using Registeration.Models.CrmModels;
using Registeration.Services.CrmServices;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

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

    [HttpPost("save")]
    public async Task<IActionResult> SavePupils([FromBody] List<CRMPupil> pupils)
    {
        if (pupils == null || !pupils.Any())
            return BadRequest("Pupil list is empty.");

        await _pupilService.SavePupilsAsync(pupils);
        return Ok("Pupils saved successfully.");
    }

    [HttpGet("fetch-from-crm")]
    public async Task<IActionResult> FetchAndSavePupilsFromCRM([FromQuery] Guid schoolId)
    {
        if (schoolId == Guid.Empty)
        {
            return BadRequest("Invalid or missing schoolId.");
        }

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
                Id = p.Id,  // Preserving the CRM Ids
                Name = p.Name ?? string.Empty,
                Surname = p.Surname ?? string.Empty,
                Role = "pupil",
                UserType = "pupil",
                AccessCode = GenerateCode($"{p.Id}-{p.Name}-{p.Surname}")
            }).ToList();

            await _pupilService.SavePupilsAsync(pupilsToSave);

            return Ok($"{pupilsToSave.Count} pupils fetched and saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error fetching pupils: {ex.Message}");
        }
    }

    private static string GenerateCode(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);

            string hex = BitConverter.ToString(hash).Replace("-", "");
            string digitsOnly = new string(hex.Where(char.IsDigit).ToArray()).PadRight(3, '0');
            string digitPart = digitsOnly.Substring(0, 3);

            char[] upper = {
                'Ա', 'Բ', 'Գ', 'Դ', 'Ե', 'Զ', 'Է', 'Ը', 'Թ', 'Ժ', 'Ի', 'Լ', 'Խ',
                'Ծ', 'Կ', 'Հ', 'Ձ', 'Ղ', 'Ճ', 'Մ', 'Յ', 'Ն', 'Շ', 'Ո', 'Չ', 'Պ',
                'Ջ', 'Ռ', 'Ս', 'Վ', 'Տ', 'Ր', 'Ց', 'Փ', 'Ք', 'Օ', 'Ֆ'
        };

            char[] lower = {
                'ա', 'բ', 'գ', 'դ', 'ե', 'զ', 'է', 'ը', 'թ', 'ժ', 'ի', 'լ', 'խ',
                'ծ', 'կ', 'հ', 'ձ', 'ղ', 'ճ', 'մ', 'յ', 'ն', 'շ', 'ո', 'չ', 'պ',
                'ջ', 'ռ', 'ս', 'վ', 'տ', 'ր', 'ց', 'փ', 'ք', 'օ', 'ֆ'
        };

            char[] symbols = { '!', '@', '#', '$', '%', '&', '*' };

            Random rand = new Random(hash[0] + hash[1] + hash[2]);

            char letter1 = rand.Next(2) == 0 ? upper[hash[3] % upper.Length] : lower[hash[4] % lower.Length];
            char letter2 = rand.Next(2) == 0 ? upper[hash[5] % upper.Length] : lower[hash[6] % lower.Length];
            char symbol = symbols[hash[7] % symbols.Length];

            char[] codeChars = new[] {
                digitPart[0], digitPart[1], digitPart[2],
                letter1, letter2,
                symbol
            };

            codeChars = codeChars.OrderBy(_ => rand.Next()).ToArray();

            return new string(codeChars);
        }

    }
}
