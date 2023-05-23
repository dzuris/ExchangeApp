using ExchangeApp.App.ViewModels.Transaction;

namespace ExchangeApp.App.Views.Transaction;

public partial class TransactionCreatePage
{
	public TransactionCreatePage(TransactionCreateViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
    }
}