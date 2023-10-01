using System.ComponentModel.DataAnnotations;
using TP24LendingApi.Services;

namespace TP24LendingApi.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidCurrencyCodeAttribute : ValidationAttribute
    {
        public ValidCurrencyCodeAttribute()
        {
            const string defaultErrorMessage = "Error with Name";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (IsValidCurrencyCode((string) value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(String.Format("Currency: {0} is not a valid currency code.", value));
            }
        }

        public bool IsValidCurrencyCode(string ISOCurrencySymbol)
        {
            var codes = CurrencyConverterService.GetAllCurrencyCodes().Any(ri => ri == ISOCurrencySymbol);
            return codes;
        }
    }
}
