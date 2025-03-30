using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class MailDTO
    {
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
