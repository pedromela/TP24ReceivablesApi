using TP24Entities.DAL;
using TP24Entities.Models;

namespace TP24Entities.Repositories
{
    public class ReceivableRepository : Repository<Receivable>, IReceivableRepository
    {
        public ReceivableRepository(ReceivablesContext receivablesContext)
        : base(receivablesContext)
        {

        }
    }
}
