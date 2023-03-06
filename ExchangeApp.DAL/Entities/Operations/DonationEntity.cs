using ExchangeApp.Common.Enums;

namespace ExchangeApp.DAL.Entities.Operations;

public record DonationEntity : IEntity
{
    public required int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal CourseRate { get; set; }
    public required decimal Quantity { get; set; }
    public required DonationType Type { get; set; }
    public required string Note { get; set; }
    public bool IsCanceled { get; set; } = false;

    public required string CurrencyCode { get; set; }
    public CurrencyEntity? Currency { get; set; }
}
