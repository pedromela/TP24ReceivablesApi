using Microsoft.AspNetCore.Mvc;
using TP24LendingApi.Models;
using TP24Entities;
using TP24Entities.Models;

namespace TP24LendingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceivablesController : ControllerBase
    {
        private readonly ReceivablesContext _context;

        public ReceivablesController(ReceivablesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> StoreReceivables(List<Receivable> data)
        {
            _context.Receivables.AddRange(data);
            await _context.SaveChangesAsync();
            return Ok("Receivables data stored successfully");
        }

        [HttpGet("summary")]
        public IActionResult GetSummaryStatistics()
        {
            decimal openInvoicesValue = _context.Receivables
                .Where(r => r.ClosedDate == null)
                .Sum(r => r.OpeningValue);

            decimal closedInvoicesValue = _context.Receivables
                .Where(r => r.ClosedDate != null)
                .Sum(r => r.PaidValue);

            var summary = new Summary
            {
                OpenInvoicesValue = openInvoicesValue,
                ClosedInvoicesValue = closedInvoicesValue
            };

            return Ok(summary);
        }

        [HttpGet("summary/{debtorReference}")]
        public IActionResult GetDebtorSummary(string debtorReference)
        {
            decimal openInvoicesValue = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.ClosedDate == null)
                .Sum(r => r.OpeningValue);

            decimal closedInvoicesValue = _context.Receivables
                .Where(r => r.DebtorReference == debtorReference && r.ClosedDate != null)
                .Sum(r => r.PaidValue);

            var summary = new Summary
            {
                OpenInvoicesValue = openInvoicesValue,
                ClosedInvoicesValue = closedInvoicesValue
            };

            return Ok(summary);
        }
    }
}