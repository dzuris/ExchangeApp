using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class OperationRepository : RepositoryBase<OperationEntityBase, int>, IOperationRepository
{
    private readonly IMapper _mapper;

    public OperationRepository(DbContext appDbContext, IMapper mapper)
        : base(appDbContext)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<OperationEntityBase>> GetLastOperationsAsync(int pageSize, int pageNumber)
    {
        var list = await AppDbContext.Set<OperationEntityBase>()
            .OrderByDescending(o => o.Created)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return list;
    }

    public async Task<IEnumerable<OperationEntityBase>> GetOperationsAsync(DateTime from, DateTime until)
    {
        var list = await AppDbContext
            .Set<OperationEntityBase>()
            .Where(o => o.Created > from && o.Created < until)
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
            query = query.Where(o => o.Created.Date >= from && o.Created.Date <= until);
        }

        var list = await query
            .OrderByDescending(o => o.Created)
            .Skip(pageSize * (pageNumber -1))
            .Take(pageSize)
            .ToListAsync();
        return list;
    }

    public async Task<int> GetTodayOperationsCount()
    {
        var today = DateTime.Today;
        var count = await AppDbContext.Set<OperationEntityBase>()
            .CountAsync(o => o.Created >= today && o.Created < today.AddDays(1));
        return count;
    }

    /// <summary>
    /// Returns list of operations which were created after given operation, the operations should have same currency code
    /// </summary>
    /// <param name="entity">Operation which we are cancelling</param>
    /// <returns>List of operations with same currency code created after given operation</returns>
    public async Task<IEnumerable<OperationEntityBase>> GetOperationsForStornoUpdate(OperationEntityBase entity)
    {
        var list = await AppDbContext
            .Set<OperationEntityBase>()
            .Where(t => t.CurrencyCode == entity.CurrencyCode && t.Created > entity.Created)
            .OrderBy(t => t.Created)
            .ToListAsync();
        return list;
    }

    public async Task UpdateAsync(OperationEntityBase entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);

        if (existingEntity == null)
        {
            return;
        }

        _mapper.Map(entity, existingEntity);
        AppDbContext
            .Set<OperationEntityBase>()
            .Update(existingEntity);
    }

    public async Task<decimal> GetAverageCourseOfOperationBefore(OperationEntityBase entity)
    {
        var result = await AppDbContext
            .Set<OperationEntityBase>()
            .Where(t => t.CurrencyCode == entity.CurrencyCode)
            .OrderByDescending(o => o.Created)
            .FirstOrDefaultAsync(o => o.Created < entity.Created);

        return result?.AverageCourseRate ?? 0;
    }

    /// <summary>
    /// Gets only sell transactions and not deposit donations, between two dates and not canceled
    /// </summary>
    /// <param name="from">Only operations after</param>
    /// <param name="until">Only operations before</param>
    /// <returns>List of OperationEntityBase</returns>
    public async Task<IEnumerable<OperationEntityBase>> GetOperationsForProfitCalculationAsync(DateTime from, DateTime until)
    {
        var list = await AppDbContext
            .Set<OperationEntityBase>()
            .Include(o => o.Currency)
            .Where(o => !o.IsCanceled)
            .Where(o => o is TransactionEntity 
                        && ((TransactionEntity)o).TransactionType == TransactionType.Sell
                        || o is DonationEntity 
                        && ((DonationEntity)o).Type != DonationType.Deposit)
            .Where(o => o.Created >= from && o.Created <= until)
            .ToListAsync();

        return list;
    }

    public async Task<bool> CanCancel(DateTime operationCreation)
    {
        var lastTotalBalance = await AppDbContext
            .Set<TotalBalanceEntity>()
            .Where(e => e.Type == TotalBalanceType.Daily)
            .OrderByDescending(e => e.Created)
            .FirstOrDefaultAsync();

        if (lastTotalBalance is not null)
        {
            return lastTotalBalance.Created < operationCreation;
        }

        return false;
    }
}