using Infrastructure.Helpers;

namespace Infrastructure.Models;

public class AccountSecurityDeleteForm
{
    [CheckBoxRequired(ErrorMessage = "Tick the checkbox if you want to delete your account")]
    public bool IsDelete { get; set; } = false;
}
