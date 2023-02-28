using ExchangeApp.App.ViewModels.Settings;

namespace ExchangeApp.App.Views;

public partial class SettingsPage
{
	public SettingsPage(SettingsPageViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}