using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Transaction;

public partial class TransactionCreateViewModel : ViewModelBase
{
    private readonly ITransactionFacade _transactionFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public TransactionCreateViewModel(
        ITransactionFacade transactionFacade, 
        ICurrencyFacade currencyFacade)
    {
        _transactionFacade = transactionFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesForTransactionAsync();

        CurrencyFrom = Currencies.ElementAt(0);
        CurrencyTo = Currencies.ElementAt(0);
    }

    [ObservableProperty]
    private List<CurrencyNewTransactionModel> _currencies = new();

    [ObservableProperty] 
    private CurrencyNewTransactionModel? _currencyFrom;

    [ObservableProperty]
    private CurrencyNewTransactionModel? _currencyTo;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Tip))]
    private decimal _quantityFrom;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ForPayment))]
    private decimal _quantityTo;

    [ObservableProperty]
    private decimal _courseRate;

    [ObservableProperty]
    private string _payment;

    [ObservableProperty]
    private TransactionType? _transactionTypeProp;

    public decimal Tip => (Utilities.Utilities.StrToDecimal(Payment) ?? 0) - QuantityFrom;

    public decimal ForPayment => QuantityTo;

    private const string DomesticCurrencyCode = "EUR";
    
    public void OnCurrencyFromChanged()
    {

        if (CurrencyFrom is null || CurrencyTo is null)
        {
            return;
        }

        if (CurrencyFrom.Code == DomesticCurrencyCode && CurrencyTo.Code == DomesticCurrencyCode)
        {
            return;
        }

        if (CurrencyFrom.Code == DomesticCurrencyCode) return;

        // Sets opposite currency to domestic
        CurrencyTo = Currencies.Find(c => c.Code == DomesticCurrencyCode);
        TransactionTypeProp = TransactionType.Buy;

        if (CurrencyFrom is not null)
        {
            CourseRate = CurrencyFrom.BuyRate;
        }
    }

    public void OnCurrencyToChanged()
    {

        if (CurrencyFrom is null || CurrencyTo is null)
        {
            return;
        }

        if (CurrencyFrom.Code == DomesticCurrencyCode && CurrencyTo.Code == DomesticCurrencyCode)
        {
            return;
        }

        if (CurrencyTo.Code == DomesticCurrencyCode) return;

        // Sets opposite currency to domestic
        CurrencyFrom = Currencies.Find(c => c.Code == DomesticCurrencyCode);
        TransactionTypeProp = TransactionType.Sell;

        if (CurrencyTo is not null)
        {
            CourseRate = CurrencyTo.SellRate;
        }
    }

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