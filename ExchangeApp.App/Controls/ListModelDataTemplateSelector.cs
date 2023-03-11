using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.Transaction;

namespace ExchangeApp.App.Controls;

public class ListModelDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate DonationTemplate { get; set; } = null!;
    public DataTemplate TransactionTemplate { get; set; } = null!;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            DonationListModel => DonationTemplate,
            TransactionListModel => TransactionTemplate,
            _ => throw new ArgumentException($"Unsupported item type: {item.GetType().Name}")
        };
    }
}