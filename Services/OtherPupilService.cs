using Registeration.Models;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace Registeration.Services
{
    public class OtherPupilService
    {
        private readonly AppDbContext _context;

        public OtherPupilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(OtherPupil otherPupil)
        {
            var exists = await _context.OtherPupil.AnyAsync(p => p.Id == otherPupil.Id);
            if (!exists)
            {
                _context.OtherPupil.Add(otherPupil);
            }
            else
            {
                _context.OtherPupil.Update(otherPupil);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<OtherPupil?> GetByIdAsync(Guid id)
        {
            return await _context.OtherPupil
                                 .Include(p => p.School)
                                 .ThenInclude(s => s.Region)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<string?> GetConcatenatedIdentityAsync(Guid id)
        {
            var pupil = await _context.OtherPupil
                .Where(p => p.Id == id)
                .Select(p => new { p.FullName, p.SocNumber })
                .FirstOrDefaultAsync();

            return pupil == null ? null : $"{pupil.FullName}{pupil.SocNumber}";
        }

    }


}
