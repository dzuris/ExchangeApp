using System.Collections.ObjectModel;
using ExchangeApp.BL.Models;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IOperationFacade : IFacade
{
    Task<ObservableCollection<OperationListModelBase>> GetOperationsAsync(int pageSize, int pageNumber);
}