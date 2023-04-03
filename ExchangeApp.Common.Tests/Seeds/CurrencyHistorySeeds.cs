using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class CurrencyHistorySeeds
{
    public static readonly DateTime DateOne = new(2007, 1, 1, 0, 0, 0);
    public static readonly DateTime DateTwo = new(2015, 12, 31, 23, 59, 59);
    public static readonly DateTime DateThree = new(2022, 6, 22, 18, 45, 15);

    public static readonly CurrencyHistoryEntity EmptyHistoryEntity = new()
    {
        Id = 0,
        Code = CurrencySeeds.EmptyCurrency.Code,
        Quantity = 0,
        AverageCourseRate = 0,
        TimeStamp = DateTime.MinValue
    };

    public static readonly CurrencyHistoryEntity EurCurrencyHistoryDateOne = new()
    {
        Id = 1,
        Code = CurrencySeeds.EurCurrency.Code,
        Quantity = 4212,
        AverageCourseRate = 1,
        TimeStamp = DateOne
    };

    public static readonly CurrencyHistoryEntity CzkCurrencyHistoryDateOne = new()
    {
        Id = 2,
        Code = CurrencySeeds.CzkCurrency.Code,
        Quantity = 241126,
        AverageCourseRate = 24.1234561342M,
        TimeStamp = DateOne
    };

    public static readonly CurrencyHistoryEntity EntityToMap = new()
    {
        Id = 3,
        Code = CurrencySeeds.CurrencyToMap.Code,
        Quantity = CurrencySeeds.CurrencyToMap.Quantity,
        AverageCourseRate = CurrencySeeds.CurrencyToMap.AverageCourseRate,
        TimeStamp = DateOne
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyHistoryEntity>().HasData(
            EurCurrencyHistoryDateOne,
            CzkCurrencyHistoryDateOne);
    }
}