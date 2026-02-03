using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP22_FUNCTION
{
   internal class Program
    {
        static void Main(string[] args)
        {

       int x =3, y = 8, myFunction;

    //    assign function Sum to variable -myFunction
       myFunction = Sum(x,y);

       Console.WriteLine(myFunction);
    //    the same output 
        Console.WriteLine(Sum(x,y));
        }

        // functions declaration write outside of Main block
        static int Sum(int x, int y){
            int sum;
            sum = x + y;
            return sum;

        }
    }
}
