using Microsoft.EntityFrameworkCore;
using Registeration.Models;

namespace Registeration.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
