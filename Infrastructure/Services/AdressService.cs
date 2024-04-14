using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressManager(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<AddressEntity> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> GetAddressAsync(int? addressId)
    {
        try
        {
            AddressEntity? addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == addressId);
            if (addressEntity != null)
            {
                return addressEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> GetOneAddressAsync(AddressEntity entity)
    {
        try
        {
            AddressEntity? addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressLine1 == entity.AddressLine1 && x.AddressLine2 == entity.AddressLine2 && x.PostalCode == entity.PostalCode && x.City == entity.City);
          
            if(addressEntity != null)
            {
                return addressEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return null!;
    }
}
