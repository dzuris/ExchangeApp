using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class TransactionFacade : ITransactionFacade
{
    private readonly string _domesticCurrencyCode = "EUR";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public TransactionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.TransactionRepository;
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
        var domesticCurrencyEntity = await currencyRepository.GetByIdAsync(_domesticCurrencyCode);
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

        // Insert transaction
        var entity = _mapper.Map<TransactionEntity>(model);
        var id = await _repository.InsertAsync(entity);

        decimal newDomesticCurrencyQuantity;
        decimal newForeignCurrencyQuantity;
        // Update quantities, for buy update average currency course rate
        if (model.TransactionType == TransactionType.Buy)
        {
            // Sets new quantities
            newDomesticCurrencyQuantity = domesticCurrencyEntity.Quantity - model.TotalAmountDomesticCurrency;
            newForeignCurrencyQuantity = foreignCurrencyEntity.Quantity + model.Quantity;

            // Update average course rate
            decimal currentValue = 0;
            if (foreignCurrencyEntity.AverageCourseRate > 0)
            {
                currentValue = foreignCurrencyEntity.Quantity / foreignCurrencyEntity.AverageCourseRate;
            }

            var depositedValue = model.Quantity / model.CourseRate;
            var newValue = currentValue + depositedValue;
            var newAverageCourseRateForeignCurrency = newForeignCurrencyQuantity / newValue;

            // Update
            await currencyRepository.UpdateAverageCourseAsync(model.CurrencyCode, newAverageCourseRateForeignCurrency);
        }
        else
        {
            newDomesticCurrencyQuantity = domesticCurrencyEntity.Quantity + model.TotalAmountDomesticCurrency;
            newForeignCurrencyQuantity = foreignCurrencyEntity.Quantity - model.Quantity;
        }

        // Update currencies quantities
        await currencyRepository.UpdateQuantityAsync(domesticCurrencyEntity.Code, newDomesticCurrencyQuantity);
        await currencyRepository.UpdateQuantityAsync(foreignCurrencyEntity.Code, newForeignCurrencyQuantity);

        await _unitOfWork.CommitAsync();

        return id;
    }
}