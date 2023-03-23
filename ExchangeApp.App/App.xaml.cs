using System.Globalization;
using System.Resources;
using ExchangeApp.App.Resources.Texts;

namespace ExchangeApp.App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        // Set the culture to Slovak
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("sk-SK");

        MainPage = new AppShell();
    }
}
