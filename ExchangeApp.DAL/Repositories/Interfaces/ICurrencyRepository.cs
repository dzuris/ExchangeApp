using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICurrencyRepository : IRepository<CurrencyEntity, string>
{
    Task<IEnumerable<CurrencyEntity>> GetNonActiveCurrenciesAsync();
    Task<IEnumerable<CurrencyEntity>> GetActiveCurrenciesAsync();
    Task UpdateAsync(CurrencyEntity currency);
    Task UpdateQuantityAsync(string code, decimal newQuantity);
    Task UpdateAverageCourseAsync (string code, decimal newAverageCourse);
    Task UpdateStatus(string code, CurrencyState status);
}