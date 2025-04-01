using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registeration.Repos;
using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;
using IAccount = Registeration.Repos.IAccount;
using Registeration.Services;
using Registeration.Data;

namespace Registeration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountrepo;

        public AccountController(IAccount accountrepo)
        {
            this.accountrepo = accountrepo;
        }


        [HttpPost("register")]
        public async Task<ActionResult<RegisterationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountrepo.RegisterAsync(model);
            return Ok(result);
        }

        [HttpGet("exists/{socNumber}")]
        public async Task<ActionResult<bool>> CheckSocNumberExists(string socNumber)
        {
            if (string.IsNullOrWhiteSpace(socNumber))
                return BadRequest("Invalid social number.");

            bool exists = await accountrepo.SocNumberExistsAsync(socNumber);
            return Ok(exists);
        }
    }
}
