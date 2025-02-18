﻿using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models.Customer;

public record BusinessCustomerDetailModel : CustomerDetailModel
{
    public required string TradeNameOfTheOwner { get; set; }
    public required string TradeAddress { get; set; }
    public required string ICO { get; set; }
    public required string Nationality { get; set; }

    public static BusinessCustomerDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Created = DateTime.Now,
        FirstName = string.Empty,
        LastName = string.Empty,
        Address = string.Empty,
        EvidenceType = EvidenceType.IdentificationCard,
        EvidenceNumber = string.Empty,
        TradeNameOfTheOwner = string.Empty,
        TradeAddress = string.Empty,
        ICO = string.Empty,
        Nationality = string.Empty
    };
}