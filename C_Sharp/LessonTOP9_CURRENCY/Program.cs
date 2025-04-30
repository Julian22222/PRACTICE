using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP9_CURRENCY
{
   class Program
    {
        static void Main(string[] args)
        {
        
       float poundsInWallet;
       float eurosInWallet;

       int poundsToEuro = 64, euroToPounds = 66;

       float exchangeCurrencyCount;

       string desiredOperation;

       Console.WriteLine("Welcome to our Currency department");
       Console.Write("Insert your pounds balance");

// converting to float
       poundsInWallet = Convert.ToSingle(Console.ReadLine());

         Console.WriteLine("Please, choose one of the options");
       Console.WriteLine("1 - to exchange pounds to euros.");
       Console.WriteLine("2 - to exchange euros to pounds.");
       Console.Write("Your choice:");
       desiredOperation = Console.ReadLine();

       switch (desiredOperation)
       {
        case "1":
        Console.WriteLine("Exchange Pounds to Euros");
        Console.Write("How many Pounds do you want to exchange?");
        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());
        if(poundsInWallet >= exchangeCurrencyCount){
            poundsInWallet -=exchangeCurrencyCount;
            eurosInWallet +=exchangeCurrencyCount / poundsToEuro;
        }else{
            Console.WriteLine("You don't have enough pounds");
        }
        break;
        case "2":
        Console.WriteLine("Exchange Euros to Pounds");
        Console.Write("How many Euros do you want to exchange?");
         exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());
          if(eurosInWallet >= exchangeCurrencyCount){
            eurosInWallet -=exchangeCurrencyCount;
            poundsInWallet +=exchangeCurrencyCount * euroToPounds;
        }else{
            Console.WriteLine("You don't have enough euros");
        }
        break;
        default:
        Console.WriteLine("Incorrect option, please insert correct option.");
        break;
       }
Console.WriteLine($"Your balance: {poundsInWallet} pounds and {eurosInWallet} euros.");

 
        }
    }
}