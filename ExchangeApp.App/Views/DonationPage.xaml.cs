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

        // Setting Donation Types in picker from resources and ItemSelected converting back to the DonationType
        var resourceManager = new ResourceManager(typeof(DonationPageResources));

        // Gets list of donation types
        var donationTypes = Enum.GetValues(typeof(DonationType)).Cast<DonationType>().ToList();

        // Creates list of KeyValuePairs of DonationType and translated type by resources
        var donationTypesPickerSource = donationTypes.Select(dt =>
        {
            var value = resourceManager.GetString("DonationType_" + dt) ?? dt.ToString();
            return new KeyValuePair<DonationType, string>(dt, value);
        }).ToList();
        
        // Filling items source and showing only string value in picker list
        DonationTypePicker.ItemsSource = donationTypesPickerSource;
        DonationTypePicker.ItemDisplayBinding = new Binding("Value");

        // On item select set DonationType in view model
        DonationTypePicker.SelectedIndexChanged += (_, _) =>
        {
            var selectedDonationType = ((KeyValuePair<DonationType, string>)DonationTypePicker.SelectedItem).Key;
            viewModel.DonationType = selectedDonationType;
        };
    }
}