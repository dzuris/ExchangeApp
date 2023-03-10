﻿using System.Collections.ObjectModel;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IOperationFacade : IFacade
{
    Task<ObservableCollection<OperationListModelBase>> GetOperationsAsync(int pageSize, int pageNumber);
    Task<ObservableCollection<OperationListModelBase>> GetFilteredOperationsAsync(int pageSize, int pageNumber, OperationFilterOption option, int? id, DateTime? from, DateTime? until);
}