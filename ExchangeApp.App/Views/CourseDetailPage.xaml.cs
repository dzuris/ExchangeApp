using ExchangeApp.App.ViewModels.Courses;

namespace ExchangeApp.App.Views;

public partial class CourseDetailPage
{
	public CourseDetailPage(CourseDetailViewModel viewModel)
	    : base(viewModel)
	{
		InitializeComponent();
	}
}