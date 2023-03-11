using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.ViewModels.Settings;

namespace ExchangeApp.App.Views.Settings;

public partial class SettingsPage
{
	public SettingsPage(SettingsPageViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}

    private readonly string[] _options =
    {
        SettingsPageResources.OperationsAutomaticSaveListItem,
        SettingsPageResources.CourseRatesListItem,
        SettingsPageResources.TotalBalanceListItem,
        SettingsPageResources.BranchInfoListItem,
        SettingsPageResources.CompanyInfoListItem,
        SettingsPageResources.LicenseInfoListItem
    };

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if ((sender as Frame)?.BindingContext is not string selectedOption) return;

        var index = Array.IndexOf(_options, selectedOption);

        switch (index)
        {
            case 0:
                //await Shell.Current.GoToAsync("");
                break;
            case 1:
                await Shell.Current.GoToAsync($"{nameof(SettingsCoursesManagerPage)}");
                break;
        }
    }
}