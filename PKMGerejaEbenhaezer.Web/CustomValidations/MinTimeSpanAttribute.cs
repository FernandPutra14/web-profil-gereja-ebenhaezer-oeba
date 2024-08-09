using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.CustomValidations
{
    public class MinTimeSpanAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} harus diatas {1}";
        private readonly int _minNumberOfSeconds;

        public MinTimeSpanAttribute(int minNumberOfSeconds)
        {
            _minNumberOfSeconds = minNumberOfSeconds;
        }

        private string GetErrorMessage()
        {
            return ErrorMessage ?? DefaultErrorMessage;
        }

        public override string FormatErrorMessage(string name)
        {
            var minTimeSpan = TimeSpan.FromSeconds(_minNumberOfSeconds);

            return string.Format(GetErrorMessage(), name, 
                $"{minTimeSpan.Hours:D2}:{minTimeSpan.Minutes:D2}:{minTimeSpan.Seconds:D2}");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not TimeSpan timeSpan)
                return ValidationResult.Success;


            if (timeSpan.TotalSeconds < _minNumberOfSeconds)
            {
                var displayName = validationContext.DisplayName;
                var membersName = validationContext.MemberName is null
                    ? null
                    : new string[] { validationContext.MemberName };

                return new ValidationResult(FormatErrorMessage(displayName), membersName);
            }

            return ValidationResult.Success;
        }
    }
}
