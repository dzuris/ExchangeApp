using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class TotalBalanceSeeds
{
    public static readonly TotalBalanceEntity DailyOne = new()
    {
        Id = 1,
        Created = new DateTime(2006, 10, 7, 20, 01, 05),
        LastTotalBalance = DateTime.MinValue,
        Type = TotalBalanceType.Daily
    };

    public static readonly TotalBalanceEntity DailyTwo = new()
    {
        Id = 2,
        Created = new DateTime(2006, 10, 8, 19, 46, 12),
        LastTotalBalance = DailyOne.Created,
        Type = TotalBalanceType.Daily
    };

    public static readonly TotalBalanceEntity DailyThree = new()
    {
        Id = 3,
        Created = new DateTime(2006, 10, 9, 19, 1, 15),
        LastTotalBalance = DailyTwo.Created,
        Type = TotalBalanceType.Daily
    };

    public static readonly TotalBalanceEntity TotalBalanceToInsert = new()
    {
        Id = 4,
        Created = new DateTime(2006, 10, 10, 20, 15, 0),
        LastTotalBalance = DailyThree.Created,
        Type = TotalBalanceType.Daily
    };

    public static readonly TotalBalanceEntity MonthlyOne = new()
    {
        Id = 5,
        Created = new DateTime(2006, 10, 31),
        LastTotalBalance = DateTime.MinValue,
        Type = TotalBalanceType.Monthly
    };

    public static readonly TotalBalanceEntity MonthlyTwo = new()
    {
        Id = 6,
        Created = new DateTime(2006, 11, 30),
        LastTotalBalance = MonthlyOne.Created,
        Type = TotalBalanceType.Monthly
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TotalBalanceEntity>().HasData(
            DailyOne,
            DailyTwo,
            DailyThree,
            MonthlyOne,
            MonthlyTwo);
    }
}