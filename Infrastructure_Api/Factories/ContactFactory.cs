using Infrastructure_Api.Entities;
using Infrastructure_Api.Models;

namespace Infrastructure_Api.Factories;

public class ContactFactory
{
    public static ContactEntity Create(ContactModel model)
    {
        return new ContactEntity
        {
            Name = model.Name,
            Email = model.Email,
            Service = model.Service,
            Message = model.Message
        };
    }
}
