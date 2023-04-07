using ExchangeApp.BL.Facades;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Exceptions;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.BL.Tests.FacadeTests;

public class DonationFacadeTests : FacadeTestsBase
{
    private readonly IDonationFacade _facadeSUT;

    public DonationFacadeTests()
    {
        _facadeSUT = new DonationFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetById_GiveTransactionId_ShouldReturnNull()
    {
        // Arrange
        var transactionId = TransactionSeeds.TransactionBuy.Id;

        // Act
        var result = await _facadeSUT.GetById(transactionId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_GiveValidId_ReturnsModel()
    {
        // Arrange
        var entity = DonationSeeds.DonationWithdraw;
        var currency = CurrencySeeds.ChfCurrency;
        var expectedModel = new DonationDetailModel
        {
            Id = entity.Id,
            Created = entity.Created,
            CourseRate = entity.CourseRate,
            AverageCourseRate = entity.AverageCourseRate,
            Quantity = entity.Quantity,
            CurrencyQuantityBefore = entity.CurrencyQuantityBefore,
            Type = entity.Type,
            Note = entity.Note,
            IsCanceled = entity.IsCanceled,
            CurrencyCode = entity.CurrencyCode,
            Currency = new CurrencyListModel
            {
                Code = currency.Code,
                AverageCourseRate = currency.AverageCourseRate,
                PhotoUrl = currency.PhotoUrl,
                Quantity = currency.Quantity,
                Status = currency.Status
            }
        };

        // Act
        var result = await _facadeSUT.GetById(entity.Id);

        // Assert
        Assert.NotNull(result);
        DeepAssert.Equal(expectedModel, result);
    }

    [Fact]
    public async Task InsertNewDonation_ShouldBe_InDatabase()
    {
        // Arrange
        var currency = CurrencySeeds.CzkCurrency;
        var model = new DonationDetailModel
        {
            Id = 8,
            Created = new DateTime(2021, 5, 6, 19, 21, 12),
            CourseRate = 24.35M,
            AverageCourseRate = currency.AverageCourseRate,
            Quantity = 5000,
            CurrencyQuantityBefore = currency.Quantity,
            Type = DonationType.Deposit,
            Note = "My new donation through facades",
            IsCanceled = false,
            CurrencyCode = currency.Code
        };

        // Act
        var result = await _facadeSUT.InsertAsync(model);

        // Assert
        var databaseContent = await DbContextFactory.CreateDbContextAsync();
        var dbCurrency = await databaseContent.Currencies.SingleAsync(e => e.Code == currency.Code);
        Assert.Equal(model.Id, result);
        Assert.Equal(10000, dbCurrency.Quantity);
        Assert.Equal(24.4247697M, dbCurrency.AverageCourseRate, 7);
    }

    [Fact]
    public async Task InsertNewDonation_InsufficientMoneyToWithdraw()
    {
        // Arrange
        var currency = CurrencySeeds.CzkCurrency;
        var model = new DonationDetailModel
        {
            Id = 9,
            Created = new DateTime(2021, 6, 30, 12, 01, 2),
            CourseRate = 24.35M,
            AverageCourseRate = currency.AverageCourseRate,
            Quantity = 500000000000,
            CurrencyQuantityBefore = currency.Quantity,
            Type = DonationType.Withdraw,
            Note = string.Empty,
            IsCanceled = false,
            CurrencyCode = currency.Code
        };

        // Act and Assert
        await Assert.ThrowsAsync<InsufficientMoneyException>(async () => await _facadeSUT.InsertAsync(model));
    }

    [Fact]
    public async Task InsertNewDonation_WrongCourseRate_ThrowsException()
    {
        // Arrange
        var currency = CurrencySeeds.CzkCurrency;
        var model = new DonationDetailModel
        {
            Id = 10,
            Created = new DateTime(2021, 6, 30, 20, 01, 19),
            CourseRate = 0,
            AverageCourseRate = currency.AverageCourseRate,
            Quantity = 5000,
            CurrencyQuantityBefore = currency.Quantity,
            Type = DonationType.Deposit,
            Note = "My new wrong donation through facades",
            IsCanceled = false,
            CurrencyCode = currency.Code
        };

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _facadeSUT.InsertAsync(model));
    }

    [Fact]
    public async Task CancelDonation_ThrowsException_OperationClosed()
    {
        // Arrange
        var model = Mapper.Map<DonationDetailModel>(DonationSeeds.DonationToCancel);

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanNotBeCanceledException>(async () => await _facadeSUT.CancelDonation(model));
    }
}