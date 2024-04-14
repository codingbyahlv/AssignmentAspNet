using WebApp.ViewModels.Sections;

namespace WebApp.ViewModels.Views;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "Courses";
    public string ViewName { get; set; } = "courses";

    //LÄGG TILL! breadcrumb-section
    public CoursesSectionViewModel CoursesSection { get; set; } = new CoursesSectionViewModel();
}
