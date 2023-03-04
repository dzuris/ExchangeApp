using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        if (string.IsNullOrWhiteSpace(Customer.Nationality))
        {
            Customer.Nationality = "Slovenská";
        }
        var a = Customer;
        await Application.Current?.MainPage?.DisplayAlert(
            "title",
            "text",
            "OK")!;
    }
}