using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Project1.Models;
using Microsoft.Extensions.Configuration;  //needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file


namespace Project1.Areas.Admin.Controllers;

[Area("Admin")]  //Admin - here we defined only the name of the area, no Routing been defined
[Route("admin/[controller]/[action]")]  //Routing to our page
public class HomeController : Controller
{

    private readonly IWebHostEnvironment _env;  //<- dependency injection to know the environment --> Staging test, deployment,Production

    private readonly IConfiguration configuration;  //create variable for Configuration, to use appsettings data


    // public HomeController(IConfiguration _configuration) //assign configuration to use it in Sketch page
    // {
    //     configuration = _configuration;
    // }


    private readonly ILogger<HomeController> _logger;


// constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration _configuration, IWebHostEnvironment env) //IConfiguration to read appsettings.json file
    {
        _logger = logger;
         configuration = _configuration;  //now using configuration --> we can read the appsetings data
        _env = env;  //<-- dependency injection to check the Environment Variable 
    }

    public IActionResult Index()
    {
        return View();
    }

  
    


  
}