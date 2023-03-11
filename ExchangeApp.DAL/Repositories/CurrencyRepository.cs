using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class CurrencyRepository : RepositoryBase<CurrencyEntity, string>, ICurrencyRepository
{
    private const string DomesticCurrencyCode = "EUR";
    private readonly IMapper _mapper;

    public CurrencyRepository(DbContext appDbContext, IMapper mapper) 
        : base(appDbContext)
    {
        _mapper = mapper;
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
        var list = await AppDbContext
            .Set<CurrencyEntity>()
            .AsNoTracking()
            .Where(e => e.Status != CurrencyState.NotInUse)
            .OrderBy(item => item.Code != DomesticCurrencyCode)
            .ToListAsync();
        return list;
    }

    public async Task UpdateQuantityAsync(string code, decimal newQuantity)
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
    }

    public async Task UpdateAverageCourseAsync(string code, decimal newAverageCourse)
    {
        var entity = await GetByIdAsync(code);

        if (entity == null)
        {
            return;
        }

        entity.AverageCourseRate = newAverageCourse;
        AppDbContext
            .Set<CurrencyEntity>()
            .Update(entity);
    }

    public async Task UpdateStatus(string code, CurrencyState status)
    {
        var entity = await GetByIdAsync(code);

        if (entity == null) return;

        entity.Status = status;
        AppDbContext
            .Set<CurrencyEntity>()
            .Update(entity);
    }

    public async Task UpdateAsync(CurrencyEntity entity)
    {
        var existingEntity = await GetByIdAsync(entity.Code);

        if (existingEntity == null)
        {
            return;
        }

        _mapper.Map(entity, existingEntity);
        AppDbContext
            .Set<CurrencyEntity>()
            .Update(existingEntity);
    }
}