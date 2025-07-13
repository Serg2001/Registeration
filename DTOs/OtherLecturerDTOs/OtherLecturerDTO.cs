using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs.OtherLecturerDTOs
{
    public class OtherLecturerDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string SurName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string TeachingPlace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string TeachingSubject { get; set; } = string.Empty;
        public int? Age { get; set; }

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        [EmailAddress(ErrorMessage = "Սխալ էլ․ հասցեի ձևաչափ։")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string Phone { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
    }
}
