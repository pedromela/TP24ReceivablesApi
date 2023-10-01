using System.Linq.Expressions;
using TP24Entities;
using TP24Entities.Models;
using TP24LendingApi.Models;

namespace TP24LendingApi.Services
{
    public class ReceivablesService : IReceivablesService
    {
        IUnitOfWork _unitOfWork;
        ICurrencyConverterService _converterService;
        public ReceivablesService(IUnitOfWork unitOfWork, ICurrencyConverterService converterService)
        {
            _unitOfWork = unitOfWork;
            _converterService = converterService;
        }

        public  void CreateReceivables(IEnumerable<Receivable> data)
        {
            _unitOfWork.Receivables.AddRange(data);
            _unitOfWork.Save();
        }

        public IEnumerable<Receivable> GetAll(Expression<Func<Receivable, bool>> predicate)
        {
            return _unitOfWork.Receivables.Find(predicate);
        }

        public Summary GetSummaryStatistics()
        {
            var openReceivables = GetAll(r => r.ClosedDate == null);
            double openReceivablesOpeningValue = openReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double openReceivablesPaidValue = openReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var closedReceivables = GetAll(r => r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double closedReceivablesPaidValue = closedReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var cancelledReceivables = GetAll(r => r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double cancelledReceivablesPaidValue = cancelledReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            return new Summary
            {
                ReceivablesOpeningValue = openReceivablesOpeningValue + closedReceivablesOpeningValue,
                ReceivablesPaidValue = openReceivablesPaidValue + closedReceivablesPaidValue,
                OpenReceivablesOpeningValue = openReceivablesOpeningValue,
                OpenReceivablesPaidValue = openReceivablesPaidValue,
                ClosedReceivablesOpeningValue = closedReceivablesOpeningValue,
                ClosedReceivablesPaidValue = closedReceivablesPaidValue,
                CancelledReceivablesOpeningValue = cancelledReceivablesOpeningValue,
                CancelledReceivablesPaidValue = cancelledReceivablesPaidValue,
            };
        }

        public Summary GetDebtorSummary(string debtorReference)
        {
            var openReceivables = GetAll(r => r.DebtorReference == debtorReference && r.ClosedDate == null);
            double openReceivablesOpeningValue = openReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double openReceivablesPaidValue = openReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var closedReceivables = GetAll(r => r.DebtorReference == debtorReference && r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double closedReceivablesPaidValue = closedReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var cancelledReceivables = GetAll(r => r.DebtorReference == debtorReference && r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double cancelledReceivablesPaidValue = cancelledReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            return new Summary
            {
                ReceivablesOpeningValue = openReceivablesOpeningValue + closedReceivablesOpeningValue,
                ReceivablesPaidValue = openReceivablesPaidValue + closedReceivablesPaidValue,
                OpenReceivablesOpeningValue = openReceivablesOpeningValue,
                OpenReceivablesPaidValue = openReceivablesPaidValue,
                ClosedReceivablesOpeningValue = closedReceivablesOpeningValue,
                ClosedReceivablesPaidValue = closedReceivablesPaidValue,
                CancelledReceivablesOpeningValue = cancelledReceivablesOpeningValue,
                CancelledReceivablesPaidValue = cancelledReceivablesPaidValue,
            };
        }
    }
}
