using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class BasicInfoModel
{
    public string UserId { get; set; } = null!;

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


    [Display(Name = "Phone", Prompt = "Enter your phone number (optional)")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }


    [Display(Name = "Bio (optional)", Prompt = "Add s short bio (optional) ")]
    [DataType(DataType.MultilineText)]
    public string? Biography { get; set; }
}
