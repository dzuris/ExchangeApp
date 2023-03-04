using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class TransactionRepository : RepositoryBase<TransactionEntity, int>, ITransactionRepository
{
    public TransactionRepository(DbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<int> InsertAsync(TransactionEntity entity)
    {
        await AppDbContext
            .Set<TransactionEntity>()
            .AddAsync(entity);
        await AppDbContext.SaveChangesAsync();

        return entity.Id;
    }
}