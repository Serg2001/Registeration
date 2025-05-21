// Registeration/DTOs/OtherDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class OtherDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string NameSurname { get; set; } = string.Empty;

        [Required]
        public string Profession { get; set; } = string.Empty;

        public string CustomProfession { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string TeachingPlace { get; set; } = string.Empty;

        public string TeachingSubject { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;
    }
}