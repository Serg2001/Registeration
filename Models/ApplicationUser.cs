namespace Registeration.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SocNumber { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
    }
}
