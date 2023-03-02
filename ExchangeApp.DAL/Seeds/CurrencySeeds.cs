using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity Eur = new()
    {
        Code = "EUR".ToUpper(),
        PhotoUrl = "eur.png",
        Status = CurrencyState.Own
    };

    public static readonly CurrencyEntity Czk = new()
    {
        Code = "CZK".ToUpper(),
        PhotoUrl = "czk.png"
    };

    public static readonly CurrencyEntity Usd = new()
    {
        Code = "USD".ToUpper(),
        PhotoUrl = "usd.png"
    };

    public static readonly CurrencyEntity Pln = new()
    {
        Code = "PLN".ToUpper(),
        PhotoUrl = "pln.png"
    };

    public static readonly CurrencyEntity Jpy = new()
    {
        Code = "JPY".ToUpper(),
        PhotoUrl = "jpn.png"
    };

    public static readonly CurrencyEntity Gbp = new()
    {
        Code = "GBP".ToUpper(),
        PhotoUrl = "gbp.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            Eur,
            Czk,
            Usd,
            Pln,
            Jpy,
            Gbp
        );
    }
}
