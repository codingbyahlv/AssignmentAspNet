using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController(HttpClient http) : Controller
{
    private readonly HttpClient _http = http;


    [HttpGet]
    [Route("/courses")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new CoursesIndexViewModel();

        var response = await _http.GetAsync("https://localhost:7283/api/courses");
        viewModel.CoursesSection.CourseList = JsonConvert.DeserializeObject<IEnumerable<CourseCardModel>>(await response.Content.ReadAsStringAsync())!;

        return View(viewModel);
    }

    [HttpGet]
    [Route("/singlecourse")]
    public IActionResult SingleCourse()
    {
        return View();
    }
}
