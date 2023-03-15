using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Donation;

namespace ExchangeApp.App.ViewModels.Donation;

[QueryProperty(nameof(Donation), "Donation")]
[QueryProperty(nameof(Id), "id")]
public partial class DonationDetailViewModel : ViewModelBase
{
    private readonly IPrinterService _printerService;
    private readonly IDonationFacade _donationFacade;

    public DonationDetailViewModel(IPrinterService printerService, IDonationFacade donationFacade)
    {
        _printerService = printerService;
        _donationFacade = donationFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Donation ??= await _donationFacade.GetById(Id);
    }

    [ObservableProperty]
    private int _id;

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
    private async Task CancelAsync()
    {
        // TODO
    }

    [RelayCommand]
    private async Task SaveDonationToFolderAsync()
    {
        if (Donation is null) return;

        var rm = new ResourceManager(typeof(DonationDetailPageResources));
        try
        {
            await _printerService.SavePdf(Donation);
        }
        catch (ArgumentNullException)
        {
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("PdfDownloadAlertTitle"),
                rm.GetString("PdfDownloadAlertErrorMessage"),
                rm.GetString("PdfDownloadAlertCancelButton"))!;
            return;
        }

        await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("PdfDownloadAlertTitle"),
            rm.GetString("PdfDownloadAlertMessage"),
            rm.GetString("PdfDownloadAlertCancelButton"))!;
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        // TODO
    }
}