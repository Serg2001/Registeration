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
        public DbSet<Region> Regions { get; set; }
        public DbSet<School> Schools { get; set; }

        public DbSet<Registration> Registrations { get; set; }
    }
}
