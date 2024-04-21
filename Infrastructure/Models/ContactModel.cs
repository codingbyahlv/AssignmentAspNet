using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactModel
{
    [Display(Name = "Full name", Prompt = "Enter your full name")]
    [Required(ErrorMessage = "You must enter a name")]
    [MinLength(2, ErrorMessage = "Invalid format")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = null!;


    [Display(Name = "E-mail", Prompt = "Enter your e-mail")]
    [Required(ErrorMessage = "You must enter an e-mail")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]{2,}$", ErrorMessage = "Invalid e-mail format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Service (optional)", Prompt = "Choose the service you are interested in")]
    [DataType(DataType.Text)]
    public string? Service { get; set; } = null!;

    [Display(Name = "Message", Prompt = "Enter your message here")]
    [Required(ErrorMessage = "You must enter a message")]
    [MinLength(2, ErrorMessage = "Invalid format")]
    [DataType(DataType.Text)]
    public string Message { get; set; } = null!;
}
