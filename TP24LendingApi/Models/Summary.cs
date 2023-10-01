namespace TP24LendingApi.Models
{
    public class Summary
    {
        public string? DebtorReference { get; set; }
        public double ReceivablesOpeningValue { get; set; }
        public double ReceivablesPaidValue { get; set; }
        public double OpenReceivablesOpeningValue { get; set; }
        public double OpenReceivablesPaidValue { get; set; }
        public double ClosedReceivablesOpeningValue { get; set; }
        public double ClosedReceivablesPaidValue { get; set; }
        public double CancelledReceivablesOpeningValue { get; set; }
        public double CancelledReceivablesPaidValue { get; set; }
    }
}
