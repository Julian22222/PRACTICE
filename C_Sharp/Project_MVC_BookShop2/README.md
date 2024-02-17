## Overview of this Project

In this Project we get and save the data of dropDown to database.
-(Add New Book)-in NavBar , in Form we have dropdown options (Language and Category) <-- they are coming from database, (they not hardcoded in the View)
-We create new class for dropdown -->Language ,in Data folder
-Create relationship between two tables --> in Books class we put property \_-->public Language(data type) Language(Name)
LanguageId (number of Language)

..............................................................................................................

# Different namespaces:

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

# How to install nuget packages(Entity Framework Core)

In terminal we write:
dotnet add package (PackageName)

PackageNames:

1. ```bash
   Microsoft.EntityFrameworkCore
   ```

   -->///this is basic package

2. ```bash
   Microsoft.EntityFrameworkCore.Relational
   ```

   -->///package to work with ralational database

3. ```bash
   Microsoft.EntityFrameworkCore.SqlServer
   ```

   -->///package to work with Sql server

4. ```bash
   Microsoft.EntityFrameworkCore.Tools
   ```

-->///package to write queries to database

5. ```bash
   Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
   ```

-->///package to update automaticaly ViewEngine

6. ```bash
   Microsoft.EntityFrameworkCore.Design
   ```

-->///this package needs to create migrations folder

7. ```bash
   Microsoft.EntityFrameworkCore.Tools.DotNet
   ```

8. ```bash
   jQuery.Ajax.Unobtrusive
   ```

--> package to use jQuery-ajax-unobtrusive library ( client side validation),not needed for to install for VScode

..............................................................................................................

# Main Locations in different folder

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

.................................................................................................................................................................

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

.........................................................................................................................................................................................

ROUTING

-Is the process of mapping incoming http request (URL) to particular resource (resource is--> controller and action method)

-We can define a unique URL(route) for each resource., All the routes should be unique

When client type in Browser URL and hit enter it goes to the server and URL hit controller. Request contains - URL that we are passing in our browser and type(of our request) -GET,POST,PUT. DELETE

/
/
/
To use routing we need to use 2 middlewares in main file -> Program.cs
2 middlewares:

1. UseRouting();
2. MapControllerRoute();

In old version of ASP.NET was: UseRouting(); and UseEndpoints();

/
/
/
Types of Routing:

1. Conventional routing
2. Attribute routing(use in app, best and easiest way to use routing in ASP.NET)

Middleware in Program.cs file explaination -->
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

pattern: "{controller=Home}/{action=Index}/{id?}" -->
this a pattern or template how variables in curly brackets will be replaced by other values, but by default controller = Home and action = Index, then Id - is optional, if we pass id it will work if we not pass id it will work as well
If we are not passing any value to controller part and in action part in URL it will take default values.

pattern -> first is nameOfController, second goes actionMethod and Id

pattern -> here we creating an order what values and in what order will be shown in URL

1. This method is Conventional Routing
   Usually to get for instance AboutUs page we put in URL --> Book/AboutUs
   -But we can change the Routing for URL in Program.cs file--> using this
   app.MapControllerRoute(
   name: "AboutUs",
   pattern: "about-us",
   defaults: new {controller ="Book" , action= "AboutUs"}
   )

Typing in URL -> loacalhost:5167/about-us
It will go to controller= Book and action = AboutUs and will show correct page, the same as you typed in URL -> Book/AboutUs

2. This method is Attribute Routing (we using it in controller), Easy way to make orchange routing

[Route("about-us")] //Attribute routing (best and easy way to make new Route to this resource)
public IActionResult AboutUs()
{
return View();
}

    inserting in URL --> localhost:5167/about-us will gives us View page AboutUs

/
/
To pass some parametrs in route we add --> /{ParametrName}:
[Route("about-us/{id?}")]
public IActionResult AboutUs( int id)
{
return View();
}

/
/
Route constraints (For example: [Route("about-us/{id?}") <-- id must be a number only not a string or other data type])

1. in Routing constraints you can define the type of your parameters --> [Route("about-us/{id? : int}")]
   id: int <-- id will definately must be am int ( data types: string,decimal,float and other)

2. in Routing constraints you can define the Length --> [Route("about-us/{id : int : min(1)}")]
   id : int : min(1) <-- id must be int, and min id is 1 (2 constraints together)

3. in Routing constraints you can define Alpha --> [Route("about-us/{name : alpha})]
   name : alpha <-- name has only alphabets (letters only)
   name : alpha : minlength(5) <-- name has letters only and min length is 5

4. in Routing constraints you can define Regex --> [Route("about-us/{name : regex( regexCodeHere)})]

5. in Routing constraints you can define Required
6. All constraints that are available in ASP.Net Core in
   https://github.com/dotnet/aspnetcore/tree/main/src/Http/Routing/src/Constraints

................................................................................................................................

DEPENDENCY INJECTION

Our Repository classes and Context class must be used with Dependency injection !!!!!!!!

We can use Dependency injection with iterfaces and without interfaces.
-->see BookRepository (without interface)
-->see LanguageRepository - ILanguageRepository (interface)

-If we not useing Dependency incection we can have some problems between Controllers and Repositories(Services).
When you make some changes in Repository data using controller or other files where this Repository was used the data can't be changed in all files (you neeed update all the files where you used this Repository ).

-if we use the Repository(Service) in different controllers or files and we need to make some changes in Repository data from one of the files then it will be allowed to do that. and it will be changed in all the files.

/
/
/

-if we not using Dependency injection we put ->
private readonly BookStoreContext \_context = new BookStoreContext();

or

private readonly BookStoreContext \_context = null; <--creating new variable, to work with some class

//then in constructor
public BookRepository(){
\_context = new BookStoreContext(); //<- assigning database data to - \_context
}

(this new object can be created in constructor or in action method)

/
/

Services lifetime:
-Transient(AddTransient<>) <-- A new instance of the service will be created every time it is requested.Every time when you use this service or call this service then new instance will be created

-Scoped (AddScoped<>) <-- These are created once per client request. instance will be the same for all http request.

-Singleton (AddSingleton<>) <--Same instance for all entire application.

.......................................................................

Dependency injection in View (chtml)

It is not mandotory to pass Repository class to controller (then we create an object from that Repository class in controller) and then pass this created object from Repository class to View.
We can straight away pass only Interfacese from Repository class to View (create in View object from Repository class) and then use it in View
--> see AboutUs View file. (Can't pass BookRepository class because it doesn't have own Interface) --> can pass only Interfaces!!!.

...........................................................................................................................................................

appsettings.json file

How to get any data from appsettings.json in our application using configuration service

We can have many appsettings.json files in our app

/
/
Here we store:

1. ConnectionString
2. Server Link
3. Application Name
4. We NOT keeping here secret passwords or passcodes!!!

appsettings.json file <-- not dependent to any environment, it is common to every environment

appsettings.Development.json <-- if we will create this file, we will overwrite some settings for Development environment. It will be dependent only to Development environment.

appsettings.Production.json <-- if we will create this file, we will overwrite some settings for Production environment. It will be dependent only to Production environment.

-->see ContactUs.cshtml and HomeController

/
/
IHostEnvironment.Production (User Secrets file) <-- we use for passwords

/
/

/

To read data from appsettings.json file we need to use IConfuguration service
Use Dependenci Injection to register the IConfuguration services and to use the IConfuguration services

IConfiguration is registred automaticaly by ASP.Net Core framework

/ /
/ /
We can access to appsetiings in Controller,Repository or straigh away from View file

1. If we want to access the appsettings.json file in Controller, Repository or any other file apart from View file:
   To use In Controller --->

using Microsoft.Extensions.Configuration; //needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file

private readonly IConfiguration configuration; <-- create variable for Cofiguration

//controller
public HomeController(IConfiguration \_configuration){
configuration = \_configuration; <--assign \_configuration to configuration
}

configuration["KeyOfAppSettingsData "] <-- now we can read the data from appsettings.json file

2. accessing appsetings.json in View file
   @inject Microsoft.Extensions.Configuration.IConfiguration \_configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).

<p>@_configuration["Name"]</p>

<p>@_configuration["infoObj:key1"]</p>

................................................................................................................................................................................................................

Accessing appsettings.json using GetValue method
Whith this approach we can define the data type that we accessing in appsettings.json --> it can be boolean, string and other (with previous options we always get string data type from appsettings.json file)
--> see Contactus.cshtml and HomeController.cs

1.  To use GetValue in Controller--> we use:
    using Microsoft.Extensions.Configuration;
    then create configuration variable and with controller use Dependency injection. then ->
    var test = configuration.GetValue<bool>("DisplayNewBookAlert"); <-- we can indicate what data type we are receiving - <bool>

2.  To use GetValue in View file --> we use:
    these two lines below are needed to use GetValue in View file, ( to get data from appsettings.json)

@using Microsoft.Extensions.Configuration
@inject IConfiguration \_configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).

Then we use-->

<p>@(_configuration.GetValue<bool>("DisplayNewBookAlert"))</p>

/........................................................................................................................................................

Different options how to read ConnectionString from appsetttings.json --> see BookStoreContext.cs file

...........................................................................................................................................................................................................

Read configuration using GetSection method from appsettings.json

```

```

```

```

```

```

```

```

```

```

```

```
