using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP13_RANDOM
{
   class Program
    {
        static void Main(string[] args)
        {
  
  Random rand = new Random();
  int value;

  while(true){
    value = rand.Next(0,10);
// random variable from 0 -10 (excluding 10) is saved in int value

    Console.WriteLine(value);
    Console.ReadKey();
    // will give random number after pressinga any key
  }


        }
    }
}
