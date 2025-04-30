using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;

namespace Project_MVC_BookShop.Data
{
    public class Books
    {
        public int Id { get; set; }

        // [Required]
        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }


        // when you put -> ? -> this field is  - not Required
        public string? Category { get; set; }
        //  when you put -> ? -> this field is  - not Required
        public string? Language { get; set; }

        public int TotalPages { get; set; }
    }
}