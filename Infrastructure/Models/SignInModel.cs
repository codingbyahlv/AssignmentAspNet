using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SignInModel
{
    [Display(Name = "E-mail", Prompt = "Enter your e-mail")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter an email")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must enter a password")]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
