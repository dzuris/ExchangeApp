using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<TransactionEntity, int>
{
    new Task<int> InsertAsync(TransactionEntity entity);
    Task<IEnumerable<TransactionEntity>> GetTransactionsForStornoUpdate(TransactionEntity entity);
    Task UpdateAsync(TransactionEntity entity);
    Task<decimal> GetAverageCourseOfTransactionBefore(TransactionEntity entity);
}