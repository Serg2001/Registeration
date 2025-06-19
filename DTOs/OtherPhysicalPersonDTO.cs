using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class OtherPhysicalPersonDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Profession { get; set; } = string.Empty;

        public string CustomProfession { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string SurName { get; set; } = string.Empty;

        public int? Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
