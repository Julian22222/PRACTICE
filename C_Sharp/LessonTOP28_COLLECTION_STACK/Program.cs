using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP28_COLLECTION_STACK
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
   
        Stack<int> numbers = new Stack<int>();

        numbers.Push(1);  //add objects, elements to our Stack
        numbers.Push(2); 
        numbers.Push(3); 

        Console.WriteLine(

        numbers.Peek()  // to see first element in the Stack -(3)

        );

        numbers.Pop();  //return the value of first element, and delete first element in Stack -(3)

        foreach(var number in numbers){
            Console.WriteLine(number);
        }



        }
    }
}