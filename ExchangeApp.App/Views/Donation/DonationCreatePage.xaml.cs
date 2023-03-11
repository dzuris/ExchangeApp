using ExchangeApp.App.ViewModels.Donation;

namespace ExchangeApp.App.Views.Donation;

public partial class DonationCreatePage
{
    public DonationCreatePage(DonationCreateViewModel viewModel) 
        : base(viewModel)
	{
        InitializeComponent();
    }
}