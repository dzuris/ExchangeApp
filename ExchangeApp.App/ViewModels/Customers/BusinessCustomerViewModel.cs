using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Customers;

[QueryProperty(nameof(Transaction), "Transaction")]
[QueryProperty(nameof(CurrencyFrom), "CurrencyFrom")]
[QueryProperty(nameof(CurrencyTo), "CurrencyTo")]
public partial class BusinessCustomerViewModel : ViewModelBase
{
    private readonly ICustomerFacade _customerFacade;
    private readonly ITransactionFacade _transactionFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public BusinessCustomerViewModel(ICustomerFacade customerFacade, ITransactionFacade transactionFacade, ICurrencyFacade currencyFacade)
    {
        _customerFacade = customerFacade;
        _transactionFacade = transactionFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
    }

    public List<EvidenceType> EvidenceTypes
        => Enum.GetValues(typeof(EvidenceType)).Cast<EvidenceType>().ToList();

    [ObservableProperty]
    private TransactionDetailModel _transaction = null!;

    [ObservableProperty]
    private CurrencyTransactionListModel? _currencyFrom;

    [ObservableProperty]
    private CurrencyTransactionListModel? _currencyTo;

    [ObservableProperty]
    private BusinessCustomerDetailModel _customer = BusinessCustomerDetailModel.Empty;

    private DateTime _selectedDate = new(1990, 1, 1);

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            SetProperty(ref _selectedDate, value);
            Customer.BirthDate = DateOnly.FromDateTime(_selectedDate);
        }
    }

    private string _identificationNumber = string.Empty;
    public string IdentificationNumber
    {
        get => _identificationNumber;
        set
        {
            SetProperty(ref _identificationNumber, value);
            Customer.IdentificationNumber = value;

            if (value.Length != 6) return;
            var newDateTime = Utilities.Utilities.GetDateTimeFromIdentificationNumber(value);
            if (newDateTime is not null)
                SelectedDate = (DateTime)newDateTime;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (CurrencyFrom is null || CurrencyTo is null) return;

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

        if (string.IsNullOrWhiteSpace(Customer.Nationality))
        {
            Customer.Nationality = "Slovenská";
        }

        Transaction.CustomerId = Customer.Id;
        Transaction.Customer = new CustomerListModel
        {
            Id = Customer.Id,
            FirstName = Customer.FirstName,
            LastName = Customer.LastName
        };

        try
        {
            await _customerFacade.InsertAsync(Customer);

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
        await Shell.Current.GoToAsync($"../../{nameof(TransactionDetailPage)}", true, new Dictionary<string, object>
        {
            {"Transaction", Transaction}
        });
    }

    public async Task NavigateToPage(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerIndividualPage)}", true, new Dictionary<string, object>
                {
                    {"Transaction", Transaction},
                    {"CurrencyFrom", CurrencyFrom!},
                    {"CurrencyTo", CurrencyTo!}
                });
                break;
            case 2:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerMinorPage)}", true, new Dictionary<string, object>
                {
                    {"Transaction", Transaction},
                    {"CurrencyFrom", CurrencyFrom!},
                    {"CurrencyTo", CurrencyTo!}
                });
                break;
        }
    }

    private string ValidateData()
    {
        var rm = new ResourceManager(typeof(CustomerResources));
        var errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Customer.FirstName) || !Utilities.CustomValidators.ValidateName(Customer.FirstName))
            errorMessage += rm.GetString("ErrorMessage_FirstNameNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.LastName) || !Utilities.CustomValidators.ValidateName(Customer.LastName))
            errorMessage += rm.GetString("ErrorMessage_LastNameNotValid") + "\n";

        if (Customer.BirthDate is null && string.IsNullOrWhiteSpace(Customer.IdentificationNumber))
            errorMessage += rm.GetString("ErrorMessage_BirthDateAndIdentificationNumberNotValid") + "\n";
        else if (Customer.IdentificationNumber is not null &&
                 !Utilities.CustomValidators.ValidateIdentificationNumber(Customer.IdentificationNumber))
            errorMessage += rm.GetString("ErrorMessage_IdentificationNumberNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.Address))
            errorMessage += rm.GetString("ErrorMessage_AddressNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.EvidenceNumber))
            errorMessage += rm.GetString("ErrorMessage_EvidenceNumberNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.TradeNameOfTheOwner))
            errorMessage += rm.GetString("ErrorMessage_BusinessCompanyNameNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.TradeAddress))
            errorMessage += rm.GetString("ErrorMessage_BusinessAddressNotValid") + "\n";

        return errorMessage;
    }
}