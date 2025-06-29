using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Registeration.Attributes;
using Registeration.Models.CrmModels;

namespace Registeration.Models.SchoolModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class Registration
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string DirectorName { get; set; } = string.Empty;

        [Required]
        [ArmenianOnly]
        [StringLength(100)]
        public string DirectorSurName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        //public bool isRegistered { get; set; } = false;

        [Required]
        [StringLength(255)]
        public string Login { get; set; } = string.Empty;  // Usually same as ParentsEmail

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;  // Store hashed or generated code

        [Required]
        [StringLength(10)]
        public string AccessCode { get; set; } = string.Empty;  // 6-digit symbolic code
    }
}
