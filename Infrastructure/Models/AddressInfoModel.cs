using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AddressInfoModel
{
    public int Id { get; set; }

    [Display(Name = "Address line 1", Prompt = "Enter your address line")]
    [Required(ErrorMessage = "You must enter an address")]
    [MinLength(2, ErrorMessage = "Invalid first name format")]
    [DataType(DataType.Text)]
    public string AddressLine1 { get; set; } = null!;

    [Display(Name = "Address line 2 (optional)", Prompt = "Enter your second address line")]
    [DataType(DataType.Text)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    [Required(ErrorMessage = "You must enter a postal code")]
    [MinLength(2, ErrorMessage = "Invalid postal code format")]
    [DataType(DataType.Text)]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city")]
    [Required(ErrorMessage = "You must enter a city")]
    [MinLength(2, ErrorMessage = "Invalid city format")]
    [DataType(DataType.Text)]
    public string City { get; set; } = null!;
}
