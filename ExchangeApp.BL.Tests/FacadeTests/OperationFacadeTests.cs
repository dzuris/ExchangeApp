using ExchangeApp.BL.Facades;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Operations;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;

namespace ExchangeApp.BL.Tests.FacadeTests;

public class OperationFacadeTests : FacadeTestsBase
{
    private readonly IOperationFacade _facadeSUT;

    public OperationFacadeTests()
    {
        _facadeSUT = new OperationFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetOperationsAsync_Returns_ExpectedList_WithCount()
    {
        // Arrange
        const int pageSize = 3;
        const int pageNumber = 2;

        // Act
        var result = await _facadeSUT.GetOperationsAsync(pageSize, pageNumber);

        // Assert
        Assert.Collection(result,
            item => DeepAssert.Equal(Mapper.Map<OperationListModelBase>(TransactionSeeds.TransactionBuy), item),
            item => DeepAssert.Equal(Mapper.Map<OperationListModelBase>(TransactionSeeds.TransactionSell), item),
            item => DeepAssert.Equal(Mapper.Map<OperationListModelBase>(TransactionSeeds.TransactionBeforeSell), item)
            );
    }

    [Fact]
    public async Task GetProfitList_ShouldReturn_OneItem()
    {
        // Arrange
        var dateFrom = new DateTime(2022, 3, 15, 10, 0, 0);
        var dateUntil = new DateTime(2022, 4, 10, 15, 0, 0);
        var currency = CurrencySeeds.UsdCurrency;
        var expectedItem = new CurrencyProfitModel
        {
            Code = currency.Code,
            PhotoUrl = currency.PhotoUrl,
            Profit = 30.68775694M
        };
        const int precision = 8;

        // Act
        var result = await _facadeSUT.GetProfitListAsync(dateFrom, dateUntil);

        // Assert
        Assert.Single(result);
        DeepAssert.Equal(expectedItem, result.ElementAt(0), nameof(expectedItem.Profit));
        Assert.Equal(expectedItem.Profit, result.ElementAt(0).Profit, precision);
    }

    [Fact]
    public async Task GetProfitListOperations_ShouldReturn_ExpectedCollection()
    {
        // Arrange
        var dateFrom = new DateTime(2022, 3, 25, 15, 0, 0);
        var dateUntil = new DateTime(2022, 4, 25, 15, 0, 0);

        // Act
        var resultList = await _facadeSUT.GetOperationsProfitAsync(dateFrom, dateUntil);

        // Assert
        var firstItem = Mapper.Map<OperationProfitModel>(TransactionSeeds.TransactionSell) with
        {
            Profit = 14.12M
        };
        var secondItem = Mapper.Map<OperationProfitModel>(TransactionSeeds.TransactionBeforeSell) with
        {
            Profit = 16.57M
        };
        Assert.Collection(resultList,
            item => DeepAssert.Equal(firstItem, item),
            item => DeepAssert.Equal(secondItem, item));
    }

    [Fact]
    public async Task GetProfitListOperations_DonationsIncluded_ShouldReturn_ExpectedCollection()
    {
        // Arrange
        var dateFrom = new DateTime(2022, 8, 5, 15, 0, 0);
        var dateUntil = new DateTime(2022, 8, 25, 15, 0, 0);

        // Act
        var resultList = await _facadeSUT.GetOperationsProfitAsync(dateFrom, dateUntil);

        // Assert
        Assert.Single(resultList);
        var expectedModel = Mapper.Map<OperationProfitModel>(DonationSeeds.DonationGbpTwo) with
        {
            Profit = 32.74M
        };
        DeepAssert.Equal(expectedModel, resultList.ElementAt(0));
    }
}