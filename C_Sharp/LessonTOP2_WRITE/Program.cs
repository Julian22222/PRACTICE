using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTop2_WRITE
{
   internal class Program
    {
        static void Main(string[] args)
        {
            int age = 27;
            string name = "Mario";
   
System.Console.WriteLine("Your name is " + name + " and you are " + age +" years old");
System.Console.WriteLine($"Your name is {name} and you are {age} years old");
// The same message
// 2nd option is called intrapolation
        }
    }
}

// CW + Tab --> will make System.Console.WriteLine();