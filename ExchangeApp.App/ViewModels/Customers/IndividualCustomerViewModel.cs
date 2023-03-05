using System.Resources;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Customers;

[QueryProperty(nameof(Transaction), "Transaction")]
public partial class IndividualCustomerViewModel : ViewModelBase
{
    private readonly ICustomerFacade _customerFacade;

    public IndividualCustomerViewModel(ICustomerFacade customerFacade)
    {
        _customerFacade = customerFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Customer.Transaction = Transaction;
    }

    public List<EvidenceType> EvidenceTypes
        => Enum.GetValues(typeof(EvidenceType)).Cast<EvidenceType>().ToList();

    [ObservableProperty] 
    private TransactionDetailModel _transaction = null!;

    [ObservableProperty]
    private IndividualCustomerDetailModel _customer = IndividualCustomerDetailModel.Empty;

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

        await Application.Current?.MainPage?.DisplayAlert(
            "Everything good",
            "message",
            "OK")!;
    }

    public async Task NavigateToPage(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 1:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerBusinessPage)}", true, new Dictionary<string, object>
                {
                    {"Transaction", Transaction}
                });
                break;
            case 2:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerMinorPage)}", true, new Dictionary<string, object>
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

        return errorMessage;
    }
}