using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   //creating new threads for computation, aslo when use async-await operations

namespace Project_MVC.Data
{
    public class Books
    {
        public int Id { get; set; }

        // [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }

        public int TotalPages { get; set; }
    }
}