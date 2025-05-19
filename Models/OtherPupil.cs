using Registeration.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registeration.Models
{
    public class OtherPupil
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public bool IsArmenianCitizen { get; set; }

        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        public GradeLevel Grade { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Social number must be exactly 10 digits.")]
        public string SocNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string ParentsEmail { get; set; } = string.Empty;

        // ✅ NEW FIELDS

        [Required]
        [StringLength(255)]
        public string Login { get; set; } = string.Empty;  // Usually same as ParentsEmail

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;  // Store hashed or generated code

        [Required]
        [StringLength(10)]
        public string AccessCode { get; set; } = string.Empty;  // 6-digit symbolic code
    }
}
