using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP5_SHOP
{
   class Program
    {
        static void Main(string[] args)
        {

    int money;
    int food;
    int foodUnitPrice = 10;
    bool isAbleToPay;

    
    System.Console.WriteLine("Welcome to our Shop! Today we sell groceries for" + foodUnitPrice + "coins.");
      System.Console.WriteLine("Insert how many coins you have:");
    money = Convert.ToInt32(Console.ReadLine());
     System.Console.WriteLine("how many items of food do you need?");
    food = Convert.ToInt32(Console.ReadLine());
   
isAbleToPay = money >= food * foodUnitPrice;
// if money is MemoryExtensions than food * foodUnitPrice -it will be true
food *= Convert.ToInt32(isAbleToPay); //true = 1 / false =0, convert to number,

// if you want to buy more food than you have coins ,than you can't buy anything
money -= food *foodUnitPrice;
money-=food * foodUnitPrice;

Console.WriteLine($"You have {food} food items in your bag and {money} coins.");
        }
    }
}