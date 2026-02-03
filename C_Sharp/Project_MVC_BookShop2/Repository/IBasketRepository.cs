using Project_MVC_BookShop2.Models;

namespace Project_MVC_BookShop2.Repository
{
    public interface IBasketRepository
{
    Task<List<Book?>> GetBasketItems();
    int AddToBasket(Book book);

    int RemoveFromBasket(int bookId);

}
}