using System.Globalization;

namespace ExchangeApp.App;

public partial class App
{
	public App()
    {
		InitializeComponent();

        // Set the culture to Slovak constantly
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("sk-SK");

        MainPage = new AppShell();
    }
}
