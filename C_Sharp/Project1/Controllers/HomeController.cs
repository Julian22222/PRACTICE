// using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;  //connection of error
using System.Threading.Tasks; //User class connection
using Project1.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project1.Controllers;

public class HomeController : Controller
{

    // variable declaration
    // Configuration -propoties, take some info from appsettings.json file
    private readonly IConfiguration Configuration;
    private readonly ILogger<HomeController> _logger;

    private readonly ICarRepository _carRepository;


    // constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ICarRepository carRepository)
    {
        _logger = logger;
        //  Configuration -keep info from appsettings.json file
        Configuration = configuration;
        _carRepository = carRepository;
    }


    // Methods
    // will show main page from Views -> Home -> Index (everithing we put there)
    [HttpGet]
    public async Task<IActionResult> Index()
    {
    
        return View();
    }



 public async Task<IActionResult> GetCar(int id)
    {
        var data = _carRepository.GetCarById(id);
        
    
        return View(data);
    }



    //this method will show the all cars from the local database List in Index.cshtml
    public IActionResult ShowCarsData()
    {

        var cars = _carRepository.GetAllCars();

        return PartialView("_CarListPartial", cars);
    }



     public IActionResult ShowCars()
    {

        var AllcarList = _carRepository.GetAllCars();

        return View(AllcarList);
    }


    public IActionResult AddNewCar()
    {
        bool isSuccess = false;
        int carId = 0;
      
      var FuelList = new List<string>(){"Petrol","Diesel","Electric"};
      ViewBag.Fuel = new SelectList(FuelList);

      ViewBag.IsSuccess = isSuccess;
      ViewBag.CarId = carId;
       
        return View();
    }

    [HttpPost]
    public IActionResult AddNewCar(Car car)
    {
        // ViewBag.Fuel = new List<string>(){"Petrol","Diesel","Electric"};

        var FuelList = new List<string>(){"Petrol","Diesel","Electric"};
        ViewBag.Fuel = new SelectList(FuelList);


        Console.WriteLine($"this is the new car from controller - {car.Name}");
        Console.WriteLine($"Car added with ID: {car.Id}");

          if (ModelState.IsValid)

        {
            Console.WriteLine($"Adding car: {car.Name}, {car.Price}, {car.Year}, {car.FuelType}");

            var carId = _carRepository.AddCar(car);

            return RedirectToAction("ShowCars");    // Redirect to the car list view

        }

        // if (!ModelState.IsValid)
        // {
        //     foreach (var modelState in ModelState.Values){
        //         foreach (var error in modelState.Errors){
        //         // Log or debug the error messages
        //         Console.WriteLine(error.ErrorMessage);
        //         }
        //     }
        // }
        
        
        // if (ModelState.IsValid)
        // {
        //     var carId = _carRepository.AddCar(car);
        //      // Console.WriteLine(carId);

        //     // if (carId > 0)
        //     // {
        //     //     ViewBag.IsSuccess = true;
        //     //     ViewBag.CarId = carId;
        //     //     return RedirectToAction("ShowCars");
        //     // }
        //     // else
        //     // {
        //     //     ViewBag.IsSuccess = false;
        //     //     ModelState.AddModelError("", "Car not added");
        //     // }
        //     return RedirectToAction("ShowCars");
        // }
        return View(car);
       
    }


    public IActionResult DeleteCar(int id)
    {

        var car = _carRepository.GetCarById(id);
        if (car != null)
        {
            // Remove the car from the list
            _carRepository.DeleteCar(id);

            ViewBag.Message = "Car Deleted Successfully";

            // Redirect to the car list view
            return RedirectToAction("ShowCars", "Home");
        }
        else
        {
            // Handle the case when the car is not found
            ModelState.AddModelError("", "Car not found");
        }
    
        return RedirectToAction("Index", "Home");
    }


    private IEnumerable<object> Allcars()
    {
        throw new NotImplementedException();
    }

   

    //  List<Car> CarsList = new List<Car>
    //     {
    //         new Car { Id = 1, Name = "Toyota", Price = 20000, Year = 2020, FuelType = "Petrol" },
    //         new Car { Id = 2, Name = "Honda", Price = 22000, Year = 2021, FuelType = "Diesel" },
    //         new Car { Id = 3, Name = "Ford", Price = 25000, Year = 2019, FuelType = "Petrol" },
    //         new Car { Id = 4, Name = "Suzuki", Price = 25000, Year = 2019, FuelType = "Diesel" },
    //         new Car { Id = 5, Name = "BMW", Price = 25000, Year = 2019, FuelType = "Petrol" },
    //         new Car { Id = 6, Name = "Audi", Price = 25000, Year = 2019, FuelType = "Electric" },
    //         new Car { Id = 7, Name = "Mercedes", Price = 25000, Year = 2019, FuelType = "Electric" },
    //         new Car { Id = 8, Name = "Chevrolet", Price = 25000, Year = 2019, FuelType = "Petrol" },
    //     };


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

