using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class CoursesDetailsViewModel
{
    public string Title { get; set; } = "Details";
    public string ViewName { get; set; } = "Details";
    public CourseModel Course { get; set; } = null!;
}
