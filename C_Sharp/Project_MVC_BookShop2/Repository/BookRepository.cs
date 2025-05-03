using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;   //Can you Task with async await , and to use Task
using Microsoft.EntityFrameworkCore;  //to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods
using Project_MVC_BookShop2.Models;  //Book class import connection
using Project_MVC_BookShop2.Controllers;   //BookControllers methods connection
using Project_MVC_BookShop2.Data;
using Microsoft.AspNetCore.Mvc;  //import BookstoreContext database and Books class from Data folder

namespace Project_MVC_BookShop2.Repository
{

    // this is a Model(or Class), all logic is here to get the data from database or data source (Here we have data and methods(functions) )
    public class BookRepository
    {

// creating new variable, (new instance) - now we work with database 
    private readonly MyBookStoreWebDbContext _context = null;
    // or we could put(2nd option) -->
    //  private readonly BookStoreContext _context = new BookStoreContext();  //<- assigning database data to - _context

    // or (3rd option)
    // private readonly BookStoreContext _context = null;  // declearing new variable and
    // public BookRepository(){
    //     _context = new BookStoreContext();   //<- assigning database data to - _context
    // }



    // constructor, here we use dependency injection, application will resolve context automatically
    // because we have written the code in our -Program.cs file -> (line 138) -> this line->
    // builder.Services.AddDbContext<BookStoreContext>(); 
    //or builder.Services.AddDbContext<MyBookStoreWebDbContext>();
    public BookRepository(MyBookStoreWebDbContext context){
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
    BookTypeId = model.BookTypeId,

    // if model.TotalPages>HasValue(contains some value) return it value, otherwise return 0
    TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
    
    //full path to uploaded img folder -->(wwwroot/books/cover)
    CoverImageUrl = model.CoverImageUrl,  //<-- we could pass data using parametre / ViewBag and assign to  CoverImageUrl, or with new property as we done it here in this example
    BookPdfUrl = model.BookPdfUrl,
    Price = model.Price,
    CreatedAt = DateTime.UtcNow

    // UpdatedOn = DateTime.UtcNow  <-- will put the local time and date of the user
};

// we add newBook to our database -> _context -> in Books2 table
await _context.Books2.AddAsync(newBook);

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
            var allbooks = await _context.Books2.ToListAsync();
            //(return typy of allbooks is - List<Books> (List of Books)) but return data type of our method(GetAllBooks) is List of Book --> List<Book>
            //Therefore we need to convert it manually, (without using any mapping- other option to convert data)  
            // We convert it manually --> List<Books> (that we receive from database) into List<Book>


            //var allbooks = await _context.Book2.OrderByDescending(p => p.Id).ToListAsync();  <--it will make descending order, reverse the order

            //await _context.Book2.Find()
            //await _context.Book2.OrderBy(c => c.Title).Select(x => new Book(){Id = x.Id, Title = x.Title, ...})

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
                        BookTypeId = book.BookTypeId,  //showing a number 
                        // Language = book.Language.Name, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
                        Price = book.Price,
                        CreatedAt = book.CreatedAt
                    
                    });
                }
            }
    return books;
        }


        //method for TopBooks (View Component)
        //we use this method in Components/ TopBooksViewComponent.cs
        public async Task<List<Book>> GetTopBooksAsync(){
            return await _context.Books2.Select(book => new Book(){
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    BookTypeId = book.BookTypeId,  //showing a number 
                    // Language = book.Language.Name, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
                    Price = book.Price,
                    CreatedAt = book.CreatedAt
            }).Take(2).ToListAsync();  //Take(2) <--taking only 2 Books from the database Books2 table
        }

        public async Task<Book?> GetWeekBook (){   //These method can return null, so we put ? after Book or Book model
            var data = await _context.Books2.ToListAsync();

            if(data?.Any() == true){

                //take random id from the data books 
                var random = new Random();
                var randomId = random.Next(0, data.Count);

                // var book = data.FirstOrDefault(x => x.Id == randomId);
                var book = data[randomId];

                return new Book(){
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    BookTypeId = book.BookTypeId,  //showing a number 
                    // Language = book.Language.Name, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
                    Price = book.Price,
                    CreatedAt = book.CreatedAt
                };
            }

            return null;
        }


  // method GetBookById return Book, Book - data type
        public async Task<Book> GetBookById(int id)
        {

            // var book = await _context.Books2.FindAsync(id);  <--another way how to find correct book by Id

        return await _context.Books2.Where(x=>x.Id ==id).Select(book => new Book(){
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        BookTypeId = book.BookTypeId,
                        BookTypeModel = book.BookType.TypeName, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
                        BookPdfUrl = book.BookPdfUrl,
                        Price = book.Price,
                        CreatedAt = book.CreatedAt
            }).FirstOrDefaultAsync();
// return DataSource().Where(x=> x.Id ==id).FirstOrDefaultAsync();



            // Converting the data manually--> List<Books>(from database) into List<Book>
            

          
        }
      


 public async Task <List<Book>> SearchBook(string title){
   return await _context.Books2.Where(x => x.Title.Contains(title)).Select(book => new Book(){
    Id = book.Id,
    Title = book.Title,
    Author = book.Author,
    Category = book.Category,
    Description = book.Description,
    BookTypeId = book.BookTypeId,
    BookTypeModel = book.BookType.TypeName, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
    TotalPages = book.TotalPages,
    CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
    BookPdfUrl = book.BookPdfUrl, //full path to uploaded img folder -->(wwwroot/books/cover)
    Price = book.Price,
    CreatedAt = book.CreatedAt
 }).ToListAsync(); 
 }


public async Task <Books> EditBook(int id ){  //For Post Method 

//need to get book from DB (Books data type ) without converting it to Book type, because we don't need to show the book but update it in the DB

    return await _context.Books2.Where(x=>x.Id == id).FirstOrDefaultAsync();

    
}



public async Task <Books> DeleteBook (int id){

 return await _context.Books2.Where(x=>x.Id ==id).FirstOrDefaultAsync();

}





          // method SearchBook return List -data type(Book)
        // public List <Book> SearchBook(string title, string authorName){
// return DataSource().Where(x => x.Title == title && x.Author == authorName).ToList();
// return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
// return null;
//         }


        // our local application data storage, here we not using data from database
        // method that return a list of books
        // private List<Book> DataSource(){
        //     return new List<Book>(){
        //         new Book(){Id=1, Title="MVC", Author= "Julian", Description="This is description for MVC", Category="Programming",Language="English",TotalPages=134},
        //          new Book(){Id=2, Title="C#", Author= "Bob", Description="This is description for C#", Category="Developer",Language="Hindi",TotalPages=897},
        //           new Book(){Id=3, Title="JavaScript", Author= "Julian", Description="This is description for JavaScript", Category="Programming",Language="English",TotalPages=345},
        //            new Book(){Id=4, Title="Dot Net Core", Author= "Tom", Description="This is description for .NET CORE", Category="Framework",Language="Chinese",TotalPages=567}
        //     };
        // }
    }
}