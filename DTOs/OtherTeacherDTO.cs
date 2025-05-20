using System;
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class OtherTeacherDTO
    {
        public Guid Id { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        public RegionDTO? Region { get; set; }

        [Required]
        public Guid SchoolId { get; set; }

        public SchoolDTO? School { get; set; }

        [Required(ErrorMessage = "Անուն Ազգանուն դաշտը պարտադիր է։")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Դասավանդվող առարկան պարտադիր է։")]
        [StringLength(100)]
        public string TeachingSubject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Սոցիալական համարը պարտադիր է։")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Սոցիալական համարը պետք է կազմված լինի正10 թվանշանից։")]
        public string SocNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Էլ․ հասցեն պարտադիր է։")]
        [EmailAddress(ErrorMessage = "Սխալ Էլ․ հասցե։")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
    }
}
