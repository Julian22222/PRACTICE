Different namespaces:

1. @using System.Runtime.InteropServices.WindowsRuntime;

2. @using System; //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");

3. @using System.Collections.Generic; //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.

4. @using System.Linq; //querying any type of data source

5. @using System.Threading.Tasks; //creating new threads for computation, aslo when use async-await operations, and to use Task

6. @using Microsoft.AspNetCore.Mvc; //allow to use Routes , //importing to inherit Controller

7. @using Project_MVC_BookShop2.Repository; //BookRepository connection and methods - GetAllBooks and others

8. @using Project_MVC_BookShop2.Models; //Book class import connection

9. @using Microsoft.AspNetCore.Mvc.Rendering; //to use SelectList, SelectListItem, SelectListGroup, use Html partial views

10. @using Microsoft.AspNetCore.Mvc; //allow to use Routes , //importing to inherit Controller

11. @using Microsoft.EntityFrameworkCore; //Enables .NET developers to work with a database using .NET objects, allow to inherit DbContext
    ////allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

12. @using Microsoft.AspNetCore.Http; //to use IFormFile (special data type to hold information about uploaded files)
    ..............................................................................................................
    In this Project we get and save the data of dropDown to database.
    -(Add New Book)-in NavBar , in Form we have dropdown options (Language and Category) are coming from database, (they not hardcoded in the View)
    -We create new class for dropdown -->Language ,in Data folder
    -Create relationship between two tables --> in Books class we put property \_-->public Language(data type) Language(Name)
    LanguageId (number of Language)

..............................................................................................................

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
8. jQuery.Ajax.Unobtrusive --> jQuery-ajax-unobtrusive library ( client side validation)

..............................................................................................................

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

....................................................................................................................

How to connect your project to SQl Server Database.

1. We create new folder with any name (in our case) Data folder. Inside Data folder we create new class to use it for database --> Books.cs
2. Create BookStoreContext.cs file in Data folder ( data connection to database)
3. After creating Data folder with all needed files and content we can create -MIGRATIONS FOLDER
4. To create Migrations folder --> dotnet ef migrations add <<AnyNameOfMigrations>> -->/creating database with tables, Class proporties converts to table columns. Also, can add new proporties to the table in database
5. To make changes to our database --> dotnet ef database update
6. All migrations commands --> dotnet ef migrations

7. dotnet ef migrations remove -->//to remove some proporties from table

..............................................................................................................

jquery, bootstrap, ajax libraries and their packages can be imported from already installed .NET Core (in wwwwroot -> lib folder) or using CDN (get the libraries from internet)

CDN - (stands for )-> Content Delivery Network
benefit of using CDN - it loads the file based on your geography location, increase performance of application

...............................................................................................................

Partial views - it is simple cshtml file that can be inserted anywhere in any View. It helps to separate (break up) the long code into small parts and insert it with partial views. (Similar to React components - components with certain code , that can be inserted anywhere,simplify the code)

How to use it-->
partial view we put in --> Views/Shared/<NameOfPartialView>.cshtml (Example--> Views/Shared/header.cshtml )
In the view where we want to insert that piece of code we put --> <partial name="header" />  
also we can pass data to the partial view --> <partial name="header" model="book"/> or <partial name="header" model="new Book()"/>

or you can use another option to render our partial view-->
@Html.Partial("header", Model)

...............................................................................................................

ViewComponent - is similar to partial views but much more powerful, don't use model binding, only depend on the data provided when calling into it. It is not part of Http life cycle.

There are 2 files in any ViewComponent:

1. server side file, (location of this file --> at the root of our app-> Components/TopBooksViewComponent.cs). TopBooks <-- can have any name, and is followed by ViewComponent sufix. In this file we creating a class and returning View().
2. cshtml file, (location of this file --> Views/Shared/Components/TopBooks/Default.cshtml). In Default.cshtml file we write any code that we want to render in our View.

To use that code in our view file we put--> @await Component.InvokeAsync("TopBooks")
Also, we can pass the data to the View Component --> @await Component.InvokeAsync("TopBooks",{data}) or @await Component.InvokeAsync("TopBooks", new{bookId=4, isSort=true})

Also, we can use other method to render our view component on particular file by using tag helpers --> <vc: top-books></vc: top-books>
To use this option, you need to add --> (@addTagHelper _, Project_MVC_BookShop2 , @addTagHelper _, Project_MVC_BookShop2.Components ) in Views/\_ViewImports.cshtml

also we can pass all parametrs that are required in View component -->
<vc: top-books book-id="2" is-sort="false"></vc: top-books>
