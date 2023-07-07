using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.Views.Courses;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.Courses;

public partial class CoursesPageViewModel : ViewModelBase
{
    private readonly ICurrencyFacade _currencyFacade;
    private readonly IPrinterService _printerService;

    public CoursesPageViewModel(ICurrencyFacade currencyFacade, IPrinterService printerService)
    {
        _currencyFacade = currencyFacade;
        _printerService = printerService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesForCoursesAsync();
    }

    [ObservableProperty]
    private List<CurrencyCoursesListModel> _currencies = new();

    [RelayCommand]
    private async Task GoToDetailsAsync(string code)
    {
        await Shell.Current.GoToAsync($"{nameof(CourseDetailPage)}?code={code}");
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        await _printerService.Print(Currencies);
    }
}