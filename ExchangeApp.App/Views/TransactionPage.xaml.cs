using ExchangeApp.App.ViewModels.Transaction;

namespace ExchangeApp.App.Views;

public partial class TransactionPage
{
	public TransactionPage(TransactionCreateViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
    }

    private void CurrencyFromChangedPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionCreateViewModel viewModel)
        {
            viewModel.OnCurrencyFromChanged();
        }
    }

    private void CurrencyToChangedPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionCreateViewModel viewModel)
        {
            viewModel.OnCurrencyToChanged();
        }
    }
}