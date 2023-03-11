using ExchangeApp.App.ViewModels.Transaction;

namespace ExchangeApp.App.Views.Transaction;

public partial class TransactionDetailPage
{
	public TransactionDetailPage(TransactionDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}