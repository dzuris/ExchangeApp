using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Exceptions;

namespace ExchangeApp.App.ViewModels.Donation;

[QueryProperty(nameof(Donation), "Donation")]
[QueryProperty(nameof(Id), "id")]
public partial class DonationDetailViewModel : ViewModelBase
{
    private readonly IDonationFacade _donationFacade;
    private readonly ISettingsFacade _settingsFacade;
    private readonly IPrinterService _printerService;

    public DonationDetailViewModel(IPrinterService printerService, IDonationFacade donationFacade, ISettingsFacade settingsFacade)
    {
        _printerService = printerService;
        _donationFacade = donationFacade;
        _settingsFacade = settingsFacade;
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

            return Donation.Created.ToString("yyyyMMdd") + " / " + Donation.Id;
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        if (Donation is null) return;

        var rm = new ResourceManager(typeof(DonationDetailPageResources));

        var result = await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("StornoAlertConfirmationTitle"),
            rm.GetString("StornoAlertConfirmationMessage"),
            rm.GetString("StornoAlertConfirmationYes"),
            rm.GetString("StornoAlertConfirmationNo"))!;

        if (!result) return;

        try
        {
            await _donationFacade.CancelDonation(Donation);
        }
        catch (OperationCanNotBeCanceledException)
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("StornoAlertErrorTitle"),
                rm.GetString("StornoAlertErrorMessageClosedDonation"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }
        catch (InsufficientMoneyException)
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("StornoAlertErrorTitle"),
                rm.GetString("StornoAlertErrorMessageInsufficientMoney"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }
        catch (CurrencyMissingException)
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("StornoAlertErrorTitle"),
                rm.GetString("StornoAlertErrorMessageCurrencyMissingException"),
                rm.GetString("AlertCancelButton"))!;
            return;
        }

        Donation.IsCanceled = true;
        OnPropertyChanged(nameof(Donation));

        if (await _settingsFacade.ShouldSaveTransactionsAutomaticallyAsync())
        {
            try
            {
                await _printerService.SavePdf(Donation);
            }
            catch (ArgumentNullException)
            {
            }
        }

        await Application.Current.MainPage?.DisplayAlert(
            rm.GetString("StornoAlertTitle"),
            rm.GetString("StornoAlertMessage"),
            rm.GetString("AlertCancelButton"))!;
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
                rm.GetString("AlertCancelButton"))!;
            return;
        }

        await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("PdfDownloadAlertTitle"),
            rm.GetString("PdfDownloadAlertMessage"),
            rm.GetString("AlertCancelButton"))!;
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        if (Donation is null) return;
        await _printerService.Print(Donation);
    }
}