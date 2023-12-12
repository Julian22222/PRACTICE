using System.ComponentModel;
using System.Security.AccessControl;
using System.Net.Cache;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using System.Dynamic;             //use dynamic ViewBag proporties
using Microsoft.AspNetCore.Mvc;   //need to -> inherit from Controller , allow to use Routes 
using Project_MVC.Models;         //connection of error, User class connection
using Project_MVC.Controllers;    //clients list connection

namespace Project_MVC.Controllers;

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
            //  Configuration -keep info from appsettings.json file, youy can access to info from appsettings.json
        Configuration = configuration;
    }



     // Methods
    // will show main page from Views -> Home -> Index (everithing we put there)
    // [HttpGet]
    public IActionResult Index()
    {


        var clients = new List<Clients>(){
        new Clients("Emy","Morgen",17,"test@mail.com", "Hello"),
        new Clients("Tom","Teky",22,"testtt2@mail.com", "Hey"),
    };


          // var adminName = Configuration.GetSection("Admin: Name");
        return View(clients);
    }



//Method
  public IActionResult BookStoreTest()
    {
        return View();
    }




//Method
     // will show another page from Views -> Home -> Privacy (everithing we put there)
    public IActionResult Test()
    {

        int age = 12;
        string lastname = "Pokemon";
        char g = 'g';
        // string name = "John";
        var numberList = new List<int>() {1,2,3,4,5,6,7,8,9};
        // in View to receive this data -> @model IEnumerable<int>

        


        var clients = new List<Clients>(){
        new Clients("Emy","Morgen",17,"test@mail.com", "Hello"),
        new Clients("Tom","Teky",22,"testtt2@mail.com", "Hey"),
    };





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

        return View(clients);
    }




  public IActionResult Calculator()
    {

// get info from appsettings.json and passing them to calculator
var myName = Configuration.GetSection("Admin:Name");

        return View(myName);
    }


///Sample of this route - /Books/?BookId=101
[Route("/Books")]
public IActionResult Book(){
    if(!Request.Query.ContainsKey("BookId")){
        return BadRequest("Book Id is not provided.");
    } 
    return Content("This is a book","text/plain");
}


///Sample of this route - /Check/?CheckId=104
[Route("/Check")]
public IActionResult Check(){
    if(!Request.Query.ContainsKey("CheckId")){
        return BadRequest("Check Id is not provided.");
    }
    return Content("This is a check","text/plain");
}




[ViewData]
public string CustomProperty { get; set; }  //needs for ViewData Attribute

     public IActionResult Vending()
    {

ViewBag.Number = 123;
ViewBag.Name ="My name is Julian";



dynamic data = new ExpandoObject();
data.Id = 1;
data.Name = "Julian";

ViewBag.Data = data;


ViewBag.Type = new Book(){Id= 5, Title = "This is Name", Author = "This is Author"};


ViewData["Property1"] = "This is a ViewData";

ViewData["book"] = new Book(){Author = "Jack", Id = 4 };  //using Model 

CustomProperty = "Custom Value";  //ViewData Attribute variable

        return View();
        // return Content("Book Id is Not provided");
        // return new BadRequestResult();
        // return BadRequest();
        // return NotFound();
        // return File("/Sample.pdf","application/pdf");
    }


     public IActionResult click(string button)
    {
        if(button == "first"){
            // assign what do you want to show 
          TempData["buttonval"]="First Button Clicked";
          ViewData["Title"] = "First Button Clicked - Title";
        }else{
            TempData["buttonval"]="Second Button Clicked";
        }

// return the same page 
return RedirectToAction("Vending");
    }

    public IActionResult add(string button)
    {
      
          @ViewData["ViewDataName"] = button;
     
// return the same page 
return RedirectToAction("Vending");
    }

   public IActionResult addNumber(string button)
    {
      string Name = "hello";
          @ViewData["ViewDataName"] = Name;
          if(button == "Hello"){
           Name+="!";
          }

     
// return the same page 
return View("Game");
    }

    




 public IActionResult changeName(string button)
 
    {
        if(button == "Suka"){
            @TempData["changeN"] = "Suka";
        }
        

        // return the same page 
return RedirectToAction("Vending");
    }




// tracking if you use unexisting page -will call Error() 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


