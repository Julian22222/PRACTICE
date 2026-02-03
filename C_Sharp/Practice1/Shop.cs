using System;

namespace Practice1
{
    class Shop
    {

        private List<Shoe> shoes = new List<Shoe>();

        // random price for each pair of shoes
        Random random = new Random();


        // constructor
        public Shop(int shoesNumber){

           

            Console.WriteLine("Please choose the shoes that you want to buy.");
            Console.WriteLine($"You have £ {random.Next(0,20)} in your walet.\n");

           for(int i=0; i < shoesNumber; i++){
            // random price from 5 -20
            shoes.Add(new Shoe(random.Next(5,21)));

        Console.Write(i + 1 + " - ");
            shoes[i].ShowAllInfo();


        }  

    Console.WriteLine(new string('-',30));
    Console.Write("\nPlease insert shoes number to buy: ");
    int ShoeNum = Convert.ToInt32(Console.ReadLine())-1;
    

        }

       

       


        
    }
}