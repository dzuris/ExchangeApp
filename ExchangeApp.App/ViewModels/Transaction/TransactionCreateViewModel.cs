using System.Globalization;
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
    private string? _originalCourseRate;

    [ObservableProperty] 
    private TransactionDetailModel _transaction = TransactionDetailModel.Empty;

    [ObservableProperty]
    private List<CurrencyTransactionListModel> _currencies = new();

    [ObservableProperty] 
    private CurrencyTransactionListModel? _currencyFrom;

    [ObservableProperty]
    private CurrencyTransactionListModel? _currencyTo;

    private bool _quantityCalculatorBlocked;
    private string _quantityFrom = string.Empty;
    public string QuantityFrom
    {
        get => _quantityFrom;
        set
        {
            SetProperty(ref _quantityFrom, value);

            // If empty then resets the fields
            if (_quantityFrom.Length == 0)
            {
                Transaction.Quantity = 0;
                OnPropertyChanged(nameof(Transaction));
                return;
            }

            var quantityFromDecimal = Utilities.Utilities.StrToDecimal(value);
            if (quantityFromDecimal is null or <= 0) return;

            var courseRateDecimal = Utilities.Utilities.StrToDecimal(CourseRate);

            switch (courseRateDecimal)
            {
                case null:
                    return;
                case > 0:
                    // Calculate quantityTo and sets it to two decimal points
                    if (_quantityCalculatorBlocked) break;
                    _quantityTo = TransactionTypeProp == TransactionType.Buy 
                        ? Math.Round((decimal)(quantityFromDecimal / courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture) 
                        : Math.Round((decimal)(quantityFromDecimal * courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture);
                    break;
            }

            // Sets transaction quantity according to transaction type
            Transaction.Quantity = TransactionTypeProp == TransactionType.Buy 
                ? (decimal)quantityFromDecimal : Utilities.Utilities.StrToDecimal(_quantityTo) ?? 1;
            
            OnPropertyChanged(nameof(Transaction));
            OnPropertyChanged(nameof(Tip));

            if (_quantityCalculatorBlocked) return;
            try
            {
                _quantityCalculatorBlocked = true;
                OnPropertyChanged(nameof(QuantityTo));
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }
        }
    }
    
    private string _quantityTo = string.Empty;
    public string QuantityTo
    {
        get => _quantityTo;
        set
        {
            SetProperty(ref _quantityTo, value);

            // If empty then resets the fields
            if (_quantityTo.Length == 0)
            {
                Transaction.Quantity = 0;
                OnPropertyChanged(nameof(Transaction));
                return;
            }

            var quantityToDecimal = Utilities.Utilities.StrToDecimal(_quantityTo);
            if (quantityToDecimal is null or <= 0) return;

            var courseRateDecimal = Utilities.Utilities.StrToDecimal(CourseRate);

            switch (courseRateDecimal)
            {
                case null:
                    return;
                case > 0:
                    // Calculate quantityFrom and sets it to two decimal points
                    if (_quantityCalculatorBlocked) break;
                    _quantityFrom = TransactionTypeProp == TransactionType.Buy 
                        ? Math.Round((decimal)(quantityToDecimal * courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture) 
                        : Math.Round((decimal)(quantityToDecimal / courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture);
                    break;
            }

            // Sets transaction quantity according to transaction type
            Transaction.Quantity = TransactionTypeProp == TransactionType.Sell 
                ? (decimal)quantityToDecimal : Utilities.Utilities.StrToDecimal(_quantityFrom) ?? 1;
            
            OnPropertyChanged(nameof(Transaction));
            OnPropertyChanged(nameof(Tip));

            if (_quantityCalculatorBlocked) return;
            try
            {
                _quantityCalculatorBlocked = true;
                OnPropertyChanged(nameof(QuantityFrom));
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }
        }
    }
    
    private string _courseRate = string.Empty;
    public string CourseRate
    {
        get => _courseRate;
        set
        {
            SetProperty(ref _courseRate, value);

            var courseRateDecimal = Utilities.Utilities.StrToDecimal(_courseRate);

            if (courseRateDecimal is null or <= 0) { return; }
            Transaction.CourseRate = (decimal)courseRateDecimal;
            OnPropertyChanged(nameof(Transaction));

            var quantityFromDecimal = Utilities.Utilities.StrToDecimal(QuantityFrom);
            if (quantityFromDecimal is null) { return; }

            try
            {
                _quantityCalculatorBlocked = true;
                var result = Math.Round((decimal)(quantityFromDecimal / courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture);
                QuantityTo = result;
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }
        }
    }

    private TransactionType? _transactionTypeProp;
    public TransactionType? TransactionTypeProp
    {
        get => _transactionTypeProp;
        set
        {
            SetProperty(ref _transactionTypeProp, value);

            Transaction.TransactionType = _transactionTypeProp ?? TransactionType.Buy;
            OnPropertyChanged(nameof(Transaction));
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Tip))]
    private string _payment = string.Empty;

    public decimal Tip
    {
        get
        {
            if (Payment.Length == 0)
            {
                return 0;
            }

            var quantityFromDecimal = Utilities.Utilities.StrToDecimal(QuantityFrom);

            if (quantityFromDecimal is null)
            {
                return 0;
            }

            var result = (Utilities.Utilities.StrToDecimal(Payment) ?? 0) - (decimal)quantityFromDecimal;

            return result < 0 ? 0 : result;
        }
    }

    // Currencies on change
    private const string DomesticCurrencyCode = "EUR";
    public void OnCurrencyFromChanged()
    {
        // Conditions same for both currencies
        if (CurrencyFrom is null || CurrencyTo is null)
        {
            return;
        }
        if (CurrencyFrom.Code == DomesticCurrencyCode && CurrencyTo.Code == DomesticCurrencyCode)
        {
            return;
        }

        // Continue only if working with foreign currency
        if (CurrencyFrom.Code == DomesticCurrencyCode) return;

        // Sets opposite currency to domestic
        CurrencyTo = Currencies.Find(c => c.Code == DomesticCurrencyCode);
        TransactionTypeProp = TransactionType.Buy;
        
        // Change course rate
        CourseRate = CurrencyFrom.BuyRate.ToString(CultureInfo.CurrentCulture);
        OriginalCourseRate = CourseRate;

        Transaction.Currency = CurrencyFrom;
        OnPropertyChanged(nameof(Transaction));

        QuantityFrom = string.Empty;
        QuantityTo = string.Empty;
        OnPropertyChanged(nameof(QuantityTo));
        OnPropertyChanged(nameof(QuantityFrom));
    }

    public void OnCurrencyToChanged()
    {
        // Conditions same for both currencies
        if (CurrencyFrom is null || CurrencyTo is null)
        {
            return;
        }
        if (CurrencyFrom.Code == DomesticCurrencyCode && CurrencyTo.Code == DomesticCurrencyCode)
        {
            return;
        }

        // Continue only if working with foreign currency
        if (CurrencyTo.Code == DomesticCurrencyCode) return;

        // Sets opposite currency to domestic
        CurrencyFrom = Currencies.Find(c => c.Code == DomesticCurrencyCode);
        TransactionTypeProp = TransactionType.Sell;
        
        CourseRate = CurrencyTo.SellRate.ToString(CultureInfo.CurrentCulture);
        OriginalCourseRate = CourseRate;

        Transaction.Currency = CurrencyTo;
        OnPropertyChanged(nameof(Transaction));

        QuantityFrom = string.Empty;
        QuantityTo = string.Empty;
        OnPropertyChanged(nameof(QuantityTo));
        OnPropertyChanged(nameof(QuantityFrom));
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