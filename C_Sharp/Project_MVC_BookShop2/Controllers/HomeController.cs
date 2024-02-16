using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_MVC_BookShop2.Models;
using Microsoft.Extensions.Configuration;  //needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file

namespace Project_MVC_BookShop2.Controllers;

public class HomeController : Controller
{

    private readonly IConfiguration configuration;  //create variable for Configuration, to use appsettings data



    // public HomeController(IConfiguration _configuration) //assign configuration to use it in ContactUs page
    // {
    //     configuration = _configuration;
    // }


    private readonly ILogger<HomeController> _logger;


// constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration _configuration) //IConfiguration to read appsettings.json file
    {
        _logger = logger;
         configuration = _configuration;  //now using configuration --> we can read the appsetings data
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("about-us")]  //Attribute routing (best and easy way to make new Route to this resource)
    public IActionResult AboutUs()
    {
        return View();
    }


    [Route("contact-us")]  //Attribute routing (best and easy way to make new Route to this resource)
    public IActionResult ContactUs(){

        var result = configuration["AppName"]; //to access appsettings.json file in action method
        var test = configuration.GetValue<bool>("DisplayNewBookAlert");


        return View(test);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
