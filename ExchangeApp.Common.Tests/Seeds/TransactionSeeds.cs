using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class TransactionSeeds
{
    public static readonly TransactionEntity EmptyTransaction = new()
    {
        Id = 0,
        Created = default,
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
        Created = new DateTime(2022, 3, 30, 13, 30, 0),
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
        Created = new DateTime(2022, 3, 28, 17, 57, 36),
        Quantity = 500,
        CurrencyQuantityBefore = 4771,
        CourseRate = 1.0617M,
        AverageCourseRate = 1.094512M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.UsdCurrency.Code,
        TransactionType = TransactionType.Sell
    };

    public static readonly TransactionEntity TransactionBeforeSell = new()
    {
        Id = 103,
        Created = new DateTime(2022, 3, 28, 17, 57, 35),
        Quantity = 600,
        CurrencyQuantityBefore = 5371,
        CourseRate = 1.0624M,
        AverageCourseRate = 1.094512M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.UsdCurrency.Code,
        TransactionType = TransactionType.Sell
    };

    public static readonly TransactionEntity TransactionGbpThree = new()
    {
        Id = 104,
        Created = new DateTime(2022, 8, 7, 10, 02, 1),
        Quantity = 70,
        CurrencyQuantityBefore = 4650,
        CourseRate = 0.9066M,
        AverageCourseRate = 0.899541M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.GbpCurrency.Code,
        TransactionType = TransactionType.Buy
    };

    public static readonly TransactionEntity ClosedTransaction = EmptyTransaction with
    {
        Id = 105,
        Created = new DateTime(2005, 2, 19, 6, 8, 16),
        Quantity = 50000,
        CurrencyQuantityBefore = 1800000,
        CourseRate = 374,
        AverageCourseRate = 396.45634615532M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.HufCurrency.Code,
        TransactionType = TransactionType.Sell
    };

    public static readonly TransactionDetailModel TransactionDetailModel = new()
    {
        Id = 15,
        Created = new DateTime(2026, 2, 28, 16, 15, 12),
        CourseRate = 4.81M,
        AverageCourseRate = 4.8664662534512M,
        Quantity = 1600,
        CurrencyQuantityBefore = 142480,
        TransactionType = TransactionType.Buy,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.PlnCurrency.Code
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionEntity>().HasData(
            TransactionBuy,
            TransactionSell,
            TransactionBeforeSell,
            TransactionGbpThree,
            ClosedTransaction);
    }
}