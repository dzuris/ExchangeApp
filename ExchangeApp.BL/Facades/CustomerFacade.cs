using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class CustomerFacade : ICustomerFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.CustomerRepository;
        _mapper = mapper;
    }
}