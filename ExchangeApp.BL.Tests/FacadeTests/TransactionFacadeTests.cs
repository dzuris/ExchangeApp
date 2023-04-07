using ExchangeApp.BL.Facades;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Exceptions;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;

namespace ExchangeApp.BL.Tests.FacadeTests;

public class TransactionFacadeTests : FacadeTestsBase
{
    private readonly ITransactionFacade _facadeSUT;

    public TransactionFacadeTests()
    {
        _facadeSUT = new TransactionFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetById_WrongId_ExpectNull()
    {
        // Arrange
        const int wrongId = 0;

        // Act
        var result = await _facadeSUT.GetById(wrongId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_CorrectId_ExpectModel()
    {
        // Arrange
        var model = Mapper.Map<TransactionDetailModel>(TransactionSeeds.TransactionBuy);

        // Act
        var result = await _facadeSUT.GetById(model.Id);

        // Assert
        Assert.NotNull(result);
        DeepAssert.Equal(model, result, nameof(model.Currency));
    }

    [Fact]
    public async Task InsertNewTransaction_ShouldBe_Valid()
    {
        // Arrange
        var newTransactionModel = new TransactionDetailModel
        {
            Id = 151,
            Created = new DateTime(2024, 8, 7, 10, 02, 1),
            Quantity = 150,
            CurrencyQuantityBefore = 4720,
            CourseRate = 0.89451M,
            AverageCourseRate = 0.8996448854M,
            IsCanceled = false,
            CurrencyCode = CurrencySeeds.GbpCurrency.Code,
            TransactionType = TransactionType.Sell
        };

        // Act
        var resultId = await _facadeSUT.InsertAsync(newTransactionModel);

        // Assert
        var dbContent = await DbContextFactory.CreateDbContextAsync();
        var dbCurrency = dbContent.Currencies.Single(e => e.Code == newTransactionModel.CurrencyCode);
        var newExpectedQuantity = newTransactionModel.CurrencyQuantityBefore - newTransactionModel.Quantity;
        Assert.Equal(newTransactionModel.Id, resultId);
        Assert.Equal(newTransactionModel.AverageCourseRate, dbCurrency.AverageCourseRate);
        Assert.Equal(newExpectedQuantity, dbCurrency.Quantity);
    }

    [Fact]
    public async Task CancelClosedTransaction_ThrowsException()
    {
        // Arrange
        var model = Mapper.Map<TransactionDetailModel>(TransactionSeeds.ClosedTransaction);

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanNotBeCanceledException>(async () =>
            await _facadeSUT.CancelTransaction(model));
    }
}