using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
            var codes = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Any(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol);

            return codes;
        }
    }
}
