// using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;  //connection of error
using System.Threading.Tasks; //User class connection
using Project1.Repository;

namespace Project1.Controllers;

public class HomeController : Controller
{

    // variable declaration
    // Configuration -propoties, take some info from appsettings.json file
    private readonly IConfiguration Configuration;
    private readonly ILogger<HomeController> _logger;

    private readonly CarRepository _carReository;


    // constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, CarRepository carReository)
    {
        _logger = logger;
        //  Configuration -keep info from appsettings.json file
        Configuration = configuration;
        _carReository = carReository;
    }


    // Methods
    // will show main page from Views -> Home -> Index (everithing we put there)
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        ViewBag.List = false;

        // var data = await _carReository.GetAllCars();
    
        return View();
    }



//  public async Task<IActionResult> GetCar(int id)
//     {

//         var data = await _carReository.GetCarById(id);
    
//         return View(data);
//     }

     public IActionResult ShowCars()
    {
        List<Car> cars = new List<Car>
        {
            new Car { Name = "Toyota", Price = 20000, Year = 2020, FuelType = "Petrol" },
            new Car { Name = "Honda", Price = 22000, Year = 2021, FuelType = "Diesel" },
            new Car { Name = "Ford", Price = 25000, Year = 2019, FuelType = "Electric" }
        };

        return PartialView("_CarListPartial", cars);
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

    public IActionResult Ajax()
    {
        return View();
    }


   public IActionResult Game()
    {
        return View();
    }


      public IActionResult Gallery()
    {
        return View();
    }


   public IActionResult Form()
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

