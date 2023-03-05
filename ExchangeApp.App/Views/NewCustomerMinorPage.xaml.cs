using ExchangeApp.App.ViewModels.Customers;

namespace ExchangeApp.App.Views;

public partial class NewCustomerMinorPage
{
    public NewCustomerMinorPage(MinorCustomerViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CustomerPicker.SelectedIndex = 2;
    }

    private async void OnPagePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedIndex = CustomerPicker.SelectedIndex;

        if (BindingContext is MinorCustomerViewModel viewModel)
        {
            await viewModel.NavigateToPage(selectedIndex);
        }
    }
}