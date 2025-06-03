using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Services
{
    public class CRMPupilService
    {
        private readonly AppDbContext _context;

        public CRMPupilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SavePupilsAsync(List<CRMPupil> pupils)
        {
            var existingPupilIds = await _context.CRMPupils
                .Where(p => pupils.Select(x => x.Id).Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            var newPupils = pupils
                .Where(p => !existingPupilIds.Contains(p.Id))
                .ToList();

            if (newPupils.Any())
            {
                _context.CRMPupils.AddRange(newPupils);
                await _context.SaveChangesAsync();
            }
        }

    }
}
