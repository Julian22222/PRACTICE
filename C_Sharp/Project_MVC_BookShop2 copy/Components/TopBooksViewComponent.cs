using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  //will allow to inherit from ViewComponent
using Project_MVC_BookShop2.Repository;    //BookRepository connection and methods - GetTopBooksAsync


namespace Project_MVC_BookShop2.Components
{
    public class TopBooksViewComponent : ViewComponent   //inherit from ViewComponent, ///ViewComponent <--must be always in the end of name of our class
    //TopBooksViewComponent <-- can have any name, and is followed by ViewComponent sufix.
    {

        // to get data from database we need to create variable here to interact with Database through BookRepository
        //we don't interact with database in controller but in TopBooksViewComponents.cs class (this file)
        private readonly BookRepository _bookRepository;  //creating new variable, to work with BookRepository class


        //constructor
        public TopBooksViewComponent(BookRepository bookRepository)
        {

            // constructor, here we use dependency injection, application will resolve context automatically
            // because we have written the code in our -Program.cs file -> (line 39) -> this line->
            // builder.Services.AddScoped<BookRepository, BookRepository>();
            _bookRepository = bookRepository; 
        }


        // GetTopBooksAsync
        //InvokeAsync method
        // <IViewComponentResult>  <-- return data type, we use only this data type in View Component
        public async Task<IViewComponentResult> InvokeAsync(){

        //interacting with our database here using GetTopBooksAsync method from BookRepository.cs
        var books = await _bookRepository.GetTopBooksAsync();

                
            return View(books);  //passing data (2 books) from database to Views/Shared/Components/TopBooks/Default.cshtml View file
        }
    }
}
