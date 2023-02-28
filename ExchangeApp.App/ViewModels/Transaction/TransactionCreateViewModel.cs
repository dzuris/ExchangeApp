using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Transaction;
using Microsoft.Extensions.Logging;

namespace ExchangeApp.App.ViewModels.Transaction;

public partial class TransactionCreateViewModel : ViewModelBase
{
    private readonly ILogger<TransactionCreateViewModel> _logger;
    private readonly ITransactionFacade _transactionFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public TransactionCreateViewModel(
        ILogger<TransactionCreateViewModel> logger, 
        ITransactionFacade transactionFacade, 
        ICurrencyFacade currencyFacade)
    {
        _logger = logger;
        _transactionFacade = transactionFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesForTransactionAsync();
    }

    [ObservableProperty]
    private List<CurrencyNewTransactionModel> _currencies = new();

    [RelayCommand]
    private async Task SaveAsync()
    {
        var transaction = TransactionDetailModel.Empty;
        await Shell.Current.GoToAsync($"{nameof(NewCustomerIndividualPage)}", true, new Dictionary<string, object>
        {
            {"Transaction", transaction}
        });
    }
}