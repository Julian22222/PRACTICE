using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project_MVC.Data
{
    public class BookStoreContext : DbContext
    {

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        
        // name of your table will be -Books
        public DbSet<Books> Books {get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");

            //  optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Trusted_Connection=True;");

              optionsBuilder.UseSqlServer("Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!");
            base.OnConfiguring(optionsBuilder);
        }




    }
}