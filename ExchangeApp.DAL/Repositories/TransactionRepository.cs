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

    public async Task<IEnumerable<TransactionEntity>> GetTransactions(DateTime from, DateTime until)
    {
        var list = await AppDbContext
            .Set<TransactionEntity>()
            .Where(t => t.Time >= from && t.Time <= until)
            .Include(t => t.Customer)
            .ToListAsync();
        return list;
    }
}