namespace Infrastructure_Api.Models;

public class ContactModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Service { get; set; }
    public string Message { get; set; } = null!;
}
