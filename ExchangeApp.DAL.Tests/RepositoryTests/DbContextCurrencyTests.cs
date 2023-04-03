using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Tests.RepositoryTests;

public class DbContextCurrencyTests : DbContextTestsBase
{
    private readonly CurrencyRepository _currencyRepository;

    public DbContextCurrencyTests()
    {
        _currencyRepository = new CurrencyRepository(ExchangeAppDbContextSUT, Mapper);
    }

    [Fact]
    public async Task Get_OneCurrency_CurrencyExists()
    {
        // Arrange
        var currencyCode = CurrencySeeds.EurCurrency.Code;

        // Act
        var entity = await _currencyRepository.GetByIdAsync(currencyCode);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(currencyCode, entity.Code);
    }

    [Fact]
    public async Task Get_OneCurrency_CurrencyNotExists()
    {
        // Arrange
        const string currencyCodeWhichDoesNotExists = "Currency_Code_Which_Does_Not_Exists";

        // Act
        var entity = await _currencyRepository.GetByIdAsync(currencyCodeWhichDoesNotExists);

        // Assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetActiveCurrencies_Count_Is_Three()
    {
        // Act
        var list = await _currencyRepository.GetActiveCurrenciesAsync();

        // Assert
        Assert.Equal(4, list.Count());
    }

    [Fact]
    public async Task GetNonActiveCurrencies_Count_IsTwo()
    {
        // Act
        var list = await _currencyRepository.GetNonActiveCurrenciesAsync();

        // Assert
        Assert.Equal(2, list.Count());
    }

    /// <summary>
    /// This test code probably fail because they ran asynchronously, so if first will ran test with inserting currency history,
    /// then the count could be three
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetCurrenciesHistory_FromDateOne_Count_IsTwo()
    {
        // Act
        var list = await _currencyRepository.GetCurrenciesHistory(CurrencyHistorySeeds.DateOne);

        // Assert
        Assert.Equal(2, list.Count());
    }

    [Fact]
    public async Task GetCurrenciesHistory_FirstItem_ShouldBe_EurHistoryCurrencyDateOne()
    {
        // Act
        var firstListElement =
            (await _currencyRepository.GetCurrenciesHistory(CurrencyHistorySeeds.DateOne)).ElementAt(0);

        // Assert
        DeepAssert.Equal(CurrencyHistorySeeds.EurCurrencyHistoryDateOne, firstListElement);
    }

    [Fact]
    public async Task InsertCurrencyHistory_ShouldBeIn_Database()
    {
        // Arrange
        var dateTimeNow = DateTime.Now;
        var newCurrencyHistory = new CurrencyHistoryEntity
        {
            Id = 3,
            Code = CurrencySeeds.EurCurrency.Code,
            Quantity = 1346.15M,
            AverageCourseRate = 1M,
            TimeStamp = dateTimeNow
        };

        // Act
        await _currencyRepository.InsertCurrencyHistory(newCurrencyHistory);
        await ExchangeAppDbContextSUT.SaveChangesAsync();

        // Assert
        var entity = await ExchangeAppDbContextSUT.CurrenciesHistory
            .SingleOrDefaultAsync(e => e.Id == newCurrencyHistory.Id);
        Assert.NotNull(entity);
        DeepAssert.Equal(newCurrencyHistory, entity);
    }

    [Fact]
    public async Task GetCurrencyBalance_FromEurCurrencyDateOne_ShouldEqualItsQuantity()
    {
        // Arrange
        var code = CurrencyHistorySeeds.EurCurrencyHistoryDateOne.Code;
        var dateTime = CurrencyHistorySeeds.EurCurrencyHistoryDateOne.TimeStamp;
        var currencyQuantity = CurrencyHistorySeeds.EurCurrencyHistoryDateOne.Quantity;

        // Act
        var quantity = await _currencyRepository.GetCurrencyBalance(code, dateTime);

        // Assert
        Assert.Equal(currencyQuantity, quantity);
    }

    [Fact]
    public async Task UpdateCurrency_AssertEntities()
    {
        // Arrange
        var entity = CurrencySeeds.GbpCurrency with
        {
            AverageCourseRate = 0.89412M,
            BuyRate = 0.8422M,
            SellRate = 0.8612M,
            Quantity = 3900
        };

        // Act
        await _currencyRepository.UpdateAsync(entity);

        // Assert
        var databaseEntity = await ExchangeAppDbContextSUT
            .Currencies
            .SingleOrDefaultAsync(e => e.Code == entity.Code);
        Assert.NotNull(databaseEntity);
        DeepAssert.Equal(entity, databaseEntity);
    }

    [Fact]
    public async Task UpdateQuantity_AssertToDatabaseEntityQuantity_ShouldBeOk()
    {
        // Arrange
        var entity = CurrencySeeds.HufCurrency;
        const int newQuantity = 1800000;

        // Act
        await _currencyRepository.UpdateQuantityAsync(entity.Code, newQuantity);

        // Assert
        var databaseEntity = await ExchangeAppDbContextSUT
            .Currencies
            .SingleOrDefaultAsync(e => e.Code == entity.Code);
        Assert.NotNull(databaseEntity);
        Assert.Equal(newQuantity, databaseEntity.Quantity);
    }
}