using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Registeration.Attributes
{
    public class ArmenianOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string input && !string.IsNullOrWhiteSpace(input))
            {
                // Unicode range for Armenian letters (\u0531-\u0587)
                var regex = new Regex(@"^[\u0531-\u0587\s]+$");
                if (!regex.IsMatch(input))
                {
                    return new ValidationResult("Խնդրում ենք մուտքագրել հայատառ։");
                }
            }
            return ValidationResult.Success;
        }
    }
}
