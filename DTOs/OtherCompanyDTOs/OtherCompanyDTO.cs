using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Registeration.Attributes;
using System.Text.RegularExpressions;

namespace Registeration.DTOs.OtherCompanyDTOs
{
    [Index(nameof(Email), IsUnique = true)]
    public class OtherCompanyDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string OrganizationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string FieldOfActivity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        [EmailAddress(ErrorMessage = "Մուտքագրեք Էլ․ Հասցեն ճիշտ ձևաչափով։")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string Phone { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
