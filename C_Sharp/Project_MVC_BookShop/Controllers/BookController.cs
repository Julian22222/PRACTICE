using System.Runtime.InteropServices.WindowsRuntime;
using System;       //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;    //querying any type of data source
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Microsoft.AspNetCore.Mvc;           //allow to use Routes , //importing to inherit Controller
using Project_MVC_BookShop.Repository;    //BookRepository connection and methods - GetAllBooks and others
using Project_MVC_BookShop.Models;        //Book class import connection
//To quickly add missing namespace(to import missing namespaces on the top of the file) --> Just use CTRL+. / or ctr + space , on the word with the red underline. No need to install other extensions.
// using System.Web.Mvc; 


namespace Project_MVC_BookShop.Controllers
{
    public class BookController : Controller
    {

// define type of this variable, data type - BookRepository. (template)
// _bookRepository -> variable name
private readonly BookRepository _bookRepository = null;

// ctor + tab -to make constructor
        // this is constructor
public BookController(BookRepository bookRepository){
// here we are assigning BookRepository class with all its methods to -> _bookRepository
// can acess to BookRepository class methods , after creating an object from BookRepository class -> _bookRepository
//also can use static class in BookRepository class, and have acess to class methods through the class folown by dot and class method
_bookRepository = bookRepository;
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
public IActionResult AddNewBook(bool isSuccess = false, int bookId = 0){

// passing English language as default to our form  -->in return View(model)
var model = new Book(){
    Language = "English"
};


ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

    // by default we passing isSuccess = false to the View page - AddNewBook
    // and create variable int bookId = 1 and by default we passing it to View page -AddNewBook
    ViewBag.IsSuccess = isSuccess;
    ViewBag.BookId = bookId;
    return View(model);
}


  // always use data return type -> Task with async methods 
[HttpPost] //this method works by clicking -add book (posting new book) , POST method
public async Task<IActionResult> AddNewBook(Book book){

    if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
    {
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

// add some custom error messages to your model -> validation-summary
ModelState.AddModelError("","This is my 1st custom error message from BookController");
ModelState.AddModelError("","This is my 2nd custom error message from BookController");

   return View();
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