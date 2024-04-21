using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class ContactViewModel
{
    public string Title { get; set; } = "Contact";
    public ContactModel ContactForm { get; set; } = new ContactModel();
}
