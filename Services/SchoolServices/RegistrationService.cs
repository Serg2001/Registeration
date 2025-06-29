using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.Models.SchoolModels;

namespace Registeration.Services.SchoolServices
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Check if this is a unique constraint violation
                if (ex.InnerException?.Message.Contains("duplicate") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    throw new InvalidOperationException("Դուք արդեն գրանցված եք։ Ստուգեք ձեր մուտքի տվյալները։");
                }
                throw;
            }
        }

        public async Task<Registration?> GetByIdAsync(Guid id)
        {
            return await _context.Registrations
                //.Include(r => r.School)
                //.ThenInclude(s => s.Region)
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

        public async Task<bool> UpdateCredentialsAsync(Guid id, string password, string accessCode)
        {
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
                return false;

            registration.Password = password;
            registration.AccessCode = accessCode;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
