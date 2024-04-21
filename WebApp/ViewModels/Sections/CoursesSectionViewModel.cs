using Infrastructure.Models;

namespace WebApp.ViewModels.Sections;

public class CoursesSectionViewModel
{
    public IEnumerable<CourseModel> CourseList { get; set; } = [];

    public IEnumerable<CategoryModel>? Categories { get; set; } = [];

    public PaginationModel? Pagination { get; set; }

    public List<int> SavedCoursesList { get; set; } = [];
}
