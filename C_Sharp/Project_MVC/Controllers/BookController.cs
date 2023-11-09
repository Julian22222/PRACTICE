using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project_MVC.Repository;  //BookRepository connection and methods - GetAllBooks and others
using Project_MVC.Models;  //Book class import connection

// using System.Web.Mvc; 


namespace Project_MVC.Controllers
{
    public class BookController : Controller
    {

// define type of this variable, data type - BookRepository. (template)
// _bookRepository -> variable name
private readonly BookRepository _bookRepository = null;

// ctor + tab -to make constructor
        // this is constructor
        public BookController(){
// here we are assigning BookRepository class with all its methods to -> _bookRepository
// can acess to BookRepository class methods , after creating an object from BookRepository class -> _bookRepository
//also can use static class in BookRepository class, and have acess to class methods through the class folown by dot and class method
_bookRepository = new BookRepository();
        }



        
// // this example return all data Array from dataSource
// public List <Book> GetAllBooks(){
//     return _bookRepository.GetAllBooks();
// }

public IActionResult GetAllBooks(){
    var data = _bookRepository.GetAllBooks();

    return View(data); //passing the data to the View
}




// // this example return full book data depanding from id
// public Book GetBook(int id){
//     return _bookRepository.GetBookById(id);
// }

public IActionResult GetBook(int id){  //returning a View - that means it should be - IActionResult / or ViewResult
   var data = _bookRepository.GetBookById(id);

    return View(data); //passing the data to the View
}

public List<Book> SearchBook(string title, string authorName){
    return _bookRepository.SearchBook(title,authorName);
}


// form Method to add new book
public IActionResult AddNewBook(){
    return View();
}


[HttpPost]
public IActionResult AddNewBook(Book book){
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