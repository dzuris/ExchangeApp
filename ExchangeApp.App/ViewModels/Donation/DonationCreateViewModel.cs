using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Donation;

public class DonationCreateViewModel : ViewModelBase
{
    //private readonly IDonationFacade _donationFacade;
    //private readonly ICurrencyFacade _currencyFacade;

    //public DonationCreateViewModel(IDonationFacade donationFacade, ICurrencyFacade currencyFacade)
    //{
    //    _donationFacade = donationFacade;
    //    _currencyFacade = currencyFacade;
    //}

    public DonationCreateViewModel()
    {
        
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        //Currencies = await _currencyFacade.GetAsync();
    }

    public IEnumerable<CurrencyListModel> Currencies { get; set; } = new List<CurrencyListModel>();

    private CurrencyListModel? _selectedCurrency;
    public CurrencyListModel? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            _selectedCurrency = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(NewQuantity));
        }
    }

    private float _quantity;
    public float Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            Donation.Quantity = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(NewQuantity));
        }
    }

    private DonationDetailModel _donation = DonationDetailModel.Empty;
    public DonationDetailModel Donation
    {
        get => _donation;
        set
        {
            _donation = value;
            OnPropertyChanged();
        }
    }

    public float NewQuantity
    {
        get
        {
            if (Donation.Type == DonationType.Deposit)
            {
                return SelectedCurrency is null ? 0 : SelectedCurrency.Quantity + Quantity;
            }

            return SelectedCurrency is null ? 0 : SelectedCurrency.Quantity - Quantity;
        }
    }
}