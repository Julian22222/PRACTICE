using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  //will allow to inherit from ViewComponent
using Project_MVC_BookShop2.Repository;    //BookRepository connection and methods - GetTopBooksAsync



namespace Project_MVC_BookShop2.Components;

public class SearchBooksViewComponent : ViewComponent
{
private readonly BookRepository _bookRepository;  //creating new variable, to work with BookRepository class

public SearchBooksViewComponent(BookRepository bookRepository)
{

    // constructor, here we use dependency injection, application will resolve context automatically
    // because we have written the code in our -Program.cs file -> (line 39) -> this line->
    // builder.Services.AddScoped<BookRepository, BookRepository>();
    _bookRepository = bookRepository; 
}

   public async Task<IViewComponentResult> InvokeAsync(string title){

        //interacting with our database here using GetTopBooksAsync method from BookRepository.cs
          var books = await _bookRepository.SearchBook(title);

            
            return View(books);  //passing data (2 books) from database to Views/Shared/Components/TopBooks/Default.cshtml View file
        }

}
