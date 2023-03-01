namespace ExchangeApp.App.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnCreateTransactionPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(TransactionPage)}");
    }

    private async void OnCreateDonationPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(DonationPage)}");
    }

    private async void OnOperationsListPageTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(OperationsListPage)}");
    }
}