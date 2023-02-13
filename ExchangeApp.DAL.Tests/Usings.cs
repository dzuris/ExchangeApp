global using Xunit;
using Xunit.Abstractions;

namespace ExchangeApp.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    public DbContextTestsBase(ITestOutputHelper output)
    {
        //XUnitTestOut
    }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
