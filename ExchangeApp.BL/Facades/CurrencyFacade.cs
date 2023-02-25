﻿using AutoMapper;
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

    public Task<CurrencyDetailModel?> GetById(string id)
    {
        throw new NotImplementedException();
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
}