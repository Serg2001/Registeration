using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Registeration.DTOs
{
    public class OtherDTO : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Պարտադիր լրացման դաշտ։")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string City { get; set; } = string.Empty;
        public string PersonType { get; set; } = string.Empty; // New field for Person Type

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string OrganizationName { get; set; } = string.Empty; // New field for Organization Name

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string FieldOfActivity { get; set; } = string.Empty; // New field for Field of Activity

        public string NameSurname { get; set; } = string.Empty;


        public string Profession { get; set; } = string.Empty;

        public string CustomProfession { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string TeachingPlace { get; set; } = string.Empty;

        public string TeachingSubject { get; set; } = string.Empty;
        public string StudyPlace { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Country.Trim().Equals("Armenia", StringComparison.OrdinalIgnoreCase))
            {
                // Validate that NameSurname is Armenian-only
                var regex = new Regex(@"^[\u0531-\u0587\s]+$");
                if ((!string.IsNullOrWhiteSpace(CustomProfession) && !regex.IsMatch(CustomProfession))
                    || (!string.IsNullOrWhiteSpace(NameSurname) && !regex.IsMatch(NameSurname))
                    || (!string.IsNullOrWhiteSpace(Purpose) && !regex.IsMatch(Purpose))
                    || (!string.IsNullOrWhiteSpace(TeachingPlace) && !regex.IsMatch(TeachingPlace)
                    || (!string.IsNullOrWhiteSpace(TeachingSubject) && !regex.IsMatch(TeachingSubject)
                    || (!string.IsNullOrWhiteSpace(StudyPlace) && !regex.IsMatch(StudyPlace)))))
                {
                    yield return new ValidationResult(
                        "Խնդրում ենք մուտքագրել հայատառ։",
                        new[] { nameof(NameSurname),
                                nameof(Purpose)});
                }
            }
        }
    }
}
