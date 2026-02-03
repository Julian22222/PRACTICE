using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP20_BOOKS
{
   class Program
    {
        static void Main(string[] args)
        {
        

bool isOpen = true;

string [,] books = {
  {"Stephen King", "George Orwell","Charles Dickens"},
  {"Jane Austen","Virginia Woolf","J.R.Tolkien"},
  {"Robert Martin", "Jessy Shell","Howard Lovecraft"}
};

while(isOpen){

Console.WriteLine("\nAuthor List: \n");
for ( int i=0; i<books.GetLength(0); i++){
  for (int j =0; j<books.GetLength(1); j++){
Console.Write(books[i,j] + " | ");

  }
  Console.WriteLine();
}
Console.WriteLine("\nLibrary");
Console.WriteLine("1 - find author name by its position.\n2 - find book by author's name.\n3 - exit.");
Console.Write("Enter one of the options:");
switch(Convert.ToInt32(Console.ReadLine())){

case 1:
int line, column;
Console.Write("Insert shelf number: ");
line = Convert.ToInt32(Console.ReadLine()) -1;
Console.Write("Insert column number: ");
column = Convert.ToInt32(Console.ReadLine()) -1;
System.Console.WriteLine($"This is author: {books[line,column]}");


break;

case 2:
string author;
bool authorIsFound = false;
Console.Write("Insert author's name: ");
author = Console.ReadLine();

for ( int i=0; i<books.GetLength(0); i++){
  for (int j =0; j<books.GetLength(1); j++){

if(books[i,j].ToLower() == author.ToLower()){
Console.Write($"Author {books[i,j]} is located on {i+1} shelf and {j+1} position.");
authorIsFound = true;
}
  }
}

// Console.ReadKey();
if(authorIsFound == false){
    Console.WriteLine("This author doesn't exist");
}
break;

case 3:
isOpen = false;
break;

default:
Console.WriteLine("Inccorect command has been insrted");
break;


};
}
if(isOpen){
    System.Console.WriteLine("\nPress any key to cantinue running the programm...");
}
Console.ReadKey();
Console.Clear();
        }
    }
}