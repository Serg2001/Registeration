using System.ComponentModel.DataAnnotations;

namespace PlatformLogin.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;

        public bool RequiresPasswordChange { get; set; } = true;

        public int FailedLoginAttempts { get; set; }

        public DateTime? LockoutStartTime { get; set; }

        public DateTime? LockoutEndTime { get; set; }

        public bool IsPermanentlyLocked { get; set; }

        [Required]
        public bool IsRegistred { get; set; }
        public string? TwoFactorCode { get; set; }
        public DateTime? TwoFactorCodeExpiration { get; set; }
    }
}