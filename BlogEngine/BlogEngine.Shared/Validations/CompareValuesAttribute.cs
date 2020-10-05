using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.Validations
{
    // System.ComponentModel.DataAnnotations.CompareAttribute is not working in server side blazor
    public sealed class CompareValuesAttribute : ValidationAttribute
    {
        public string ConfirmProperty { get; private set; }

        public CompareValuesAttribute(string confirmProperty)
        {
            ConfirmProperty = confirmProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string confirmValue = validationContext.ObjectInstance.GetType()
                    .GetProperty(ConfirmProperty)
                    .GetValue(validationContext.ObjectInstance)
                    .ToString();

            if (!value.Equals(confirmValue))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}