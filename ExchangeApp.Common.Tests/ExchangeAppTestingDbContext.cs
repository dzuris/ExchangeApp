using ExchangeApp.Common.Tests.Seeds;
using ExchangeApp.DAL.Data;
using ExchangeApp.DAL.Entities.Customers;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests;

public class ExchangeAppTestingDbContext : ExchangeAppDbContext
{
    private readonly bool _seedDemoData;

    public ExchangeAppTestingDbContext(DbContextOptions options, bool seedDemoData = false) : base(options, seedCurrencyData: false)
    {
        _seedDemoData = seedDemoData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedDemoData)
        {
            CurrencySeeds.Seed(modelBuilder);
            CurrencyHistorySeeds.Seed(modelBuilder);
            CustomerSeeds.Seed(modelBuilder);
            DonationSeeds.Seed(modelBuilder);
            TransactionSeeds.Seed(modelBuilder);
            TotalBalanceSeeds.Seed(modelBuilder);
        }
    }
}