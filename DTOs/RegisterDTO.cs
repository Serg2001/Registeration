using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Required, Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
