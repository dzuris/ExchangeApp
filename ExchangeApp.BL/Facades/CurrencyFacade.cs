using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class CurrencyFacade : ICurrencyFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyRepository _repository;
    private readonly IMapper _mapper;

    public CurrencyFacade(
        IUnitOfWorkFactory unitOfWorkFactory, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.CurrencyRepository;
        _mapper = mapper;
    }

    public async Task<CurrencyDetailModel?> GetById(string id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return entity is null ? null : _mapper.Map<CurrencyDetailModel>(entity);
    }

    public async Task<List<CurrencyListModel>> GetNonActiveCurrenciesAsync()
    {
        var entities = await _repository.GetNonActiveCurrenciesAsync();
        return _mapper.Map<List<CurrencyListModel>>(entities);
    }

    public async Task<List<CurrencyListModel>> GetActiveCurrenciesAsync()
    {
        var entities = await _repository.GetActiveCurrenciesAsync();
        return _mapper.Map<List<CurrencyListModel>>(entities);
    }

    public async Task<List<CurrencyNewTransactionModel>> GetActiveCurrenciesForTransactionAsync()
    {
        var entities = await _repository.GetActiveCurrenciesAsync();
        return _mapper.Map<List<CurrencyNewTransactionModel>>(entities);
    }

    public async Task UpdateQuantityAsync(string code, float newQuantity)
    {
        await _repository.UpdateQuantityAsync(code, newQuantity);
        await _unitOfWork.CommitAsync();
    }
}