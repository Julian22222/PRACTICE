using Microsoft.EntityFrameworkCore;  //import installed nuget package
using Project_MVC.Models.Domain;  //import Icon class

namespace Project_MVC.Data
{
    // DbContext class - is a bridge between models and database
    // BdContext class - responsible for interacting with database, performing CRUD operations on our database tables using Entity Framework Core(nuget packages)

    // Nuget packages to install->  dotnet add package Microsoft.EntityFrameworkCore.Tools (in terminal)
    // Microsoft.EntityFrameworkCore.Tools 
    // Microsoft.EntityFrameworkCore.SqlServer
    public class MyappDbContext : DbContext
    {
        
        // constructor
        public MyappDbContext(DbContextOptions options) : base(options)
        {

        }


        // entities, or tables that EntityFrameworkCore will create inside our database
        public DbSet<Icon> IconsTable { get; set; }
        public DbSet<Tag> TagsTable { get; set; }
    }
}