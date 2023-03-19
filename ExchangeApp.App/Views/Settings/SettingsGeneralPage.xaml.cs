using ExchangeApp.App.ViewModels.Settings;

namespace ExchangeApp.App.Views.Settings;

public partial class SettingsGeneralPage
{
	public SettingsGeneralPage(SettingsAutomaticOperationsSaveViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}