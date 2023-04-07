using ExchangeApp.App.ViewModels.Courses;
using System.Resources;
using ExchangeApp.App.Resources.Texts;

namespace ExchangeApp.App.Views.Courses;

public partial class CourseDetailPage
{
	public CourseDetailPage(CourseDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}

    private void SellRateEntry_OnFocused(object? sender, FocusEventArgs e)
    {
        SellRateEntry.CursorPosition = 0;
        SellRateEntry.SelectionLength = SellRateEntry.Text.Length;
    }

    private void BuyRateEntry_OnFocused(object? sender, FocusEventArgs e)
    {
        BuyRateEntry.CursorPosition = 0;
        BuyRateEntry.SelectionLength = SellRateEntry.Text.Length;
    }
}