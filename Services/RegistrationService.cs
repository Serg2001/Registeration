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
    }
}
