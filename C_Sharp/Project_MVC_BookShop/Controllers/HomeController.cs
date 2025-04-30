// all this link - is namespace  --> available nuget packages:  https://www.nuget.org/packages
using System.Diagnostics;  //already build up in the .NET
using Microsoft.AspNetCore.Mvc;  //allow to use Routes  //importing to inherit from Controller
using Project_MVC_BookShop.Models;

namespace Project_MVC_BookShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
