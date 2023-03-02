using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;
using System.Collections.ObjectModel;

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

    public async Task<ObservableCollection<CurrencyListModel>> GetNonActiveCurrenciesAsync()
    {
        var entities = await _repository.GetNonActiveCurrenciesAsync();
        return _mapper.Map<ObservableCollection<CurrencyListModel>>(entities);
    }

    public async Task<ObservableCollection<CurrencyListModel>> GetActiveCurrenciesAsync()
    {
        var entities = await _repository.GetActiveCurrenciesAsync();
        return _mapper.Map<ObservableCollection<CurrencyListModel>>(entities);
    }

    public async Task<List<CurrencyNewTransactionModel>> GetActiveCurrenciesForTransactionAsync()
    {
        var entities = await _repository.GetActiveCurrenciesAsync();
        return _mapper.Map<List<CurrencyNewTransactionModel>>(entities);
    }

    public async Task<List<CurrencyCoursesListModel>> GetActiveCurrenciesForCoursesAsync()
    {
        var entities = await _repository.GetActiveCurrenciesAsync();
        return _mapper.Map<List<CurrencyCoursesListModel>>(entities);
    }

    public async Task UpdateQuantityAsync(string code, decimal newQuantity)
    {
        await _repository.UpdateQuantityAsync(code, newQuantity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateStatus(string code, CurrencyState status)
    {
        await _repository.UpdateStatus(code, status);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(CurrencyDetailModel model)
    {
        var entity = _mapper.Map<CurrencyEntity>(model);
        await _repository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}