using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity Eur = new()
    {
        Code = "EUR",
        Name = "Euro",
        State = "Európska menová únia",
        Symbol = "€",
        PhotoUrl = "eur.png",
        MiddleCourse = 1,
        AverageCourseRate = 1
    };

    public static readonly CurrencyEntity Czk = new()
    {
        Code = "CZK",
        Name = "Česká koruna",
        State = "Česko",
        Symbol = "kč",
        PhotoUrl = "czk.png",
        MiddleCourse = 1,
        AverageCourseRate = 1
    };

    public static readonly CurrencyEntity Usd = new()
    {
        Code = "USD",
        Name = "Americký dolár",
        State = "Spojené štáty americké",
        Symbol = "$",
        PhotoUrl = "usd.png",
        MiddleCourse = 1,
        AverageCourseRate = 1
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            Eur,
            Czk,
            Usd
        );
    }
}
