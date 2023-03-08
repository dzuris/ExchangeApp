using System.Globalization;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.App.Views.Transaction;
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

    private bool _isFirstAppear = true;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (_isFirstAppear)
        {
            Currencies = await _currencyFacade.GetActiveCurrenciesForTransactionAsync();
            CurrencyFrom = Currencies.ElementAt(0);
            CurrencyTo = Currencies.ElementAt(0);
            _isFirstAppear = false;
        }
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
                Transaction.QuantityForeignCurrency = 0;
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
            Transaction.QuantityForeignCurrency = TransactionTypeProp == TransactionType.Buy 
                ? (decimal)quantityFromDecimal 
                : Utilities.Utilities.StrToDecimal(_quantityTo) ?? 1;
            
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

            OnPropertyChanged(nameof(ToPay));
            OnPropertyChanged(nameof(ForPayment));
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
                Transaction.QuantityForeignCurrency = 0;
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
            Transaction.QuantityForeignCurrency = TransactionTypeProp == TransactionType.Sell 
                ? (decimal)quantityToDecimal 
                : Utilities.Utilities.StrToDecimal(_quantityFrom) ?? 1;
            
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

            OnPropertyChanged(nameof(ToPay));
            OnPropertyChanged(nameof(ForPayment));
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
                var result = TransactionTypeProp == TransactionType.Buy 
                    ? Math.Round((decimal)(quantityFromDecimal / courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture) 
                    : Math.Round((decimal)(quantityFromDecimal * courseRateDecimal), 2).ToString(CultureInfo.CurrentCulture);
                QuantityTo = result;
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }

            OnPropertyChanged(nameof(ToPay));
            OnPropertyChanged(nameof(ForPayment));
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

            var result = (Utilities.Utilities.StrToDecimal(Payment) ?? 0) - ToPay;

            return result < 0 ? 0 : result;
        }
    }

    public decimal ToPay
    {
        get
        {
            return TransactionTypeProp switch
            {
                null => 0,
                TransactionType.Buy => Transaction.QuantityForeignCurrency,
                _ => Transaction.TotalAmountDomesticCurrency
            };
        }
    }

    public decimal ForPayment
    {
        get
        {
            return TransactionTypeProp switch
            {
                null => 0,
                TransactionType.Buy => Transaction.TotalAmountDomesticCurrency,
                _ => Transaction.QuantityForeignCurrency
            };
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

        Payment = string.Empty;

        OnPropertyChanged(nameof(ToPay));
        OnPropertyChanged(nameof(ForPayment));
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

        Payment = string.Empty;

        OnPropertyChanged(nameof(ToPay));
        OnPropertyChanged(nameof(ForPayment));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var validationMessage = ValidateData();

        if (validationMessage != string.Empty)
        {
            var rm = new ResourceManager(typeof(ErrorResources));
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("DisplayAlertValidationErrorTitle"),
                validationMessage,
                rm.GetString("DisplayAlertCancelButtonText"))!;
            return;
        }
        
        Transaction.CurrencyCode = Transaction.Currency!.Code;
        
        if (Transaction.TotalAmountDomesticCurrency > 1000)
        {
            // Needs to go to the customer create page
            await Shell.Current.GoToAsync($"{nameof(NewCustomerIndividualPage)}", true, new Dictionary<string, object>
            {
                {"Transaction", Transaction},
                {"CurrencyFrom", CurrencyFrom!},
                {"CurrencyTo", CurrencyTo!}
            });
        }
        else
        {
            try
            {
                // Create transaction because it is not over 1000 euros
                var id = await _transactionFacade.InsertAsync(Transaction);
                Transaction.Id = id;

                // Updates currencies quantities
                if (Transaction.TransactionType == TransactionType.Buy)
                {
                    await _currencyFacade.UpdateQuantityAsync(CurrencyFrom!.Code,
                        CurrencyFrom.Quantity + Transaction.QuantityForeignCurrency);
                    await _currencyFacade.UpdateQuantityAsync(CurrencyTo!.Code, 
                        CurrencyTo.Quantity - Transaction.TotalAmountDomesticCurrency);
                }
                else
                {
                    await _currencyFacade.UpdateQuantityAsync(CurrencyFrom!.Code,
                        CurrencyFrom.Quantity + Transaction.TotalAmountDomesticCurrency);
                    await _currencyFacade.UpdateQuantityAsync(CurrencyTo!.Code,
                        CurrencyTo.Quantity - Transaction.QuantityForeignCurrency);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // Go to transaction detail page
            await Shell.Current.GoToAsync($"../{nameof(TransactionDetailPage)}", true, new Dictionary<string, object>
            {
                {"Transaction", Transaction}
            });
        }
    }

    private string ValidateData()
    {
        var rm = new ResourceManager(typeof(TransactionPageResources));
        var errorMessage = string.Empty;

        const string domesticCurrencyCode = "EUR";

        // Currencies can not be null, one must be domestic and the other one different
        if (CurrencyTo is null 
            || CurrencyFrom is null 
            ||
            (CurrencyTo.Code != domesticCurrencyCode 
                && CurrencyFrom.Code != domesticCurrencyCode) 
            || (CurrencyFrom.Code == domesticCurrencyCode 
                && CurrencyTo.Code == domesticCurrencyCode))
            errorMessage += rm.GetString("ErrorMessage_CurrencySelection") + "\n";

        // Quantity from validation
        var quantityFromDecimal = Utilities.Utilities.StrToDecimal(QuantityFrom);
        if (string.IsNullOrWhiteSpace(QuantityFrom) || quantityFromDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_QuantityFromNotValid") + "\n";

        // Quantity to validation
        var quantityToDecimal = Utilities.Utilities.StrToDecimal(QuantityTo);
        if (string.IsNullOrWhiteSpace(QuantityTo) || quantityToDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_QuantityToNotValid") + "\n";

        // Course rate validation
        var courseRateToDecimal = Utilities.Utilities.StrToDecimal(CourseRate);
        if (string.IsNullOrWhiteSpace(CourseRate) || courseRateToDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_CourseRateNotValid") + "\n";
        // Course rate can not exceed opposite course rate
        else if (CurrencyFrom is not null && CurrencyTo is not null)
        {
            switch (TransactionTypeProp)
            {
                case TransactionType.Buy when (decimal)courseRateToDecimal < CurrencyFrom.SellRate:
                case TransactionType.Sell when (decimal)courseRateToDecimal > CurrencyTo.BuyRate:
                    errorMessage += rm.GetString("ErrorMessage_CourseRateExceededOtherOne") + "\n";
                    break;
            }
        }

        // Not enough money in cash register check
        if (CurrencyTo is not null)
        {
            if (quantityToDecimal is null || quantityToDecimal > CurrencyTo.Quantity)
                errorMessage += rm.GetString("ErrorMessage_InsufficientMoneyInCashRegister") + "\n";
        }

        return errorMessage;
    }
}