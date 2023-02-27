using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface ICurrencyRepository : IRepository<CurrencyEntity, string>
{
    new Task<IEnumerable<CurrencyEntity>> GetAllAsync();
    Task<IEnumerable<CurrencyEntity>> GetNonActiveCurrenciesAsync();
    Task<IEnumerable<CurrencyEntity>> GetActiveCurrenciesAsync();
    Task UpdateQuantityAsync(string code, float newQuantity);
}