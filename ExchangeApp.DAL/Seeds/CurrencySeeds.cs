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

    public static readonly CurrencyEntity Chf = new()
    {
        Code = "CHF".ToUpper(),
        PhotoUrl = "chf.png"
    };

    public static readonly CurrencyEntity Huf = new()
    {
        Code = "HUF".ToUpper(),
        PhotoUrl = "huf.png"
    };

    public static readonly CurrencyEntity Cad = new()
    {
        Code = "CAD".ToUpper(),
        PhotoUrl = "cad.png"
    };

    public static readonly CurrencyEntity Nok = new()
    {
        Code = "NOK".ToUpper(),
        PhotoUrl = "nok.png"
    };

    public static readonly CurrencyEntity Bgn = new()
    {
        Code = "BGN".ToUpper(),
        PhotoUrl = "bgn.png"
    };

    public static readonly CurrencyEntity Rub = new()
    {
        Code = "RUB".ToUpper(),
        PhotoUrl = "rub.png"
    };

    public static readonly CurrencyEntity Dkk = new()
    {
        Code = "DKK".ToUpper(),
        PhotoUrl = "dkk.png"
    };

    public static readonly CurrencyEntity Ron = new()
    {
        Code = "RON".ToUpper(),
        PhotoUrl = "ron.png"
    };

    public static readonly CurrencyEntity Sek = new()
    {
        Code = "SEK".ToUpper(),
        PhotoUrl = "sek.png"
    };

    public static readonly CurrencyEntity Try = new()
    {
        Code = "TRY".ToUpper(),
        PhotoUrl = "try.png"
    };

    public static readonly CurrencyEntity Aud = new()
    {
        Code = "AUD".ToUpper(),
        PhotoUrl = "aud.png"
    };

    public static readonly CurrencyEntity Brl = new()
    {
        Code = "BRL".ToUpper(),
        PhotoUrl = "brl.png"
    };

    public static readonly CurrencyEntity Cny = new()
    {
        Code = "CNY".ToUpper(),
        PhotoUrl = "cny.png"
    };

    public static readonly CurrencyEntity Hkd = new()
    {
        Code = "HKD".ToUpper(),
        PhotoUrl = "hkd.png"
    };

    public static readonly CurrencyEntity Idr = new()
    {
        Code = "IDR".ToUpper(),
        PhotoUrl = "idr.png"
    };

    public static readonly CurrencyEntity Ils = new()
    {
        Code = "ILS".ToUpper(),
        PhotoUrl = "ils.png"
    };

    public static readonly CurrencyEntity Inr = new()
    {
        Code = "INR".ToUpper(),
        PhotoUrl = "inr.png"
    };

    public static readonly CurrencyEntity Krw = new()
    {
        Code = "KRW".ToUpper(),
        PhotoUrl = "krw.png"
    };

    public static readonly CurrencyEntity Mxn = new()
    {
        Code = "MXN".ToUpper(),
        PhotoUrl = "mxn.png"
    };

    public static readonly CurrencyEntity Myr = new()
    {
        Code = "MYR".ToUpper(),
        PhotoUrl = "myr.png"
    };

    public static readonly CurrencyEntity Nzd = new()
    {
        Code = "NZD".ToUpper(),
        PhotoUrl = "nzd.png"
    };

    public static readonly CurrencyEntity Php = new()
    {
        Code = "PHP".ToUpper(),
        PhotoUrl = "php.png"
    };

    public static readonly CurrencyEntity Sgd = new()
    {
        Code = "SGD".ToUpper(),
        PhotoUrl = "sgd.png"
    };

    public static readonly CurrencyEntity Thb = new()
    {
        Code = "THB".ToUpper(),
        PhotoUrl = "thb.png"
    };

    public static readonly CurrencyEntity Zar = new()
    {
        Code = "ZAR".ToUpper(),
        PhotoUrl = "zar.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().HasData(
            Eur,
            Czk,
            Usd,
            Pln,
            Jpy,
            Gbp,
            Chf,
            Huf,
            Cad,
            Nok,
            Bgn,
            Rub,
            Dkk,
            Ron,
            Sek,
            Try,
            Aud,
            Brl,
            Cny,
            Hkd,
            Idr,
            Ils,
            Inr,
            Krw,
            Mxn,
            Myr,
            Nzd,
            Php,
            Sgd,
            Thb,
            Zar
        );
    }
}
