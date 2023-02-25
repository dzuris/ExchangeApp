using System.Resources;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.ViewModels.Donation;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.Views;

public partial class DonationPage
{
    public DonationPage(DonationCreateViewModel viewModel) 
        : base(viewModel)
	{
        InitializeComponent();

        // This code is for setting Donation Types in picker from resources and ItemSelected converting back to the DonationType
        var resourceManager = new ResourceManager("ExchangeApp.App.Resources.Texts.DonationEnumResources", typeof(DonationPage).Assembly);
        var donationTypes = Enum.GetValues(typeof(DonationType)).Cast<DonationType>().ToList();
        var donationTypesPickerSource = donationTypes.Select(dt =>
        {
            var value = resourceManager.GetString("DonationType_" + dt) ?? dt.ToString();
            return new KeyValuePair<DonationType, string>(dt, value);
        }).ToList();
        DonationTypePicker.ItemsSource = donationTypesPickerSource;
        DonationTypePicker.ItemDisplayBinding = new Binding("Value");

        DonationTypePicker.SelectedIndexChanged += (_, _) =>
        {
            var selectedDonationType = ((KeyValuePair<DonationType, string>)DonationTypePicker.SelectedItem).Key;
            viewModel.DonationType = selectedDonationType;
        };
    }
}