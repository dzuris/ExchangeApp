using ExchangeApp.App.ViewModels.Donation;

namespace ExchangeApp.App.Views;

public partial class DonationDetailPage
{
	public DonationDetailPage(DonationDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        TestLabel.Text = Shell.Current.CurrentState.Location.OriginalString;
    }
}