using Infrastructure_Api.Entities;
using Infrastructure_Api.Models;

namespace Infrastructure_Api.Factories;

public class CourseFactory
{
    public static CourseEntity Create(CourseModel model)
    {
        if(model.Id == 0)
        {
            return new CourseEntity
            {
                Title = model.Title,
                ImageName = model.ImageName,
                Price = model.Price,
                DiscountPrice = model.DiscountPrice,
                Hours = model.Hours,
                IsBestseller = model.IsBestseller,
                LikesInNumbers = model.LikesInNumbers,
                LikesInPercent = model.LikesInPercent,
                Author = model.Author,
                CategoryId = model.CategoryId != 0 ? model.CategoryId : null,
            };
        } 
        else
        {
            return new CourseEntity
            {
                Id = model.Id,
                Title = model.Title,
                ImageName = model.ImageName,
                Price = model.Price,
                DiscountPrice = model.DiscountPrice,
                Hours = model.Hours,
                IsBestseller = model.IsBestseller,
                LikesInNumbers = model.LikesInNumbers,
                LikesInPercent = model.LikesInPercent,
                Author = model.Author,
                CategoryId = model.CategoryId != 0 ? model.CategoryId : null,
            };
        }
    }

    public static IEnumerable<CourseModel> Create(List<CourseEntity> entities)
    {
        List<CourseModel> courses = [];

        foreach (var entity in entities)
        {
            courses.Add(Create(entity));
        }

        return courses;
    }

    public static CourseModel Create(CourseEntity entity)
    {
        return new CourseModel
        {
            Id = entity.Id,
            Title = entity.Title,
            ImageName = entity.ImageName,
            Price = entity.Price,
            DiscountPrice = entity.DiscountPrice,
            Hours = entity.Hours,
            IsBestseller = entity.IsBestseller,
            LikesInNumbers = entity.LikesInNumbers,
            LikesInPercent = entity.LikesInPercent,
            Author = entity.Author,
            CategoryId = entity.CategoryId,
            Category = entity.Category != null ? entity.Category!.CategoryName : null
        };
    }
}
