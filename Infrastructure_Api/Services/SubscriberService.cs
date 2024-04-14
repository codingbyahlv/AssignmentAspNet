using Infrastructure_Api.Context;
using Infrastructure_Api.Entities;
using Infrastructure_Api.Factories;
using Infrastructure_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure_Api.Services;

public class SubscriberService(ApiDbContext context)
{
    private readonly ApiDbContext _context = context;

    public async Task<bool> IsEmailExist(string email)
    {
        bool result = await _context.Subscribers.AnyAsync(x => x.Email == email);
        return result;
    }

    public async Task<bool> CreateSubscriber(SubscriberModel model)
    {
        try
        {
            SubscriberEntity subscriberEntity = SubscriberFactory.Create(model);
            _context.Subscribers.Add(subscriberEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

    public async Task<bool> DeleteSubscriber(string email)
    {
        try
        {
            SubscriberEntity? subscriber = await _context.Subscribers.FirstOrDefaultAsync(y => y.Email == email);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

}
