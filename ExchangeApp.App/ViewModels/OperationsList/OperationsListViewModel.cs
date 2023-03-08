using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.OperationsList;

public partial class OperationsListViewModel : ViewModelBase
{
    private readonly IOperationFacade _operationFacade;

    public OperationsListViewModel(IOperationFacade operationFacade)
    {
        _operationFacade = operationFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Operations = await _operationFacade.GetOperationsAsync(20, 1);
    }

    [ObservableProperty]
    private ObservableCollection<OperationListModelBase> _operations = new();
}