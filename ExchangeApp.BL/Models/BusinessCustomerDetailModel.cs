using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models;

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
        EvidenceType = EvidenceType.None,
        EvidenceNumber = string.Empty,
        TransactionId = 0,
        TradeNameOfTheOwner = string.Empty,
        TradeAddress = string.Empty,
        ICO = string.Empty,
        Nationality = string.Empty
    };
}