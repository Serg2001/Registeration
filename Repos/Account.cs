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

        private async Task<ApplicationUser> GetUser(string email)
            => await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);

        public async Task<RegisterationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser != null)
                return new RegisterationResponse(false, "User already exist");

            appDbContext.Users.Add(
                new ApplicationUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });

            await appDbContext.SaveChangesAsync();
            return new RegisterationResponse(true, "Success");
        }
    }
}
