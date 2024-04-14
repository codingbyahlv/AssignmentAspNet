using Microsoft.AspNetCore.Mvc;
using Infrastructure_Api.Models;
using Infrastructure_Api.Entities;
using Infrastructure_Api.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(CourseService courseService) : ControllerBase
{
    private readonly CourseService _courseService = courseService;

    #region CreateCourse
    [HttpPost]
    public async Task<IActionResult> Create(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            if (!await _courseService.IsCourseExist(model.Title))
            {
                if (await _courseService.CreateCourse(model))
                    return Created("", null);
                else return Problem("Unable to create subscription");
            }
            else return Conflict("Email address already exist");
        }
        return BadRequest();
    }
    #endregion


    #region GetAllCourses
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<CourseEntity> courses = await _courseService.ReadAllCourses();
        if(courses != null)
            return Ok(courses);
        else return BadRequest();
    }
    #endregion


    #region GetOneCourse
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _courseService.ReadOneCourse(id);
        if (course != null)
            return Ok(course);
        return NotFound();
    }
    #endregion
}
