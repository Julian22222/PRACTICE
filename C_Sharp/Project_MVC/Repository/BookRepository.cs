using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_MVC.Models;  //Book class import connection
using Project_MVC.Controllers;   //BookControllers methods connection


namespace Project_MVC.Repository
{

    // all logic is here to get the data from database
    public class BookRepository
    {

        // method GetAllBooks which return List -data type(Book)
        public List<Book> GetAllBooks(){
return DataSource();
        }


  // method GetBookById return Book, Book - data type
        public Book GetBookById(int id){
return DataSource().Where(x=> x.Id ==id).FirstOrDefault();
        }


          // method SearchBook return List -data type(Book)
        public List <Book> SearchBook(string title, string authorName){
// return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }


        // our local application data storage, here we not using data from database
        // method that return a list of books
        private List<Book> DataSource(){
            return new List<Book>(){
                new Book(){Id=1, Title="MVC", Author= "Julian", Description="This is description for MVC", Category="Programming",Language="English",TotalPages=134},
                 new Book(){Id=2, Title="C#", Author= "Bob", Description="This is description for C#", Category="Developer",Language="Hindi",TotalPages=897},
                  new Book(){Id=3, Title="JavaScript", Author= "Julian", Description="This is description for JavaScript", Category="Programming",Language="English",TotalPages=345},
                   new Book(){Id=4, Title="Dot Net Core", Author= "Tom", Description="This is description for .NET CORE", Category="Framework",Language="Chinese",TotalPages=567}
            };
        }
    }
}