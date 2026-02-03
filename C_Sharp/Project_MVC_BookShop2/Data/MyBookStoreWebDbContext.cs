using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;  //to use Task with async await , and to use Task
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  // to inherit from IdentityDbContext
using Microsoft.EntityFrameworkCore;   //Enables .NET developers to work with a database using .NET objects, allow to inherit DbContext
////allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

using Project_MVC_BookShop2.Models; // to use ApplicationUser Class

//// --> This file is to use with Web SQL Database in Azure portal

namespace Project_MVC_BookShop2.Data
{

  // this class allow to interact with database

    //we can inherit this class from DbContext class to work with database -->
    //We inherit from DbContext if we not planning to use UserNames, Passwords, LogIn,SignUp,Security functions in our App
    // public class BookStoreContext : DbContext

    //or we can inherit this class from IdentityDbContext class to work with Identity
    //IdentityDbContext class is inherited from DbContext class under the hood

    //  public class BookStoreContext : IdentityDbContext  <--use this code if you are planning to use Usernames, Passwords, LogIn, SignUp in yur App (it has already build in properties), it has standard AspNetUsers table, use this approach if we don't add any extra properties to AspNetUsers table
   

public class MyBookStoreWebDbContext : IdentityDbContext<ApplicationUser>  //<-- use this code if you are planning to use Usernames, Passwords, LogIn, SignUp in your App and planning to ADD some extra properties to AspNetUsers table, will create all needed tables for users and security automatically in our database, we use Class ApplicationUser with added properties
    // BookStoreContext <-- can have any name, and is followed by Context sufix.
{
    
   private readonly IConfiguration _configuration;   //<--using IConfiguration we can have access to appsettings.json and secrets

// constructor , DbContextOptions <-- standard name, MyBookStoreWebDbContext <-- name of database, base(options) <-- inherit from base costructor
//options <-- can be any name, and then pass the same name to --> : base(options)
        public MyBookStoreWebDbContext(DbContextOptions<MyBookStoreWebDbContext> options, IConfiguration configuration) : base(options)
        {
_configuration = configuration;  //<--needs to connects to appsettings.json and secret files
        }



  // this creates Books table in the database
        // name of your table in the database will be -Books2
        //<Books> -> data type(from Data folder), will create columns from Book class proporties
        public DbSet<Books> Books2 {get; set; }


         // this creates Language table in the database, this table is needed for dropdown language options in Form
        // name of your table will be -Language
        //<Language> -> data type(from Data folder), will create columns from Language class proporties
        public DbSet<BookType> BookType {get;set;}





// DbContext connection with database, there is 2 ways,
//first way - > we can configure all the detail over here, in this DbContext class (we write--> override onc + Tab)
//second way -> we can define the same connection string in main file ->Program.cs, and remove all -protected override void OnConfiguring metod from here
       
       
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {


///////////////////////////////// different options to connect to our database
            // optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");  <-- if you use Windows Authentication --> Integrated Security=True;

            //  optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Trusted_Connection=True;");

            // if we use remote database we --> optionsBuilder.UseSqlServer("Server= api of your database here;Database=NameOfYourDatabase;TrustServerCertificate=true;User ID=sa(userName);Password=julik3322J!(Password for database)"  (this is for SQL Authentication)

            //working Connection string, not using data from appsettings.json, Server=.; <--local server, for SQL authentication we include User name and password over here
            //   optionsBuilder.UseSqlServer("Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!");

              //Reading Connection String from appsettings.json file as always , (we create variable to Configuration and in constructor assign variables)
            //   optionsBuilder.UseSqlServer(_configuration["ConnectionStrings : DefaultConnection"]);

            //Reading Connection String from appsettings.json file, when you using EntityFrameworkCore we can write--> (2nd option)
        //     optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
/////////////////////////////////////////


        ///here we use Manager Secrets from --> secrets.json file (to connect to database)
        // optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:WebConnection"]);

        //     base.OnConfiguring(optionsBuilder);
        // }


}

}