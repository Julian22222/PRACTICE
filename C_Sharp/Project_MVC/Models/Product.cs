namespace Project_MVC.Models
{
    public class Product
    {
        // variable declaration
        public int Id {get;set;}
        public string Name {get;set;}
        public string Img {get;set;}
        public double Price {get;set;}


        // constructor
        public Product(int id, string name, string img, double price){
            Id = id;
            Name = name;
            Img = img;
            Price = price;
        }

        //method
        // public static void addProduct(double productPrice,float money){
        // Console.WriteLine("Hello");
        // //  return money-productPrice;
        // }

         // public double addProduct(double productPrice,float money){
    //      return money-productPrice;
    //     }
        
    }
}