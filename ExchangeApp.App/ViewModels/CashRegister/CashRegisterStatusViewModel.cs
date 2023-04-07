using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.CashRegister;

public partial class CashRegisterStatusViewModel : ViewModelBase
{
    public string DomesticCurrencyCode => "EUR";
    private readonly ICurrencyFacade _currencyFacade;
    private readonly IPrinterService _printerService;

    public CashRegisterStatusViewModel(ICurrencyFacade currencyFacade, IPrinterService printerService)
    {
        _currencyFacade = currencyFacade;
        _printerService = printerService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = (await _currencyFacade.GetActiveCurrenciesAsync()).ToList();

        var foreignSum = Currencies.Where(currency => currency.Code != DomesticCurrencyCode).Sum(currency => currency.ExchangeRateValue);
        ForeignCurrenciesValue = Math.Round(foreignSum, 2);

        var totalSum = ForeignCurrenciesValue +
                       Currencies.Single(c => c.Code == DomesticCurrencyCode).ExchangeRateValue;
        TotalCurrenciesValue = Math.Round(totalSum, 2);
    }

    [ObservableProperty]
    private List<CurrencyListModel> _currencies = new();

    [ObservableProperty] 
    private decimal _foreignCurrenciesValue;

    [ObservableProperty]
    private decimal _totalCurrenciesValue;

    [RelayCommand]
    private async Task PrintAsync()
    {
        await _printerService.Print(Currencies);
    }
}