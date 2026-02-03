using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP6_IF
{
   class Program
    {
        static void Main(string[] args)
        {
            int age;
            Console.Write("Insert your age:");
            age = Convert.ToInt32(Console.ReadLine());
if(age>=18){
    Console.WriteLine("Welcome to our Bar!");
}else{
 Console.WriteLine("You are too young, entrance is from 18 years old.");
 Console.WriteLine("Come back to us in " + (18 - age) + " years " );
}
 
        }
    }
}