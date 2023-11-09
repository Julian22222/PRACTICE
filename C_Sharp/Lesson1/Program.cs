// to use Console (programm). we need to access to console -> console located in System library

// import/use System library in our project (comand)-> using System 
// System is a basic library in .Net

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Lesson1
// name of the folder Lesson1

{
    class Program
    {
        static void Main()
        {
System.Console.WriteLine("Hello");

// standard class example -> can acess to class methods , after creating an object from that class
// showMethod - Is a new object created from MyUsualMethod class
// MyUsualMethod showMethod -> showMethod( Is a type of data we creating, showMethod -Is variable name )
// also can use -> var showMethod (insted of ->  MyUsualMethod showMethod)
MyUsualMethod showMethod = new MyUsualMethod();
Console.WriteLine(showMethod.ShowMyMessege("Julian"));


// static class example -> can acess to class methods through the class 
Console.WriteLine(MyMethod.ShowMSG("Julian"));
        }
    }
}

// Console.WriteLine("Hello, World!");


// dotnet --version
// to run file -> dotnet run
// dotnet new list -> lsit of all available template projects
// dotnet new console