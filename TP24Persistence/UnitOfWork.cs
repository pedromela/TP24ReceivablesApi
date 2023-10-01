using TP24Entities.Repositories;

namespace TP24Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReceivablesContext _context;

        public UnitOfWork(ReceivablesContext context)
        {
            _context = context;
            Receivables = new ReceivableRepository(_context);
        }
        public IReceivableRepository Receivables { get; private set; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
