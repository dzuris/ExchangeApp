using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class OperationRepository : RepositoryBase<OperationEntityBase, int>, IOperationRepository
{
    public OperationRepository(DbContext appDbContext)
        : base(appDbContext)
    {
    }

    public async Task<IEnumerable<OperationEntityBase>> GetLastOperationsAsync(int pageSize, int pageNumber)
    {
        var list = await AppDbContext.Set<OperationEntityBase>()
            .OrderByDescending(o => o.Time)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return list;
    }
}