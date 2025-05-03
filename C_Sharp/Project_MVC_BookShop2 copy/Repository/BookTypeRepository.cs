using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   //Can you Task with async await , and to use Task
using Microsoft.EntityFrameworkCore;  //to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods
using Project_MVC_BookShop2.Models;  //Book class import connection
using Project_MVC_BookShop2.Controllers;   //BookControllers methods connection
using Project_MVC_BookShop2.Data;  //import BookstoreContext database and Books class from Data folder

namespace Project_MVC_BookShop2.Repository
{

    // this is a Model(or Class), all logic is here to get the data from database or data source (Here we have data and methods(functions) )
    public class BookTypeRepository : IBookTypeRepository  //inherit from ILanguageRepository (interface)
    {

// creating new variable, (new instance) - now we work with database 
    private readonly MyBookStoreWebDbContext _context;
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
        public BookTypeRepository(MyBookStoreWebDbContext context){
        _context = context;
    }
// using _context -> we have access to all data from database


    public async Task<List<BookTypeModel>> GetBookTypes(){
       return await _context.BookType.Select(x=> new BookTypeModel(){ //converting _context.BookType -> to BookTypeModel data type
            Id = x.Id,
            Description = x.Description,
            TypeName = x.TypeName
        }).ToListAsync();
    }

    }
}