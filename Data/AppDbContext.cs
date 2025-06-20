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
        public DbSet<OtherTeacher> OtherTeacher { get; set; }
        public DbSet<CRMPupil> CRMPupils { get; set; }
        public DbSet<OtherCompany> OtherCompanies { get; set; }
        public DbSet<OtherPhysicalPerson> OtherPhysicalPersons { get; set; }
        public DbSet<OtherStudent> OtherStudents { get; set; }
        public DbSet<OtherLecturer> OtherLecturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Region → School
            modelBuilder.Entity<School>()
                .HasOne(s => s.Region)
                .WithMany(r => r.Schools)
                .HasForeignKey(s => s.RegionId)
                .OnDelete(DeleteBehavior.NoAction);

            // School → Registration
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.School)
                .WithMany()
                .HasForeignKey(r => r.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            // School → OtherPupil
            modelBuilder.Entity<OtherPupil>()
                .HasOne(p => p.School)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            // School → OtherTeacher
            modelBuilder.Entity<OtherTeacher>()
                .HasOne(t => t.School)
                .WithMany()
                .HasForeignKey(t => t.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            // Region → OtherTeacher (if Region is navigated in OtherTeacher)
            modelBuilder.Entity<OtherTeacher>()
                .HasOne(t => t.Region)
                .WithMany()
                .HasForeignKey(t => t.RegionId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
