using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class TransactionRepository : RepositoryBase<TransactionEntity, int>, ITransactionRepository
{
    public TransactionRepository(DbContext appDbContext) : base(appDbContext)
    {
    }
}