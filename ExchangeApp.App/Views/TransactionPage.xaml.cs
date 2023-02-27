using ExchangeApp.App.ViewModels.Transaction;

namespace ExchangeApp.App.Views;

public partial class TransactionPage
{
	public TransactionPage(TransactionCreateViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Currency1.SelectedIndex = 0;
        Currency2.SelectedIndex = 0;
    }
}