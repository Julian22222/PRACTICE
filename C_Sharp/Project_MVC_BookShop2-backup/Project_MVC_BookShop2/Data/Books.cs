using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;

namespace Project_MVC_BookShop2.Data
{
    public class Books
    {
        public int Id { get; set; }

        // [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }


        // when you put -> ? -> this field is  - not Required
        // public string? Category { get; set; }
        public string Category { get; set; }
        //  when you put -> ? -> this field is  - not Required
        public int BookTypeId { get; set; }  //store Id of our each booktype

        public int TotalPages { get; set; }

        // property to store uploaded img file  - path, from wwwroot folder ,(Not full path -->not serverFolder from BookController)
        public string CoverImageUrl { get; set; }
        

        //property to store uploaded pdf file - full path starting from wwwroot folder
         public string BookPdfUrl { get; set; }

         public decimal Price { get; set; }

         public DateTime CreatedAt { get; set; }

// first Language ->data type
//second Language ->Name of property
        public BookType BookType { get; set; } //we create relationship between Books2 table and BookType table

    }
}