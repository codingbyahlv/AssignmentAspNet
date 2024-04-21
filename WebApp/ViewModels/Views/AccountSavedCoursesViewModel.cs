using Infrastructure.Models;
using WebApp.ViewModels.Sections;

namespace WebApp.ViewModels.Views;

public class AccountSavedCoursesViewModel
{
    public string Title { get; set; } = "Account | Saved Courses";
    public string ViewName { get; set; } = "courses";
    public AccountAsideInfoModel ProfileInfo { get; set; } = null!;
    public CoursesSectionViewModel CoursesSection { get; set; } = new CoursesSectionViewModel();
}
