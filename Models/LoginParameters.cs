namespace Registeration.Models
{
    public class LoginParameters
    {
        public bool RequiresPasswordChange { get; set; } = true;

        public int FailedLoginAttempts { get; set; }

        public DateTime? LockoutStartTime { get; set; }

        public DateTime? LockoutEndTime { get; set; }

        public bool IsPermanentlyLocked { get; set; }
        public string? TwoFactorCode { get; set; }
        public DateTime? TwoFactorCodeExpiration { get; set; }
    }
}
