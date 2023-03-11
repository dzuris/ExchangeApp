using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.CashRegister;

public partial class CashRegisterStatusViewModel : ViewModelBase
{
    public string DomesticCurrencyCode => "EUR";
    private readonly ICurrencyFacade _currencyFacade;

    public CashRegisterStatusViewModel(ICurrencyFacade currencyFacade)
    {
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesAsync();

        var foreignSum = Currencies.Where(currency => currency.Code != DomesticCurrencyCode).Sum(currency => currency.ExchangeRateValue);
        ForeignCurrenciesValue = Math.Round(foreignSum, 2);

        var totalSum = ForeignCurrenciesValue +
                       Currencies.Single(c => c.Code == DomesticCurrencyCode).ExchangeRateValue;
        TotalCurrenciesValue = Math.Round(totalSum, 2);
    }

    [ObservableProperty]
    private IEnumerable<CurrencyListModel> _currencies = new List<CurrencyListModel>();

    [ObservableProperty] 
    private decimal _foreignCurrenciesValue;

    [ObservableProperty]
    private decimal _totalCurrenciesValue;
}