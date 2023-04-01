using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ExchangeApp.DAL.Tests;

public class DbContextOperationTests : DbContextTestsBase
{
    private readonly OperationRepository _operationRepository;

    public DbContextOperationTests()
    {
        _operationRepository = new OperationRepository(ExchangeAppDbContextSUT, Mapper);
    }

    [Fact]
    public async Task GetById_ShouldReturn_SameEntity()
    {
        // Arrange
        var entity = DonationSeeds.DonationDeposit;

        // Act
        var result = await _operationRepository.GetByIdAsync(entity.Id);

        // Assert
        DeepAssert.Equal(entity, result);
    }

    [Fact]
    public async Task GetLastOperations_CountShouldBe_PageSize()
    {
        // Arrange
        const int pageSize = 3;

        // Act
        var operations = await _operationRepository.GetLastOperationsAsync(pageSize, 1);

        // Assert
        Assert.Equal(pageSize, operations.Count());
    }

    [Fact]
    public async Task GetOperationsByDates_ListCountShouldBe_One_And_Entity_DeepAssert_Equal_Ok()
    {
        // Arrange
        var entity = DonationSeeds.DonationWithdraw;
        var dateFrom = entity.Time.AddMicroseconds(-10);
        var dateUntil = entity.Time.AddMicroseconds(10);

        // Act
        var list = (await _operationRepository.GetOperationsAsync(dateFrom, dateUntil)).ToList();

        // Assert
        Assert.Single(list);
        DeepAssert.Equal(entity, list.ElementAt(0));
    }

    [Fact]
    public async Task GetFilteredOperations_OnlyTransactions()
    {
        // Arrange
        const OperationFilterOption option = OperationFilterOption.Transactions;

        // Act
        var list = (await _operationRepository.GetFilteredOperationsAsync(10, 1, option, null, null, null)).ToList();

        // Assert
        foreach (var operation in list)
        {
            Assert.True(operation is TransactionEntity);
        }
    }

    [Fact]
    public async Task UpdateOperation_CancelOperation_ResultFromDbContext()
    {
        // Arrange
        var entity = DonationSeeds.DonationToCancel;
        entity.IsCanceled = true;

        // Act
        await _operationRepository.UpdateAsync(entity);
        await ExchangeAppDbContextSUT.SaveChangesAsync();

        // Assert
        var databaseEntity = 
            await ExchangeAppDbContextSUT.Operations.SingleOrDefaultAsync(e => e.Id == entity.Id);
        Assert.NotNull(databaseEntity);
        Assert.True(databaseEntity.IsCanceled);
    }

    [Fact]
    public async Task GetAverageCourseRateFromOperationBefore_ShouldEqual()
    {
        // Arrange
        var entity = TransactionSeeds.TransactionSell;

        // Act
        var result = await _operationRepository.GetAverageCourseOfOperationBefore(entity);

        // Assert
        var operationBeforeAverageCourseRate = TransactionSeeds.TransactionBeforeSell.AverageCourseRate;
        Assert.Equal(operationBeforeAverageCourseRate, result);
    }

    [Fact]
    public async Task GetOperationsForCancellation_ShouldReturn_ValidList()
    {
        // Arrange
        var entity = DonationSeeds.DonationGbpOne;

        // Act
        var operations = (await _operationRepository.GetOperationsForStornoUpdate(entity)).ToList();

        // Assert
        Assert.Collection(operations, 
            item => DeepAssert.Equal(DonationSeeds.DonationGbpTwo, item),
            item => DeepAssert.Equal(TransactionSeeds.TransactionGbpThree, item));
    }

    [Fact]
    public async Task CanCancel_ShouldBeFalse()
    {
        // Arrange
        var entity = DonationSeeds.DonationToCancel;

        // Act
        var result = await _operationRepository.CanCancel(entity.Time);

        // Assert
        Assert.False(result);
    }
}