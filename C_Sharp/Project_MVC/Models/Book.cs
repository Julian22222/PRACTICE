// using System.IO;
// using System.Security.AccessControl;
// using System.ComponentModel.DataAnnotations;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;   //querying any type of data source
using System.Threading.Tasks;


namespace Project_MVC.Models
{
    public class Book
    {
        // [Key] //automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
        public int Id { get; set; }

        // [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }

        public int TotalPages { get; set; }

       

       




        // constructor
        // public Book()
        // {
            
        // }
        // prop + tab  --> quick btns for get,set
        //ctor + tab --> constructor


    }
}