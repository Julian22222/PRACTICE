// using System.Security.AccessControl;
// using System;
// using System.Collections.Generic;
// using System.Text;
// // using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

// using Project_MVC.Models.Domain; 


// namespace Project_MVC.Data
// {
//     // public class ApplicationDbContext : IdentityDbContext
//      public class ApplicationDbContext : DbContext
//     {
     
//     public DbSet<Icon> IconsTable { get; set; }
//     public DbSet<Tag> TagsTable { get; set; }


// public ApplicationDbContext(){
    
// }
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//         {

//         }
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
// optionsBuilder.UseSqlite("Filename=./confbarrel.db");
//     }
//     }
// }



///////////////////////
///

// MyappDbContext.cs   --file name

// using Microsoft.EntityFrameworkCore;  //import installed nuget package, DbContext
// using Project_MVC.Data;
// using Project_MVC.Models.Domain;  //import Icon class

// namespace Project_MVC.Data
// {
//     // DbContext class - is a bridge between models and database
//     // BdContext class - responsible for interacting with database, performing CRUD operations on our database tables using Entity Framework Core(nuget packages)

//     // Nuget packages to install->  dotnet add package Microsoft.EntityFrameworkCore.Tools (in terminal)
//     // Microsoft.EntityFrameworkCore.Tools 
//     // Microsoft.EntityFrameworkCore.SqlServer
//     public class MyappDbContext : DbContext
//     {
        
//         // constructor
//         public MyappDbContext(DbContextOptions options) : base(options)
//         {

//         }


//         // entities, or tables that EntityFrameworkCore will create inside our database
//         public DbSet<Icon> IconsTable { get; set; }
//         public DbSet<Tag> TagsTable { get; set; }
//     }

// //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// //     {
// // optionsBuilder.UseSqlite("Filename=./confbarrel.db");
// //     }
// }