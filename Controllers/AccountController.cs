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
        public async Task<ActionResult<RegisterationResponse>> RegisterAsync([FromBody] RegisterWithMailDTO model)
        {
            var result = await accountrepo.RegisterAsync(model.Register, model.Mail, model.User);
            return Ok(result);
        }


        [HttpGet("exists")]
        public async Task<ActionResult<bool>> CheckSocNumberAndPassportExists(
    [FromQuery] string socNumber,
    [FromQuery] string passport)
        {
            if (string.IsNullOrWhiteSpace(socNumber) || string.IsNullOrWhiteSpace(passport))
                return BadRequest("Invalid input.");

            bool exists = await accountrepo.SocNumberAndPassportExistsAsync(socNumber, passport);
            return Ok(exists);
        }

        [HttpGet("username-exists/{username}")]
        public async Task<ActionResult<bool>> CheckUsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Invalid Username.");

            bool exists = await accountrepo.UsernameExistsAsync(username);
            return Ok(exists);
        }

    }
}
