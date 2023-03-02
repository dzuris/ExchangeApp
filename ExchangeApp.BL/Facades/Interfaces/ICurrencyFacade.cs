using System.Collections.ObjectModel;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICurrencyFacade : IFacade
{
    Task<CurrencyDetailModel?> GetById(string id);
    Task<ObservableCollection<CurrencyListModel>> GetNonActiveCurrenciesAsync();
    Task<ObservableCollection<CurrencyListModel>> GetActiveCurrenciesAsync();
    Task<List<CurrencyNewTransactionModel>> GetActiveCurrenciesForTransactionAsync();
    Task<List<CurrencyCoursesListModel>> GetActiveCurrenciesForCoursesAsync();
    Task UpdateAsync(CurrencyDetailModel model);
    Task UpdateQuantityAsync(string code, decimal newQuantity);
    Task UpdateStatus(string code, CurrencyState status);
}