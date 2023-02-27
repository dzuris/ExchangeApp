using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class CurrencyRepository : RepositoryBase<CurrencyEntity, string>, ICurrencyRepository
{
    private const string DomesticCurrencyCode = "EUR";

    public CurrencyRepository(DbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<IEnumerable<CurrencyEntity>> GetAllAsync()
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .OrderBy(item => item.Code != DomesticCurrencyCode)
            .ToListAsync();
    }

    public async Task<IEnumerable<CurrencyEntity>> GetNonActiveCurrenciesAsync()
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .Where(e => e.Status == CurrencyState.NotInUse)
            .OrderBy(item => item.Code != DomesticCurrencyCode)
            .ToListAsync();
    }

    public async Task<IEnumerable<CurrencyEntity>> GetActiveCurrenciesAsync()
    {
        return await AppDbContext
            .Set<CurrencyEntity>()
            .Where(e => e.Status != CurrencyState.NotInUse)
            .OrderBy(item => item.Code != DomesticCurrencyCode)
            .ToListAsync();
    }

    public async Task UpdateQuantityAsync(string code, float newQuantity)
    {
        var entity = await GetByIdAsync(code);

        if (entity == null)
        {
            return;
        }

        entity.Quantity = newQuantity;
        AppDbContext
            .Set<CurrencyEntity>()
            .Update(entity);
        await AppDbContext.SaveChangesAsync();
    }
}