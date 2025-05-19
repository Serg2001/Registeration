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


        //// ✅ Code generator
        //private static string GenerateCode(string input)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        byte[] bytes = Encoding.UTF8.GetBytes(input);
        //        byte[] hash = sha256.ComputeHash(bytes);

        //        string hex = BitConverter.ToString(hash).Replace("-", "");
        //        string digits = new string(hex.Where(char.IsDigit).ToArray()).PadRight(4, '0');
        //        string digitPart = digits.Substring(0, 4);

        //        char[] armenian = new[] {
        //        'Ա', 'Բ', 'Գ', 'Դ', 'Ե', 'Զ', 'Է', 'Ը', 'Թ', 'Ժ', 'Ի', 'Լ', 'Խ',
        //        'Ծ', 'Կ', 'Հ', 'Ձ', 'Ղ', 'Ճ', 'Մ', 'Յ', 'Ն', 'Շ', 'Ո', 'Չ', 'Պ',
        //        'Ջ', 'Ռ', 'Ս', 'Վ', 'Տ', 'Ր', 'Ց', 'Փ', 'Ք', 'և', 'Օ', 'Ֆ'
        //    };

        //        char[] symbols = new[] { '@', '#', '$', '%', '&', '*' };
        //        char arm = armenian[hash[0] % armenian.Length];
        //        char sym = symbols[hash[1] % symbols.Length];

        //        return digitPart + arm + sym;
        //    }
        //}

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
