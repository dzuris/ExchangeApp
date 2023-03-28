using System.Collections.ObjectModel;
using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class TotalBalanceFacade : ITotalBalanceFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITotalBalanceRepository _repository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public TotalBalanceFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.TotalBalanceRepository;
        _currencyRepository = _unitOfWork.CurrencyRepository;
        _mapper = mapper;
    }

    public async Task<ObservableCollection<TotalBalanceModel>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<ObservableCollection<TotalBalanceModel>>(entities.OrderByDescending(e => e.Created));
    }

    public async Task<IEnumerable<TotalBalanceModel>> GetAllAsync(DateTime from, DateTime until)
    {
        var entities = await _repository.GetAllAsync(from, until);
        return _mapper.Map<IEnumerable<TotalBalanceModel>>(entities);
    }

    public async Task<ObservableCollection<TotalBalanceModel>> GetFilteredAsync(TotalBalanceFilterOption option, DateTime? dateFrom, DateTime? dateUntil)
    {
        var entities = await _repository.GetFilteredAsync(option, dateFrom, dateUntil);
        return _mapper.Map<ObservableCollection<TotalBalanceModel>>(entities);
    }

    public async Task<int> InsertAsync(TotalBalanceModel model)
    {
        model.LastTotalBalance = await _repository.GetLastTotalBalanceDate(model.Type);
        var entity = _mapper.Map<TotalBalanceEntity>(model);

        var currenciesHistoryData = (await _currencyRepository.GetActiveCurrenciesAsync()).ToList();

        foreach (var currencyHistory in currenciesHistoryData.Select(item => new CurrencyHistoryEntity
                 {
                     Code = item.Code,
                     Quantity = item.Quantity,
                     AverageCourseRate = item.AverageCourseRate,
                     TimeStamp = model.Created
                 }))
        {
            await _currencyRepository.InsertCurrencyBalance(currencyHistory);
        }

        var id = await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();

        return id;
    }

    /// <summary>
    /// Monthly total balance can be created only if there is no operation created after last daily total balance
    /// </summary>
    /// <returns>Boolean value if you can create monthly total balance</returns>
    public async Task<bool> CanCreateMonthlyTotalBalance()
    {
        return !await _repository.ExistsOperationAfterLastDailyTotalBalance();
    }
}