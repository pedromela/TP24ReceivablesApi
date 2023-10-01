using Microsoft.AspNetCore.Mvc;
using TP24LendingApi.Models;
using TP24Entities;
using TP24Entities.Models;
using AutoMapper;

namespace TP24LendingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceivablesController : ControllerBase
    {
        private readonly ReceivablesContext _context;
        private IMapper _mapper;

        public ReceivablesController(ReceivablesContext context, IMapper mapper)
        {
            _context = context;
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
            double openReceivablesOpeningValue = openReceivables.Sum(r => r.OpeningValue);
            double openReceivablesPaidValue = openReceivables.Sum(r => r.PaidValue);

            var closedReceivables = _context.Receivables
                .Where(r => r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Sum(r => r.OpeningValue);
            double closedReceivablesPaidValue = closedReceivables.Sum(r => r.PaidValue);

            var cancelledReceivables = _context.Receivables
                .Where(r => r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Sum(r => r.OpeningValue);
            double cancelledReceivablesPaidValue = cancelledReceivables.Sum(r => r.PaidValue);

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
            double openReceivablesOpeningValue = openReceivables.Sum(r => r.OpeningValue);
            double openReceivablesPaidValue = openReceivables.Sum(r => r.PaidValue);

            var closedReceivables = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.ClosedDate != null);
            double closedReceivablesOpeningValue = closedReceivables.Sum(r => r.OpeningValue);
            double closedReceivablesPaidValue = closedReceivables.Sum(r => r.PaidValue);

            var cancelledReceivables = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.Cancelled == true);
            double cancelledReceivablesOpeningValue = cancelledReceivables.Sum(r => r.OpeningValue);
            double cancelledReceivablesPaidValue = cancelledReceivables.Sum(r => r.PaidValue);

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