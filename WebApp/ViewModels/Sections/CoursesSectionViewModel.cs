using Infrastructure.Models;

namespace WebApp.ViewModels.Sections;

public class CoursesSectionViewModel
{
    public IEnumerable<CourseCardModel> CourseList { get; set; } = [];
}
