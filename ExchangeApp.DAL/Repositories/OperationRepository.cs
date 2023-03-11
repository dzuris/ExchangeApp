using ExchangeApp.Common.Enums;
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

    public async Task<IEnumerable<OperationEntityBase>> GetFilteredOperationsAsync(int pageSize, int pageNumber, OperationFilterOption option, int? id, DateTime? from, DateTime? until)
    {
        IQueryable<OperationEntityBase> query = AppDbContext.Set<OperationEntityBase>();

        switch (option)
        {
            case OperationFilterOption.Transactions:
                query = query.OfType<TransactionEntity>();
                break;
            case OperationFilterOption.Donations:
                query = query.OfType<DonationEntity>();
                break;
        }

        if (id is not null)
        {
            query = query.Where(o => o.Id == id);
        }

        if (from is not null && until is not null)
        {
            query = query.Where(o => o.Time.Date >= from && o.Time.Date <= until);
        }

        var list = await query
            .OrderByDescending(o => o.Time)
            .Skip(pageSize * (pageNumber -1))
            .Take(pageSize)
            .ToListAsync();
        return list;
    }
}