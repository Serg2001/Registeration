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
        public DbSet<Region> Regions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<OtherPupil> OtherPupil { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable cascading delete to prevent multiple cascade paths
            modelBuilder.Entity<School>()
                .HasOne(s => s.Region)
                .WithMany(r => r.Schools)
                .HasForeignKey(s => s.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OtherPupil>()
                .HasOne(p => p.School)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne<Region>()
                .WithMany()
                .HasForeignKey("RegionId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne<School>()
                .WithMany()
                .HasForeignKey("SchoolId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
