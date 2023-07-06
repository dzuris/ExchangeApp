using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Utilities;

namespace ExchangeApp.App.ViewModels.Customers;

[QueryProperty(nameof(Transaction), "Transaction")]
public partial class MinorCustomerViewModel : ViewModelBase
{
    private readonly ICustomerFacade _customerFacade;
    private readonly ITransactionFacade _transactionFacade;
    private readonly ISettingsFacade _settingsFacade;
    private readonly IPrinterService _printerService;

    public MinorCustomerViewModel(ICustomerFacade customerFacade, ITransactionFacade transactionFacade, ISettingsFacade settingsFacade, IPrinterService printerService)
    {
        _customerFacade = customerFacade;
        _transactionFacade = transactionFacade;
        _settingsFacade = settingsFacade;
        _printerService = printerService;
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
    private MinorCustomerDetailModel _customer = MinorCustomerDetailModel.Empty;

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
            var newDateTime = Utilities.GetDateTimeFromIdentificationNumber(value);
            if (newDateTime is not null)
                SelectedDate = (DateTime)newDateTime;
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

        Transaction.CustomerId = Customer.Id;
        Transaction.Customer = new CustomerListModel
        {
            Id = Customer.Id,
            FirstName = Customer.FirstName,
            LastName = Customer.LastName,
            EvidenceNumber = Customer.EvidenceNumber
        };

        try
        {
            await _customerFacade.InsertAsync(Customer);

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
                    {"Transaction", Transaction}
                });
                break;
            case 1:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerBusinessPage)}", true, new Dictionary<string, object>
                {
                    {"Transaction", Transaction}
                });
                break;
        }
    }

    private string ValidateData()
    {
        var rm = new ResourceManager(typeof(CustomerResources));
        var errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Customer.FirstName) || !CustomValidators.ValidateName(Customer.FirstName))
            errorMessage += rm.GetString("ErrorMessage_FirstNameNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.LastName) || !CustomValidators.ValidateName(Customer.LastName))
            errorMessage += rm.GetString("ErrorMessage_LastNameNotValid") + "\n";

        if (Customer.BirthDate is null && string.IsNullOrWhiteSpace(Customer.IdentificationNumber))
            errorMessage += rm.GetString("ErrorMessage_BirthDateAndIdentificationNumberNotValid") + "\n";
        else if (Customer.IdentificationNumber is not null &&
                 !CustomValidators.ValidateIdentificationNumber(Customer.IdentificationNumber))
            errorMessage += rm.GetString("ErrorMessage_IdentificationNumberNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.Address))
            errorMessage += rm.GetString("ErrorMessage_AddressNotValid") + "\n";

        if (string.IsNullOrWhiteSpace(Customer.EvidenceNumber))
            errorMessage += rm.GetString("ErrorMessage_EvidenceNumberNotValid") + "\n";

        return errorMessage;
    }
}