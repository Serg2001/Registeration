using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Registeration.Attributes;
using Registeration.Enums;
using Registeration.Models.CrmModels;

namespace Registeration.Models
{
    [Index(nameof(SocNumber), nameof(SchoolId), IsUnique = true)]
    public class OtherTeacher : LoginParameters
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; } = null!;

        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string TeachingSubject { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Social number must be exactly 10 digits.")]
        public string SocNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        // ✅ Login credentials
        [StringLength(255)]
        public string? Login { get; set; }

        [StringLength(255)]
        public string? Password { get; set; }

        [StringLength(255)]
        public string? AccessCode { get; set; }
        public string Role { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
    }
}
