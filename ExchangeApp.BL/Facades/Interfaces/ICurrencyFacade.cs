using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICurrencyFacade : IFacade
{
    Task<CurrencyDetailModel?> GetById(string id);
    Task<List<CurrencyListModel>> GetNonActiveCurrenciesAsync();
    Task<List<CurrencyListModel>> GetActiveCurrenciesAsync();
}