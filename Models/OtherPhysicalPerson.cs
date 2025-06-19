using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Registeration.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class OtherPhysicalPerson
    {
       public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty ;

        public string Profession { get; set; } = string.Empty;

        public string CustomProfession { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string SurName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Email {  get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;    
    }
}
