namespace ExchangeApp.App.Views;

public partial class NewCustomerIndividualPage : ContentPage
{
	public NewCustomerIndividualPage()
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
                await Shell.Current.GoToAsync($"//NewCustomerBusinessPage");
                break;
            case 2:
                await Shell.Current.GoToAsync($"//NewCustomerMinorPage");
                break;
        }
    }
}