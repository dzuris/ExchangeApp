using ExchangeApp.App.ViewModels.Donation;
using ExchangeApp.App.Views.Base;

namespace ExchangeApp.App.Views;

public partial class DonationPage
{
    //private readonly DonationCreateViewModel _viewModel;

	public DonationPage()
	{
		InitializeComponent();
        
        BindingContext = new DonationCreateViewModel();
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    await _viewModel.OnAppearingAsync();
    //}
}