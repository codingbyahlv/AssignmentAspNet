using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";
    public string ViewName { get; set; } = "details";
    public bool IsExternalAccount { get; set; }
    public AccountAsideInfoModel ProfileInfo { get; set;} = null!;
    public BasicInfoModel BasicInfoForm { get; set; } = null!;
    public AddressInfoModel AddressInfoForm { get; set; } = null!;
}
