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
using Project_MVC_BookShop2.Repository;
using Project_MVC_BookShop2.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Project_MVC_BookShop2.Components;

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

    public readonly BookRepository _bookRepository;

    public readonly IBasketRepository _basketRepository;

    // public HomeController(IConfiguration _configuration) //assign configuration to use it in ContactUs page
    // {
    //     configuration = _configuration;
    // }


    private readonly ILogger<HomeController> _logger;
    private readonly MyBookStoreWebDbContext _context = null; //create instance of MyBookStoreWebDbContext class, to use it in ConnectDatabase action, needed to check if it is able to connect to the database if not - show message Loading


// constructor
    public HomeController(ILogger<HomeController> logger, IConfiguration _configuration, IWebHostEnvironment env, UserService userService, BookRepository bookRepository, IBasketRepository basketRepository, MyBookStoreWebDbContext context) //IConfiguration to read appsettings.json file
    {
        _logger = logger;
        configuration = _configuration;  //now using configuration --> we can read the appsetings data
        _env = env;  //<-- dependency injection to check the Environment Variable 
        _userService = userService;
        _basketRepository = basketRepository;  //<-- create instance of BasketRepository class
        _bookRepository = bookRepository;  //<-- create instance of BookRepository class
        _context = context;  //<-- create instance of MyBookStoreWebDbContext class, to use it in ConnectDatabase action, needed to check if it is able to connect to the database if not - show message Loading
        Console.WriteLine("HomeController constructor called");
    }




    [HttpGet]
    public IActionResult LoadTopBooks(){  //this method return ViewComponent

        //You don't need to use async/await in the Controller. The ViewComponent() method handles it, even if your InvokeAsync() inside the View Component is asynchronous.
        return ViewComponent("TopBooks");

    }



    [HttpGet]  //this method return ViewComponent
    public  IActionResult LoadWeekBook(){
    
        return ViewComponent("WeekBook");

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

    
    [HttpGet]
    public async Task <IActionResult> Basket()
    {
        var basketItems = await _basketRepository.GetBasketItems();

        return View(basketItems);
    }


    [HttpPost]
    // public JsonResult Basket([FromBody] Book book){/...}
    // public JsonResult Basket(int id){..}

    public async Task <JsonResult> Basket(int id){

        try
        {    //Start a try block to catch any exceptions (errors) that might happen during this process, so the app doesn’t crash and can return a helpful message.
            var presentBook = await _bookRepository.GetBookById(id);

            if (presentBook == null)
            {
                return Json(new { success = false, message = "Book not found" }); //If no book was found for the given ID, return a JSON response saying so. This prevents trying to add a null book to the basket.
                ////success = false,message = "Book not found" -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
            }



            // Check if already in basket
            var items = await _basketRepository.GetBasketItems(); //can't add .Any here, because GetBasketItems() returns a Task<List<Book>>, not a List<Book>, should be awaited first
            // var alreadyInBasket = _basketRepository.GetBasketItems().Result.Any(b => b.Id == id);
            //Why we use .Result - To get the actual List<Book>, you must use - await (in async code) or use .Result (in sync code). We have locally created a Task<List<Book>> in the repository, so we need to use .Result to get the actual list of books.
            //Also, GetBasketItems() returns a Task<List<Book>>, not a List<Book>
            //You cannot call .Any() directly on a Task<T>
            //'IEnumerable<Book>' does not contain a definition for 'Any'

            var alreadyInBasket = items.Any(b => b.Id == id); //Get the list of books in the basket


            if (alreadyInBasket) //Check if the book is already in the basket. If it is, return a message saying so.
            {
                return Json(new { success = false, message = "Book already in basket" }); // return a message telling the user it's already there — no duplicates.
                ////success = false, message = "Book already in basket" -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
            }


            _basketRepository.AddToBasket(presentBook);
            return Json(new { success = true, message = "Book added to basket" });  // It returns JSON (e.g. { success: true, message: "..." }) back to the browser. return a successful JSON response to the browser to let the user know the item was added.
                                                                                    // success = true or false, is not mandatory fild, can be skipped, but it is a good practice to use it, so the frontend can know if the operation was successful or not. success property is your own custom field — it's not built-in or required by ASP.NET.
        }
        catch (Exception ex)  //If something goes wrong, like the database is down or the code has a bug, it catches the error and returns a JSON response with an error message. This way, the app doesn't crash and the user gets feedback. If any error occurs (e.g., null reference, repository not working), catch the exception. Log the error to the server console. Return a JSON error response so the frontend knows something went wrong.
        {
            Console.WriteLine("ERROR IN Basket POST: " + ex.Message);
            return Json(new { success = false, message = "Server error: " + ex.Message });
        ////success = false, message = ... -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
    }   
   


    // if(presentBook.Id == book.Id)
    // {
    //      _basketRepository.AddToBasket(book);

    //     return Json(new { success = false, message = "Book already in basket" });
    // }

    // // return Json(new { success = true, message = "Book added to basket" });

    // // return Ok();
    // // return Ok(new { success = true, message = "Book added to basket" });
    }


public async Task<IActionResult> RemoveFromBasket(int id)
{
        try
        {
            var presentBook = await _bookRepository.GetBookById(id);

            if (presentBook == null)
            {
                return Json(new { success = false, message = "Book not found" });
                //success = false, message = "Book not found" -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
            }

            _basketRepository.RemoveFromBasket(presentBook.Id);
            // return Json(new { success = true, message = "Book removed from basket" });
            return RedirectToAction("Basket"); //Redirect to the Basket action after removing the book
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR IN RemoveFromBasket POST: " + ex.Message);
            return Json(new { success = false, message = "Server error: " + ex.Message });
        //success = false, message = .... -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
    }
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
