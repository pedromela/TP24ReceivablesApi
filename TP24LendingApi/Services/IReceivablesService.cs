using System.Globalization;
using System.Linq.Expressions;
using TP24Entities.Models;
using TP24LendingApi.Models;

namespace TP24LendingApi.Services
{
    public interface IReceivablesService
    {
        void CreateReceivables(IEnumerable<Receivable> data);
        IEnumerable<Receivable> GetAll(Expression<Func<Receivable, bool>> predicate);
        Summary GetSummaryStatistics();
        Summary GetDebtorSummary(string debtorReference);
    }
}
