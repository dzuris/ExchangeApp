using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Customers;

[QueryProperty(nameof(Transaction), "Transaction")]
public partial class MinorCustomerViewModel : ViewModelBase
{
    private readonly ICustomerFacade _customerFacade;

    public MinorCustomerViewModel(ICustomerFacade customerFacade)
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

    [RelayCommand]
    private async Task SaveAsync()
    {
        Transaction.CustomerId = Customer.Id;
        Transaction.Customer = new CustomerListModel
        {
            Id = Customer.Id,
            FirstName = Customer.FirstName,
            LastName = Customer.LastName
        };

        // TODO saving transaction and customer
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
}