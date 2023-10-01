using TP24Entities.Repositories;

namespace TP24Entities
{
    public interface IUnitOfWork : IDisposable
    {
        IReceivableRepository Receivables { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
