global using Xunit;
using ExchangeApp.Common.Tests;
using ExchangeApp.Common.Tests.Factories;
using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace ExchangeApp.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ExchangeAppDbContextSut = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<ExchangeAppDbContext> DbContextFactory { get; }
    protected ExchangeAppDbContext ExchangeAppDbContextSut { get; }

    public async Task InitializeAsync()
    {
        await ExchangeAppDbContextSut.Database.EnsureDeletedAsync();
        await ExchangeAppDbContextSut.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ExchangeAppDbContextSut.Database.EnsureDeletedAsync();
        await ExchangeAppDbContextSut.DisposeAsync();
    }
}
