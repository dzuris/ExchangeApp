using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity Eur = new()
    {
        Code = "EUR".ToUpper(),
        State = "Európska menová únia",
        PhotoUrl = "eur.png"
    };

    public static readonly CurrencyEntity Czk = new()
    {
        Code = "CZK".ToUpper(),
        State = "Česko",
        PhotoUrl = "czk.png"
    };

    public static readonly CurrencyEntity Usd = new()
    {
        Code = "USD".ToUpper(),
        State = "Spojené štáty americké",
        PhotoUrl = "usd.png"
    };

    public static readonly CurrencyEntity Pln = new()
    {
        Code = "PLN".ToUpper(),
        State = "Poľsko",
        PhotoUrl = "pln.png"
    };

    public static readonly CurrencyEntity Jpy = new()
    {
        Code = "JPY".ToUpper(),
        State = "Japonsko",
        PhotoUrl = "jpn.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            Eur,
            Czk,
            Usd,
            Pln,
            Jpy
        );
    }
}
