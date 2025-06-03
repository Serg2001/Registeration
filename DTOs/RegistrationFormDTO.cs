using Registeration.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class RegistrationFormDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public RegionDTO Region { get; set; } = new RegionDTO();

        [Required(ErrorMessage = "School is required.")]
        public SchoolDTO School { get; set; } = new SchoolDTO();

        [Required(ErrorMessage = "Հասցեն պարտադիր է")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Անուն / Ազգանունը պարտադիր է")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Էլ․ հասցեն պարտադիր է")]
        [EmailAddress(ErrorMessage = "Սխալ էլ․ հասցեի ձևաչափ")]
        public string Email { get; set; } = string.Empty;

        //// Optional: Display names for UI
        //public string RegionName { get; set; } = string.Empty;
        //public string SchoolName { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string AccessCode { get; set; } = string.Empty;
    }
}
