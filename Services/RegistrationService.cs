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

        public async Task<Registration?> GetByIdAsync(Guid id)
        {
            return await _context.Registrations
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var registration = await _context.Registrations
                                             .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
                return false;

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
