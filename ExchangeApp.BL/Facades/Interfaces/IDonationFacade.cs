using ExchangeApp.BL.Models.Donation;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IDonationFacade : IFacade
{
    Task<DonationDetailModel?> GetById(int id);
    Task<int> InsertAsync(DonationDetailModel model);
}