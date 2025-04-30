using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP10_PASSWORD
{
   class Program
    {
        static void Main(string[] args)
        {
        
       int triesCount =3;
       string password = "123";
       string userInput;

       for(int i = 0; i < triesCount; i++){
        Console.Write("Insert your password: ");
        userInput = Console.ReadLine();

        if(userInput == password){
            Console.WriteLine("Password is correct!");
            break;
        }else{
            Console.WriteLine("Password is incorrect!");
            Console.WriteLine("You have left " + (triesCount -(i +1)) + " attempts.");
        }
       }

 
        }
    }
}