using System;

namespace Lesson2
{
    class Program
    {
        static void Main()
        {
// to save values in different variables, you need to stick with this template -> (data type) (variable name) = value
// for variable names always use -> CamelCase
string myString = "Hey Julian";

// ALso, we can rewrite our variable 
//or put -> const , unchable variable -> const string myString = "Hey Julian"
// But it always must be a string , because from begining we put string <-data type, we can't put any other data types, when we rewrite it

myString = "Hey C Sharp";

Console.WriteLine(myString);
        }
    }
}

// C Sharp -is object oriented language, all data is an object in C Sharp


// Basic data types:
// Int - number ( for example 5)
// Double - a fractional number (for example 5.23)
// String - "Hello World"
// Char - Symbol (for example ,can put only one symbol in the quotes -> 'a' )
// Bool - logical yes / no ( for example true/false)
//
// Examples:
// int num = 5;
// double money = 5.23;
// string myString = "Hello C Sharp";
// char sim = 's';
// bool isCorrect = true;



// dotnet --version
// to run file -> dotnet run
// dotnet new list -> list of all available template projects
// dotnet new console