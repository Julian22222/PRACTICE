using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP17_ARRAY3
{
   class Program
    {
        static void Main(string[] args)
        {

//  find max number in array
int [] array = {1,3,5,8,12,2,25};

// assign min value to the maxElement variable
int maxElement = int.MinValue;

for( int i=0; i <array.Length; i++){
    if(maxElement < array[i]){
        maxElement = array[i];
    }
}
    System.Console.WriteLine(maxElement);


        }
    }
}