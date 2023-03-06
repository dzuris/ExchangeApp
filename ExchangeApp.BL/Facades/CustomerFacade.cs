using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.DAL.Entities.Customers;
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

    public async Task InsertAsync(IndividualCustomerDetailModel model)
    {
        var entity = _mapper.Map<IndividualCustomerEntity>(model);
        await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task InsertAsync(BusinessCustomerDetailModel model)
    {
        var entity = _mapper.Map<BusinessCustomerEntity>(model);
        await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task InsertAsync(MinorCustomerDetailModel model)
    {
        var entity = _mapper.Map<MinorCustomerEntity>(model);
        await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}