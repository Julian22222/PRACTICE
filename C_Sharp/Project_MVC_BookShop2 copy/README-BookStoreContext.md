```C#

using System.Collections.Immutable;
using System;
using System.IO;
using dotenv.net;
using Microsoft.Extensions.Hosting;

using System.Security.AccessControl;
using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;  //to use Task with async await , and to use Task
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  // to inherit from IdentityDbContext
using Microsoft.EntityFrameworkCore;   //Enables .NET developers to work with a database using .NET objects, allow to inherit DbContext
////allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

using Project_MVC_BookShop2.Models;

//// --> This file is to use with localhost Database

namespace Project_MVC_BookShop2.Data
{




    // this class allow to interact with database

    //we can inherit this class from DbContext class to work with database -->
    // public class BookStoreContext : DbContext

    //or we can inherit this class from IdentityDbContext class to work with Identity
    //but IdentityDbContext class is inherited from DbContext class

    //  public class BookStoreContext : IdentityDbContext  <--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table
     public class BookStoreContext : IdentityDbContext<ApplicationUser>  //will create all needed tables for users and security automatically in our database, use Class ApplicationUser with added properties
    // BookStoreContext <-- can have any name, and is followed by Context sufix.
    {

        private readonly IConfiguration _configuration;

// constructor
        public BookStoreContext(DbContextOptions<BookStoreContext> options, IConfiguration configuration) : base(options)
        {
_configuration = configuration;


        }


        // this creates Books table in the database
        // name of your table will be -Books2
        //<Books> -> data type(from Data folder), will create columns from Book class proporties
        public DbSet<Books> Books2 {get; set; }


         // this creates Language table in the database, this table is needed for dropdown language options in Form
        // name of your table will be -Language
        //<Language> -> data type(from Data folder), will create columns from Language class proporties
        public DbSet<Language> Language {get;set;}





// DbContext connection with database, there is 2 ways
//first way - > we can configure all the detail over here, in this DbContext class (we write--> override onc + Tab)
//second way -> we can define the same connection string in main file ->Program.cs, and remove all -protected override void OnConfiguring metod from here



        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {



            ////// optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");  <-- if you use Windows Authentication

           ///////  optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Trusted_Connection=True;");

            /////// if we use remote database we --> optionsBuilder.UseSqlServer("Server= api of your database here;Database=NameOfYourDatabase;TrustServerCertificate=true;User ID=sa(userName);Password=julik3322J!(Password for database)"  (this is for SQL Authentication)

            ////////working Connection string, not using data from appsettings.json
           ////////   optionsBuilder.UseSqlServer("Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!");

              //////Reading Connection String from appsettings.json file as always , (we create create variable fo Configuration and in constructor assign variables)
            /////   optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);

            //Reading Connection String from appsettings.json file, when you using EntityFrameworkCore we can write--> (2nd option)
            /////// optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));



        //       optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);

        //     base.OnConfiguring(optionsBuilder);
        // }



```
