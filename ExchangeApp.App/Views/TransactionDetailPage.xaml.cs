using ExchangeApp.App.ViewModels.Transaction;

namespace ExchangeApp.App.Views;

public partial class TransactionDetailPage
{
	public TransactionDetailPage(TransactionDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}