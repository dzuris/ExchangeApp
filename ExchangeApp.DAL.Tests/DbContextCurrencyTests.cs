using ExchangeApp.Common.Tests;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ExchangeApp.DAL.Tests;

public class DbContextCurrencyTests : DbContextTestsBase
{
    public DbContextCurrencyTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_Currency()
    {
        // Arrange
        var entity = new CurrencyEntity
        {
            Code = "JPN_TestData",
            State = "Japan_TestData",
            PhotoUrl = "jpn_TestData.png"
        };

        // Act
        ExchangeAppDbContextSut.Currencies.Add(entity);
        await ExchangeAppDbContextSut.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Currencies.SingleAsync(i => i.Code == entity.Code);

        DeepAssert.Equal(entity, actualEntity);
    }
}
