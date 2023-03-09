using ExchangeApp.App.Views.Courses;
using ExchangeApp.App.Views.Customers;
using ExchangeApp.App.Views.Donation;
using ExchangeApp.App.Views.OperationsList;
using ExchangeApp.App.Views.Settings;
using ExchangeApp.App.Views.Transaction;

namespace ExchangeApp.App;

public partial class AppShell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(TransactionCreatePage), typeof(TransactionCreatePage));
		Routing.RegisterRoute(nameof(TransactionDetailPage), typeof(TransactionDetailPage));

		Routing.RegisterRoute(nameof(NewCustomerIndividualPage), typeof(NewCustomerIndividualPage));
		Routing.RegisterRoute(nameof(NewCustomerBusinessPage), typeof(NewCustomerBusinessPage));
		Routing.RegisterRoute(nameof(NewCustomerMinorPage), typeof(NewCustomerMinorPage));
		
		Routing.RegisterRoute(nameof(DonationCreatePage), typeof(DonationCreatePage));
		Routing.RegisterRoute(nameof(DonationDetailPage), typeof(DonationDetailPage));

		Routing.RegisterRoute(nameof(OperationsListPage), typeof(OperationsListPage));

		Routing.RegisterRoute(nameof(CourseDetailPage), typeof(CourseDetailPage));

		Routing.RegisterRoute(nameof(SettingsCoursesManagerPage), typeof(SettingsCoursesManagerPage));
	}
}
