using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.DTOs;
using Registeration.Models;
using Registeration.Responses;
using System;
using static Registeration.Responses.CustomResponses;

namespace Registeration.Repos
{
    public class Account : IAccount
    {

        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }

        private async Task<ApplicationUser> GetUser(string socqart)
            => await appDbContext.Users.FirstOrDefaultAsync(e => e.SocNumber == socqart);

        public async Task<RegisterationResponse> RegisterAsync(RegisterDTO model, MailDTO mail, UserDTO user)
        {
            appDbContext.Users.Add(
                new ApplicationUser()
                {
                    SocNumber = model.SocNumber,
                    Passport = model.Passport,
                    Email = mail.Email,
                    UserName = user.UserName,
                    Password = user.Password
                    //Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });

            await appDbContext.SaveChangesAsync();
            return new RegisterationResponse(true, "Success");
        }

        public async Task<bool> SocNumberExistsAsync(string socNumber)
        {
            return await appDbContext.Users.AnyAsync(u => u.SocNumber == socNumber);
        }
    }
}
