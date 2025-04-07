using System.ComponentModel.DataAnnotations;

namespace Registeration.DTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Գաղտնաբառը պետք է պարունակի ամենաքիչը 8նիշ")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Գաղտնաբառը պետք է պարունակի ամենաքիչը մեկ մեծատառ,մեկ փոէրատառ, մեկ թվանշան, և մեկ սիմվոլ.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
