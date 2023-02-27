using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class TransactionFacade : ITransactionFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public TransactionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.TransactionRepository;
        _mapper = mapper;
    }

    public async Task InsertAsync(TransactionDetailModel model)
    {
        var entity = _mapper.Map<TransactionEntity>(model);
        await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}