using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.Validations
{
    public sealed class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext?.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}