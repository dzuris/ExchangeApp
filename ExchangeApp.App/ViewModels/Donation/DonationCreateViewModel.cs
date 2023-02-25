using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;
using Microsoft.Extensions.Logging;

namespace ExchangeApp.App.ViewModels.Donation;

public partial class DonationCreateViewModel : ViewModelBase
{
    private readonly ILogger<DonationCreateViewModel> _logger;
    private readonly IDonationFacade _donationFacade;
    private readonly ICurrencyFacade _currencyFacade;

    public DonationCreateViewModel(ILogger<DonationCreateViewModel> logger, IDonationFacade donationFacade, ICurrencyFacade currencyFacade)
    {
        _logger = logger;
        _donationFacade = donationFacade;
        _currencyFacade = currencyFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Currencies = await _currencyFacade.GetAllAsync();

        _logger.LogInformation("Load async Donation Create View Model done.");
    }

    [ObservableProperty] 
    private IEnumerable<CurrencyListModel> _currencies = new List<CurrencyListModel>();

    //public IEnumerable<CurrencyListModel> Currencies { get; set; } = new List<CurrencyListModel>();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private CurrencyListModel? _selectedCurrency;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private float _quantity;

    [ObservableProperty]
    private DonationDetailModel _donation = DonationDetailModel.Empty;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NewQuantity))]
    private DonationType? _donationType;
    
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
                Common.Enums.DonationType.Deposit => SelectedCurrency.Quantity - Quantity,
                _ => SelectedCurrency.Quantity + Quantity
            };
        }
    }
}