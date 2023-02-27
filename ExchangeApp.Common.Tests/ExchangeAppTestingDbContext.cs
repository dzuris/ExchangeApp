using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests;

public class ExchangeAppTestingDbContext : ExchangeAppDbContext
{
    private readonly bool _seedTestData;

    public ExchangeAppTestingDbContext(DbContextOptions<ExchangeAppDbContext> options, bool seedTestData = false) : base(options)
    {
        _seedTestData = seedTestData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestData)
        {
            CurrencySeeds.Seed(modelBuilder);
        }
    }
}