using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AccountSavedCoursesViewModel
{
    public string Title { get; set; } = "Account Saved Courses";
    public string ViewName { get; set; } = "courses";
    public AccountAsideInfoModel ProfileInfo { get; set; } = null!;

    //lägg in för kurserna
}
