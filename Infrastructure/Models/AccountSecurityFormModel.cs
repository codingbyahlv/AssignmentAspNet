using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountSecurityFormModel
{
    [Display(Name = "Password", Prompt = "Enter current password")]
    [Required(ErrorMessage = "You must enter a password")]
    [RegularExpression("^(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Invalid password format")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;


    [Display(Name = "New password", Prompt = "Enter new password")]
    [Required(ErrorMessage = "You must enter a password")]
    [RegularExpression("^(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Invalid password format")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm new password", Prompt = "Re-enter password")]
    [Required(ErrorMessage = "You must confirm password")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords don´t match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
