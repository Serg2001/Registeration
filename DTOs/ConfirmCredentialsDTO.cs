namespace Registeration.DTOs
{
    public class ConfirmCredentialsDTO
    {
        public string Password { get; set; } = null!;
        public string AccessCode { get; set; } = null!;
        //public bool IsRegistered { get; set; }

        public string Role { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
    }
}
