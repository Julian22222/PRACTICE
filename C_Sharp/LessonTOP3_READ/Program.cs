using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTop3_READ
{
   internal class Program
    {
        static void Main(string[] args)
        {
           string name;
        //    Console.WriteLine("Insert your name:"); //move cursor to next line
        // Console.Write("Insert your name:"); // the cursor will stay on the same line
           Console.Write("Insert your name:");
           name = Console.ReadLine();
        //    Read the line that we write and assign it to name
            Console.WriteLine($"Your name {name}");
Console.WriteLine("Insert your age:");
int age = Convert.ToInt32(Console.ReadLine());
// Convert.ToInt32(Console.ReadLine()) - Convert string to number, because -> int age
Console.WriteLine($"You are : {age} years old!");

        }
    }
}