using System;
using System.ComponentModel.DataAnnotations;

namespace Registeration.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string RegionName { get; set; }

        [Required]
        [StringLength(150)]
        public string SchoolName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}