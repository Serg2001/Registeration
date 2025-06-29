using Microsoft.EntityFrameworkCore;
using Registeration.Enums;
using Registeration.Models;
using Registeration.Services;
using System.ComponentModel.DataAnnotations;
using static MudBlazor.Colors;
using System.Text.RegularExpressions;
using Registeration.Attributes;
using Registeration.DTOs.CrmDTOs;
using Registeration.DTOs.SchoolDTOs;

namespace Registeration.DTOs.OtherPupilDTOs
{
    public class OtherPupilDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "Մարզը Պարտադիր է։")]
        public RegionDTO Region { get; set; } = new RegionDTO();

        [Required(ErrorMessage = "Դպրոցը Պարտադիր է։")]
        public SchoolDTO School { get; set; } = new SchoolDTO();

        [Required(ErrorMessage = "Դասարանը Պարտադիր է։")]
        public EGrade Grade { get; set; }

        [Required(ErrorMessage = "Անունը Պարտադիր է։")]
        [ArmenianOnly]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ազգանունը Պարտադիր է։")]
        [ArmenianOnly]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters.")]
        public string SurName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        public string SocNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Պարտադիր լրացման դաշտ։")]
        [EmailAddress(ErrorMessage = "Սխալ էլ․ հասցեի ձևաչափ։")]
        public string ParentsEmail { get; set; } = string.Empty;

        // ✅ NEW FIELDS

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string AccessCode { get; set; } = string.Empty;
    }
}
