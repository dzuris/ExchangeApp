using ExchangeApp.App.ViewModels.Customers;

namespace ExchangeApp.App.Views;

public partial class NewCustomerIndividualPage
{
    public NewCustomerIndividualPage(IndividualCustomerViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CustomerPicker.SelectedIndex = 0;
    }

    private async void OnPagePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedIndex = CustomerPicker.SelectedIndex;

        switch (selectedIndex)
        {
            case 1:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerBusinessPage)}");
                break;
            case 2:
                await Shell.Current.GoToAsync($"../{nameof(NewCustomerMinorPage)}");
                break;
        }
    }
}