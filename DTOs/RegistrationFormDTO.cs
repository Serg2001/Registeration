using System;
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class RegistrationFormDTO
    {
        public Guid Id { get; set; }  // Needed for confirmation and deletion

        [Required(ErrorMessage = "Մարզը պարտադիր է")]
        public string RegionName { get; set; }

        [Required(ErrorMessage = "Դպրոցը պարտադիր է")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Հասցեն պարտադիր է")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Անուն / Ազգանունը պարտադիր է")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Էլ․ հասցեն պարտադիր է")]
        [EmailAddress(ErrorMessage = "Սխալ էլ․ հասցեի ձևաչափ")]
        public string Email { get; set; }
    }
}
