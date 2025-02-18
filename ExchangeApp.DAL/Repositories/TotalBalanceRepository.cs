﻿using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class TotalBalanceRepository : RepositoryBase<TotalBalanceEntity, Guid>, ITotalBalanceRepository
{
    public TotalBalanceRepository(DbContext appDbContext)
        : base(appDbContext)
    {
    }

    public async Task<IEnumerable<TotalBalanceEntity>> GetAllAsync(DateTime from, DateTime until)
    {
        var list = await AppDbContext
            .Set<TotalBalanceEntity>()
            .Where(e => e.Created > from && e.Created <= until)
            .ToListAsync();
        return list;
    }

    public override async Task<int> InsertAsync(TotalBalanceEntity entity)
    {
        await AppDbContext
            .Set<TotalBalanceEntity>()
            .AddAsync(entity);
        await AppDbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<DateTime> GetLastTotalBalanceDate(TotalBalanceType type)
    {
        var result = await AppDbContext
            .Set<TotalBalanceEntity>()
            .Where(e => e.Type == type)
            .OrderByDescending(e => e.Created)
            .FirstOrDefaultAsync();

        return result?.Created ?? DateTime.MinValue;
    }

    public async Task<IEnumerable<TotalBalanceEntity>> GetFilteredAsync(TotalBalanceFilterOption option, DateTime? dateFrom, DateTime? dateUntil)
    {
        IQueryable<TotalBalanceEntity> query = AppDbContext.Set<TotalBalanceEntity>();

        switch (option)
        {
            case TotalBalanceFilterOption.Daily:
                query = query.Where(e => e.Type == TotalBalanceType.Daily);
                break;
            case TotalBalanceFilterOption.Monthly:
                query = query.Where(e => e.Type == TotalBalanceType.Monthly);
                break;
            case TotalBalanceFilterOption.All:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }

        if (dateFrom is not null && dateUntil is not null)
        {
            query = query.Where(e => e.Created > dateFrom && e.Created < dateUntil);
        }

        var list = await query.ToListAsync();

        return list;
    }

    /// <summary>
    /// Checks if exist any unclosed operations
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ExistsOperationAfterLastDailyTotalBalance()
    {
        var lastTotalBalanceDate = await GetLastTotalBalanceDate(TotalBalanceType.Daily);
        var result = await AppDbContext
            .Set<OperationEntityBase>()
            .Where(e => e.Created > lastTotalBalanceDate)
            .FirstOrDefaultAsync();

        return result is not null;
    }
}