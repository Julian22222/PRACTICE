using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTop
{
   internal class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
           int y= 2;

            float result = Convert.ToSingle(x) / y;
// Convert.ToSingle - convert number to float
// to get fractions one of the numbers must be - float type, otherwise 5 /2 = 2

Console.WriteLine(result);
// Console.ReadKey();
        }
    }
}


// Basic data types

// Whole number types:
// byte b; from 0 to 255
// sbyte sb; from -128 to 127
// short s; from -32768 to 32767
// ushort us; from 0 to 65535
// int i; from -2147483648 to 2147483647
// uint ui; from 0 to 4294672295
// long l = long.MaxValue;( we can check what number range is there)
// ulong ul = long.MinValue; ( we can check what number range is there)

// But usually we use int


//  Fractional number types:
//  float - > 5.7f;  (after namber always put f <-means float)
//  double -> 5.7;


//Symbol type
// char c = 'T'  <- alsways use one one symbol in the quotes, if you put - char c = 'tc' (it will give an error)

//String type
// string str = "Hello"

// logical type
// bool bl = true

//arifmetical operators
// + - * / %

// DEVIDING NUMBERS
// 5 / 2 = 2 
// 5.0 /2 =2.5
// 5 / 2.0 = 2.5
// 5 % 2 = 1 <-remaining from devision

//logic operators
// ==  !=  >=  <=  >  <


// dotnet --version
// to run file -> dotnet run
// dotnet new list -> list of all available template projects
// dotnet new console
