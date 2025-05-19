using Registeration.Enums;
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class OtherPupilDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Citizen status is required.")]
        public bool IsArmenianCitizen { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public RegionDTO Region { get; set; } = new RegionDTO();

        [Required(ErrorMessage = "School is required.")]
        public SchoolDTO School { get; set; } = new SchoolDTO();

        [Required(ErrorMessage = "Grade is required.")]
        public GradeLevel Grade { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Social number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Social number must be exactly 10 digits.")]
        public string SocNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parent's email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string ParentsEmail { get; set; } = string.Empty;

        // ✅ NEW FIELDS

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string AccessCode { get; set; } = string.Empty;
    }
}
