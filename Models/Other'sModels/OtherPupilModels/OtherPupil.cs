using Microsoft.EntityFrameworkCore;
using Registeration.Attributes;
using Registeration.Enums;
using Registeration.Models.CrmModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registeration.Models
{
    [Index(nameof(Grade), nameof(SocNumber), nameof(SchoolId), IsUnique = true)]
    public class OtherPupil
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        public EGrade Grade { get; set; }

        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;


        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string SurName { get; set; } = string.Empty;

        [Required]
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
