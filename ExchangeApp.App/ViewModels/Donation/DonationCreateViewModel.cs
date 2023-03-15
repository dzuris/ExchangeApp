using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.App.Views.Donation;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Donation;

public partial class DonationCreateViewModel : ViewModelBase
{
    private readonly IDonationFacade _donationFacade;
    private readonly ICurrencyFacade _currencyFacade;
    private readonly ISettingsFacade _settingsFacade;
    private readonly IPrinterService _printerService;

    public DonationCreateViewModel(
        IDonationFacade donationFacade, 
        ICurrencyFacade currencyFacade, ISettingsFacade settingsFacade, IPrinterService printerService)
    {
        _donationFacade = donationFacade;
        _currencyFacade = currencyFacade;
        _settingsFacade = settingsFacade;
        _printerService = printerService;
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
    
    private CurrencyListModel? _selectedCurrency;
    public CurrencyListModel? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            SetProperty(ref _selectedCurrency, value);

            OnPropertyChanged(nameof(NewQuantity));

            CourseRate = "1";
            if (DonationType is null or not Common.Enums.DonationType.Withdraw || value is null) return;

            var averageCourseRate = value.AverageCourseRate.ToString();
            if (averageCourseRate != null)
            {
                CourseRate = averageCourseRate;
            }
        }
    }
    
    private DonationType? _donationType;
    public DonationType? DonationType
    {
        get => _donationType;
        set
        {
            SetProperty(ref _donationType, value);

            OnPropertyChanged(nameof(NewQuantity));

            if (SelectedCurrency is null || value is not Common.Enums.DonationType.Withdraw) return;
            var averageCourseRate = SelectedCurrency.AverageCourseRate.ToString();
            if (averageCourseRate != null)
            {
                CourseRate = averageCourseRate;
            }
        }
    }

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
            AverageCourseRate = SelectedCurrency!.AverageCourseRate,
            Quantity = Quantity,
            Type = DonationType ?? Common.Enums.DonationType.Deposit,
            Note = Note,
            CurrencyCode = SelectedCurrency!.Code,
            Currency = SelectedCurrency
        };
        
        try
        {
            var id = await _donationFacade.InsertAsync(donation);
            donation.Id = id;

            try
            {
                if (await _settingsFacade.ShouldSaveDonationsAutomaticallyAsync())
                {
                    await _printerService.SavePdf(donation);
                }
            }
            catch (ArgumentNullException)
            {
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
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