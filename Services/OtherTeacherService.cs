using Registeration.Models;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;

namespace Registeration.Services
{
    public class OtherTeacherService
    {
        private readonly AppDbContext _context;

        public OtherTeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(OtherTeacher otherTeacher)
        {
            var exists = await _context.OtherTeacher.AnyAsync(t => t.Id == otherTeacher.Id);
            if (!exists)
            {
                _context.OtherTeacher.Add(otherTeacher);
            }
            else
            {
                _context.OtherTeacher.Update(otherTeacher);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<OtherTeacher?> GetByIdAsync(Guid id)
        {
            return await _context.OtherTeacher
                                 .Include(t => t.School)
                                 .ThenInclude(s => s.Region)
                                 .Include(t => t.Region)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<string?> GetConcatenatedIdentityAsync(Guid id)
        {
            var teacher = await _context.OtherTeacher
                .Where(t => t.Id == id)
                .Select(t => new { t.FullName, t.SocNumber })
                .FirstOrDefaultAsync();

            return teacher == null ? null : $"{teacher.FullName}{teacher.SocNumber}";
        }
    }
}
