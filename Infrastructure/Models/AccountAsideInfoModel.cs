namespace Infrastructure.Models;

public class AccountAsideInfoModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ImageUrl { get; set; } = "profile-img.svg";
}
