using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity Eur = new()
    {
        Code = "EUR",
        State = "Európska menová únia",
        PhotoUrl = "eur.png"
    };

    public static readonly CurrencyEntity Czk = new()
    {
        Code = "CZK",
        State = "Česko",
        PhotoUrl = "czk.png"
    };

    public static readonly CurrencyEntity Usd = new()
    {
        Code = "USD",
        State = "Spojené štáty americké",
        PhotoUrl = "usd.png"
    };

    public static readonly CurrencyEntity Pln = new()
    {
        Code = "PLN",
        State = "Poľsko",
        PhotoUrl = "pln.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            Eur,
            Czk,
            Usd,
            Pln
        );
    }
}
