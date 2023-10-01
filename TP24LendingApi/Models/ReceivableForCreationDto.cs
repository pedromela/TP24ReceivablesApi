using System.ComponentModel.DataAnnotations;
using TP24LendingApi.CustomValidations;

namespace TP24LendingApi.Models
{
    public class ReceivableForCreationDto
    {
        [Required(ErrorMessage = "Reference is required.")]
        public string? Reference { get; set; }
        [Required(ErrorMessage = "CurrencyCode is required.")]
        [ValidCurrencyCode]
        public string? CurrencyCode { get; set; }
        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime? IssueDate { get; set; }
        [Required(ErrorMessage = "OpeningValue is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1} for OpeningValue.")]
        public double OpeningValue { get; set; }
        [Required(ErrorMessage = "PaidValue is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1} for PaidValue.")]
        public double PaidValue { get; set; }
        [Required(ErrorMessage = "DueDate is required.")]
        [DateGreaterThan(DateToCompareToFieldName = "IssueDate")]
        public DateTime? DueDate { get; set; }
        [DateGreaterThan(DateToCompareToFieldName = "IssueDate")]
        public DateTime? ClosedDate { get; set; }
        public bool? Cancelled { get; set; }
        [Required(ErrorMessage = "DebtorName is required.")]
        public string? DebtorName { get; set; }
        [Required(ErrorMessage = "DebtorReference is required.")]
        public string? DebtorReference { get; set; }
        public string? DebtorAddress1 { get; set; }
        public string? DebtorAddress2 { get; set; }
        public string? DebtorTown { get; set; }
        public string? DebtorState { get; set; }
        public string? DebtorZip { get; set; }
        [Required(ErrorMessage = "DebtorCountryCode is required.")]
        [ValidCountryCode]
        public string? DebtorCountryCode { get; set; }
        public string? DebtorRegistrationNumber { get; set; }
    }
}
