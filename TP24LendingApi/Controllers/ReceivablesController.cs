using Microsoft.AspNetCore.Mvc;
using TP24LendingApi.Models;
using TP24Entities;
using TP24Entities.Models;
using AutoMapper;
using TP24LendingApi.Services;

namespace TP24LendingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ReceivablesController : ControllerBase
    {
        private readonly ReceivablesContext _context;
        private ICurrencyConverterService _converterService;
        private IMapper _mapper;

        public ReceivablesController(ReceivablesContext context, ICurrencyConverterService converterService, IMapper mapper)
        {
            _context = context;
            _converterService = converterService;
            _mapper = mapper;
        }

        [HttpPost(Name = "StoreReceivables")]
        public async Task<IActionResult> StoreReceivables(List<ReceivableForCreationDto> data)
        {
            if (data is null)
            {
                return BadRequest("Data is null.");
            }

            var entitiyData = _mapper.Map<List<Receivable>>(data);

            _context.Receivables.AddRange(entitiyData);
            await _context.SaveChangesAsync();
            return Ok("Receivables data stored successfully.");
        }

        [HttpGet("summary")]
        public IActionResult GetSummaryStatistics()
        {
            var openReceivables = _context.Receivables
                .Where(r => r.ClosedDate == null);
            double openReceivablesOpeningValue = openReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double openReceivablesPaidValue = openReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var closedReceivables = _context.Receivables
                .Where(r => r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double closedReceivablesPaidValue = closedReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var cancelledReceivables = _context.Receivables
                .Where(r => r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double cancelledReceivablesPaidValue = cancelledReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var summary = new Summary
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

            return Ok(summary);
        }

        [HttpGet("summary/{debtorReference}")]
        public IActionResult GetDebtorSummary(string debtorReference)
        {
            var openReceivables = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.ClosedDate == null);
            double openReceivablesOpeningValue = openReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double openReceivablesPaidValue = openReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var closedReceivables = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double closedReceivablesPaidValue = closedReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var cancelledReceivables = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Select(r => _converterService.Convert(r.OpeningValue, r.CurrencyCode, "USD")).Sum();
            double cancelledReceivablesPaidValue = cancelledReceivables.Select(r => _converterService.Convert(r.PaidValue, r.CurrencyCode, "USD")).Sum();

            var summary = new Summary
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

            return Ok(summary);
        }
    }
}