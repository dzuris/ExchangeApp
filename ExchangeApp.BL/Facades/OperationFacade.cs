﻿using System.Collections.ObjectModel;
using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class OperationFacade : IOperationFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationRepository _repository;
    private readonly IMapper _mapper;

    public OperationFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.OperationRepository;
        _mapper = mapper;
    }

    public async Task<ObservableCollection<OperationListModelBase>> GetOperationsAsync(int pageSize, int pageNumber)
    {
        var entities = await _repository.GetLastOperationsAsync(pageSize, pageNumber);
        return _mapper.Map<ObservableCollection<OperationListModelBase>>(entities);
    }

    public async Task<ObservableCollection<OperationListModelBase>> GetFilteredOperationsAsync(int pageSize, int pageNumber, OperationFilterOption option, int? id, DateTime? from, DateTime? until)
    {
        var entities = await _repository.GetFilteredOperationsAsync(pageSize, pageNumber, option, id, from, until);
        return _mapper.Map<ObservableCollection<OperationListModelBase>>(entities);
    }
}