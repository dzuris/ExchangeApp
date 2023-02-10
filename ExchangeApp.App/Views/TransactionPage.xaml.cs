using ExchangeApp.App.Resources.Texts;

namespace ExchangeApp.App.Views;

public partial class TransactionPage : ContentPage
{
    string _currency = "EUR";

	public TransactionPage()
	{
		InitializeComponent();
        CurrencyLabel.Text = String.Format(TransactionPageResources.CurrencyInCashRegisterAmountLabel, _currency);

    }
}