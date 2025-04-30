using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP23_FUNCTION
{
   internal class Program
    {
        static void Main(string[] args)
        {

    System.Console.WriteLine("Hello");

    // invoke a function WriteError with 2 parametrs
    WriteError("No connection to the internet.", ConsoleColor.Red);
    System.Console.WriteLine("Check your connection.");

     // invoke a function WriteError with 1 parametr
    WriteError("Router not found.");
        }

        // functions declaration write outside of Main block
        // ConsoleColor color = ConsoleColor.Red  <- It is default color ,if there is not color passed to WriteError function
        static void WriteError(string text, ConsoleColor color = ConsoleColor.Red){
            // assign default color to the cariable defaultColor
            ConsoleColor defaultColor = Console.ForegroundColor;

            // color that we receiving we assign to the variable
            Console.ForegroundColor = color;

            // showing the text that we receiving
            Console.WriteLine(text);

            // return console color to default color 
            Console.ForegroundColor = defaultColor;
     

        }
    }
}