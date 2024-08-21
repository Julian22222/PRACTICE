using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System;       //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;    //querying any type of data source
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Microsoft.AspNetCore.Mvc;           //allow to use Routes , //importing to inherit Controller
using Project_MVC_BookShop2.Repository;    //BookRepository connection and methods - GetAllBooks and others
using Project_MVC_BookShop2.Models;        //Book class import connection
using Microsoft.AspNetCore.Mvc.Rendering;   //to use SelectList, SelectListItem, SelectListGroup, use Html partial views
using Microsoft.AspNetCore.Hosting;  // to use IWebHostEnvironment
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;  // to use Path.Combine function
using Microsoft.AspNetCore.Authorization; // to use [Authorize] Attribute, only loged In users can access this action method
using NuGet.Protocol;  


// using System.Web.Mvc; 


namespace Project_MVC_BookShop2.Controllers
{

    //if we don't want write routes atributes for each action method then we can use tokken replacement at controller level--> (concept)
    // [Route("[controller]/[action]")]  

    public class BookController : Controller  //inherit this class from Controller class
    {

// define type of this variable, data type - BookRepository. (template)
// _bookRepository -> variable name
private readonly BookRepository _bookRepository = null;
private readonly ILanguageRepository _languageRepository = null;

private readonly IWebHostEnvironment _webHostEnvironment;  ///dependency injection for server path to store uploaded photos on the server, contains all details about this environment
//helps to make part of path and to save the path in wwwroot/books/cover 
//Also used to identify environment

// ctor + tab -to make constructor
        // this is constructor
public BookController(BookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment){
// here we are assigning BookRepository class with all its methods to -> _bookRepository
// can acess to BookRepository class methods , after creating an object from BookRepository class -> _bookRepository
//also can use static class in BookRepository class, and have acess to class methods through the class folown by dot and class method
_bookRepository = bookRepository;             //dependency injection, to make object from bookRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<BookRepository, BookRepository>(); (we can use this Depenedency injection because we wrote - line 48 in Program.cs)
_languageRepository = languageRepository;    //dependency injection using interface, to make object from languageRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<ILanguageRepository, LanguageRepository>(); (we can use this Depenedency injection because we wrote  - line 49 in Program.cs)
_webHostEnvironment = webHostEnvironment;   //dependency injection for server path to store uploaded photos on the server, (we don't write this variable in Program.cs to use it here) , Also used to identify environment ---> if(_webHostEnvironment.IsDevelopment){} <-- if it envionment Development do some code
}



        
// // this example return all data Array from dataSource
// public List <Book> GetAllBooks(){
//     return _bookRepository.GetAllBooks();
// }

 // always use data return type -> Task with async methods 
public async Task<IActionResult> GetAllBooks(){
    var data = await _bookRepository.GetAllBooks();

    return View(data); //passing the data to the View
}




// // this example return full book data depanding from id
// public Book GetBook(int id){
//     return _bookRepository.GetBookById(id);
// }

//id is commiing from URL, client request
public async Task<IActionResult> GetBook (int id){  //returning a View - that means it should be - IActionResult / or ViewResult
   var data = await _bookRepository.GetBookById(id);

    return View(data); //passing the data to the View
}

// public List<Book> SearchBook(string title, string authorName){
//     return _bookRepository.SearchBook(title,authorName);
// }


[Authorize] //only logedIn user will be able to access this method
// form Method to add new book, GET method
public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0){

ViewBag.TitlePage = "Add new book";

// passing English language as default to our form  -->in return View(model)
var model = new Book(){
    LanguageId = 1  //need to pass an Id of the language, because we used SelectList --> new SelectList(await _languageRepository.GetLanguages(), "Id","Name")
};

// here we get all languages from database , Language Table
// and passing the data in ViewBag
ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>

ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

    // by default we passing isSuccess = false to the View page --> AddNewBook
    // and create variable int bookId = 0 and by default we passing it to View page -->AddNewBook
    ViewBag.IsSuccess = isSuccess;
    ViewBag.BookId = bookId;
    return View(model);
}


  // always use data return type -> Task with async methods 
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method (attribute)
public async Task<IActionResult> AddNewBook(Book book){ //book <--is the data coming from AddNewBook.cshtml filled form

    Console.WriteLine($"this is the posted book from controller - {book.ToJson()}");

    ViewBag.TitlePage = "Add new book";

    // if(!ModelState.IsValid){....}  <-- if model is filled incorrectly

    if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
    {


        // to save uploaded cover photo in the wwwroot/books/cover
        if(book.CoverPhoto != null){

            string folder ="books/cover/";  //path to folder where we store uploaded photos
            // if we deploy this app on a server (using the folder path only)then we will get an error (because this folder (path) is not accessable,
            //or this folder (path) is not available because we need a server actual path to store images in this app  --> 
            //therefore we need to use this parametr --> IWebHostEnvironment webHostEnvironment (it contains all details about environment), in contructor we create it and then assign to _webHostEnvironment) and then use it in lne 133

            //add uploaded img file Name to the path --> book.CoverPhoto.FileName;
            //also we need to avoid errors when upload images with the same name, make the img files name unique -> + Guid.NewGuid().ToString()
            folder += Guid.NewGuid().ToString() + "_" + book.CoverPhoto.FileName;

            // assign folder variable to CoverImageUrl property, / <--must be added in front of folder to display an image from database
            book.CoverImageUrl = "/" + folder;  //<-saving CoverPhoto path to the variable(we don't use serverFolder variable, to save path in database we use only path from wwwtoot folder and we don't need full path using environment variable --> _webHostEnvironment), 
            //We use --> "/" picture to be visible in UI

            // we need server path to store(Save) these imges in this application using Global access WEB (When app is deployed),(we need to use IWebHostEnvironment dependency injection)
            // Define the path for a server of the actual folder where we keep imgs, add the server path + folder (join server path and folder)
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);  // <-- this function combine 2 variables --> _webHostEnvironment.WebRootPath  +  folder. 
            //This serverFolder variable will allow server to save the file using our path to correct folder

            //this line needs to save Image to wwwroot/books/cover, in our Application
            // we save image (this is uploaded image -->book.CoverPhoto) and make a copy of the full img path in wwwroot/books/cover,
            // new FileStream(serverFolder <- the server path)
            await book.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        }

        if(book.BookPdf != null){
        
        string folder = "books/pdf/";
        book.BookPdfUrl = await UploadFile(folder, book.BookPdf);  //invoke UploadeFile function (line 172), this function can be used for all uploaded files
        }



          int id = await _bookRepository.AddNewBook(book);

    if(id > 0){
    // here after pressing form button we redirect to the same page and passing isSuccess = true and correct id
    // ViewBag.IsSuccess = isSuccess; <--Don't need to write here, just assign values in AddNewBook action method 
    //ViewBag.BookId = bookId; <--Don't need to write here, just assign values in AddNewBook action method 
    return RedirectToAction("AddNewBook", new{isSuccess = true, bookId = id});  //isSuccess = true,bookId = id -->shows in the URL
}
    }

    // if form is not ValidateAntiForgeryTokenAttribute = return false, and call this code
    // ViewBag.IsSuccess = false;
    // ViewBag.BookId = 0;



    ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

// ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>



// add some custom error messages to your model -> validation-summary
ModelState.AddModelError("","This is my 1st custom error message from BookController"); // "" <-is a key, second is an error msg, if we dont have any key then keep it blank
ModelState.AddModelError("","This is my 2nd custom error message from BookController");


   return View();
}


// function, can pass different variable as arguments in this function
//This function receive 2 parametrs --> folder path (where to save file) and file with all info about that uploaded file
//this function return the URL of particular image
private async Task<string> UploadFile(string folderPath, IFormFile file){
    folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);  //<-- add the server path + folder (join server path and folderPath)
    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));   //<-- this line needs to save Image to wwwroot/books/cover, in our Application

    return "/" + folderPath;  //<-- return uploaded file URL 
}


public async Task<IActionResult> SearchBook(string SearchTitle){ //here we receive the typed input from the form

if(string.IsNullOrEmpty(SearchTitle)){  //if received SearchTitle is null or empty we show all the books from database
    var data = await _bookRepository.GetAllBooks();
return View(data);
}else{

var data = await _bookRepository.SearchBook(SearchTitle);  //if received SearchTitle exists( has some value) it will show that searched book 
return View(data);

}

}


public async Task <IActionResult> EditBook(int id) {

ViewBag.TitlePage = "Edit book";


var data = await _bookRepository.GetBookById(id);


ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

// ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>
//also can use --> new SelectListItem(){text="Name, value="1};

return View("AddNewBook",data);
}



// [HttpPost]
// public async Task <IActionResult> EditBook(Book book) {

// ViewBag.TitlePage = "Edit book";

// if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
// {

// var data = await _bookRepository.EditBook(book);

// if(data){
// Viewbag.Message = "Data updated successfully";
// }else{
// Viewbag.Message = "Internal Error. Not able to update data";
// }

// ViewBag.Category = new List<string>(){
// "programming","animals", "technology", "sports"
// };

// // ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
// ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>


// return View("AddNewBook");
// }
// }



// public async Task <IActionResult> DeleteBook(int id) {

//     var data = await _bookRepository.DeleteBook(id);

//     return RedirectToAction("Index");
// }










// [HttpPost]
// public async Task<IActionResult> SearchBook(string title, bool isSuccess = true){ 

// // if(!string.IsNullOrEmpty(title)){ //if title exist then we return this code

// var data = await _bookRepository.SearchBook(title);
// Console.WriteLine(title);

// ViewBag.IsSuccess = isSuccess;

// return View(data);

// }



// ----------------------------------------------------------------------------------------

// /book/seeallbooks
public string SeeAllBooks(){
    return "This is a list of all books";
}


// /book/findbookbyid/1
public string findBookById(int id){
return $"this book Id = {id}";
}

// query string 
// /book/findbook?bookName=SherlockHolmes&authorName=Doile
public string FindBook(string bookName, string authorName){
    return $"Book with name = {bookName} and Author = {authorName}";
}

[Route("test/a{a}")] //route must be /test/ + and starts with "a" letter, otherwise is 404 error page
public string Test(string a){ //<--in this atring a we will have {a} variable from URL that client will type, example URl --> localhost:65352.test/abb, should start with a and variable {a} = bb
return a;
}



    }
}