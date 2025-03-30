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
        private readonly IEmailService emailService; // Add email service

        public AccountController(IAccount accountrepo, IEmailService emailService)
        {
            this.accountrepo = accountrepo;
            this.emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountrepo.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("send-verification-code")]
        public async Task<ActionResult<RegisterationResponse>> SendVerificationCode([FromBody] MailDTO mail)
        {
            if (!ModelState.IsValid)
                return BadRequest(new RegisterationResponse
                {
                    Flag = false,
                    Message = "Invalid email format."
                });

            // Generate random 6-digit code
            var code = new Random().Next(100000, 999999).ToString();

            // TODO: Save the code to a temporary store (e.g., memory, DB, cache) with expiry

            await emailService.SendEmailAsync(mail.Email, "Your Verification Code", $"Your code is: {code}");

            return Ok(new RegisterationResponse
            {
                Flag = true,
                Message = "Verification code sent successfully."
            });
        }
    }
}
