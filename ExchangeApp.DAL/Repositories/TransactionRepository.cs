using AutoMapper;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class TransactionRepository : RepositoryBase<TransactionEntity, int>, ITransactionRepository
{
    private readonly IMapper _mapper;

    public TransactionRepository(DbContext appDbContext, IMapper mapper) : base(appDbContext)
    {
        _mapper = mapper;
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

    /// <summary>
    /// Gets list of transaction on which update is required, update will be on quantity before and average course rate columns
    /// </summary>
    /// <param name="entity">Transaction entity which is canceled</param>
    /// <returns>List of transactions after canceled transaction which is included</returns>
    public async Task<IEnumerable<TransactionEntity>> GetTransactionsForStornoUpdate(TransactionEntity entity)
    {
        var list = await AppDbContext
            .Set<TransactionEntity>()
            .Where(t => t.CurrencyCode == entity.CurrencyCode && t.Time >= entity.Time)
            .OrderBy(t => t.Time)
            .ToListAsync();

        return list;
    }

    public async Task UpdateAsync(TransactionEntity entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);

        if (existingEntity == null)
        {
            return;
        }

        _mapper.Map(entity, existingEntity);
        AppDbContext
            .Set<TransactionEntity>()
            .Update(existingEntity);
    }

    public async Task<decimal> GetAverageCourseOfTransactionBefore(TransactionEntity entity)
    {
        var result = await AppDbContext
            .Set<TransactionEntity>()
            .Where(t => t.CurrencyCode == entity.CurrencyCode)
            .OrderBy(t => t.Id)
            .LastOrDefaultAsync(t => t.Id < entity.Id);

        return result?.AverageCourseRate ?? 0;
    }
}