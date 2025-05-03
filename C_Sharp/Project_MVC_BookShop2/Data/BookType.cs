using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;

namespace Project_MVC_BookShop2.Data
{
    public class BookType
    {
        
// proporties-> colums in Language Table

        public int Id { get; set; } //Primary key

        public string TypeName { get; set; }

        public string Description { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}