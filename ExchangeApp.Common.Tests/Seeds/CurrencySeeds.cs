using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity CurrencyEntity = new()
    {
        Code = "EUR_TestSeed",
        Quantity = 15415.55F,
        PhotoUrl = "eur_TestSeed.png",
        BuyRate = 1F,
        Status = CurrencyState.Own
    };

    public static readonly CurrencyEntity CurrencyEntityUpdate = CurrencyEntity with { Code = "CZK_TestSeed" };
    public static readonly CurrencyEntity CurrencyEntityDelete = CurrencyEntity with { Code = "USD_TestSeed", PhotoUrl = "usd_TestSeed.png" };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            CurrencyEntity,
            CurrencyEntityUpdate,
            CurrencyEntityDelete
        );
    }
}