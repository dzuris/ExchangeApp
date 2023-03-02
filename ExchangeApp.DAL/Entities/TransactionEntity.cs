using AutoMapper;
using ExchangeApp.Common.Enums;
using ExchangeApp.DAL.Entities.Persons;
using ExchangeApp.DAL.Entities.Persons.Customers;

namespace ExchangeApp.DAL.Entities;

public record TransactionEntity : IEntity
{
    public required int Id { get; set; }
    public required DateTime Time { get; set; }
    public required decimal CourseRate { get; set; }
    public required decimal Quantity { get; set; }
    public required TransactionType TransactionType { get; set; }
    public bool IsCanceled { get; set; }

    public required Guid EmployeeId { get; set; }
    public EmployeeEntity? Employee { get; set; }

    public Guid? CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }

    public required string CurrencyCode { get; set; }
    public CurrencyEntity? Currency { get; set; }
}
