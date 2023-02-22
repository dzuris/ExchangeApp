using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICurrencyFacade : IFacade<CurrencyListModel, CurrencyDetailModel, string>
{
}