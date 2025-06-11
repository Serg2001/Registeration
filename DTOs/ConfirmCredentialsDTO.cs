namespace Registeration.DTOs
{
    public class ConfirmCredentialsDTO
    {
        public string Password { get; set; } = null!;
        public string AccessCode { get; set; } = null!;
        //public bool IsRegistered { get; set; }
    }
}
