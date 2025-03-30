using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class RegisterDTO
    {

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "SocNumber must be between 4 and 15 characters.")]
        public string SocNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Passport must be between 4 and 15 characters.")]
        public string Passport { get; set; } = string.Empty;


    }
}
