using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration, AppDbContext context, UserManager<UserEntity> userManager)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;
    private readonly AppDbContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<CourseResultModel> GetCoursesAsync(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            HttpResponseMessage response = await _http.GetAsync($"https://localhost:7283/api/courses?key={_configuration["ApiKey"]}&category={Uri.EscapeDataString(category)}&searchQuery={Uri.EscapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                CourseResultModel? result = JsonConvert.DeserializeObject<CourseResultModel>(await response.Content.ReadAsStringAsync());
                if(result != null && result.Succeeded)
                {
                    return result;
                }
            }
            return null!;
        }
        catch { }
        return null!;
    }

    public async Task<CourseModel> GetOneCourseAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _http.GetAsync($"https://localhost:7283/api/courses/{id}/?key={_configuration["ApiKey"]}");
            CourseModel course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync())!;
            if (course != null)
            {
                return course;
            }
        }
        catch { }
        return null!;
    }

    public async Task <List<CourseModel>> GetSavedCoursesAsync(List<int> idList)
    {
        try
        {
            List<CourseModel> savedCourses = new List<CourseModel>();
            foreach (int id in idList)
            {
                CourseModel course = await GetOneCourseAsync(id);
                if (course != null)
                {
                    savedCourses.Add(course);
                }
            }
            return savedCourses;
        }
        catch { }
        return null!;
    }

    public async Task<bool> SaveCourseToUserAsync(int courseId, string userId) {
        try
        {
            UserEntity? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                List<int> currentList = user.SavedCoursesIdList;
                currentList.Add(courseId);
                user.SavedCoursesIdList = currentList;
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch { }
        return false;
    }

    public async Task<bool> RemoveCourseFromUserAsync(int courseId, string userId)
    {
        try
        {
            UserEntity? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                List<int> currentList = user.SavedCoursesIdList;
                currentList.Remove(courseId);
                user.SavedCoursesIdList = currentList;
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch { }
        return false;
    }

    public async Task<bool> RemoveAllCoursesFromUserAsync(string userId)
    {
        try
        {
            UserEntity? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.SavedCoursesIdList = new List<int>();
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch { }
        return false;
    }

}





