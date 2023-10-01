using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TP24LendingApi.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidCountryCodeAttribute : ValidationAttribute
    {
        public ValidCountryCodeAttribute()
        {
            const string defaultErrorMessage = "Error with Name";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (IsValidCountryCode((string) value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(String.Format("Currency: {0} is not a valid country code.", value));
            }
        }

        public bool IsValidCountryCode(string countryCode)
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
                .Any(ri => ri != null && ri.TwoLetterISORegionName == countryCode);

            return codes;
        }
    }
}
