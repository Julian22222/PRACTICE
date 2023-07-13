using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP27_COLLECTION_QUEUE
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
        Queue<string> patients = new Queue<string>();

        patients.Enqueue("Mark");  //add element to the queue
        patients.Enqueue("Alex");
        patients.Enqueue("Kevin");

        patients.Dequeue();  //will give a value of first element, and delete first element -(Mark)

Console.WriteLine(
        patients.Peek()  //will show first element in the queue
);
       
        foreach( var patient in patients){
            Console.WriteLine(patient);
        }





        }
    }
}
