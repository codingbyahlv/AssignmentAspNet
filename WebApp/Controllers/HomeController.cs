using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

public class HomeController(HttpClient http) : Controller
{

    private readonly HttpClient _http = http;

    #region IndexGet
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        HomeIndexViewModel viewModel = new();
        return View(viewModel);
    }
    #endregion

    #region IndexPost/Subscribe
    [HttpPost]
    [Route("/")]
    public async Task<IActionResult> Index(HomeIndexViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel.SubscribeSection), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("https://localhost:7283/api/subscribers", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "Already Exist";
                }
            }
            catch
            {
                ViewData["Status"] = "Connection Failed";
            }
        }
        else
        {
            ViewData["Status"] = "Invalid";
        }
        return View(viewModel);
    }
    #endregion

    [Route("/contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("/error")]
    public IActionResult Error(int statusCode)
    {
        return View();
    }
}
