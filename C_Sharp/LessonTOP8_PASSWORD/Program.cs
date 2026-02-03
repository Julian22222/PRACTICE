using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP8_PASSWORD
{
   class Program
    {
        static void Main(string[] args)
        {
        
        string password = "123";
        string userInput;

        Console.Write("Insert your password");
        userInput = Console.ReadLine();

        if(userInput == password){
            Console.WriteLine("Password has been accepted and access to database is open.");
        }else{
            Console.WriteLine("Password is incorrect, try again.");
        }
 
        }
    }
}