using ExchangeApp.BL.Models.Customer;

namespace ExchangeApp.App.Controls;

public class CustomerDetailDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate IndividualCustomerTemplate { get; set; } = null!;
    public DataTemplate BusinessCustomerTemplate { get; set; } = null!;
    public DataTemplate MinorCustomerTemplate { get; set; } = null!;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            IndividualCustomerDetailModel => IndividualCustomerTemplate,
            BusinessCustomerDetailModel => BusinessCustomerTemplate,
            MinorCustomerDetailModel => MinorCustomerTemplate,
            _ => throw new ArgumentException($"Unsupported item type: {item.GetType().Name}")
        };
    }
}