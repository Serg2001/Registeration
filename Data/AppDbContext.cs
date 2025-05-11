using Microsoft.EntityFrameworkCore;
using Registeration.Models;

namespace Registeration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }

    }
}
