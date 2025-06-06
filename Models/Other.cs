// Registeration/Models/Other.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Registeration.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Other
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        public string PersonType { get; set; } = string.Empty; // New field for Person Type

        public string OrganizationName { get; set; } = string.Empty; // New field for Organization Name

        public string FieldOfActivity { get; set; } = string.Empty; // New field for Field of Activity

        public string NameSurname { get; set; } = string.Empty;

        public string Profession { get; set; } = string.Empty;

        public string CustomProfession { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string TeachingPlace { get; set; } = string.Empty;

        public string TeachingSubject { get; set; } = string.Empty;
        public string StudyPlace { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Login { get; set; } = string.Empty;   // Added for confirmation
        public string Password { get; set; } = string.Empty; // Added for confirmation
        public string AccessCode { get; set; } = string.Empty; // Added for confirmation
    }
}