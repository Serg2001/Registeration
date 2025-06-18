using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Registeration.DTOs
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

        [Required(ErrorMessage ="Պարտադիր դաշտ։")]
        [EmailAddress(ErrorMessage = "Մուտքագրեք Էլ․ Հասցեն ճիշտ ձևաչափով։")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր դաշտ։")]
        public string Phone { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Country.Trim().Equals("Armenia", StringComparison.OrdinalIgnoreCase))
            {
                // Validate that NameSurname is Armenian-only
                var regex = new Regex(@"^[\u0531-\u0587\s]+$");
                if ((!string.IsNullOrWhiteSpace(FieldOfActivity) && !regex.IsMatch(FieldOfActivity))
                    || (!string.IsNullOrWhiteSpace(Purpose) && !regex.IsMatch(Purpose)))
                {
                    yield return new ValidationResult(
                        "Խնդրում ենք մուտքագրել հայատառ։",
                        new[] { nameof(FieldOfActivity),
                                nameof(Purpose)});
                }
            }
        }
    }
}
