using ExchangeApp.App.ViewModels.OperationsList;

namespace ExchangeApp.App.Views;

public partial class OperationsListPage
{
	public OperationsListPage(OperationsListViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}