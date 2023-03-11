using ExchangeApp.App.Views.Donation;
using ExchangeApp.App.Views.OperationsList;
using ExchangeApp.App.Views.Transaction;

namespace ExchangeApp.App.Views;

public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnCreateTransactionPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(TransactionCreatePage)}");
    }

    private async void OnCreateDonationPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(DonationCreatePage)}");
    }

    private async void OnOperationsListPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(OperationsListPage)}");
    }
}