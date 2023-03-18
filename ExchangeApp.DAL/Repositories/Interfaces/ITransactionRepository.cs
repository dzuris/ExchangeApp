using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<TransactionEntity, int>
{
    new Task<int> InsertAsync(TransactionEntity entity);
}