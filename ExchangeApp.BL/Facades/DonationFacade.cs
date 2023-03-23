using System.Resources;
using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Exceptions;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class DonationFacade : IDonationFacade
{
    private const string DomesticCurrencyCode = "EUR";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDonationRepository _repository;
    private readonly IOperationRepository _operationRepository;
    private readonly IMapper _mapper;

    public DonationFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.DonationRepository;
        _operationRepository = _unitOfWork.OperationRepository;
        _mapper = mapper;
    }

    public async Task<DonationDetailModel?> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return entity is null ? null : _mapper.Map<DonationDetailModel>(entity);
    }

    public async Task<int> InsertAsync(DonationDetailModel model)
    {
        // Gets currency entity
        var currencyRepository = _unitOfWork.CurrencyRepository;

        if (model.CourseRate <= 0)
        {
            throw new ArgumentException("Course rate can't be 0 or lower");
        }

        decimal newCurrencyQuantity;
        // Update currency average course when donation is deposit, sets new currency quantity
        if (model.Type == DonationType.Deposit)
        {
            newCurrencyQuantity = model.CurrencyQuantityBefore + model.Quantity;
            decimal currentValue = 0;
            if (model.AverageCourseRate > 0)
            {
                currentValue = model.CurrencyQuantityBefore / model.AverageCourseRate;
            }
            var depositedValue = model.Quantity / model.CourseRate;
            var newValue = currentValue + depositedValue;
            var newAverageCurrencyCourseRate = newCurrencyQuantity / newValue;
            model.AverageCourseRate = newAverageCurrencyCourseRate;

            // Update
            await currencyRepository.UpdateAverageCourseAsync(model.CurrencyCode, newAverageCurrencyCourseRate);
        }
        else
        {
            if (model.Quantity > model.CurrencyQuantityBefore)
            {
                throw new InsufficientMoneyException("Model quantity can't be more than currency quantity");
            }

            newCurrencyQuantity = model.CurrencyQuantityBefore - model.Quantity;
        }

        // Insert donation
        var entity = _mapper.Map<DonationEntity>(model);
        var id = await _repository.InsertAsync(entity);

        // Update quantity
        await currencyRepository.UpdateQuantityAsync(model.CurrencyCode, newCurrencyQuantity);

        await _unitOfWork.CommitAsync();

        return id;
    }

    /// <summary>
    /// Cancel donation
    /// </summary>
    /// <param name="model">Donation detail model for cancellation</param>
    /// <returns></returns>
    /// <exception cref="CurrencyMissingException">Internal error while getting foreign and domestic currencies</exception>
    /// <exception cref="InsufficientMoneyException">Can not cancel because you do not have enough money in cash register or some operation after cancel has before quantity below 0</exception>
    public async Task CancelDonation(DonationDetailModel model)
    {
        // Gets domestic and foreign currencies
        var currencyRepository = _unitOfWork.CurrencyRepository;
        var domesticCurrencyEntity = await currencyRepository.GetByIdAsync(DomesticCurrencyCode);
        var foreignCurrencyEntity = await currencyRepository.GetByIdAsync(model.CurrencyCode);
        
        // Check if currencies exists
        if (domesticCurrencyEntity is null)
        {
            throw new CurrencyMissingException("Domestic currency can't be null");
        }

        if (foreignCurrencyEntity is null)
        {
            throw new CurrencyMissingException("Foreign currency can't be null");
        }

        var donationEntity = _mapper.Map<DonationEntity>(model);

        var operations = (await _operationRepository.GetOperationsForStornoUpdate(donationEntity)).ToList();
        var canceledQuantity = donationEntity.Quantity;

        // Canceled donation is domestic currency
        if (donationEntity.CurrencyCode == DomesticCurrencyCode)
        {
            donationEntity.IsCanceled = true;
            await _operationRepository.UpdateAsync(donationEntity);

            foreach (var operation in operations)
            {
                if (donationEntity.Type == DonationType.Deposit)
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
            }

            var newCurrencyQuantityInCashRegister = donationEntity.Type == DonationType.Deposit
                ? domesticCurrencyEntity.Quantity - canceledQuantity
                : domesticCurrencyEntity.Quantity + canceledQuantity;

            if (newCurrencyQuantityInCashRegister < 0)
            {
                throw new InsufficientMoneyException();
            }

            await currencyRepository.UpdateQuantityAsync(DomesticCurrencyCode, newCurrencyQuantityInCashRegister);

            return;
        }

        // Canceled donation is foreign currency
        var lastAverageCourseRate = model.AverageCourseRate;
        donationEntity.IsCanceled = true;

        var isCanceledDonationDeposit = false;
        if (model.Type == DonationType.Deposit)
        {
            lastAverageCourseRate = await _operationRepository.GetAverageCourseOfOperationBefore(donationEntity);
            donationEntity.AverageCourseRate = lastAverageCourseRate;
            isCanceledDonationDeposit = true;
        }

        await _operationRepository.UpdateAsync(donationEntity);
        
        foreach (var operation in operations)
        {
            if (isCanceledDonationDeposit)
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

        // Update donation currency quantity
        if (model.Type == DonationType.Deposit && foreignCurrencyEntity.Quantity < model.Quantity)
        {
            throw new InsufficientMoneyException();
        }

        var newForeignCurrencyQuantityInCashRegister = donationEntity.Type == DonationType.Deposit
            ? foreignCurrencyEntity.Quantity - canceledQuantity
            : foreignCurrencyEntity.Quantity + canceledQuantity;

        await currencyRepository.UpdateQuantityAsync(foreignCurrencyEntity.Code,
            newForeignCurrencyQuantityInCashRegister);

        await _unitOfWork.CommitAsync();
    }
}