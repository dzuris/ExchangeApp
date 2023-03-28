using System.Collections.ObjectModel;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICurrencyFacade : IFacade
{
    Task<CurrencyDetailModel?> GetById(string id);
    Task<ObservableCollection<CurrencyListModel>> GetNonActiveCurrenciesAsync();
    Task<ObservableCollection<CurrencyListModel>> GetActiveCurrenciesAsync();
    Task<List<CurrencyTransactionListModel>> GetActiveCurrenciesForTransactionAsync();
    Task<List<CurrencyCoursesListModel>> GetActiveCurrenciesForCoursesAsync();
    Task<List<CurrencyHistoryModel>> GetCurrenciesHistory(DateTime dateTime);
    Task<decimal> GetCurrencyBalance(string currencyCode, DateTime date);
    Task UpdateAsync(CurrencyDetailModel model);
    Task UpdateStatus(string code, CurrencyState status);
}