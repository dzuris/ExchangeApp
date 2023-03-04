using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Views;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Donation;

public partial class DonationCreateViewModel : ViewModelBase
{
    private readonly IDonationFacade _donationFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public DonationCreateViewModel(
        IDonationFacade donationFacade, 
        ICurrencyFacade currencyFacade)
    {
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
    private decimal _quantity;

    public decimal NewQuantity
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
            var rm = new ResourceManager(typeof(ErrorResources));
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("DisplayAlertValidationErrorTitle"), 
                validationMessage, 
                rm.GetString("DisplayAlertCancelButtonText"))!;
            return;
        }

        var courseRate = Utilities.Utilities.StrToDecimal(CourseRate) ?? -1;
        var donation = new DonationDetailModel
        {
            Time = DateTime.Now,
            CourseRate = courseRate,
            Quantity = Quantity,
            Type = DonationType ?? Common.Enums.DonationType.Deposit,
            Note = Note,
            Code = SelectedCurrency!.Code
        };

        int id;
        try
        {
            id = await _donationFacade.InsertAsync(donation);
            await _currencyFacade.UpdateQuantityAsync(SelectedCurrency.Code, NewQuantity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
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
        var rm = new ResourceManager(typeof(DonationPageResources));
        var errorMessage = string.Empty;

        if (DonationType is null)
            errorMessage += rm.GetString("ErrorMessage_DonationTypeEmpty") + "\n";

        if (SelectedCurrency is null)
            errorMessage += rm.GetString("ErrorMessage_SelectedCurrencyEmpty") + "\n";
        
        if (Quantity <= 0 || (DonationType != Common.Enums.DonationType.Deposit && SelectedCurrency?.Quantity < Quantity))
            errorMessage += rm.GetString("ErrorMessage_QuantityNotValid") + "\n";

        var courseRateRes = Utilities.Utilities.StrToDecimal(CourseRate);
        if (courseRateRes is null || courseRateRes <= 0)
            errorMessage += rm.GetString("ErrorMessage_CourseRateNotValid") + "\n";
        
        return errorMessage;
    }
}