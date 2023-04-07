using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsCoursesManagerViewModel : ViewModelBase
{
    private readonly ICurrencyFacade _currencyFacade;

    public SettingsCoursesManagerViewModel(ICurrencyFacade currencyFacade)
    {
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        ActiveCurrencies = await _currencyFacade.GetActiveCurrenciesAsync();
        NonActiveCurrencies = await _currencyFacade.GetNonActiveCurrenciesAsync();
    }

    [ObservableProperty]
    private ObservableCollection<CurrencyListModel> _activeCurrencies = new();

    [ObservableProperty]
    private ObservableCollection<CurrencyListModel> _nonActiveCurrencies = new();

    [RelayCommand]
    private async Task CurrencyToNonActiveAsync(CurrencyListModel currency)
    {
        ActiveCurrencies.Remove(currency);
        NonActiveCurrencies.Add(currency);

        await _currencyFacade.UpdateStatus(currency.Code, CurrencyStatus.NotInUse);
    }

    [RelayCommand]
    private async Task CurrencyToActiveAsync(CurrencyListModel currency)
    {
        NonActiveCurrencies.Remove(currency);
        ActiveCurrencies.Add(currency);

        await _currencyFacade.UpdateStatus(currency.Code, CurrencyStatus.Own);
    }
}