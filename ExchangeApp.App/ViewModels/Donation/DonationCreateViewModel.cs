using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using Microsoft.Extensions.Logging;

namespace ExchangeApp.App.ViewModels.Donation;

public partial class DonationCreateViewModel : ViewModelBase
{
    private readonly ILogger<DonationCreateViewModel> _logger;
    private readonly IDonationFacade _donationFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public DonationCreateViewModel(
        ILogger<DonationCreateViewModel> logger, 
        IDonationFacade donationFacade, 
        ICurrencyFacade currencyFacade)
    {
        _logger = logger;
        _donationFacade = donationFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetActiveCurrenciesAsync();
    }

    public List<DonationType> DonationTypes 
        => Enum.GetValues(typeof(DonationType)).Cast<DonationType>().ToList();

    [ObservableProperty] 
    private IEnumerable<CurrencyListModel> _currencies = new List<CurrencyListModel>();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private CurrencyListModel? _selectedCurrency;

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private DonationType? _donationType;

    [ObservableProperty]
    private string _note = string.Empty;
    
    [ObservableProperty]
    private string _courseRate = "1";

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private float _quantity;

    public float NewQuantity
    {
        get
        {
            if (SelectedCurrency is null)
            {
                return 0;
            }

            return DonationType switch
            {
                null => SelectedCurrency.Quantity + Quantity,
                Common.Enums.DonationType.Deposit => SelectedCurrency.Quantity + Quantity,
                _ => SelectedCurrency.Quantity - Quantity
            };
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var validationMessage = ValidateData();

        if (validationMessage != string.Empty)
        {
            var rm = new ResourceManager(typeof(DonationPageResources));
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("DisplayAlertValidationErrorTitle"), 
                validationMessage, 
                rm.GetString("DisplayAlertCancelButtonText"))!;
            return;
        }

        var donation = new DonationDetailModel
        {
            Time = DateTime.Now,
            CourseRate = StrToFloat(CourseRate),
            Quantity = Quantity,
            Type = DonationType ?? Common.Enums.DonationType.Deposit,
            Note = Note,
            Code = SelectedCurrency!.Code
        };

        //var id = await _donationFacade.InsertAsync(donation);
        //await _currencyFacade.UpdateQuantityAsync(SelectedCurrency.Code, NewQuantity);

        var id = 9999;
        donation.Id = id;
        await Shell.Current.GoToAsync($"../{nameof(DonationDetailPage)}", true, new Dictionary<string, object>
        {
            {"Donation", donation}
        });
    }

    /// <summary>
    /// Validates donation create view model data for saving
    /// </summary>
    /// <returns>Empty string if there is no error, or error message with all things which are bad</returns>
    private string ValidateData()
    {
        var resourceManager = new ResourceManager(typeof(DonationPageResources));
        var errorMessage = string.Empty;

        if (DonationType is null)
            errorMessage += resourceManager.GetString("ErrorMessage_DonationTypeEmpty") + "\n";

        if (SelectedCurrency is null)
            errorMessage += resourceManager.GetString("ErrorMessage_SelectedCurrencyEmpty") + "\n";

        //if (string.IsNullOrEmpty(Note))
        //    errorMessage += resourceManager.GetString("ErrorMessage_NoteEmpty") + "\n";

        if (Quantity <= 0 || (DonationType != Common.Enums.DonationType.Deposit && SelectedCurrency?.Quantity < Quantity))
            errorMessage += resourceManager.GetString("ErrorMessage_QuantityNotValid") + "\n";

        var courseRateRes = StrToFloat(CourseRate);
        if (courseRateRes <= 0)
            errorMessage += resourceManager.GetString("ErrorMessage_CourseRateNotValid") + "\n";
        
        return errorMessage;
    }

    /// <summary>
    /// Converts string to float for both 3.14 and 3,14 formats (dots, comma)
    /// </summary>
    /// <param name="str">Float as string</param>
    /// <returns>Converted float number or 0 if string is not valid</returns>
    private static float StrToFloat(string str)
    {
        if (float.TryParse(str, CultureInfo.CurrentCulture, out var result))
        {
            return result;
        }

        if (float.TryParse(str, CultureInfo.InvariantCulture, out result))
        {
            return result;
        }

        return 0;
    }
}