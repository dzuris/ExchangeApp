using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IDonationFacade : IFacade
{
    Task InsertAsync(DonationDetailModel model);
}