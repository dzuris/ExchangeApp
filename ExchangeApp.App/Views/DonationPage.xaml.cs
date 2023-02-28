using ExchangeApp.App.ViewModels.Donation;

namespace ExchangeApp.App.Views;

public partial class DonationPage
{
    public DonationPage(DonationCreateViewModel viewModel) 
        : base(viewModel)
	{
        InitializeComponent();
    }
}