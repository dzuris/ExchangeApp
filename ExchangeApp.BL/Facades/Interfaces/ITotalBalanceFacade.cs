using System.Collections.ObjectModel;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ITotalBalanceFacade : IFacade
{
    Task<ObservableCollection<TotalBalanceModel>> GetAllAsync();
    Task<IEnumerable<TotalBalanceModel>> GetAllAsync(DateTime from, DateTime until);
    Task<ObservableCollection<TotalBalanceModel>> GetFilteredAsync(TotalBalanceFilterOption option, DateTime? dateFrom, DateTime? dateUntil);
    Task<int> InsertAsync(TotalBalanceModel model);
    Task<bool> CanCreateMonthlyTotalBalance();
}