using System.Reflection.Emit;
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
using Project_MVC_BookShop2.Data;


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
private readonly IBookTypeRepository _typeRepository;

private readonly MyBookStoreWebDbContext _context;

private readonly IWebHostEnvironment _webHostEnvironment;  ///dependency injection for server path to store uploaded photos on the server, contains all details about this environment
//helps to make part of path and to save the path in wwwroot/books/cover 
//Also used to identify environment

// ctor + tab -to make constructor
        // this is constructor

public BookController(BookRepository bookRepository, IBookTypeRepository typeRepository, IWebHostEnvironment webHostEnvironment, MyBookStoreWebDbContext context){
// here we are assigning BookRepository class with all its methods to -> _bookRepository
// can acess to BookRepository class methods , after creating an object from BookRepository class -> _bookRepository
//also can use static class in BookRepository class, and have acess to class methods through the class folown by dot and class method
_bookRepository = bookRepository;             //dependency injection, to make object from bookRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<BookRepository, BookRepository>(); (we can use this Depenedency injection because we wrote - line 48 in Program.cs)
_typeRepository = typeRepository;    //dependency injection using interface, to make object from languageRepository class, to use it here we write in Program.cs -> builder.Services.AddScoped<ILanguageRepository, LanguageRepository>(); (we can use this Depenedency injection because we wrote  - line 49 in Program.cs)
_webHostEnvironment = webHostEnvironment;   //dependency injection for server path to store uploaded photos on the server, (we don't write this variable in Program.cs to use it here) , Also used to identify environment ---> if(_webHostEnvironment.IsDevelopment){} <-- if it envionment Development do some code
_context = context;
}



        
// // this example return all data Array from dataSource
// public List <Book> GetAllBooks(){
//     return _bookRepository.GetAllBooks();
// }

 // always use data return type -> Task with async methods 
public async Task<IActionResult> GetAllBooks(){  //using IActionReult we can return any datatype from this action method, ViewResult - can return only View()
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


// [Authorize] //only logedIn user will be able to access this method
// form Method to add new book, GET method
[HttpGet]
public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0){

// passing English language as default to our form  -->in return View(model)
var model = new Book(){
    BookTypeId = 1  //need to pass an Id of the language, because we used SelectList --> new SelectList(await _languageRepository.GetLanguages(), "Id","Name")
};


// here we get all languages from database , Language Table
// and passing the data in ViewBag
ViewBag.Booktype = new SelectList(await _typeRepository.GetBookTypes(), "Id","TypeName");  //under the hood --> Id- value property(in our case =1), TypeName - Text property(in our case =children)  -> <option value="1" > Children </option>

ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

    // by default we passing isSuccess = false to the View page --> AddNewBook
    // and create variable int bookId = 0 and by default we passing it to View page -->AddNewBook
    ViewBag.IsSuccess = isSuccess;
    ViewBag.BookId = bookId;
    
    // return View(model);  //passing the model to the View
    return View();
}


  // always use data return type -> Task with async methods 
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method (attribute)
public async Task<IActionResult> AddNewBook(Book book){ //book <--is the data coming from AddNewBook.cshtml filled form

    Console.WriteLine($"this is the posted book from controller - {book.ToJson()}");


    if(book.CoverPhoto == null){  //If the user didn't added CoverPhoto we add Customized Error specifically for this property
        ModelState.AddModelError("CoverPhoto", "The Image file is required");  //<-- assign the error for CoverPhoto (it is a key), with the message --> The Image file is required (this is a value)
    }  

    if(book.BookPdf == null){  //If the user didn't added BookPdf we add Customized Error specifically for this property
        ModelState.AddModelError("BookPdf", "The Pdf file is required");  //<-- assign the error for BookPdf (it is a key), with the message --> The Pdf file is required (this is a value)
    } 


    ViewBag.Category = new List<string>(){
        "programming","animals", "technology", "sports"
    };

    // ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
    ViewBag.Booktype = new SelectList(await _typeRepository.GetBookTypes(), "Id","TypeName");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>




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


            /////////////           Another option how to Save file
            ////////////    First we need to make unique name for each file that we are saving--> which contains DateTime + FileName
            // string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");   //<-- first part of Saved file
            // newFileName += path.GetExtension(book.CoverPhoto!.FileName);    //<-- in addition we adding to the curent string + FileName
            ////Then we need a full path where we save the image , books <-- folder where we keep all the images in the wwwroot folder, _webHostEnvironment.WebRootPath <-- full path of our public folder of our application in WEB (which is wwwroot)
            // string imageFullPath = _webHostEnvironment.WebRootPath + "/books/" + newFileName;
            // using (var steam = System.IO.File.Create(imageFullPath)){book.CoverPhoto.CopyTo(steam);}  //<--allow us to save received image, System.IO.File.Create(imageFullPath))  or   System.IO.AddNewBook(imageFullPath)
            ////////// Then we need to Save new book in the database-->
            /// book.CoverImageUrl = newFileName;
        }

        if(book.BookPdf != null){
            
            string folder = "books/pdf/";
            book.BookPdfUrl = await UploadFile(folder, book.BookPdf);  //invoke UploadeFile function (line 172), this function can be used for all uploaded files
        }



        int id = await _bookRepository.AddNewBook(book);

        Console.WriteLine($"This is new Book id - {id}");

            if(id > 0){

                
                // here after pressing form button we redirect to the same page and passing isSuccess = true and correct id
                // ViewBag.IsSuccess = isSuccess; <--Don't need to write here, just assign values in AddNewBook action method 
                //ViewBag.BookId = bookId; <--Don't need to write here, just assign values in AddNewBook action method 
                return RedirectToAction("AddNewBook", new{isSuccess = true, bookId = id});  //isSuccess = true,bookId = id -->shows in the URL, passing in URL with the Post method,  This will pass these values as query string parameters.
           
                // return View("AddNewBook", id);
            }
    }
    // else{

        // if form is not ValidateAntiForgeryTokenAttribute = return false, and call this code
        // ViewBag.IsSuccess = false;
        // ViewBag.BookId = 0;


        // add some custom error messages to your model -> validation-summary
        ModelState.AddModelError("", "This is my 1st custom error message from BookController"); // "" <-is a key (Key can be any of Model properties such as: Title, Author, Description, CAtegory, LanguageID, Language, TotalPages, CoverPhoto, CoverImageUrl, BookPdf, BookPdfUrl, Price, CreatedAt, if it is empty it is general message ), second is an error msg, if we dont have any key then keep it blank
        ModelState.AddModelError("", "Please check all fields for errors");


    // }
        return View(book);  //if Model.State == false  -->return a View

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

    ViewBag.IsSuccess = false;


    var data = await _bookRepository.GetBookById(id);


    ViewBag.Category = new List<string>(){
    "programming","animals", "technology", "sports"
    };

    // ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
    ViewBag.Booktype = new SelectList(await _typeRepository.GetBookTypes(), "Id","TypeName"); //under the hood --> Id- value property(in our case =1), TypeName - Text property(in our case =children)  -> <option value="1" > Children </option>
    //also can use --> new SelectListItem(){text="Name, value="1};


    if(data == null){  // if we can't find the id of the book then it will be null
        return RedirectToAction("Index", "Home");   //--> we redirect to main page if the id is not existing
    }

    return View(data);
}



[HttpPost]
public async Task <IActionResult> EditBook(int id, Book book) {  //takes id from URL, and book from post method

    ViewBag.IsSuccess = true;

    var data = await _bookRepository.EditBook(id);

    if(data == null){  //<-- if there is no Id, if we can't find a book
    return RedirectToAction("Index", "Home");
    }

    ViewBag.Category = new List<string>(){
    "programming","animals", "technology", "sports"
    };

    // ViewBag.Language = new SelectList(new List<string>(){"Spanish", "Chinese", "Dutch"}); <--hardcoded List, not from database
    ViewBag.Language = new SelectList(await _typeRepository.GetBookTypes(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>


    Console.WriteLine($"This is Edited book data - {book.ToJson()}");

    if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
    {

        ViewBag.Message = "Data updated successfully";

        ///////////update the image file if we have a new submitted image file
        string newURLFileName = data.CoverImageUrl;  //<-- assign a CoverImageUrl string from data that from DB (old file image URL)

        Console.WriteLine($"This is URL From DB - {newURLFileName}");

        if(book.CoverPhoto != null ){                             //<-- if we have Uploaded new image file we can update the file
            newURLFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");   //<-- creating Unique file name using current date and time
            newURLFileName += Path.GetExtension(book.CoverPhoto.FileName);  //<-- adding a Uploaded file name to the string of time

            //_webHostEnvironment.WebRootPath  --> /home/codenitro/VSCode/northocoders/project/practice/C_Sharp/Project_MVC_BookShop2/wwwroot
            string imageFullPath = _webHostEnvironment.WebRootPath + "/books/cover/" + newURLFileName;
            using (var stream = System.IO.File.Create(imageFullPath)){
            await book.CoverPhoto.CopyToAsync(stream);
            }

            //delete the old image
            //_webHostEnvironment.WebRootPath  --> /home/codenitro/VSCode/northocoders/project/practice/C_Sharp/Project_MVC_BookShop2/wwwroot
            string oldImageFullPath = _webHostEnvironment.WebRootPath + data.CoverImageUrl;
            System.IO.File.Delete(oldImageFullPath);
        }
        //////////////////////////////////////////



        ///////////update the PDF file if we have a new submitted image file
        string newPdfFileName = data.CoverImageUrl;  //<-- assign a CoverImageUrl string from data that from DB (old file image URL)

        Console.WriteLine($"This is URL From DB - {newPdfFileName}");

        if(book.BookPdf != null ){                             //<-- if we have Uploaded new image file we can update the file
            newPdfFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");   //<-- creating Unique file name using current date and time
            newPdfFileName += Path.GetExtension(book.BookPdf.FileName);  //<-- adding a Uploaded file name to the string of time

            //_webHostEnvironment.WebRootPath  --> /home/codenitro/VSCode/northocoders/project/practice/C_Sharp/Project_MVC_BookShop2/wwwroot
            string imageFullPath = _webHostEnvironment.WebRootPath + "/books/cover/" + newPdfFileName;
            using (var stream = System.IO.File.Create(imageFullPath)){
            await book.BookPdf.CopyToAsync(stream);
            }

            //delete the old image
            //_webHostEnvironment.WebRootPath  --> /home/codenitro/VSCode/northocoders/project/practice/C_Sharp/Project_MVC_BookShop2/wwwroot
            string oldImageFullPath = _webHostEnvironment.WebRootPath + data.CoverImageUrl;
            System.IO.File.Delete(oldImageFullPath);
        }
        //////////////////////////////////////////


        //Update the book in the database
        data.Title = book.Title;
        data.Author = book.Author;
        data.Category = book.Category;
        data.Description = book.Description;
        data.BookTypeId = book.BookTypeId;
        data.TotalPages = book.TotalPages.HasValue ? book.TotalPages.Value : 0;
        data.CoverImageUrl = newURLFileName;
        data.BookPdfUrl = newPdfFileName;
        data.Price = book.Price;


        await _context.SaveChangesAsync();

        // return RedirectToAction("SearchBook");  //if the action is located in the same Controller, we don't need to indicate controller name
        return View ("SearchBook");

    }

ViewBag.Message = "Internal Error. Not able to update data";
return View(book);  //if ModelState is unccessful
}





public async Task <IActionResult> DeleteBook(int id) {

    var data = await _bookRepository.DeleteBook(id);

    if(data == null){
        return RedirectToAction("SearchBook", "Book");
    }

    Console.WriteLine($"This isfull path - {_webHostEnvironment.WebRootPath}");

    string imageFullPath = _webHostEnvironment.WebRootPath + data.CoverImageUrl;

    System.IO.File.Delete(imageFullPath);


    //Remove is not async. Removing an object is very simple and fast operation, it doesn't involve waiting any external resources db call,making network calls, and reading from disk
    //The reason why Remove() doesn't allow await --> it doesn't involve any immediate database interaction when called, doesn't involve waiting and it is quick in-memory operation
    _context.Books2.Remove(data);    //<-- Remove is NOT asynchronous function, Don't need to add --> await in front of --> _context
    _context.SaveChanges();
    ViewBag.Message = "Book Deleted Successfully";
    
    return RedirectToAction("SearchBook", "Book");
}










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