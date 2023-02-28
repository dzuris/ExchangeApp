using ExchangeApp.App.ViewModels.Donation;
using DonationDetailViewModel = ExchangeApp.App.ViewModels.Donation.DonationDetailViewModel;

namespace ExchangeApp.App.Views;

public partial class DonationDetailPage
{
	public DonationDetailPage(DonationDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
    }
}