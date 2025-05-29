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
            _context.CRMPupils.AddRange(pupils);
            await _context.SaveChangesAsync();
        }
    }
}
