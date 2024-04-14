using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SignUpModel
{
    [Display(Name = "First name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "You must enter a first name")]
    [MinLength(2, ErrorMessage = "Invalid first name format")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last name", Prompt = "Enter your last name")]
    [Required(ErrorMessage = "You must enter a last name")]
    [MinLength(2, ErrorMessage = "Invalid last name format")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = null!;


    [Display(Name = "E-mail", Prompt = "Enter your e-mail")]
    [Required(ErrorMessage = "You must enter an e-mail")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$", ErrorMessage = "Invalid e-mail format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password")]
    [Required(ErrorMessage = "You must enter a password")]
    [RegularExpression("^(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Invalid password format")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Re-enter password")]
    [Required(ErrorMessage = "You must confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords don´t match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;


    [CheckBoxRequired(ErrorMessage = "You have to accept Terms and Conditions")]
    public bool TermsAndConditions { get; set; } = false;
}
