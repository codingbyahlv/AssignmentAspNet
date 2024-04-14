using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public class UserFactory
{
    public static UserEntity Create(SignUpModel model)
    {
        //ev fyll på med fler fält
        return new UserEntity
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email
        };
    }

    public static BasicInfoModel Create(UserEntity entity)
    {
        return new BasicInfoModel
        {
            UserId = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            Biography = entity.Biography,
        };
    }

    public static AddressEntity Create(AddressInfoModel model)
    {
        return new AddressEntity
        {
            Id = model.Id,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            PostalCode = model.PostalCode,
            City = model.City,
        };
    }

    public static AddressInfoModel Create(AddressEntity entity)
    {
        return new AddressInfoModel
        {
            Id = entity.Id,
            AddressLine1 = entity.AddressLine1,
            AddressLine2 = entity.AddressLine2,
            PostalCode = entity.PostalCode,
            City = entity.City,
        };
    }



    public static AccountAsideInfoModel Create(string firstName, string lastName, string email)
    {
        return new AccountAsideInfoModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
        };
    }
}
