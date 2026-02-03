using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;  //Can you Task with async await , and to use Task

using Microsoft.EntityFrameworkCore;   //Enables .NET developers to work with a database using .NET objects, allow to inherit DbContext
////allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

namespace Project_MVC_BookShop.Data
{

    // this class allow to interact with database

    // inherit this class from DbContext class
    public class BookStoreContext : DbContext
    {

// constructor
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        

        // this creates Books table in the database
        // name of your table will be -Books
        //<Books> -> data type(from Data folder), will create columns from Book class proporties
        public DbSet<Books> Books {get; set; }





// DbContext connection with database, there is 2 ways
//first way - > we can configure all the detail over here, in this DbContext class (we write--> override onc + Tab)
//second way -> we can define the same connection string in main file ->Program.cs, and remove all -protected override void OnConfiguring metod from here
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");  <-- if you use Windows Authentication

            //  optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Trusted_Connection=True;");

            // if we use remote database we --> optionsBuilder.UseSqlServer("Server= api of your database here;Database=NameOfYourDatabase;TrustServerCertificate=true;User ID=sa(userName);Password=julik3322J!(Password for database)"  (this is for SQL Authentication)

              optionsBuilder.UseSqlServer("Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!");
            base.OnConfiguring(optionsBuilder);
        }




    }
}