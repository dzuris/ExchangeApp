namespace ExchangeApp.App.Views;

public partial class NewCustomerIndividualPage : ContentPage
{
    public int SpacingLabelInput { get; set; } = 20;

    public NewCustomerIndividualPage()
	{
		InitializeComponent();

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CustomerPicker.SelectedIndex = 0;
    }

    private async void OnPagePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedIndex = CustomerPicker.SelectedIndex;

        //foreach (var route in Shell.Current.Items)
        //{
        //    Console.WriteLine(route.Route);
        //}
        //return;

        switch (selectedIndex)
        {
            case 1:
                await Shell.Current.GoToAsync($"{nameof(NewCustomerBusinessPage)}");
                break;
            case 2:
                await Shell.Current.GoToAsync($"{nameof(NewCustomerMinorPage)}");
                break;
        }
    }
}