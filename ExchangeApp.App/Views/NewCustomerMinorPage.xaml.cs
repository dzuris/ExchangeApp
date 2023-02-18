namespace ExchangeApp.App.Views;

public partial class NewCustomerMinorPage : ContentPage
{
	public NewCustomerMinorPage()
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

        switch (selectedIndex)
        {
            case 0:
                await Shell.Current.GoToAsync($"//NewCustomerIndividualPage");
                break;
            case 1:
                await Shell.Current.GoToAsync($"//NewCustomerBusinessPage");
                break;
        }
    }
}