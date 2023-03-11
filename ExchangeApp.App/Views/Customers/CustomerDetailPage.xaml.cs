using ExchangeApp.App.ViewModels.Customers;

namespace ExchangeApp.App.Views.Customers;

public partial class CustomerDetailPage
{
	public CustomerDetailPage(CustomerDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}