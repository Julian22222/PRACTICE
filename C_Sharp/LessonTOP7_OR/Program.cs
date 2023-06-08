using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP7_OR
{
   class Program
    {
        static void Main(string[] args)
        {
            int money =500;
            int level =10;

            if(money >=500 || level >9){
                // with OR - || , one of the side must be true ,to get true in result
                // with AND -&& , both sides must be true , to get true in result
                System.Console.WriteLine("Welcome to next stage.");
            }else {
                System.Console.WriteLine("You are not passing this level.");
            }
 
        }
    }
}