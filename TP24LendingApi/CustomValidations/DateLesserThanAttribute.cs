using System.ComponentModel.DataAnnotations;

namespace TP24LendingApi.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateLesserThanAttribute : ValidationAttribute
    {
        public DateLesserThanAttribute()
        {
            const string defaultErrorMessage = "Error with Name";
            ErrorMessage ??= defaultErrorMessage;
        }

        public string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(String.Format("Date: {0} must be lesser than {1}", validationContext.MemberName, DateToCompareToFieldName));
            }
        }
    }
}
