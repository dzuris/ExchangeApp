namespace ExchangeApp.App.Views;

public partial class NewCustomerBusinessPage : ContentPage
{
	public NewCustomerBusinessPage()
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

        switch (selectedIndex)
        {
            case 0:
                await Shell.Current.GoToAsync($"//NewCustomerIndividualPage");
                break;
            case 2:
                await Shell.Current.GoToAsync($"//NewCustomerMinorPage");
                break;
        }
    }
}