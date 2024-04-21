using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Account | Security";
    public string ViewName { get; set; } = "security";
    public bool IsExternalAccount { get; set; }
    public AccountAsideInfoModel ProfileInfo { get; set; } = null!;
    public AccountSecurityFormModel SecurityForm { get; set; } = null!;
    public AccountSecurityDeleteForm DeleteForm { get; set; } = null!;
}
