using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.CustomValidations
{
    public class MinTimeSpanAttribute : ValidationAttribute
    {
        private readonly int _minimum;

        public MinTimeSpanAttribute(int minimum)
        {
            _minimum = minimum;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not TimeSpan valueTimeSpan)
                throw new ArgumentException(nameof(value));

            var displayName = validationContext.DisplayName;

            if (valueTimeSpan.TotalSeconds < _minimum)
            {
                return new ValidationResult($"Total {displayName} tidak boleh dibawah 1");
            }

            return ValidationResult.Success;
        }
    }
}
