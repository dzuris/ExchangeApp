using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class CurrencyRepository : RepositoryBase<CurrencyEntity, string>, ICurrencyRepository
{
    public CurrencyRepository(DbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<CurrencyEntity?> GetById(string id)
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .SingleOrDefaultAsync(e => e.Code == id);
    }

    public async Task<IEnumerable<CurrencyEntity>> GetNonActiveCurrenciesAsync()
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .Where(e => e.Status == CurrencyState.NotInUse)
            .ToListAsync();
    }

    public async Task<IEnumerable<CurrencyEntity>> GetActiveCurrenciesAsync()
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .Where(e => e.Status != CurrencyState.NotInUse)
            .ToListAsync();
    }
}