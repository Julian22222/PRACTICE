using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   //Can you Task with async await , and to use Task
using Microsoft.EntityFrameworkCore;  //to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods
using Project_MVC_BookShop.Models;  //Book class import connection
using Project_MVC_BookShop.Controllers;   //BookControllers methods connection
using Project_MVC_BookShop.Data;  //import BookstoreContext database and Books class from Data folder

namespace Project_MVC_BookShop.Repository
{

    // this is a Model(or Class), all logic is here to get the data from database or data source (Here we have data and methods(functions) )
    public class BookRepository
    {

// creating new variable, (new instance) - now we work with database 
    private readonly BookStoreContext _context = null;
    // or we could put(2nd option) -->
    //  private readonly BookStoreContext _context = new BookStoreContext();  //<- assigning database data to - _context

    // or (3rd option)
    // private readonly BookStoreContext _context = null;  // declearing new variable and
    // public BookRepository(){
    //     _context = new BookStoreContext();   //<- assigning database data to - _context
    // }



    // constructor, here we use dependency injection, application will resolve context automatically
    // because we have written the code in our -Program.cs file -> (line 39) -> this line->
    // builder.Services.AddDbContext<BookStoreContext>();
    public BookRepository(BookStoreContext context){
        _context = context;
    }
// using _context -> we have access to all data from database


// new method to add new book  from field form to data base
// always use data type -> Task with async methods 
// model coming from BookController [HttpPost] method
public async Task<int> AddNewBook(Book model){


// new varible
var newBook = new Books(){
// assign all proporties from received model(data from form) to our proporties in the table
// id -will be assign to it automatically to newBook object
    Title = model.Title,
    Author = model.Author,
    Description = model.Description,
    Category = model.Category,
    Language = model.Language,

    // if model.TotalPages>HasValue(contains some value) return it value, otherwise return 0
    TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0
};

// we add newBook to our database -> _context -> in Books table
await _context.Books.AddAsync(newBook);

// then we need save changes, otherwise application won't hit the database ( async method)
await _context.SaveChangesAsync();

return newBook.Id;
    }


        // method GetAllBooks which return List -data type(Book)
         // always use type -> Task with async methods 
        public async Task<List<Book>> GetAllBooks(){

            // creating new variable with List<Book> data type, 
            //we will assign all data from database to this list (convert the data)
            var books = new List<Book>();
            
            // getting all the books from our database -> Books table, 
            //to get all books --> ToList() or ToListAsync()
            var allbooks = await _context.Books.ToListAsync();
            //(return typy of allbooks is - List<Books> (List of Books)) but return data type of our method(GetAllBooks) is List of Book --> List<Book>
            //Therefore we need to convert it manually, (without using any mapping- other option to convert data)  
            // We convert it manually --> List<Books> (that we receive from database) into List<Book>

            // if there is any value in the data(in the Books table) then we do this code
            if(allbooks?.Any() == true){
                foreach (var book in allbooks){
                    books.Add(new Book(){
                        //assign each property of this book model to our proporties(Id,Title,Author,Category etc.)
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Language = book.Language,
                        TotalPages = book.TotalPages
                    });
                }
            }
    return books;
        }


  // method GetBookById return Book, Book - data type
        public async Task<Book> GetBookById(int id){

        var book = await _context.Books.FindAsync(id);
// return DataSource().Where(x=> x.Id ==id).FirstOrDefaultAsync();

// if book not null -> do this code
        if(book != null){

            // Converting the data manually--> List<Books>(from database) into List<Book>
            var bookDetails = new Book(){
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Language = book.Language,
                        TotalPages = book.TotalPages
            };
            return bookDetails;
        }
        return null;
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