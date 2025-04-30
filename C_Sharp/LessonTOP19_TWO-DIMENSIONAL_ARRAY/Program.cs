using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP19_TWO_DIMENSIONAL_ARRAY
{
   class Program
    {
        static void Main(string[] args)
        {
        

    // different options of array recording 

    // Array initialization with name -array
      int [,] array = {{2,3},{4,6}};

      int [,] array2 = new int[2,3];

      int[,] array3 ={ 
        {2,3,4},
        {4,5,1},
        {7,8,9}};
 
//  new int [2,3] - 2 lines, 3 elements in each line
      int [,] array4 = new int[2, 3]{
        {9,8,7},
        {6,5,4}
      };

Console.WriteLine(array4[0,1]);  //8
Console.WriteLine(array4.Length); //6

// looping through two-demention array
for ( int i=0; i<array3.GetLength(0); i++){
  for (int j =0; j<array3.GetLength(1); j++){
Console.WriteLine(array3[i,j]);
  }
}

/// <summary>
/// ///////////////////////////////string array
/// </summary>
bool isOpen = true;

string [,] books = {
  {"Stephen King", "George Orwell","Charles Dickens"},
  {"Jane Austen","Virginia Woolf","J.R.Tolkien"},
  {"Robert Martin", "Jessy Shell","Howard Lovecraft"}
};

while(isOpen){
System.Console.WriteLine("Author List: ");
for ( int i=0; i<books.GetLength(0); i++){
  for (int j =0; j<books.GetLength(1); j++){
Console.WriteLine(books[i,j]);
Console.ReadKey();
  }
}
}


        }
    }
}
