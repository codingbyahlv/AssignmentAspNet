using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.ViewModels.Views;

namespace WebApp.Controllers;

public class HomeController(IConfiguration configuration, HttpClient http) : Controller
{
    private readonly IConfiguration _configuration = configuration;
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
                StringContent content = new(JsonConvert.SerializeObject(viewModel.SubscribeSection), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _http.PostAsync($"https://localhost:7283/api/subscribers?key={_configuration["ApiKey"]}", content);
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

    #region ContactGet
    [HttpGet]
    [Route("/contact")]
    public IActionResult Contact()
    {
        ContactViewModel viewModel = new();
        return View(viewModel);
    }
    #endregion

    #region ContactPost/SendForm
    [HttpPost]
    [Route("/contact")]
    public async Task<IActionResult> Contact(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                StringContent content = new(JsonConvert.SerializeObject(viewModel.ContactForm), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _http.PostAsync($"https://localhost:7283/api/contact?key={_configuration["ApiKey"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
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

    #region Error
    [Route("/error")]
    public IActionResult Error(int statusCode)
    {
        return View();
    }
    #endregion
}
