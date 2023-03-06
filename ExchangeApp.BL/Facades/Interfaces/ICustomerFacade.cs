using ExchangeApp.BL.Models.Customer;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ICustomerFacade : IFacade
{
    Task InsertAsync(IndividualCustomerDetailModel model);
    Task InsertAsync(BusinessCustomerDetailModel model);
    Task InsertAsync(MinorCustomerDetailModel model);
}