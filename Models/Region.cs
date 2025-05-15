using System.ComponentModel.DataAnnotations;

namespace Registeration.Models
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Optional: navigation property
        public ICollection<School>? Schools { get; set; }
    }
}
