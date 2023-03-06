using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Donation;

namespace ExchangeApp.App.ViewModels.Donation;

[QueryProperty(nameof(Donation), "Donation")]
public partial class DonationDetailViewModel : ViewModelBase
{
    private readonly IPrinterService _printerService;
    private readonly ICustomerFacade _customerFacade;

    public DonationDetailViewModel(IPrinterService printerService, ICustomerFacade customerFacade)
    {
        _printerService = printerService;
        _customerFacade = customerFacade;
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

    [RelayCommand]
    private async Task GoToCustomerDetailAsync()
    {

    }
}