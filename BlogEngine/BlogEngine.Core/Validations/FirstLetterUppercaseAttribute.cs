using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Validations
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        public new string ErrorMessage { get; set; } = "First letter should be uppercase";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}