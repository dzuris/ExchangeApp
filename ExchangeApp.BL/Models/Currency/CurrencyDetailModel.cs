﻿using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Currency;

public record CurrencyDetailModel : ModelBase
{
    public required string Code { get; set; }
    public required float Quantity { get; set; }
    public required string PhotoUrl { get; set; }
    public required float AverageCourseRate { get; set; }
    public float? BuyRate { get; set; }
    public float? SellRate { get; set; }
    public required CurrencyState Status { get; set; }
}