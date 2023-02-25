using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class CurrencyFacade :
    FacadeBase<CurrencyEntity, CurrencyListModel, CurrencyDetailModel, string>, ICurrencyFacade
{
    public CurrencyFacade(
        IUnitOfWorkFactory unitOfWorkFactory, 
        IMapper mapper) 
        : base(unitOfWorkFactory, mapper)
    {
    }

    public Task<IEnumerable<CurrencyListModel>> GetActiveCurrencyAsync()
    {
        throw new NotImplementedException();
    }
}