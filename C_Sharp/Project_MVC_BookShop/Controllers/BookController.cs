using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  //allow to use Routes , //importing to inherit Controller
using Project_MVC_BookShop.Repository;  //BookRepository connection and methods - GetAllBooks and others
using Project_MVC_BookShop.Models;  //Book class import connection

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


// form Method to add new book
public IActionResult AddNewBook(bool isSuccess = false, int bookId = 1){
    // by default we passing isSuccess = false to the View page - AddNewBook
    // and create variable int bookId = 1 and by default we passing it to View page -AddNewBook
    ViewBag.IsSuccess = isSuccess;
    ViewBag.BookId = bookId;
    return View();
}


  // always use data return type -> Task with async methods 
[HttpPost] //this method works by clicking -add book (posting new book)
public async Task<IActionResult> AddNewBook(Book book){
  int id = await _bookRepository.AddNewBook(book);

if(id > 0){
    // here after pressing form button we redirect to the same page and passing isSuccess = true
    return RedirectToAction("AddNewBook", new{isSuccess = true, bookId = id});
}
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