using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views.Donation;
using ExchangeApp.App.Views.OperationsList;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Facades.Interfaces;

namespace ExchangeApp.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IOperationFacade _operationFacade;

    public MainViewModel(IOperationFacade operationFacade)
    {
        _operationFacade = operationFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        TodayOperationsCount = await _operationFacade.GetTodayOperationsCount();
    }

    [ObservableProperty]
    private int _todayOperationsCount;

    [RelayCommand]
    private async Task GoToTransactionCreatePageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(TransactionCreatePage)}");
    }

    [RelayCommand]
    private async Task GoToDonationCreatePageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(DonationCreatePage)}");
    }

    [RelayCommand]
    private async Task GoToOperationsListPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(OperationsListPage)}");
    }
}