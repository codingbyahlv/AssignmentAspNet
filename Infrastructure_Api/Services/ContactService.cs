using Infrastructure_Api.Context;
using Infrastructure_Api.Entities;
using Infrastructure_Api.Factories;
using Infrastructure_Api.Models;
using System.Diagnostics;

namespace Infrastructure_Api.Services;

public class ContactService(ApiDbContext context)
{
    private readonly ApiDbContext _context = context;

    public async Task<bool> SendForm(ContactModel model)
    {
        try
        {
            ContactEntity contactEntity = ContactFactory.Create(model);
            _context.Contacts.Add(contactEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }
}
