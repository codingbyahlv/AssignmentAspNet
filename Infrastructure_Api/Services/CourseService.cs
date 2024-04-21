using Infrastructure_Api.Context;
using Infrastructure_Api.Entities;
using Infrastructure_Api.Factories;
using Infrastructure_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure_Api.Services;

public class CourseService(ApiDbContext context)
{
    private readonly ApiDbContext _context = context;

    public async Task<bool> IsCourseExist(string title)
    {
        try
        {
            bool result = await _context.Courses.AnyAsync(x => x.Title == title);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

    public async Task<bool> IsCourseExist(int id)
    {
        try
        {
            bool result = await _context.Courses.AnyAsync(x => x.Id == id);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

    public async Task<bool> CreateCourse(CourseModel model)
    {
        try
        {
            CourseEntity courseEntity = CourseFactory.Create(model);
            _context.Courses.Add(courseEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

    public async Task <CourseResultModel> ReadAllCourses(string category, string searchQuery, int pageNumber, int pageSize)
    {
        try
        {
            IQueryable<CourseEntity> query = _context.Courses.Include(i => i.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "all")
                query = query.Where(x => x.Category!.CategoryName == category);


            if(!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(x => x.Title.Contains(searchQuery) || (x.Author != null && x.Author.Contains(searchQuery)));

            query = query.OrderBy(i => i.Id);
            List<CourseEntity> courses = await query.ToListAsync();

            int totalItems = courses.Count;
            int totalPages = (int)Math.Ceiling(courses.Count / (double)pageSize);

            CourseResultModel response = new()
            {
                Succeeded = true,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Courses = CourseFactory.Create(courses.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()),
            };

            return response;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return null!;
    }

    public async Task<CourseEntity> ReadOneCourse(int id)
    {
        try
        {
            CourseEntity? course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
                return course;
            else return null!;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return null!;
    }

    public async Task<bool> UpdateCourse(CourseModel model)
    {
        try
        {
            CourseEntity? currentCourse = await _context.Courses.FirstOrDefaultAsync(y => y.Id == model.Id);
            if(currentCourse!= null)
            {
                CourseEntity courseEntity = CourseFactory.Create(model);
                _context.Entry(currentCourse).CurrentValues.SetValues(courseEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

    public async Task<bool> DeleteCourse(CourseModel model)
    {
        try
        {
            CourseEntity? course = await _context.Courses.FirstOrDefaultAsync(y => y.Id == model.Id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        catch (Exception ex) { Debug.WriteLine("Error : " + ex.Message); }
        return false;
    }

}
