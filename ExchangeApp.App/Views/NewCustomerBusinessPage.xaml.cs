using ExchangeApp.App.ViewModels.Customers;

namespace ExchangeApp.App.Views;

public partial class NewCustomerBusinessPage
{
	public NewCustomerBusinessPage(BusinessCustomerViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CustomerPicker.SelectedIndex = 1;
    }

    private async void OnPagePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedIndex = CustomerPicker.SelectedIndex;

        if (BindingContext is BusinessCustomerViewModel viewModel)
        {
            await viewModel.NavigateToPage(selectedIndex);
        }
    }
}