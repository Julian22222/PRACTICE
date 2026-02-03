// using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;  //connection of error
using System.Threading.Tasks; //User class connection, //creating new threads for computation, aslo when use async-await operations, and to use Task
using Project1.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using Project1.Interfaces;
using Microsoft.Extensions.Configuration;
using Project_MVC_BookShop2.Models;  //needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file

namespace Project1.Controllers;

public class Test {
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; }
}

public class HomeController : Controller
{

    // variable declaration
    // Configuration -propoties, take some info from appsettings.json file
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;

    private readonly ICarRepository _carRepository;


    // constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ICarRepository carRepository)
    {
        _logger = logger;
        //  Configuration -keep info from appsettings.json file
        _configuration = configuration;
        _carRepository = carRepository;
    }


    // Methods
    // will show main page from Views -> Home -> Index (everithing we put there)
    [HttpGet]
    public async Task<IActionResult> Index(string? message)
    {
        ViewBag.Message = message; //assigning received message to ViewBag.Message
    
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



     public IActionResult ShowCars(string? message) //receive message from AddNewCar method -Redirection method
    {
        ViewBag.Message = message; //assigning received message to ViewBag.Message

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

        if (ModelState.IsValid){
            Console.WriteLine($"Adding car: {car.Name}, {car.Price}, {car.Year}, {car.FuelType}");

            var carId = _carRepository.AddCar(car);
            Console.WriteLine(carId);
            
            return RedirectToAction("ShowCars", new { message = "Car was added Successfully" });    // Redirect to the car list view
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

    
    public IActionResult EditCar(int id)
    {
        var car = _carRepository.GetCarById(id);

        var FuelList = new List<string>(){"Petrol","Diesel","Electric"};
        ViewBag.Fuel = new SelectList(FuelList);

        if (car != null)
        {
            // Pass the car object to the view for editing
            return View(car);
        }
        else
        {
            // Handle the case when the car is not found
            ModelState.AddModelError("", "Car not found");
        }
    
        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
     public IActionResult EditCar(Car car, int id){ // car <--new edited car info, id <-- id of the car we want to edit
        var data = _carRepository.GetCarById(id);
        if(data == null)
        {
            // Handle the case when the car is not found
            ModelState.AddModelError("", "Car not found");
            // return RedirectToAction("Index", "Home");
        }

        var FuelList = new List<string>(){"Petrol","Diesel","Electric"};
        ViewBag.Fuel = new SelectList(FuelList);

        if(ModelState.IsValid)
        {
            ViewBag.Message = "Data updated successfully";
            _carRepository.EditCar(id,car);

            return RedirectToAction("ShowCars", new { message="Car was updated Successfully"});    // sending message to ShowCars method in URL
        }
        else
        {
            // Handle the case when the car is not found
            return RedirectToAction("Index", "Home", new { message = "Not able to update data" }); //sending message to ShowCars method in URL
        }
     }



    [HttpPost]
    public IActionResult DeleteCar(int id)
    {

        var car = _carRepository.GetCarById(id);
        if (car != null)
        {
            // Remove the car from the list
            _carRepository.DeleteCar(id);

            ViewBag.Message = "Car Deleted Successfully";

            // Redirect to the car list view
            return RedirectToAction("ShowCars", "Home", new { message = "Car was deleted successfully" }); //sending message to ShowCars method in URL
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


    
    //can insert many routes for one page at the same time
    // [Route("")]    //<-- will be home page
    // [Route("something/{id?}")]
    [Route("my-draft")]  //Attribute routing (best and easy way to make new Route to this resource)
    public IActionResult Draft()
    {

        int age = 12;
        string lastname = "Pokemon";
        char g = 'g';
        // string name = "John";
        var numberList = new List<int> {1,2,3,4,5,6,7,8,9};
        // in View to receive this data -> @model IEnumerable<int>


        var numbersArray = new string[] {"1","2","3"};
          // in View to receive this data -> @model string[]




        var obj = new {Id =2, Name = "Hello!!!"}; //<--creating a variable with 2 properties
        //return View("ContactUs",obj) <-- returning ContactUs View from this action method and passing obj to the View

        ViewBag.Obj = obj;  //<--in View show --> {Id =2, Name = "Hello!!!"}, can't get separately Id or Name
        //It is better to use Dynamic object

        dynamic data = new ExpandoObject();
        data.Id = 2;
        data.Name = "Hellllooo!!!";

        ViewBag.Data = data;




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
        //return View("AboutUs");  <-- we can indicate the View name to return from View/Home folder
        //return View("ContactUs");
    }

  public IActionResult Sketch(){

     var result = _configuration["Name"]; //to access appsettings.json file in action method
        var test = _configuration.GetValue<bool>("DisplayNewCarAlert");  //to acess appsettings.json file using GetValue (here we indicate the data type)
        Console.WriteLine(_configuration["NewCarAlertObj:CarName"]);  //<--use this as console.log in JS

        var newCarAlert = new AlertConfig(); ///create new object using AlertConfig class model and assigning it to new variable -> newBookAlert
        _configuration.Bind("NewCarAlertObj", newCarAlert);  //In Bind method we pass 2 parametrs - > first (key of object in appsettings.json), second (object of instance - just created new object)

        bool isDisplay = newCarAlert.DisplayNewCarAlert;  //now we can acces all the properties of appsetiings.json --> NewBookAlertObj object using newBookAlert

    


        //We can use Class Test from line 16
        Test myObject= new Test(){
            EmployeeID = 1,
            EmployeeName = "Jack"
        };

        ViewBag.Employee = myObject;

        return View(test);
    
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

