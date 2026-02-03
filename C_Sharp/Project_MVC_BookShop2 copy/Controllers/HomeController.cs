using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Project_MVC_BookShop2.Models;
using Microsoft.Extensions.Configuration;  //needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file
using DotNetEnv;
using Project_MVC_BookShop2.Service;    //needs to use Service/UserService.cs class
using System.Dynamic;
using Microsoft.AspNetCore.Diagnostics;

namespace Project_MVC_BookShop2.Controllers;


public class Test {
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; }
}


public class HomeController : Controller
{

    private readonly IWebHostEnvironment _env;  //<- dependency injection to know the environment --> Staging test, deployment,Production

    private readonly IConfiguration configuration;  //create variable for Configuration, to use appsettings data

    public readonly UserService _userService;

    // public HomeController(IConfiguration _configuration) //assign configuration to use it in ContactUs page
    // {
    //     configuration = _configuration;
    // }


    private readonly ILogger<HomeController> _logger;


// constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration _configuration, IWebHostEnvironment env, UserService userService) //IConfiguration to read appsettings.json file
    {
        _logger = logger;
         configuration = _configuration;  //now using configuration --> we can read the appsetings data
        _env = env;  //<-- dependency injection to check the Environment Variable 
        _userService = userService;
    }

    public IActionResult Index()
    {
        Console.WriteLine(_env.EnvironmentName);  //<-- will show Environment variable in which we are working

        var userId = _userService.GetUserId();
        Console.WriteLine($"Show User Id (From Home/Index) - {userId}");  //<--will show UserId

        var isLoggedIn = _userService.IsAuthenticated();
        Console.WriteLine($"Is user Loged In(From Home/Index)? - {isLoggedIn}");  //<--will show true or false, is the user Logged-In or not



        // string num = "13";
        // var number = Convert.ToInt32(num);
        //Convert.ToSingle(num)  <-- convert to float 13.0
        // Console.WriteLine($"ConsoleWriteLine - number from Home/Index - {number}");
        // Console.WriteLine(num is int);  //<-- sill show true or false, chech the data type of the variable
        //Console.WriteLine(num is float);   //<-- data type is float, will give true or false
        //Console.WriteLine(num is Book);  //<-- is the variable num created from Class Book, will show true or false


        //  var modifiedReleaseDate = DateTime.Now.Date;

        return View();
    }

    
    public IActionResult Basket()
    {
        return View();
    }


//first option of managing error page, declaration of this use Status code in-> Program.cs file line 175
    // [Route("/StatusCodeError/{statusCode}")]
    // public IActionResult Error(int statusCode)
    // {
    //     if(statusCode == 404){
    //         ViewBag.ErrorMessage = "404 page Not Found Exception.";
    //     }

    //     return View("MyCustomError");
    // }


//second option of managing error page, declaration of this use Status code in-> Program.cs file line 182
    [Route("/StatusCodeError")]
    public IActionResult Error(int statusCode)
    {

        //The endpoint that processes the error can get the original URL that generated the error. Here we assign typed route to ViewBag.OriginalPath and then we can show it to the user
        var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>(); 
        if(statusCodeReExecuteFeature != null){
            ViewBag.OriginalPath = statusCodeReExecuteFeature.OriginalPath;
        }


        if(statusCode == 404){
            ViewBag.ErrorMessage = "404 page Not Found Exception.";
        }

        return View("MyCustomError");
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
