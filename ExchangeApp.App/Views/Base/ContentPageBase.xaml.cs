using ExchangeApp.App.ViewModels;

namespace ExchangeApp.App.Views.Base;

public partial class ContentPageBase
{
	protected IViewModel ViewModel { get; }

	public ContentPageBase(IViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.OnAppearingAsync();
    }
}