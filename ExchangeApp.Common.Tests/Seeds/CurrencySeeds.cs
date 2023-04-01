using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity EmptyCurrency = new()
    {
        Code = "EMPTY",
        PhotoUrl = string.Empty,
        AverageCourseRate = default,
        BuyRate = default,
        SellRate = default,
        Quantity = default,
        Status = default
    };

    public static readonly CurrencyEntity EurCurrency = new()
    {
        Code = "EUR",
        PhotoUrl = "eur.png",
        AverageCourseRate = 1,
        BuyRate = 1,
        SellRate = 1,
        Quantity = 10000,
        Status = CurrencyStatus.Own
    };

    public static readonly CurrencyEntity CzkCurrency = new()
    {
        Code = "CZK",
        PhotoUrl = "czk.png",
        AverageCourseRate = 24.5M,
        BuyRate = 23.2M,
        SellRate = 24.2M,
        Quantity = 5000,
        Status = CurrencyStatus.Own
    };

    public static readonly CurrencyEntity GbpCurrency = new()
    {
        Code = "GBP",
        PhotoUrl = "gbp.png",
        AverageCourseRate = 0.88821M,
        BuyRate = 0.8512M,
        SellRate = 0.8851M,
        Quantity = 3500,
        Status = CurrencyStatus.Own
    };

    public static readonly CurrencyEntity UsdCurrency = new()
    {
        Code = "USD",
        PhotoUrl = "usd.png",
        AverageCourseRate = 1.012M,
        Quantity = 6500
    };

    public static readonly CurrencyEntity HufCurrency = new()
    {
        Code = "HUF",
        PhotoUrl = "huf.png",
        AverageCourseRate = 394,
        BuyRate = 374,
        SellRate = 389,
        Quantity = 2000000,
        Status = CurrencyStatus.NotInUse
    };

    public static readonly CurrencyEntity ChfCurrency = new()
    {
        Code = "CHF",
        PhotoUrl = "chf.png",
        AverageCourseRate = 1.05124M,
        BuyRate = 1.05212M,
        SellRate = 1.02154M,
        Quantity = 3500,
        Status = CurrencyStatus.Own
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            EurCurrency,
            CzkCurrency,
            GbpCurrency,
            UsdCurrency,
            HufCurrency,
            ChfCurrency);
    }
}