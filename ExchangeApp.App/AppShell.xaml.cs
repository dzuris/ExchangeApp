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
		
		// Transactions
		Routing.RegisterRoute(nameof(TransactionCreatePage), typeof(TransactionCreatePage));
		Routing.RegisterRoute(nameof(TransactionDetailPage), typeof(TransactionDetailPage));

		// Customers
		Routing.RegisterRoute(nameof(CustomerDetailPage), typeof(CustomerDetailPage));
		Routing.RegisterRoute(nameof(NewCustomerIndividualPage), typeof(NewCustomerIndividualPage));
		Routing.RegisterRoute(nameof(NewCustomerBusinessPage), typeof(NewCustomerBusinessPage));
		Routing.RegisterRoute(nameof(NewCustomerMinorPage), typeof(NewCustomerMinorPage));
		
		// Donations
		Routing.RegisterRoute(nameof(DonationCreatePage), typeof(DonationCreatePage));
		Routing.RegisterRoute(nameof(DonationDetailPage), typeof(DonationDetailPage));

		// Operations
		Routing.RegisterRoute(nameof(OperationsListPage), typeof(OperationsListPage));

		// Courses
		Routing.RegisterRoute(nameof(CourseDetailPage), typeof(CourseDetailPage));

		// Settings
		Routing.RegisterRoute(nameof(SettingsCoursesManagerPage), typeof(SettingsCoursesManagerPage));
		Routing.RegisterRoute(nameof(SettingsGeneralPage), typeof(SettingsGeneralPage));
		Routing.RegisterRoute(nameof(SettingsInfoBranchPage), typeof(SettingsInfoBranchPage));
		Routing.RegisterRoute(nameof(SettingsInfoCompanyPage), typeof(SettingsInfoCompanyPage));
		Routing.RegisterRoute(nameof(SettingsLicencePage), typeof(SettingsLicencePage));
	}
}
