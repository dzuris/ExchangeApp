using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ExchangeApp.DAL.Tests;

public class DbContextTotalBalanceTests : DbContextTestsBase
{
    private readonly TotalBalanceRepository _totalBalanceRepository;

    public DbContextTotalBalanceTests()
    {
        _totalBalanceRepository = new TotalBalanceRepository(ExchangeAppDbContextSUT);
    }

    [Fact]
    public async Task GetAllTotalBalances_Should_AssertCollection()
    {
        // Act
        var entities = await _totalBalanceRepository.GetAllAsync();

        // Assert
        Assert.Collection(entities,
            item => DeepAssert.Equal(TotalBalanceSeeds.DailyOne, item),
            item => DeepAssert.Equal(TotalBalanceSeeds.DailyTwo, item),
            item => DeepAssert.Equal(TotalBalanceSeeds.DailyThree, item),
            item => DeepAssert.Equal(TotalBalanceSeeds.MonthlyOne, item),
            item => DeepAssert.Equal(TotalBalanceSeeds.MonthlyTwo, item)
            );
    }

    [Fact]
    public async Task GetAllTotalBalances_ByDatesFilter_Should_AssertCollection()
    {
        // Arrange
        var dateFrom = TotalBalanceSeeds.DailyTwo.Created.AddSeconds(-1);
        var dateUntil = TotalBalanceSeeds.DailyThree.Created.AddSeconds(1);

        // Act
        var entities = await _totalBalanceRepository.GetAllAsync(dateFrom, dateUntil);

        // Assert
        Assert.Collection(entities,
            item => DeepAssert.Equal(TotalBalanceSeeds.DailyTwo, item),
            item => DeepAssert.Equal(TotalBalanceSeeds.DailyThree, item)
            );
    }

    [Fact]
    public async Task InsertTotalBalance_ShouldBe_InDatabase()
    {
        // Arrange
        var totalBalanceToInsert = TotalBalanceSeeds.TotalBalanceToInsert;

        // Act
        var returnedId = await _totalBalanceRepository.InsertAsync(totalBalanceToInsert);
        await ExchangeAppDbContextSUT.SaveChangesAsync();

        // Assert
        var databaseEntity =
            await ExchangeAppDbContextSUT.TotalBalances.SingleOrDefaultAsync(e => e.Id == totalBalanceToInsert.Id);
        Assert.Equal(totalBalanceToInsert.Id, returnedId);
        Assert.NotNull(databaseEntity);
        DeepAssert.Equal(totalBalanceToInsert, databaseEntity);
    }

    [Fact]
    public async Task GetLastTotalBalance_TypeMonthly_ShouldBe_LastMonthlySeedDate()
    {
        // Arrange
        var totalBalanceDate = TotalBalanceSeeds.MonthlyTwo.Created;

        // Act
        var result = await _totalBalanceRepository.GetLastTotalBalanceDate(TotalBalanceType.Monthly);

        // Assert
        Assert.Equal(totalBalanceDate, result);
    }

    [Fact]
    public async Task CheckIfThereAreAnyUnclosedOperations_ShouldBeTrue()
    {
        // Act
        var result = await _totalBalanceRepository.ExistsOperationAfterLastDailyTotalBalance();

        // Assert
        Assert.True(result);
    }
}