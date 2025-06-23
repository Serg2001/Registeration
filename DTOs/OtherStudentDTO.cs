using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class OtherStudentDTO
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
        public string StudyPlace { get; set; } = string.Empty;
        public int? Age { get; set; }

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        [EmailAddress(ErrorMessage ="Սխալ էլ․ հասցեի ձևաչափ։")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string Phone { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
