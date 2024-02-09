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
-(Add New Book)-in NavBar , in Form we have dropdown options (Language and Category) <-- they are coming from database, (they not hardcoded in the View)
-We create new class for dropdown -->Language ,in Data folder
-Create relationship between two tables --> in Books class we put property \_-->public Language(data type) Language(Name)
LanguageId (number of Language)

..............................................................................................................

How to install nuget packages(Entity Framework Core)
In terminal we write:
dotnet add package (PackageName)

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

3. Repository - Class where we keep all logic.
   Connecting with database through -> \_context.Books......
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
4. To create Migrations folder, we write --> dotnet ef migrations add (AnyMigrationsName) -->/creating database with tables, Class proporties converts to table columns. Also, can add new proporties to the table in database
5. To make changes to our database, we write --> dotnet ef database update
6. All migrations commands, we write --> dotnet ef migrations

7. dotnet ef migrations remove -->//to remove some proporties from table

..............................................................................................................

jquery, bootstrap, ajax libraries and their packages can be imported from already installed .NET Core (in wwwwroot -> lib folder) or using CDN (get the libraries from internet)
Example --> <,script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"><,/script> <--(remove comas)

CDN - (stands for )-> Content Delivery Network --> (get the libraries from internet)
benefit of using CDN - it loads the file based on your geography location, increase performance of application
With CDN you can get any library from internet.
Example --> <,script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"><,/script> <--(remove comas)

...............................................................................................................

Partial views - it is simple cshtml file that can be inserted anywhere in any View. It helps to separate (break up) the long code into small parts and insert it with partial views. (Similar to React components - components with certain code , that can be inserted anywhere,simplify the code).Helps to remove duplicate code from app.

How to use it-->
partial view we put in --> Views/Shared/ (NameOfPartialView).cshtml (Example--> Views/Shared/header.cshtml )
In the view where we want to insert that piece of code we put --> <,partial name="header" /> <--(remove coma),
Usually we should put underscore before partial view name.
Partial View is a self closing tag.
also we can pass data,(parameters) to the partial view --> <,partial name="header" model="book"/> <--(remove coma)
or
<,partial name="header" model="new Book()"/> <--(remove coma)

or you can use another option to render our partial view-->
@Html.Partial("header", Model)

...............................................................................................................

ViewComponent - is similar to partial views but much more powerful, don't use model binding, only depend on the data provided when calling into it. It is not part of Http life cycle.

There are 2 files in any ViewComponent:

1. server side file, (location of this file --> at the root of our app-> Components/TopBooksViewComponent.cs). Components folder <-- can have any name. TopBooks <-- can have any name, and is followed by ViewComponent sufix. In this file we creating a class and returning View().
   If we need to connect to database we can do it here --> create variable and through BookRepository get data from database. Then pass this data further --> to cshtml file in Default.cshtml

2. cshtml file, (location of this file --> Views/Shared/Components/TopBooks/Default.cshtml). In Default.cshtml file we write any code that we want to render in our View.

To use that code in our view file we put--> @await Component.InvokeAsync("TopBooks")
Also, we can pass the data, (parameters) to the View Component --> @await Component.InvokeAsync("TopBooks",{data}) or @await Component.InvokeAsync("TopBooks", new{bookId=4, isSort=true})

Also, we can use other method to render our view component on particular file by using tag helpers --> <,vc: top-books></,vc: top-books> <--(remove coma)
VC -->stands for ViewComponent

To use this option, you need to add -->
@addTagHelper *, Project*MVC_BookShop2  
@addTagHelper \*, Project_MVC_BookShop2.Components
in Views/\_ViewImports.cshtml file

also we can pass all parametrs that are required in View component -->
<,vc: top-books book-id="2" is-sort="false"></,vc: top-books> <--(remove coma)

...............................................................................................................

We can combine Partial View and View Component

......................................................................

How to pass data(parameters) in Partial View and View Components-->
Examaple:

@await Component.InvokeAsync("TopBooks", new{count=4 })

or in partial view:

<,vc: top-books count="4" ></,vc: top-books> <--(remove coma)

then in TopBooksViewComponent.cs file we receive this data(parameters)-->

public async Task<,IViewComponentResult> InvokeAsync(int count <--HERE WE HAVE 4){
.......can use this count in here

var books = await \_bookRepository.GetTopBooksAsync(count); <-- then we can pass count further to the Views/Shared/Components/TopBooks/Default.cshtml View file

}

then in Default.cshtml file-->
@model IEnumerable<,Book> <--Here will be 4 books

...................................................................................

Routing

-Is the process of mapping incoming http request (URL) to particular resource (resource is--> controller and action method)

-We can define a unique URL(route) for each resource., All the routes should be unique

When client type in Browser URL and hit enter it goes to the server and URL hit controller. Request contains - URL that we are passing in our browser and type(of our request) -GET,POST,PUT. DELETE

To use routing we need to use 2 middlewares in main file -> Program.cs
2 middlewares:

1. UseRouting();
2. MapControllerRoute();

In old version of ASP.NET was: UseRouting(); and UseEndpoints();

Types of Routing:
-Conventional routing
-Attribute routing(use in app, best and easiest way to use routing in ASP.NET)

Middleware in Program.cs file explaination -->
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

pattern: "{controller=Home}/{action=Index}/{id?}" -->
this a pattern or template how variables in curly brackets will be replaced by other values, but by default controller = Home and action = Index, then Id - is optional, if we pass id it will work if we not pass id it will work as well
If we are not passing any value to controller part and in action part in URL it will take default values.

pattern -> first is nameOfController, second goes actionMethod and Id

pattern -> here we creating an order what values and in what order will be shown in URL

Usually to get for instance AboutUs page we put in URL --> Book/AboutUs
-But we can change the Routing for URL in Program.cs file--> using this
app.MapControllerRoute(
name: "AboutUs",
pattern: "about-us",
defaults: new {controller ="Book" , action= "AboutUs"}
)

Typing in URL -> loacalhost:5167/about-us
It will go to controller= Book and action = AboutUs and will show correct page, the same as you typed in URL -> Book/AboutUs
