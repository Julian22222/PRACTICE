using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_MVC_BookShop2.Data;
using Project_MVC_BookShop2.Models;


namespace Project_MVC_BookShop2.Repository
{

    public class BasketRepository : IBasketRepository
    {


    public async Task<List<Book?>> GetBasketItems()
    {
        Console.WriteLine($"Returning {basketItems.Count} items from basket");
        return basketItems.ToList();
    }

    public int AddToBasket(Book book)
    {
        basketItems.Add(book);
        
        Console.WriteLine($"Added book {book.Id} - basket now has {basketItems.Count} items");
        return book.Id;
    }

    public int RemoveFromBasket(int bookId)
    {
        var book = basketItems.FirstOrDefault(b => b.Id == bookId);
        if (book != null)
        {
            basketItems.Remove(book);
            Console.WriteLine($"Removed book {bookId} - basket now has {basketItems.Count} items");
            return bookId;
        }
        else
        {
            Console.WriteLine($"Book {bookId} not found in basket");
            return -1;
        }
    }


   



    private static List<Book> basketItems = new List<Book>();

    }  
}