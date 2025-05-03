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
        return basketItems.ToList();
    }

        public int AddToBasket(Book book)
        {
            throw new NotImplementedException();
        }

        // public int AddToBasket(Book book)
        // {
        //     basketItems.Add(book);
        // }



        private static List<Book> basketItems = new List<Book>();

    }  
}