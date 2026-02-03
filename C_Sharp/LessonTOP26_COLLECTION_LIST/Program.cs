using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP26_COLLECTION_LIST 
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
        // 5 is size of of the LIST
        // List<int> numbers = new List<int>(5);
        // numbers[0] = 1;

        // List<int> numbers = new List<int>(); <- is epty List, without any elements inside

        List<int> numbers = new List<int>(){1,2,3,4};
        // function Add , will add object to the end of the List 
        numbers.Add(5); // add 1 element to the List
        numbers.Add(6);
        numbers.Add(7);
        numbers.Add(8);
        numbers.Add(9);
        
        numbers.AddRange(new int[] {3,4,6,8,2,1} ); //add this numbers at the same time

        numbers.RemoveAt(3);  //removes element by its index

        numbers.Remove(5);  // removes firs found element by matching 

        // numbers.Clear();  //will delete all data from the List

        numbers.IndexOf(22);  // find out index of element in the List, (find index of 22)

        numbers.Insert(1, 555);  // will insert element 555 on index position 1, and shiftind ather elements in the list

        numbers.Sort();  //will sort all numbers
        
        // numbers.Reverse();  will reverse the numbers

        for( int i=0; i<numbers.Count; i++){
            Console.WriteLine(numbers[i]);
        }





        }
    }
}
