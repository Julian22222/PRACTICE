using System.Runtime.InteropServices.WindowsRuntime;
using System;       //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;    //querying any type of data source
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Microsoft.AspNetCore.Mvc;           //allow to use Routes , //importing to inherit Controller
using Project_MVC_BookShop2.Repository;    //BookRepository connection and methods - GetAllBooks and others
using Project_MVC_BookShop2.Models;        //Book class import connection
using Microsoft.AspNetCore.Mvc.Rendering;   //to use SelectList, SelectListItem, SelectListGroup, use Html partial views
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;


// using System.Web.Mvc; 


namespace Project_MVC_BookShop2.Controllers
{
    public class BookController : Controller
    {

// define type of this variable, data type - BookRepository. (template)
// _bookRepository -> variable name
private readonly BookRepository _bookRepository = null;
private readonly LanguageRepository _languageRepository = null;

private readonly IWebHostEnvironment _webHostEnvironment;  ///dependency injection for server path to store uploaded photos on the server, contains all details about this environment
//helps to make part of path and to save the path in wwwroot/books/cover

// ctor + tab -to make constructor
        // this is constructor
public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment){
// here we are assigning BookRepository class with all its methods to -> _bookRepository
// can acess to BookRepository class methods , after creating an object from BookRepository class -> _bookRepository
//also can use static class in BookRepository class, and have acess to class methods through the class folown by dot and class method
_bookRepository = bookRepository;             //dependency injection, to make object from bookRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<BookRepository, BookRepository>();
_languageRepository = languageRepository;    //dependency injection, to make object from languageRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<LanguageRepository, LanguageRepository>();
_webHostEnvironment = webHostEnvironment;   //dependency injection for server path to store uploaded photos on the server, (we don't write this variable in Program.cs to use it here)
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

public async Task<IActionResult> GetBook (int id){  //returning a View - that means it should be - IActionResult / or ViewResult
   var data = await _bookRepository.GetBookById(id);

    return View(data); //passing the data to the View
}

public List<Book> SearchBook(string title, string authorName){
    return _bookRepository.SearchBook(title,authorName);
}


// form Method to add new book, GET method
public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0){

// passing English language as default to our form  -->in return View(model)
var model = new Book(){
    LanguageId = 1
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
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method
public async Task<IActionResult> AddNewBook(Book book){

    if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
    {


        // to save uploaded cover photo in the wwwroot/books/cover
        if(book.CoverPhoto != null){

            string folder ="books/cover/";  //path to folder where we store uploaded photos
            // if we deploy this app on a server (using the folder path only)then we will get an error (because this folder (path) is not accessable,or this folder (path) is not available)

            //add uploaded img file Name to the path --> book.CoverPhoto.FileName;
            //also we need to avoid errors when upload images with the same name, make the img files name unique -> + Guid.NewGuid().ToString()
            folder += Guid.NewGuid().ToString() + "_" + book.CoverPhoto.FileName;

            // assign folder variable to CoverImageUrl property, / <--must be added in front of folder to display an image from database
            book.CoverImageUrl = "/" + folder;

            // we need server path to store these imgs in this application(we need to use IWebHostEnvironment dependency injection)
            // Define the path for a server of the actual folder where we keep imgs, add the server path + folder (join server path and folder)
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);  // <- _webHostEnvironment.WebRootPath  +  folder

            // we need to save the copy of the full img path in wwwroot/books/cover,  
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
    return RedirectToAction("AddNewBook", new{isSuccess = true, bookId = id});
}
    }

    // if form is not ValidateAntiForgeryTokenAttribute = return false, and call this code
    // ViewBag.IsSuccess = false;
    // ViewBag.BookId = 0;



    ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};


ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>



// add some custom error messages to your model -> validation-summary
ModelState.AddModelError("","This is my 1st custom error message from BookController");
ModelState.AddModelError("","This is my 2nd custom error message from BookController");


   return View();
}


// function, can pass different variable as arguments in this function
private async Task<string> UploadFile(string folderPath, IFormFile file){
    folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

    return "/" + folderPath;
}

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



    }
}