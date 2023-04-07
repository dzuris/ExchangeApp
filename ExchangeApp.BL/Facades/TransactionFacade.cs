using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;
using ExchangeApp.Common.Exceptions;

namespace ExchangeApp.BL.Facades;

public class TransactionFacade : ITransactionFacade
{
    private const string DomesticCurrencyCode = "EUR";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _repository;
    private readonly IOperationRepository _operationRepository;
    private readonly IMapper _mapper;

    public TransactionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.TransactionRepository;
        _operationRepository = _unitOfWork.OperationRepository;
        _mapper = mapper;
    }

    public async Task<TransactionDetailModel?> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return entity is null ? null : _mapper.Map<TransactionDetailModel>(entity);
    }

    public async Task<int> InsertAsync(TransactionDetailModel model)
    {
        // Gets domestic and foreign currencies
        var currencyRepository = _unitOfWork.CurrencyRepository;
        var domesticCurrencyEntity = await currencyRepository.GetByIdAsync(DomesticCurrencyCode);

        // Check if currencies exists
        if (domesticCurrencyEntity is null)
        {
            throw new ArgumentNullException(nameof(domesticCurrencyEntity), "Domestic currency can't be null");
        }

        // Update quantities, for buy update average currency course rate
        decimal newDomesticCurrencyQuantity;
        decimal newForeignCurrencyQuantity;
        if (model.TransactionType == TransactionType.Buy)
        {
            // Sets new quantities
            newDomesticCurrencyQuantity = domesticCurrencyEntity.Quantity - model.TotalAmountDomesticCurrency;
            newForeignCurrencyQuantity = model.CurrencyQuantityBefore + model.Quantity;

            // Update average course rate
            decimal currentValue = 0;
            if (model.CurrencyQuantityBefore > 0)
            {
                currentValue = model.CurrencyQuantityBefore / model.AverageCourseRate;
            }

            var depositedValue = model.Quantity / model.CourseRate;
            var newValue = currentValue + depositedValue;
            var newAverageCourseRateForeignCurrency = newForeignCurrencyQuantity / newValue;
            model.AverageCourseRate = newAverageCourseRateForeignCurrency;

            // Update
            await currencyRepository.UpdateAverageCourseAsync(model.CurrencyCode, newAverageCourseRateForeignCurrency);
        }
        else
        {
            newDomesticCurrencyQuantity = domesticCurrencyEntity.Quantity + model.TotalAmountDomesticCurrency;
            newForeignCurrencyQuantity = model.CurrencyQuantityBefore - model.Quantity;
        }

        // Insert transaction
        var entity = _mapper.Map<TransactionEntity>(model);
        var id = await _repository.InsertAsync(entity);

        // Update currencies quantities
        await currencyRepository.UpdateQuantityAsync(domesticCurrencyEntity.Code, newDomesticCurrencyQuantity);
        await currencyRepository.UpdateQuantityAsync(model.CurrencyCode, newForeignCurrencyQuantity);

        await _unitOfWork.CommitAsync();

        return id;
    }

    public async Task CancelTransaction(TransactionDetailModel model)
    {
        var canCancel = await _operationRepository.CanCancel(model.Created);
        if (!canCancel)
        {
            throw new OperationCanNotBeCanceledException();
        }

        // Gets domestic and foreign currencies
        var currencyRepository = _unitOfWork.CurrencyRepository;
        var domesticCurrencyEntity = await currencyRepository.GetByIdAsync(DomesticCurrencyCode);
        var foreignCurrencyEntity = await currencyRepository.GetByIdAsync(model.CurrencyCode);

        // Check if currencies exists
        if (domesticCurrencyEntity is null)
        {
            throw new ArgumentNullException(nameof(domesticCurrencyEntity), "Domestic currency can't be null");
        }

        if (foreignCurrencyEntity is null)
        {
            throw new ArgumentNullException(nameof(foreignCurrencyEntity), "Foreign currency can't be null");
        }

        var transactionEntity = _mapper.Map<TransactionEntity>(model);
        
        var canceledQuantity = model.Quantity;
        var lastAverageCourseRate = model.AverageCourseRate;
        transactionEntity.IsCanceled = true;

        var isCanceledTransactionBuy = false;
        if (model.TransactionType == TransactionType.Buy)
        {
            lastAverageCourseRate = await _operationRepository.GetAverageCourseOfOperationBefore(transactionEntity);
            transactionEntity.AverageCourseRate = lastAverageCourseRate;
            isCanceledTransactionBuy = true;
        }

        await _operationRepository.UpdateAsync(transactionEntity);

        // Update every following operation after canceled one
        var operations = (await _operationRepository.GetOperationsForStornoUpdate(transactionEntity)).ToList();
        foreach (var operation in operations)
        {
            if (isCanceledTransactionBuy)
            {
                operation.CurrencyQuantityBefore -= canceledQuantity;

                if (operation.CurrencyQuantityBefore < 0)
                {
                    throw new InsufficientMoneyException(); 
                }
            }
            else
            {
                operation.CurrencyQuantityBefore += canceledQuantity;
            }

            if (operation is TransactionEntity { TransactionType: TransactionType.Buy } 
                or DonationEntity { Type: DonationType.Deposit })
            {
                var valueBefore = operation.CurrencyQuantityBefore / lastAverageCourseRate;
                var totalQuantity = operation.CurrencyQuantityBefore + operation.Quantity;
                var operationAmount = Math.Round(operation.Quantity / operation.CourseRate, 2);
                var newAverage = totalQuantity / (valueBefore + operationAmount);
                lastAverageCourseRate = newAverage;
                operation.AverageCourseRate = newAverage;
            }
            else
            {
                operation.AverageCourseRate = lastAverageCourseRate;
            }

            await _operationRepository.UpdateAsync(operation);
        }

        // Update domestic and foreign quantities in cash register
        // Check if there is enough money for cancellation
        if (model.TransactionType == TransactionType.Buy && foreignCurrencyEntity.Quantity < model.Quantity
            || model.TransactionType == TransactionType.Sell && domesticCurrencyEntity.Quantity < model.TotalAmountDomesticCurrency)
        {
            throw new InsufficientMoneyException();
        }

        // Domestic currency
        var newDomesticCurrencyQuantity = model.TransactionType == TransactionType.Buy
            ? domesticCurrencyEntity.Quantity + model.TotalAmountDomesticCurrency
            : domesticCurrencyEntity.Quantity - model.TotalAmountDomesticCurrency;

        // Foreign currency
        var newForeignCurrencyQuantity = model.TransactionType == TransactionType.Buy
            ? foreignCurrencyEntity.Quantity - model.Quantity
            : foreignCurrencyEntity.Quantity + model.Quantity;

        await currencyRepository.UpdateQuantityAsync(domesticCurrencyEntity.Code, newDomesticCurrencyQuantity);
        await currencyRepository.UpdateQuantityAsync(foreignCurrencyEntity.Code, newForeignCurrencyQuantity);

        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<TransactionListModel>> GetTransactions(DateTime from, DateTime until)
    {
        var entities = await _repository.GetTransactions(from, until);
        return _mapper.Map<IEnumerable<TransactionListModel>>(entities);
    }
}