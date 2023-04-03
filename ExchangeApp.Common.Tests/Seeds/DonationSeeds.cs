using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Operations;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.Common.Tests.Seeds;

public static class DonationSeeds
{
    public static readonly DonationEntity EmptyDonation = new()
    {
        Id = 0,
        Time = default,
        Quantity = default,
        CurrencyQuantityBefore = default,
        CourseRate = default,
        AverageCourseRate = default,
        IsCanceled = default,
        CurrencyCode = string.Empty,
        Type = DonationType.Deposit,
        Note = string.Empty
    };

    public static readonly DonationEntity DonationToMap = new()
    {
        Id = 5,
        Time = new DateTime(1975, 11, 30, 19, 0, 0),
        Quantity = 54000,
        CurrencyQuantityBefore = 754000,
        CourseRate = 4.25M,
        AverageCourseRate = 4.55365451124M,
        IsCanceled = true,
        Type = DonationType.Levy,
        Note = "My note",
        CurrencyCode = CurrencySeeds.CurrencyToMap.Code,
        Currency = CurrencySeeds.CurrencyToMap
    };

    public static readonly DonationEntity DonationDeposit = new()
    {
        Id = 1,
        Time = new DateTime(2020, 5, 28, 19, 44, 59),
        Quantity = 200,
        CurrencyQuantityBefore = 3400,
        CourseRate = 1.02145M,
        AverageCourseRate = 1.05142M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.ChfCurrency.Code,
        Type = DonationType.Deposit,
        Note = "My deposit note"
    };

    public static readonly DonationEntity DonationWithdraw = new()
    {
        Id = 2,
        Time = new DateTime(2019, 12, 31, 20, 0, 0),
        Quantity = 600,
        CurrencyQuantityBefore = 3350,
        CourseRate = 1.0112M,
        AverageCourseRate = 1.05312M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.ChfCurrency.Code,
        Type = DonationType.Withdraw,
        Note = "My withdraw note"
    };

    public static readonly DonationEntity DonationLevy = EmptyDonation with
    {
        Id = 3,
        Time = new DateTime(2020, 12, 12, 15, 16, 1),
        Quantity = 500,
        CurrencyQuantityBefore = 3900,
        CourseRate = 1.02654M,
        AverageCourseRate = 1.05112M,
        CurrencyCode = CurrencySeeds.ChfCurrency.Code,
        Type = DonationType.Levy
    };

    public static readonly DonationEntity DonationToInsert = EmptyDonation with
    {
        Id = 4,
        Time = new DateTime(2023, 4, 1, 14, 6, 51),
        Quantity = 1500,
        CurrencyQuantityBefore = 4150,
        CourseRate = 1.05M,
        AverageCourseRate = 1.05612M,
        CurrencyCode = CurrencySeeds.ChfCurrency.Code,
        Type = DonationType.Deposit,
        Note = "My new donation"
    };

    public static readonly DonationEntity DonationToCancel = EmptyDonation with
    {
        Id = 5,
        Time = new DateTime(2006, 10, 7, 16, 38, 16),
        Quantity = 601,
        CurrencyQuantityBefore = 200000,
        CourseRate = 23.2M,
        AverageCourseRate = 24.512546142M,
        IsCanceled = false,
        CurrencyCode = CurrencySeeds.CzkCurrency.Code,
        Type = DonationType.Deposit
    };

    public static readonly DonationEntity DonationGbpOne = EmptyDonation with
    {
        Id = 6,
        Time = new DateTime(2022, 8, 5, 18, 12, 11),
        Quantity = 1500,
        CurrencyQuantityBefore = 3850,
        CourseRate = 0.895845M,
        AverageCourseRate = 0.8902154M,
        CurrencyCode = CurrencySeeds.GbpCurrency.Code,
        Type = DonationType.Deposit
    };

    public static readonly DonationEntity DonationGbpTwo = DonationGbpOne with
    {
        Id = 7,
        Time = new DateTime(2022, 8, 6, 10, 02, 1),
        Quantity = 700,
        CurrencyQuantityBefore = 5350,
        CourseRate = 0.856412M,
        AverageCourseRate = 0.8921542M,
        Type = DonationType.Levy
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DonationEntity>().HasData(
            DonationDeposit,
            DonationWithdraw,
            DonationLevy,
            DonationToCancel,
            DonationGbpOne,
            DonationGbpTwo);
    }
}