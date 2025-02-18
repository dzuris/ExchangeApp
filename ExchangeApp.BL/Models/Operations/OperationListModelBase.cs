﻿namespace ExchangeApp.BL.Models.Operations;

public record OperationListModelBase : ModelBase
{
    public int Id { get; set; }
    public required DateTime Created { get; set; }
    public required decimal Quantity { get; set; }
    public required string CurrencyCode { get; set; }
    public required decimal CourseRate { get; set; }
    public required bool IsCanceled { get; set; }

    public decimal ExchangeRateValue => CourseRate != 0 ? Math.Round(Quantity / CourseRate, 2) : 0;
}