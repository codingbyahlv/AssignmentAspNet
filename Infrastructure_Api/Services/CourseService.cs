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
        bool result = await _context.Courses.AnyAsync(x => x.Title == title);
        return result;
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

    public async Task<IEnumerable<CourseEntity>> ReadAllCourses()
    {
        try
        {
            IEnumerable<CourseEntity> courses = await _context.Courses.ToListAsync();
            return courses;
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

    public async Task<bool> DeleteCourse(string title)
    {
        try
        {
            CourseEntity? course = await _context.Courses.FirstOrDefaultAsync(y => y.Title == title);
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
