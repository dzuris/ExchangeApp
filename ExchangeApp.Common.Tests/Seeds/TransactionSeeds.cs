using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class TransactionSeeds
{
    public static readonly TransactionEntity EmptyTransaction = new()
    {
        Id = 0,
        Time = default,
        Quantity = default,
        CurrencyQuantityBefore = default,
        CourseRate = default,
        AverageCourseRate = default,
        IsCanceled = default,
        CurrencyCode = string.Empty,
        TransactionType = TransactionType.Buy,
    };

    public static readonly TransactionEntity TransactionBuy = new()
    {
        Id = 101,
        Time = new DateTime(2022, 3, 30, 13, 30, 0),
        Quantity = 6000,
        CurrencyQuantityBefore = 212200,
        CourseRate = 24.19M,
        AverageCourseRate = 24.46378041523461M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.CzkCurrency.Code,
        TransactionType = TransactionType.Buy,
    };

    public static readonly TransactionEntity TransactionSell = new()
    {
        Id = 102,
        Time = new DateTime(2022, 3, 28, 17, 57, 36),
        Quantity = 500,
        CurrencyQuantityBefore = 4771,
        CourseRate = 1.0617M,
        AverageCourseRate = 1.1092645546154M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.UsdCurrency.Code,
        TransactionType = TransactionType.Sell
    };

    public static readonly TransactionEntity TransactionBeforeSell = new()
    {
        Id = 103,
        Time = new DateTime(2022, 3, 28, 17, 57, 35),
        Quantity = 600,
        CurrencyQuantityBefore = 5371,
        CourseRate = 1.0624M,
        AverageCourseRate = 1.1089543413245M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.UsdCurrency.Code,
        TransactionType = TransactionType.Sell
    };

    public static readonly TransactionEntity TransactionGbpThree = new()
    {
        Id = 104,
        Time = new DateTime(2022, 8, 7, 10, 02, 1),
        Quantity = 70,
        CurrencyQuantityBefore = 4650,
        CourseRate = 0.9066M,
        AverageCourseRate = 1.155426123M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.GbpCurrency.Code,
        TransactionType = TransactionType.Buy
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionEntity>().HasData(
            TransactionBuy,
            TransactionSell,
            TransactionBeforeSell,
            TransactionGbpThree);
    }
}