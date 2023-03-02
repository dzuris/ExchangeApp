using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICurrencyFacade : IFacade
{
    Task<CurrencyDetailModel?> GetById(string id);
    Task<List<CurrencyListModel>> GetNonActiveCurrenciesAsync();
    Task<List<CurrencyListModel>> GetActiveCurrenciesAsync();
    Task<List<CurrencyNewTransactionModel>> GetActiveCurrenciesForTransactionAsync();
    Task<List<CurrencyCoursesListModel>> GetActiveCurrenciesForCoursesAsync();
    Task UpdateQuantityAsync(string code, float newQuantity);
    Task UpdateAsync(CurrencyDetailModel model);
}