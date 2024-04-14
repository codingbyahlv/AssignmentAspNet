using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Account Security";
    public string ViewName { get; set; } = "security";
    public AccountAsideInfoModel ProfileInfo { get; set; } = null!;

    //lägg in för security form
}
