﻿using System.ComponentModel.DataAnnotations;

namespace TP24Entities.Models
{
    public class Receivable
    {
        [Key]
        [Required]
        public string? Reference { get; set; }
        [Required]
        public string? CurrencyCode { get; set; }
        [Required]
        public DateTime? IssueDate { get; set; }
        [Required]
        public double OpeningValue { get; set; }
        [Required]
        public double PaidValue { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool? Cancelled { get; set; }
        [Required]
        public string? DebtorName { get; set; }
        [Required]
        public string? DebtorReference { get; set; }
        public string? DebtorAddress1 { get; set; }
        public string? DebtorAddress2 { get; set; }
        public string? DebtorTown { get; set; }
        public string? DebtorState { get; set; }
        public string? DebtorZip { get; set; }
        [Required]
        public string? DebtorCountryCode { get; set; }
        public string? DebtorRegistrationNumber { get; set; }

    }
}
