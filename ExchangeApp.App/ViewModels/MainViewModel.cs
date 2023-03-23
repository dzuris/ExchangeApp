using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views.Donation;
using ExchangeApp.App.Views.OperationsList;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Facades.Interfaces;

namespace ExchangeApp.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string DomesticCurrencySymbol => "€";
    private readonly IOperationFacade _operationFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public MainViewModel(IOperationFacade operationFacade, ICurrencyFacade currencyFacade)
    {
        _operationFacade = operationFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        TodayOperationsCount = await _operationFacade.GetTodayOperationsCount();
        var domesticCurrency = await _currencyFacade.GetById("EUR");

        if (domesticCurrency is not null)
        {
            DomesticCurrencyQuantity = domesticCurrency.Quantity;
        }
    }

    [ObservableProperty] private int _todayOperationsCount;

    [ObservableProperty] private decimal _domesticCurrencyQuantity;

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