using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.Courses;

public partial class CoursesPageViewModel : ViewModelBase
{
    private readonly ICurrencyFacade _currencyFacade;

    public CoursesPageViewModel(ICurrencyFacade currencyFacade)
    {
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesForCoursesAsync();
    }

    [ObservableProperty]
    private IEnumerable<CurrencyCoursesListModel> _currencies = new List<CurrencyCoursesListModel>();

    [RelayCommand]
    private async Task GoToDetailsAsync(string code)
    {
        await Shell.Current.GoToAsync($"{nameof(CourseDetailPage)}?code={code}");
    }
}