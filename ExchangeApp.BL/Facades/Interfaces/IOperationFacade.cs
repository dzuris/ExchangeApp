using System.Collections.ObjectModel;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Operations;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IOperationFacade : IFacade
{
    Task<ObservableCollection<OperationListModelBase>> GetOperationsAsync(int pageSize, int pageNumber);
    Task<IEnumerable<OperationListModelBase>> GetOperationsAsync(DateTime from, DateTime until);
    Task<ObservableCollection<OperationListModelBase>> GetFilteredOperationsAsync(int pageSize, int pageNumber, OperationFilterOption option, int? id, DateTime? from, DateTime? until);
    Task<int> GetTodayOperationsCount();
    Task<List<CurrencyProfitModel>> GetProfitListAsync(DateTime from, DateTime until);
    Task<List<OperationProfitModel>> GetOperationsProfitAsync(DateTime from, DateTime until);
    Task<bool> CanCancel(DateTime operationCreation);
}