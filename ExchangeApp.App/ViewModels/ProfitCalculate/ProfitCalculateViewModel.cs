using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;

namespace ExchangeApp.App.ViewModels.ProfitCalculate;

public partial class ProfitCalculateViewModel : ViewModelBase
{
    private readonly IOperationFacade _operationFacade;
    private readonly IPrinterService _printerService;

    public ProfitCalculateViewModel(IOperationFacade operationFacade, IPrinterService printerService)
    {
        _operationFacade = operationFacade;
        _printerService = printerService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate.AddDays(1));
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
        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate.AddDays(1));
        TotalProfit = ProfitList.Sum(e => e.Profit);
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        UntilDate = DateTime.Today;

        ProfitList = await _operationFacade.GetProfitListAsync(FromDate, UntilDate.AddDays(1));
        TotalProfit = ProfitList.Sum(e => e.Profit);
    }

    [RelayCommand]
    public async Task PrintAsync()
    {
        await _printerService.Print(ProfitList, FromDate, UntilDate);
    }
}