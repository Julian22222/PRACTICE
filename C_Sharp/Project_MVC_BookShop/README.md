Project where Form -- in (Add New Book)-NavBar , we have dropdown options (Language and Category) are hardcoded in the View, (they not coming from database)

............................................................................................................................................................

how to install nuget packages(Entity Framework Core)
In terminal we write:
dotnet add package <<PackageName>>

PackageNames:

1. Microsoft.EntityFrameworkCore -->///basic package
2. Microsoft.EntityFrameworkCore.Relational -->///to work with ralational database
3. Microsoft.EntityFrameworkCore.SqlServer -->///to work with Sql server
4. Microsoft.EntityFrameworkCore.Tools -->///to write queries to database
5. Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation -->///update automaticaly ViewEngine
6. Microsoft.EntityFrameworkCore.Design -->///this package needs to create migrations folder
7. Microsoft.EntityFrameworkCore.Tools.DotNet

...................................................................................................................................................................................................................................................................................
Main Locations in different folder

1. Data folder - we keep all data for database here.

2. Data / BookStoreContext - this class allow to interact with database, set up for connection to database. Also, here we create tables in databse.
   BookStoreContext ////BookStore <--can be any name, this is a name of our database
   BookStoreContext ////Context <--must be always in the end of name of our class

3. Repository - Class where we keep all logic. Connecting with database through -> \_context.Books......
   \_context <--database connection
   .Books <--table name in our database
   Repository is a place where you can get, post,edit,delete the data in database.
   We use Repository class methods in BookController

4. In View , when we fill Form (it is completing throug Model class -from Models folder) , then in BookRepository we convert Model data to Database Model(Model class from Data folder)

...................................................................................................................................................................................................................................................................................
How to connect your project to SQl Server Database.

1. We create new folder with any name (in our case) Data folder. Inside Data folder we create new class to use it for database --> Books.cs
2. Create BookStoreContext.cs file in Data folder ( data connection to database)
3. After creating Data folder with all needed files and content we can create -MIGRATIONS FOLDER
4. To create Migrations folder --> dotnet ef migrations add <<AnyNameOfMigrations>> -->/creating database with tables, Class proporties converts to table columns.
5. dotnet ef database update -->/to make changes to our database
6. All migrations commands --> dotnet ef migrations

7. dotnet ef migrations remove -->//to remove some proporties from table
