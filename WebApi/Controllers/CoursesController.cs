using Microsoft.AspNetCore.Mvc;
using Infrastructure_Api.Models;
using Infrastructure_Api.Services;
using WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Infrastructure_Api.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CoursesController(CourseService courseService) : ControllerBase
{
    private readonly CourseService _courseService = courseService;

    #region CreateCourse
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            if (!await _courseService.IsCourseExist(model.Title))
            {
                if (await _courseService.CreateCourse(model))
                    return Created("", null);
                else return Problem("Unable to create course");
            }
            else return Conflict("Course already exist");
        }
        return BadRequest();
    }
    #endregion

    #region GetAllCourses
    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        CourseResultModel courseResult = await _courseService.ReadAllCourses(category, searchQuery, pageNumber, pageSize);
        if (courseResult.Succeeded == true)
            return Ok(courseResult);
        else return BadRequest();
    }
    #endregion

    #region GetOneCourse
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        CourseEntity course = await _courseService.ReadOneCourse(id);
        if (course != null)
            return Ok(course);
        return NotFound();
    }
    #endregion

    #region UpdateCourse
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _courseService.IsCourseExist(model.Id))
            {
                if (await _courseService.UpdateCourse(model))
                    return Ok("Successfully updated");
                else return Problem("Unable to update");
            }
            else return Problem("No course to update");
        }
        return BadRequest();
    }
    #endregion

    #region DeleteCourse
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(CourseModel model) 
    {
        if (ModelState.IsValid)
        {
            if (await _courseService.IsCourseExist(model.Id))
            {
                if (await _courseService.DeleteCourse(model))
                    return Ok("Successfully deleted");
                else return Problem("Unable to delete");
            }
            else return Problem("No course to delete");
        }
        return BadRequest();
    }
    #endregion 
}
