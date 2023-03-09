using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public class DonationFacade : IDonationFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDonationRepository _repository;
    private readonly IMapper _mapper;

    public DonationFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IMapper mapper)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _repository = _unitOfWork.DonationRepository;
        _mapper = mapper;
    }

    public async Task<int> InsertAsync(DonationDetailModel model)
    {
        // Gets currency entity
        var currencyRepository = _unitOfWork.CurrencyRepository;
        var currentCurrencyEntity = await currencyRepository.GetByIdAsync(model.CurrencyCode);

        // Currency must exists
        if (currentCurrencyEntity is null)
        {
            throw new ArgumentNullException(nameof(currentCurrencyEntity), $"Unknown currency code: '{model.CurrencyCode}'");
        }

        if (model.CourseRate <= 0)
        {
            throw new ArgumentException("Course rate can't be 0 or lower");
        }

        // Insert donation
        var entity = _mapper.Map<DonationEntity>(model);
        var id = await _repository.InsertAsync(entity);

        decimal newQuantity;
        // Update currency average course when donation is deposit, sets new currency quantity
        if (model.Type == DonationType.Deposit)
        {
            newQuantity = currentCurrencyEntity.Quantity + model.Quantity;
            decimal currentValue = 0;
            if (currentCurrencyEntity.AverageCourseRate > 0)
            {
                currentValue = currentCurrencyEntity.Quantity / currentCurrencyEntity.AverageCourseRate;
            }
            var depositedValue = model.Quantity / model.CourseRate;
            var newValue = currentValue + depositedValue;
            var newAverageCurrencyCourseRate = newQuantity / newValue;

            // Update
            await currencyRepository.UpdateAverageCourseAsync(model.CurrencyCode, newAverageCurrencyCourseRate);
        }
        else
        {
            if (model.Quantity > currentCurrencyEntity.Quantity)
            {
                throw new ArgumentException("Model quantity can't be more than currency quantity");
            }

            newQuantity = currentCurrencyEntity.Quantity - model.Quantity;
        }

        // Update quantity
        await currencyRepository.UpdateQuantityAsync(model.CurrencyCode, newQuantity);

        await _unitOfWork.CommitAsync();

        return id;
    }
}