using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class TransactionRepository : RepositoryBase<TransactionEntity, int>, ITransactionRepository
{
    public TransactionRepository(DbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<TransactionEntity?> GetByIdAsync(int id)
    {
        var entity = await AppDbContext
            .Set<TransactionEntity>()
            .Include(t => t.Customer)
            .Include(t => t.Currency)
            .FirstOrDefaultAsync(t => t.Id == id);

        return entity;
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