using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Donation;

namespace ExchangeApp.App.ViewModels.Donation;

[QueryProperty(nameof(Donation), "Donation")]
public partial class DonationDetailViewModel : ViewModelBase
{
    private readonly IDonationFacade _donationFacade;
    private readonly IPrinterService _printerService;

    public DonationDetailViewModel(IDonationFacade donationFacade, IPrinterService printerService)
    {
        _donationFacade = donationFacade;
        _printerService = printerService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DonationNumber))]
    private DonationDetailModel? _donation;

    public string DonationNumber
    {
        get
        {
            if (Donation is null)
            {
                return string.Empty;
            }

            return Donation.Time.ToString("yyyyMMdd") + " / " + Donation.Id;
        }
    }
}