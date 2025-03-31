using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registeration.Repos;
using Registeration.DTOs;
using static Registeration.Responses.CustomResponses;
using IAccount = Registeration.Repos.IAccount;
using Registeration.Services;

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
    }
}
