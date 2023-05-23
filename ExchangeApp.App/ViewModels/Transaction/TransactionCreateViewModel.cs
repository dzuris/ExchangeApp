using System.Globalization;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Transaction;

public partial class TransactionCreateViewModel : ViewModelBase
{
    public string DomesticCurrencyCode => "EUR";
    public List<TransactionType> TransactionTypes
        => Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList();
    
    private readonly ITransactionFacade _transactionFacade;
    private readonly ICurrencyFacade _currencyFacade;
    private readonly ISettingsFacade _settingsFacade;
    private readonly IPrinterService _printerService;

    public TransactionCreateViewModel(
        ITransactionFacade transactionFacade, 
        ICurrencyFacade currencyFacade, IPrinterService printerService, ISettingsFacade settingsFacade)
    {
        _transactionFacade = transactionFacade;
        _currencyFacade = currencyFacade;
        _printerService = printerService;
        _settingsFacade = settingsFacade;
    }

    private bool _isFirstAppear = true;
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (_isFirstAppear)
        {
            var currencies = await _currencyFacade.GetActiveCurrenciesForTransactionAsync();
            Currencies = currencies.Where(i => i.Code != DomesticCurrencyCode).ToList();
            var item = currencies.Single(i => i.Code == DomesticCurrencyCode);
            DomesticCurrencyQuantity = item.Quantity;

            _isFirstAppear = false;
        }
    }

    [ObservableProperty]
    private string? _originalCourseRate;

    [ObservableProperty]
    private decimal _domesticCurrencyQuantity;

    [ObservableProperty] 
    private TransactionDetailModel _transaction = TransactionDetailModel.Empty;

    [ObservableProperty]
    private List<CurrencyTransactionListModel> _currencies = new();
    
    private CurrencyTransactionListModel? _currency;

    public CurrencyTransactionListModel? Currency
    {
        get => _currency;
        set
        {
            SetProperty(ref _currency, value);
            Transaction.Currency = value;

            SetCourseRate(value, TransactionTypeProp);

            OnPropertyChanged(nameof(Transaction));
        }
    }

    private bool _quantityCalculatorBlocked;
    private string _quantityForeign = string.Empty;
    public string QuantityForeign
    {
        get => _quantityForeign;
        set
        {
            SetProperty(ref _quantityForeign, value);

            // If empty then resets the fields
            if (_quantityForeign.Length == 0)
            {
                Transaction.Quantity = 0;
                OnPropertyChanged(nameof(Transaction));
                return;
            }

            var quantityForeignDecimal = Utilities.Utilities.StrToDecimal(value);
            if (quantityForeignDecimal is null or <= 0) return;

            var courseRateDecimal = Utilities.Utilities.StrToDecimal(CourseRate);
            if (courseRateDecimal is null or <= 0) return;

            if (!_quantityCalculatorBlocked)
            {
                _quantityDomestic = Math.Round((decimal)(quantityForeignDecimal / courseRateDecimal), 2)
                    .ToString(CultureInfo.CurrentCulture);
            }

            // Sets transaction quantity according to transaction type
            Transaction.Quantity = (decimal)quantityForeignDecimal;
            
            OnPropertyChanged(nameof(Transaction));
            OnPropertyChanged(nameof(Tip));

            if (_quantityCalculatorBlocked) return;
            try
            {
                _quantityCalculatorBlocked = true;
                OnPropertyChanged(nameof(QuantityDomestic));
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }

            OnPropertyChanged(nameof(ToPay));
            OnPropertyChanged(nameof(ForPayment));
        }
    }
    
    private string _quantityDomestic = string.Empty;
    public string QuantityDomestic
    {
        get => _quantityDomestic;
        set
        {
            SetProperty(ref _quantityDomestic, value);

            // If empty then resets the fields
            if (_quantityDomestic.Length == 0)
            {
                Transaction.Quantity = 0;
                OnPropertyChanged(nameof(Transaction));
                return;
            }

            var quantityDomesticDecimal = Utilities.Utilities.StrToDecimal(_quantityDomestic);
            if (quantityDomesticDecimal is null or <= 0) return;

            var courseRateDecimal = Utilities.Utilities.StrToDecimal(CourseRate);
            if (courseRateDecimal is null or <= 0) return;

            if (!_quantityCalculatorBlocked)
            {
                _quantityForeign = Math.Round((decimal)(quantityDomesticDecimal * courseRateDecimal), 2)
                    .ToString(CultureInfo.CurrentCulture);
            }

            // Sets transaction quantity according to transaction type
            Transaction.Quantity = Utilities.Utilities.StrToDecimal(_quantityForeign) ?? 1;
            
            OnPropertyChanged(nameof(Transaction));
            OnPropertyChanged(nameof(Tip));

            if (_quantityCalculatorBlocked) return;
            try
            {
                _quantityCalculatorBlocked = true;
                OnPropertyChanged(nameof(QuantityForeign));
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

            var quantityForeignDecimal = Utilities.Utilities.StrToDecimal(QuantityForeign);
            if (quantityForeignDecimal is null) { return; }

            try
            {
                _quantityCalculatorBlocked = true;
                var result = Math.Round((decimal)(quantityForeignDecimal / courseRateDecimal), 2)
                    .ToString(CultureInfo.CurrentCulture);
                QuantityDomestic = result;
            }
            finally
            {
                _quantityCalculatorBlocked = false;
            }

            OnPropertyChanged(nameof(ToPay));
            OnPropertyChanged(nameof(ForPayment));
        }
    }

    private TransactionType _transactionTypeProp = TransactionType.Buy;
    public TransactionType TransactionTypeProp
    {
        get => _transactionTypeProp;
        set
        {
            SetProperty(ref _transactionTypeProp, value);

            SetCourseRate(Currency, value);
            Transaction.TransactionType = _transactionTypeProp;
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
                TransactionType.Buy => Transaction.Quantity,
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
                TransactionType.Buy => Transaction.TotalAmountDomesticCurrency,
                _ => Transaction.Quantity
            };
        }
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

        Transaction.CurrencyQuantityBefore = Transaction.Currency!.Quantity;
        Transaction.CurrencyCode = Transaction.Currency!.Code;
        Transaction.AverageCourseRate = Transaction.Currency!.AverageCourseRate;

        if (Transaction.TotalAmountDomesticCurrency > 1000)
        {
            // Needs to go to the customer create page
            await Shell.Current.GoToAsync($"{nameof(NewCustomerIndividualPage)}", true, new Dictionary<string, object>
            {
                {"Transaction", Transaction}
            });
        }
        else
        {
            try
            {
                // Create transaction because it is not over 1000 euros
                var id = await _transactionFacade.InsertAsync(Transaction);
                Transaction.Id = id;

                try
                {
                    if (await _settingsFacade.ShouldSaveTransactionsAutomaticallyAsync())
                    {
                        // Save pdf to computer
                        await _printerService.SavePdf(Transaction);
                    }
                }
                catch (ArgumentNullException)
                {
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

        // Currency must be selected
        if (Currency is null)
        {
            errorMessage += rm.GetString("ErrorMessage_CurrencyNull") + "\n";
        }

        // Quantity foreign validation
        var quantityForeignDecimal = Utilities.Utilities.StrToDecimal(QuantityForeign);
        if (string.IsNullOrWhiteSpace(QuantityForeign) || quantityForeignDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_QuantityForeignNotValid") + "\n";

        // Quantity domestic validation
        var quantityDomesticDecimal = Utilities.Utilities.StrToDecimal(QuantityDomestic);
        if (string.IsNullOrWhiteSpace(QuantityDomestic) || quantityDomesticDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_QuantityDomesticNotValid") + "\n";

        // Course rate validation
        var courseRateToDecimal = Utilities.Utilities.StrToDecimal(CourseRate);
        if (string.IsNullOrWhiteSpace(CourseRate) || courseRateToDecimal is null or <= 0)
            errorMessage += rm.GetString("ErrorMessage_CourseRateNotValid") + "\n";

        // Course rate can not exceed opposite course rate
        else if (Currency is not null)
        {
            switch (TransactionTypeProp)
            {
                case TransactionType.Buy when (decimal)courseRateToDecimal < Currency.SellRate:
                case TransactionType.Sell when (decimal)courseRateToDecimal > Currency.BuyRate:
                    errorMessage += rm.GetString("ErrorMessage_CourseRateExceededOtherOne") + "\n";
                    break;
            }
        }

        // Not enough money in cash register check
        if (Currency is not null)
        {
            if (TransactionTypeProp is TransactionType.Buy &&
                DomesticCurrencyQuantity < Transaction.TotalAmountDomesticCurrency
                || TransactionTypeProp is TransactionType.Sell
                && (quantityForeignDecimal is not null || quantityForeignDecimal > Currency.Quantity))
            {
                errorMessage += rm.GetString("ErrorMessage_InsufficientMoneyInCashRegister") + "\n";
            }
        }

        return errorMessage;
    }

    /// <summary>
    /// Sets course rate according to transaction type and selected currency
    /// </summary>
    /// <param name="currency">Selected currency</param>
    /// <param name="transactionType">Selected transaction type</param>
    private void SetCourseRate(CurrencyTransactionListModel? currency, TransactionType transactionType)
    {
        if (transactionType is TransactionType.Buy)
        {
            CourseRate = currency?.BuyRate.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            OriginalCourseRate = currency?.BuyRate.ToString(CultureInfo.CurrentCulture);
        }
        else
        {
            CourseRate = currency?.SellRate.ToString(CultureInfo.CurrentCulture) ?? string.Empty;
            OriginalCourseRate = currency?.SellRate.ToString(CultureInfo.CurrentCulture);
        }
    }
}