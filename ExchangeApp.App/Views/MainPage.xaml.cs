using ExchangeApp.App.ViewModels;

namespace ExchangeApp.App.Views;

public partial class MainPage
{
	public MainPage(MainViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
	}
}