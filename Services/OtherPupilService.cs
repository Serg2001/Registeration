using Registeration.Models;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}