using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.ProfitCalculate;

public partial class ProfitCalculateViewModel : ViewModelBase
{
    private readonly IOperationFacade _operationFacade;

    public ProfitCalculateViewModel(IOperationFacade operationFacade)
    {
        _operationFacade = operationFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate);
        TotalProfit = ProfitList.Sum(e => e.Profit);
    }

    [ObservableProperty] 
    private string _domesticCurrencyCode = "EUR";

    [ObservableProperty]
    private decimal _totalProfit;

    [ObservableProperty]
    private DateTime _fromDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);

    [ObservableProperty]
    private DateTime _untilDate = DateTime.Today;

    [ObservableProperty]
    private List<CurrencyProfitModel> _profitList = new();

    [RelayCommand]
    private async Task CalculateAsync()
    {
        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate);
        TotalProfit = ProfitList.Sum(e => e.Profit);
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        UntilDate = DateTime.Today;

        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate);
        TotalProfit = ProfitList.Sum(e => e.Profit);
    }
}