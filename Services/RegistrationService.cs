using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Services
{
    public class RegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<Registration?> GetByEmailAsync(string email)
        {
            return await _context.Registrations
                                 .FirstOrDefaultAsync(r => r.Email == email);
        }


        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var registrations = await _context.Registrations
                .Where(r => r.Email == email)
                .ToListAsync();

            if (!registrations.Any())
                return false;

            _context.Registrations.RemoveRange(registrations);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
