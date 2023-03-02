﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.Courses;

[QueryProperty(nameof(Code), "code")]
public partial class CourseDetailViewModel : ViewModelBase
{
    private readonly ICurrencyFacade _currencyFacade;

    public CourseDetailViewModel(ICurrencyFacade currencyFacade)
    {
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currency = await _currencyFacade.GetById(Code);

        if (Currency is null) return;

        if (Currency.BuyRate is not null)
        {
            BuyRate = Currency.BuyRate.ToString();
        }

        if (Currency.SellRate is not null)
        {
            SellRate = Currency.SellRate.ToString();
        }
    }

    [ObservableProperty]
    string _code = string.Empty;

    [ObservableProperty] 
    private CurrencyDetailModel? _currency;

    [ObservableProperty] 
    private string _buyRate = string.Empty;

    [ObservableProperty] 
    private string _sellRate = string.Empty;

    [ObservableProperty] 
    private bool _isErrorMessageVisible;

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Currency is null)
            return;

        var resBuyRate = Utilities.Utilities.StrToDecimal(BuyRate);
        if (resBuyRate is null)
        {
            return;
        }

        var resSellRate = Utilities.Utilities.StrToDecimal(SellRate);
        if (resSellRate is null)
        {
            return;
        }

        if (resSellRate > resBuyRate)
        {
            IsErrorMessageVisible = true;
            return;
        }

        Currency.BuyRate = resBuyRate;
        Currency.SellRate = resSellRate;

        await _currencyFacade.UpdateAsync(Currency);

        await Shell.Current.GoToAsync("..");
    }
}