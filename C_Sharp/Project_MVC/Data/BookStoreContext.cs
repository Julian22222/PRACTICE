using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  //creating new threads for computation, aslo when use async-await operations
using Microsoft.EntityFrameworkCore; //to inherit DbContext

namespace Project_MVC.Data
{
    public class BookStoreContext : DbContext
    {

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        
        // name of your table will be -Books
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