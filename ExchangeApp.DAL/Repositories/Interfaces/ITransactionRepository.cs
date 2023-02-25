using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<TransactionEntity, int>
{
}