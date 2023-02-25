using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class DonationFacade : 
    FacadeBase<DonationEntity, DonationListModel, DonationDetailModel, int>, IDonationFacade
{
    public DonationFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
        : base(unitOfWorkFactory, mapper)
    {
    }
}