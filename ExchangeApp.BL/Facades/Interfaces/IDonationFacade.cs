using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IDonationFacade : IFacade<DonationListModel, DonationDetailModel, int>
{
}