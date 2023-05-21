using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Seeds;

public static class CurrencySeeds
{
    public static readonly CurrencyEntity Eur = new()
    {
        Code = "EUR".ToUpper(),
        PhotoUrl = "eur_flag.png",
        Status = CurrencyStatus.Own,
        AverageCourseRate = 1
    };

    public static readonly CurrencyEntity Czk = new()
    {
        Code = "CZK".ToUpper(),
        PhotoUrl = "czk_flag.png"
    };

    public static readonly CurrencyEntity Usd = new()
    {
        Code = "USD".ToUpper(),
        PhotoUrl = "usd_flag.png"
    };

    public static readonly CurrencyEntity Pln = new()
    {
        Code = "PLN".ToUpper(),
        PhotoUrl = "pln_flag.png"
    };

    public static readonly CurrencyEntity Jpy = new()
    {
        Code = "JPY".ToUpper(),
        PhotoUrl = "jpn_flag.png"
    };

    public static readonly CurrencyEntity Gbp = new()
    {
        Code = "GBP".ToUpper(),
        PhotoUrl = "gbp_flag.png"
    };

    public static readonly CurrencyEntity Chf = new()
    {
        Code = "CHF".ToUpper(),
        PhotoUrl = "chf_flag.png"
    };

    public static readonly CurrencyEntity Huf = new()
    {
        Code = "HUF".ToUpper(),
        PhotoUrl = "huf_flag.png"
    };

    public static readonly CurrencyEntity Cad = new()
    {
        Code = "CAD".ToUpper(),
        PhotoUrl = "cad_flag.png"
    };

    public static readonly CurrencyEntity Nok = new()
    {
        Code = "NOK".ToUpper(),
        PhotoUrl = "nok_flag.png"
    };

    public static readonly CurrencyEntity Bgn = new()
    {
        Code = "BGN".ToUpper(),
        PhotoUrl = "bgn_flag.png"
    };

    public static readonly CurrencyEntity Rub = new()
    {
        Code = "RUB".ToUpper(),
        PhotoUrl = "rub_flag.png"
    };

    public static readonly CurrencyEntity Dkk = new()
    {
        Code = "DKK".ToUpper(),
        PhotoUrl = "dkk_flag.png"
    };

    public static readonly CurrencyEntity Ron = new()
    {
        Code = "RON".ToUpper(),
        PhotoUrl = "ron_flag.png"
    };

    public static readonly CurrencyEntity Sek = new()
    {
        Code = "SEK".ToUpper(),
        PhotoUrl = "sek_flag.png"
    };

    public static readonly CurrencyEntity Try = new()
    {
        Code = "TRY".ToUpper(),
        PhotoUrl = "try_flag.png"
    };

    public static readonly CurrencyEntity Aud = new()
    {
        Code = "AUD".ToUpper(),
        PhotoUrl = "aud_flag.png"
    };

    public static readonly CurrencyEntity Brl = new()
    {
        Code = "BRL".ToUpper(),
        PhotoUrl = "brl_flag.png"
    };

    public static readonly CurrencyEntity Cny = new()
    {
        Code = "CNY".ToUpper(),
        PhotoUrl = "cny_flag.png"
    };

    public static readonly CurrencyEntity Hkd = new()
    {
        Code = "HKD".ToUpper(),
        PhotoUrl = "hkd_flag.png"
    };

    public static readonly CurrencyEntity Idr = new()
    {
        Code = "IDR".ToUpper(),
        PhotoUrl = "idr_flag.png"
    };

    public static readonly CurrencyEntity Ils = new()
    {
        Code = "ILS".ToUpper(),
        PhotoUrl = "ils_flag.png"
    };

    public static readonly CurrencyEntity Inr = new()
    {
        Code = "INR".ToUpper(),
        PhotoUrl = "inr_flag.png"
    };

    public static readonly CurrencyEntity Krw = new()
    {
        Code = "KRW".ToUpper(),
        PhotoUrl = "krw_flag.png"
    };

    public static readonly CurrencyEntity Mxn = new()
    {
        Code = "MXN".ToUpper(),
        PhotoUrl = "mxn_flag.png"
    };

    public static readonly CurrencyEntity Myr = new()
    {
        Code = "MYR".ToUpper(),
        PhotoUrl = "myr_flag.png"
    };

    public static readonly CurrencyEntity Nzd = new()
    {
        Code = "NZD".ToUpper(),
        PhotoUrl = "nzd_flag.png"
    };

    public static readonly CurrencyEntity Php = new()
    {
        Code = "PHP".ToUpper(),
        PhotoUrl = "php_flag.png"
    };

    public static readonly CurrencyEntity Sgd = new()
    {
        Code = "SGD".ToUpper(),
        PhotoUrl = "sgd_flag.png"
    };

    public static readonly CurrencyEntity Thb = new()
    {
        Code = "THB".ToUpper(),
        PhotoUrl = "thb_flag.png"
    };

    public static readonly CurrencyEntity Zar = new()
    {
        Code = "ZAR".ToUpper(),
        PhotoUrl = "zar_flag.png"
    };

    public static readonly CurrencyEntity Uah = new()
    {
        Code = "UAH".ToUpper(),
        PhotoUrl = "uah_flag.png"
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
            Zar,
            Uah
        );
    }
}
