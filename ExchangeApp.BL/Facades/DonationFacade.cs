using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class DonationFacade : IDonationFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDonationRepository _donationRepository;
    private readonly IMapper _mapper;

    public DonationFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _donationRepository = _unitOfWork.DonationRepository;
        _mapper = mapper;
    }

    public async Task InsertAsync(DonationDetailModel model)
    {
        var entity = _mapper.Map<DonationEntity>(model);
        await _donationRepository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}