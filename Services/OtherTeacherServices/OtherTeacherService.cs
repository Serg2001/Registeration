using Registeration.Models;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;

namespace Registeration.Services.OtherTeacherServices
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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("UNIQUE") == true ||
                    ex.InnerException?.Message.Contains("duplicate") == true)
                {
                    throw new InvalidOperationException("Այս Ուսուցիչն արդեն գրանցված է։");
                }
                throw;
            }
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
