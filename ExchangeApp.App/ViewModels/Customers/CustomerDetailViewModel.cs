using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Customer;

namespace ExchangeApp.App.ViewModels.Customers;

[QueryProperty(nameof(Id), "id")]
public partial class CustomerDetailViewModel : ViewModelBase
{
    private readonly ICustomerFacade _customerFacade;

    public CustomerDetailViewModel(ICustomerFacade customerFacade)
    {
        _customerFacade = customerFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Customer = await _customerFacade.GetByIdAsync(_id);

        if (Customer is null)
        {
            Customers = new List<CustomerDetailModel>();
        }
        else
        {
            Customers = new List<CustomerDetailModel>
            {
                Customer
            };
        }
    }
    
    private Guid _id;
    public string? Id
    {
        get => _id.ToString();
        set
        {
            Guid parsedId;
            if (Guid.TryParse(value, out parsedId))
            {
                _id = parsedId;
            }
        }
    }

    [ObservableProperty]
    private CustomerDetailModel? _customer;

    [ObservableProperty]
    private IEnumerable<CustomerDetailModel> _customers = new List<CustomerDetailModel>();
}