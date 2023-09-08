// using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;  //connection of error
using Project1.Views.Home; //User class connection

namespace Project1.Controllers;

public class HomeController : Controller
{

    // variable declaration
    // Configuration -propoties, take some info from appsettings.json file
    private readonly IConfiguration Configuration;
    private readonly ILogger<HomeController> _logger;


    // constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        //  Configuration -keep info from appsettings.json file
        Configuration = configuration;
    }




    // Methods
    // will show main page from Views -> Home -> Index (everithing we put there)
    // [HttpGet]
    public IActionResult Index()
    {
        // var adminName = Configuration.GetSection("Admin: Name");
        return View();
    }


    // [HttpPost]
    //   public async Task<IActionResult> PostFormMethodToDatabase( string name, string age, string description)
    // {

    //     var product = new Product(){

    //         Name = name,
    //         Age = age,
    //         Description = description,
    //     };

    //     await _productRepository.Insert(product);

    //     return View();
    // }



   public IActionResult Game()
    {
        return View();
    }


     // will show another page from Views -> Home -> Privacy (everithing we put there)
    public IActionResult Test()
    {

        int age = 12;
        string lastname = "Pokemon";
        char g = 'g';
        // string name = "John";
        var numberList = new List<int> {1,2,3,4,5,6,7,8,9};
        // in View to receive this data -> @model IEnumerable<int>


        var numbersArray = new string[] {"1","2","3"};
          // in View to receive this data -> @model string[]


// don't work with these data
        // var users = new List<User>{
        //     new User {Name= "Bill",Age = 24},
        //     new User {Name= "Mick",Age = 28},
        //     new User {Name= "Tom",Age = 27},
        // }

//in View to receive this data -> @using Project1.Domain.Entity
// /in View to receive this data -> @model List<User>

        var user = new User() {Lastname = lastname};
          // in View to receive this data -> @using Project1.Domain.Entity 

        return View(numbersArray);
    }


  public IActionResult Calculator()
    {
        return View();
    }

     public IActionResult Vending()
    {


        return View();
    }

//      public string changeName(){

//         string name = "";

// return name = "Kerillllllllllll";
//      }


    // [HttpPost]
    // public double addProduct(double productPrice,float money){
    //      return money-productPrice;
    //     }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

