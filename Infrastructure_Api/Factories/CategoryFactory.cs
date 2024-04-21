using Infrastructure_Api.Entities;
using Infrastructure_Api.Models;

namespace Infrastructure_Api.Factories;

public class CategoryFactory
{
    public static CategoryModel Create(CategoryEntity categoryEntity)
    {
        return new CategoryModel
        {
            Id = categoryEntity.Id,
            CategoryName = categoryEntity.CategoryName,
        };
    }

    public static IEnumerable<CategoryModel> Create(List<CategoryEntity> entities)
    {
        List<CategoryModel> categories = [];

        foreach (CategoryEntity entity in entities)
        {
            categories.Add(Create(entity));
        }

        return categories;
    }
}
