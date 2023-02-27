using ExchangeApp.App.Views;

namespace ExchangeApp.App;

public partial class AppShell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NewCustomerIndividualPage), typeof(NewCustomerIndividualPage));
		Routing.RegisterRoute(nameof(NewCustomerBusinessPage), typeof(NewCustomerBusinessPage));
		Routing.RegisterRoute(nameof(NewCustomerMinorPage), typeof(NewCustomerMinorPage));

		Routing.RegisterRoute(nameof(DonationDetailPage), typeof(DonationDetailPage));
		Routing.RegisterRoute(nameof(TransactionDetailPage), typeof(TransactionDetailPage));
	}
}
