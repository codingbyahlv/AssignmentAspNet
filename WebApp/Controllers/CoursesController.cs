using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(CategoryService categoryService, CourseService courseService, UserManager<UserEntity> userManager) : Controller
{
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;
    private readonly UserManager<UserEntity> _userManager = userManager;

    #region CourseIndex
    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Index(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 9)
    {
        CourseResultModel courseResult = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);  
        CoursesIndexViewModel viewModel = new();
        viewModel.CoursesSection.Categories = await _categoryService.GetCategoriesAsync();
        viewModel.CoursesSection.CourseList = courseResult.Courses!;
        viewModel.CoursesSection.Pagination = new PaginationModel 
        { 
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = courseResult.TotalPages,
            TotalItems = courseResult.TotalItems
        };

        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            if (user.SavedCoursesIdList.Count > 0)
            {
                viewModel.CoursesSection.SavedCoursesList = user.SavedCoursesIdList;
            }
        }

        return View(viewModel);
    }
    #endregion

    #region CourseDetails
    [HttpGet("{id}")]
    [Route("/courses/details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        CoursesDetailsViewModel viewModel = new();
        viewModel.Course = await _courseService.GetOneCourseAsync(id);
        return View(viewModel);
    }
    #endregion

    #region JoinCourse
    public async Task<IActionResult> JoinCourse(int courseId)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if(user != null)
        {
            bool result = await _courseService.SaveCourseToUserAsync(courseId, user.Id);
            if (result)
            {
                return RedirectToAction("SavedCourses", "Account");
            }
        }
        return BadRequest();
    }
    #endregion

    #region RemoveCourse
    public async Task<IActionResult> RemoveCourse(int courseId)
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            bool result = await _courseService.RemoveCourseFromUserAsync(courseId, user.Id);
            if (result)
            {
                return RedirectToAction("SavedCourses", "Account");
            }
        }
        return BadRequest();
    }
    #endregion

    #region RemoveAllCourses
    public async Task<IActionResult> RemoveAllCourses()
    {
        UserEntity? user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            bool result = await _courseService.RemoveAllCoursesFromUserAsync(user.Id);
            if (result)
            {
                return RedirectToAction("SavedCourses", "Account");
            }
        }
        return BadRequest();
    }
    #endregion
}
