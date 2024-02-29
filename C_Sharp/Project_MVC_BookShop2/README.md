## Overview of this Project

In this Project we have options where we get data from controller and get data from database for dropDown menu when we adding a book.

-Add New Book --> in NavBar , we are filling the Form and we have dropdown options:

- Language dropdown menu <-- is coming from database, (they not hardcoded in the View)
- Category dropdown menu <-- is coming from Controller (BookController), -hardcoded options

- We create new class for dropdown -->Language ,in Data folder and in Models folder
- -We Create connection, relationship between two tables - Books table and language table in Data folder--> (--> See Data/Language.cs file)

in Books class we put property-->public Language(data type) Language(Name)
LanguageId (number of Language)

- For each table in databse we create Class in Data folder

- Each table has their own Repository file with the logic related to that table

- Everithing you change in Classes in Data folder (Database) --> we need to update the database using:

1. ```bash
   dotnet ef migrations add (AnyMigrationsName) //to add changes to database
   ```

2. ```bash
      dotnet ef database update  //to update database
   ```

- Server side validation is written in Model class/ Model folder

  ..............................................................................................................

# Install new .Net version to work in certain project

As example:

- before starting a project with .NET 7.0 we need to install .NET 7.0 in terminal
- If we want to start working on project which was created with NET 8.0 , we need to install .NET 8.0 and uninstal .NET 7.0 from the system

1. ```C#
      sudo apt remove 'dotnet*' 'aspnet*' 'netstandard*'
   ```

   -->//Uninstall all .NET package

2. ```C#
   sudo rm -f /etc/apt/sources.list.d/microsoft-prod.list
   ```

-->///Remove the Microsoft repository

3. ```C#
   sudo apt install dotnet-sdk-7.0
   ```

-->///Install .NET (which should pull down the SDK and the runtime from the Ubuntu repository)

- Also,you can install multiple versions of .Net SDK using dotnet binary packages
- ```C#
  dotnet --list-sdk //will show all installed .Net versions on your computer. And then you can switch between these versions.
  ```

### To install nuget packages with different version , not the latest version we put: (EXMAPLE)

```C#
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0
```

..............................................................................................................

# To Deploy ASP.Net web application in Azure

1. go to --> [Azure](https://portal.azure.com/)

2. install Azure App Service extension

3. ```C
   dotnet publish -c Release -o ./bin/Publish
   ```

4. Right click the bin\Publish folder and select Deploy to Web App...

5. Once the deployment is finished, click Browse Website to validate the deployment

..............................................................................................................

# Different namespaces:

1. ```bash
   @using System.Runtime.InteropServices.WindowsRuntime;
   ```

````

2. ```bash
      @using System;
   ```

   -->///using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");

3. ```bash
   @using System.Collections.Generic;
   ```

   -->///allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.

4. ```C#
   @using System.Linq;
   ```

   -->///querying any type of data source

5. ```C#
   @using System.Threading.Tasks;
   ```

   -->///creating new threads for computation, aslo when use async-await operations, and to use Task

6. ```C#
   @using Microsoft.AspNetCore.Mvc;
   ```

   -->///allow to use Routes , //importing to inherit Controller

7. ```C#
   @using Project_MVC_BookShop2.Repository;
   ```

   -->//BookRepository connection and methods - GetAllBooks and others

8. ```C#
   @using Project_MVC_BookShop2.Models;
   ```

   -->//Book class import connection

9. ```C#
   @using Microsoft.AspNetCore.Mvc.Rendering;
   ```

   -->///to use SelectList, SelectListItem, SelectListGroup, use Html partial views

10. ```C#
     @using Microsoft.AspNetCore.Mvc;
    ```

    -->//allow to use Routes , //importing to inherit Controller

11. ```C#
    @using Microsoft.EntityFrameworkCore;
    ```

--> Enables .NET developers to work with a database using .NET objects, allow to inherit DbContext
--> allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

12. ```C#
    @using Microsoft.AspNetCore.Http;
    ```

-->to use IFormFile (special data type to hold information about uploaded files)

13. ```C#
     using Microsoft.Extensions.Configuration;
    ```

    -->//needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file

14. ```C#
    @using Microsoft.Extensions.Configuration //we use these 2 lines of code to access appsettings.json in View (import configuration, needs to use IConfiguration service)
    @inject IConfiguration _configuration  //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).
    ```

15. ```C#
    @inject Microsoft.Extensions.Configuration.IConfiguration _configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).
    ```

16. ```C#
     using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    ```

    -->// to inherit from IdentityDbContext, in BookStoreContext.cs file

..............................................................................................................

# How to install nuget packages(Entity Framework Core)

In terminal we write:
dotnet add package (PackageName)

### PackageNames:

1. ```bash
   Microsoft.EntityFrameworkCore
   ```

-->/// this is basic package

2. ```bash
   Microsoft.EntityFrameworkCore.Relational
   ```

   -->/// package to work with ralational database

3. ```bash
   Microsoft.EntityFrameworkCore.SqlServer
   ```

   -->/// package to work with Sql server

4. ```bash
   Microsoft.EntityFrameworkCore.Tools
   ```

   -->/// package to write queries to database

5. ```bash
   Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
   ```

   -->/// package to update automaticaly ViewEngine

6. ```bash
   Microsoft.EntityFrameworkCore.Design
   ```

   -->/// this package needs to create migrations folder

7. ```bash
   Microsoft.EntityFrameworkCore.Tools.DotNet
   ```

8. ```bash
   Microsoft.aspnetcore.identity.entityframeworkcore
   ```

-->///this package needs to work with Identity core (LogIn,SignUp, Password etc.)

9. ```bash
   jQuery.Ajax.Unobtrusive
   ```

-->/// package to use jQuery-ajax-unobtrusive library ( client side validation),not needed for to install for VScode

10. ```bash
    Microsoft.aspnetcore.identity.entityframeworkcore
    ```

-->/// package to use Identity Core (Authentication, Authorisation, SignIn, SignOut etc.)

..............................................................................................................

# Main Locations in different folder

- Data folder --> we keep all data for database here.

- Data / BookStoreContext --> this class allow to interact with database, set up for connection to database. Also, here we create tables in databse.
  BookStoreContext ////BookStore <--can be any name, this is a name of our database
  BookStoreContext ////Context <--must be always in the end of name of our class

- Repository --> Class where we keep all logic.
  Connecting with database through ->

```c#
_context.Books......

_context  // <-- database connection

.Books // <--table name in our database

```

Repository is a place where you can get, post,edit,delete the data in database.
We use Repository class methods in BookController

- In View , when we fill Form (it is completing through Model class -from Models folder) , then in BookRepository we convert Model data to Database Model(Model class from Data folder)

- Components folder --> we use for ViewComponent
  Also we use Views/Shared folder to display ViewComponent View

- Properties / launchSettings.json --> here we keep global settings of our app, here we have PORT, URL of our website (local ports). We have 2 PORTS -if one is engaged application will use second PORT.
  we can change App Environment here:

1. Development
2. Production
3. Staging

- Shared / \_Layout.cshtml --> Here we put common code for all pages. (It is a template, basic layout - these elements will be shown on all pages.)
  Aslo, here we have all meta tags, css links, bootstrap, js links connections.

- wwwroot folder --> here we have CSS, JS, Img folders <--all extras that we want to show to our user. Also we have different frameworks - Bootstrap, jQuery, JS libraries

- Views / \_ViewImports.cshtml --> here we can connect additional libraries and tag helpers which will be added to all View pages

- ....................................................................................................................

# How data travel through the files

### When we addidng some Data to the database using form (--> From AddnewBook.cshtml)

1. Field data from the form go to controller, acion method --> (--> See BookController.cs)

```C#
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method (this is Attribute routing)
public async Task<IActionResult> AddNewBook(Book book){  //<--book is data from the form

///code ...

}
```

2. The we pass data from controller action method to Repository --> (--> See BookController.cs)

```C#
[HttpPost]
public async Task<IActionResult> AddNewBook(Book book){

///code ...

int id = await _bookRepository.AddNewBook(book);  //<-- using Repository method we pass data from the form further down to the Repository

}
```

3. In Repository we interact with database (-->See BookRepository.cs)

```C#
var newBook = new Books(){
// assign all proporties from received model(data from form) to our proporties in the table
// id -will be assign to it automatically to newBook object
    Title = model.Title,
    Author = model.Author,
    Description = model.Description,
    Category = model.Category,
    LanguageId = model.LanguageId,

    // if model.TotalPages>HasValue(contains some value) return it value, otherwise return 0
    TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,

    //full path to uploaded img folder -->(wwwroot/books/cover)
    CoverImageUrl = model.CoverImageUrl,
    BookPdfUrl = model.BookPdfUrl
};

// we add newBook to our database -> _context -> in Books table
await _context.Books2.AddAsync(newBook);

// then we need save changes, otherwise application won't hit the database ( async method)
await _context.SaveChangesAsync();

return newBook.Id;
    }
```

##### Also, when we want to use form to send the data to database we write:

1. in View -->

```C#
<form method="post" >

...other html tags

</form>

```

2. In Controller, action method -->

```C#
[HttpPost]  //<--above needed action method (attribute)
public async Task<IActionResult> AddNewBook(Book book){...}
```

### When we passing id from URL tobthe database to find element (--> From BookController.cs)

1. id comes from URL to controller, action method -->
   (example) -> /book/getbook/1

2. Then we passing id to Repository to interact with database

```C#
public async Task<IActionResult> GetBook (int id){  //returning a View - that means it should be - IActionResult / or ViewResult
 var data = await _bookRepository.GetBookById(id);  //passing id to Repository

  return View(data); //passing the data to the View
}
```

3. In Repository we interact with database (-->See BookRepository.cs)

```C#
     public async Task<Book> GetBookById(int id)
        {
        return await _context.Books2.Where(x=>x.Id ==id).Select(book => new Book(){
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name, //you can Use JOINT or if you created relationship, then we can use navigation property(we can get)
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,  //full path to uploaded img folder -->(wwwroot/books/cover)
                        BookPdfUrl = book.BookPdfUrl
            }).FirstOrDefaultAsync();
        }
```

....................................................................................................................

# How to make a link reference from View file to certain action method in Controller

1. use href

```C#
  <a class="nav-link text-dark" href="/book/GetAllBooks">All Books</a>

  //book <--name of controller
  //GetAllBooks <--action method
```

2. use tag helpers

```C#

<a class="btn btn-primary" asp-controller="Account" asp-action="Signup">Register</a>
// asp-controller="Account"  <-- name of controller
// asp-action="Signup"  <--action method
```

....................................................................................................................

# How to connect your project to SQl Server Database.

1. We create new folder with any name (in our case) Data folder. Inside Data folder we create new class to use it for database --> Books.cs
2. Create BookStoreContext.cs file in Data folder ( data connection to database)
3. After creating Data folder with all needed files and content we can create --> MIGRATIONS FOLDER
4. To create Migrations folder, we write -->

```C#
# creating database with tables, Class proporties converts to table columns.
# Also, can add new proporties to the table in database
# use this command when add something in Classes in Data folder(Database)

dotnet ef migrations add (AnyMigrationsName)  //add changes and create databases with tables

```

5. To make update to our database, we write -->

```C#

dotnet ef database update

```

6. All migrations commands, we write -->

```c#

dotnet ef migrations

```

7. To remove some proporties from table

```c#

dotnet ef migrations remove

```

..............................................................................................................

# How to use different Libraries (jQuery, Bootstrap, Ajax libraries)

jquery, bootstrap, ajax libraries and their packages can be imported from already installed(build up) .NET Core (in wwwwroot -> lib folder) or using CDN (get the libraries from internet --> using special links)

Example:

```c#
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
```

- CDN - (stands for )-> Content Delivery Network --> (get the libraries from internet)
  benefit of using CDN - it loads the file based on your geography location, increase performance of application
  With CDN you can get any library from internet.

Example:

```C#
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
```

(-->See \_Layout.cshtml file)

...............................................................................................................

# Partial views (Similar to React JS components)

- it is simple cshtml file that can be inserted anywhere in any View.
- It helps to separate (break up) the long code into small parts and insert it with partial views. (Similar to React components - components with certain code , that can be inserted anywhere,simplify the code).
- Helps to remove duplicate code from app.

How to use it-->
partial view we put in --> Views/Shared/ (NameOfPartialView).cshtml (Example--> Views/Shared/header.cshtml )
In the view where we want to insert that piece of code we put -->
(-->See \_Layout.cshtml file)

```C#
<partial name="header" />
```

- Usually we should put underscore before partial view name.
- Partial View is a self closing tag.
- Also we can pass data,(parameters) to the partial view -->

```C#
<partial name="partialViewName" model="book"/>
```

or

```C#
<partial name="partialViewName" model="new Book()"/>
```

or you can use another option to render our partial view-->

```C#
@Html.Partial("partialViewName", Model)
```

.................................................................................................................................................................

# ViewComponent

- is similar to partial views but much more powerful, don't use model binding, only depend on the data provided when calling into it. It is not part of Http life cycle.

There are 2 files in any ViewComponent:

1. server side file, (location of this file --> at the root of our app-> Components/TopBooksViewComponent.cs).

- Components folder <-- can have any name.
- TopBooks <-- can have any name, and is followed by ViewComponent sufix. In this file we creating a class and returning View().
- If we need to connect to database we can do it here --> create variable and through BookRepository get data from database. Then pass this data further --> to cshtml file in Default.cshtml

2. then in cshtml file, (location of this file --> Views/Shared/Components/TopBooks/Default.cshtml). In Default.cshtml file we write any code that we want to render in our View.

To use that code in our view file we put--> (See Views/Home/Index.cahtml)

```C#
@await Component.InvokeAsync("TopBooks")
```

Also, we can pass the data, (parameters) to the View Component -->

```C
@await Component.InvokeAsync("TopBooks",{data})
```

or

```C#
@await Component.InvokeAsync("TopBooks", new{bookId=4, isSort=true})
```

- Also, we can use other method to render our view component in particular file by using tag helpers -->

```C#
<vc: top-books></vc: top-books>
```

VC -->stands for ViewComponent

To use this option, you need to add --> in Views/\_ViewImports.cshtml file

```C#
@addTagHelper *, Project*MVC_BookShop2
@addTagHelper *, Project_MVC_BookShop2.Components
```

also we can pass all parametrs that are required in View component -->

```C#
<vc: top-books book-id="2" is-sort="false"></vc: top-books>
```

...............................................................................................................

## We can combine Partial View and View Component

......................................................................

# How to pass data(parameters) in Partial View and View Components

Examaple: (we write this code in View file)

```C#
@await Component.InvokeAsync("TopBooks", new{count=4 })
```

or in partial view:

```C#
<vc: top-books count="4" ></vc: top-books>
```

- then in TopBooksViewComponent.cs file we receive this data(parameters)-->

```C#

private readonly BookRepository _bookRepository;  //creating new variable, to work with BookRepository class

#int count <-- here we will receive 4
public async Task<IViewComponentResult> InvokeAsync(int count){
......can use this count in here

# then we can pass count further to the Views/Shared/Components/TopBooks/Default.cshtml View file
//interacting with our database here using GetTopBooksAsync method from BookRepository.cs
var books = await _bookRepository.GetTopBooksAsync(count)

return View(count)

}

```

- then in Default.cshtml file we can receive "count"-->

```C#
#Here will be 4 books
@model IEnumerable<Book>
```

.........................................................................................................................................................................................

# ROUTING

- It Is the process of mapping incoming http request (URL) to particular resource (resource is--> controller and action method)

- We can define a unique URL(route) for each resource., All the routes should be unique

When client type in Browser URL and hit enter it goes to the server and URL hit controller. Request contains - URL that we are passing in our browser and type(of our request) -GET,POST,PUT, DELETE

### To use routing we need to use 2 middlewares in main file -> Program.cs

2 middlewares:

1. UseRouting();
2. MapControllerRoute();

- In old version of ASP.NET was: UseRouting(); and UseEndpoints();

### Types of Routing:

1. Conventional routing
2. Attribute routing(use in app, best and easiest way to use routing in ASP.NET)

### Middleware in Program.cs file explanation -->

```C#
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
```

- pattern: "{controller=Home}/{action=Index}/{id?}" -->
  this a pattern or template how variables in curly brackets will be replaced by other values, but by default controller = Home and action = Index, then id - is optional, if we pass id it will work if we not passing id it will work as well
- If we are not passing any value to controller part and in action part in URL it will take default values.

- pattern of routing -> first is nameOfController, second goes actionMethod and Id

- pattern -> here we creating an order what values and in what order will be shown in URL

#### Types of Routing:

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

2. This method is Attribute Routing (we using it in controller), Easy way to make or change routing

```C
[Route("about-us")] //Attribute routing (best and easy way to make new Route to this resource)
public IActionResult AboutUs()
{
return View();
}
```

Inserting in URL --> localhost:5167/about-us will gives us View page AboutUs

To pass some parametrs in route we add --> /{ParametrName}:

```C#
[Route("about-us/{id?}")]
public IActionResult AboutUs( int id)
{
return View();
}
```

# Route constraints

Route constraints (For example: [Route("about-us/{id?}") <-- id must be a number only not a string or other data type])

1. in Routing constraints you can define the type of your parameters -->

```C
[Route("about-us/{id? : int}")]
```

id: int <-- id will definately must be am int ( data types: string,decimal,float and other)

2. in Routing constraints you can define the Length -->

```C#
[Route("about-us/{id : int : min(1)}")]
```

id : int : min(1) <-- id must be int, and min id is 1 (2 constraints together)

3. in Routing constraints you can define Alpha -->

```C#
[Route("about-us/{name : alpha}")]
```

name : alpha <-- name has only alphabets (letters only)
name : alpha : minlength(5) <-- name has letters only and min length is 5

4. in Routing constraints you can define Regex -->

```C#
[Route("about-us/{name : regex( regexCodeHere)}")]
```

5. in Routing constraints you can define Required
6. All constraints that are available in ASP.Net Core in
   https://github.com/dotnet/aspnetcore/tree/main/src/Http/Routing/src/Constraints

................................................................................................................................

# DEPENDENCY INJECTION

- Our Repository classes and Context class must be used with Dependency injection !!!!!!!!

- We can use Dependency injection with iterfaces and without interfaces.
  -->see BookRepository (without interface)
  -->see LanguageRepository - ILanguageRepository (interface)

- If we not useing Dependency incection we can have some problems between Controllers and Repositories(Services). When you make some changes in Repository data using controller or other files where this Repository was used the data can't be changed in all files (you neeed update all the files where you used this Repository ).

- But using Dependency Injections, if we use the Repository(Service) in different controllers or files and we need to make some changes in Repository data in one of the files then it will be allowed to do that. and it will be changed in all the files.

-if we not using Dependency injection we put ->

```C#

private readonly BookStoreContext _context = new BookStoreContext(); //create variable and assign it straightaway with new class, (creating new object -_context, based from BookStoreContext class)

or

private readonly BookStoreContext _context = null;  //creating new variable, to work with some class(BookStoreContext class)




public BookRepository(){  //In CONSTRUCTOR, we assigning database data to _context variable
_context = new BookStoreContext();
}

//(this new object can be created in constructor or in action method)

```

### Services lifetime:

- Transient(AddTransient<>) <-- A new instance of the service will be created every time it is requested.Every time when you use this service or call this service then new instance will be created

- Scoped (AddScoped<>) <-- These are created once per client request. instance will be the same for all http request.

- Singleton (AddSingleton<>) <--Same instance for all entire application. (When you change something you need to stop application, and rerun it againg to apply new changes)

...................................................................................................

### Dependency injection in View (chtml)

It is not mandotory to pass Repository class to controller (then we create an object from that Repository class in controller) and then pass this created object from Repository class to View.
We can straight away pass only Interfacese from Repository class to View (create in View object from Repository class) and then use it in View
--> see AboutUs View file. (Can't pass BookRepository class because it doesn't have own Interface) --> can pass only Interfaces!!!.

...........................................................................................................................................................

# How to read the data from different variables in appsettings.json file

- Using configuration service allow us to get get any data from appsettings.json in our application
- We can have many appsettings.json files in our app

### In appsettings.json we can keep :

- ConnectionString
- Server Link
- Application Name
- We NOT keeping here secret passwords or passcodes!!! ( IHostEnvironment.Production (User Secrets file) <-- we use for passwords )

* appsettings.json file <-- not dependent to any environment, it is common to every environment

* appsettings.Development.json <-- if we will create this file, we will overwrite some settings for Development environment. It will be dependent only to Development environment.
* appsettings.Production.json <-- if we will create this file, we will overwrite some settings for Production environment. It will be dependent only to Production environment.

To read data from appsettings.json file we need to use IConfuguration service
Use Dependency Injection to register the IConfuguration services and to use the IConfuguration services

IConfiguration is registred automaticaly by ASP.Net Core framework

/ /
We can access to appsetiings in Controller,Repository or straigh away from View file
-->see ContactUs.cshtml and HomeController

1. If we want to access the appsettings.json file in Controller, Repository or any other file apart from View file:

To use In Controller --->

```bash
using Microsoft.Extensions.Configuration;
```

-->// using this namespace needs to use IConfiguration service, to read appsettings.json file in Controller or any file apart from View file

```C#
# IN CONTROLLER ,

private readonly IConfiguration _configuration; //we create variable for Configuration

public HomeController(IConfiguration configuration){
_configuration = configuration;  //and assign _configuration to configuration in constructor
}


var result = _configuration["KeyOfAppSettingsData"];  //now we can read the data from appsettings.json file in Controller action method

```

2. accessing appsetings.json in View file
   @inject Microsoft.Extensions.Configuration.IConfiguration \_configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).

<p>@_configuration["Name"]</p>

<p>@_configuration["infoObj:key1"]</p>

................................................................................................................................................................................................................

# Accessing appsettings.json using GetValue method

Whith this approach we can define the data type that we accessing in appsettings.json --> it can be boolean, string and other (with previous options we always get string data type from appsettings.json file)
--> see Contactus.cshtml and HomeController.cs

1.  To use GetValue in Controller--> we use:

```C#
using Microsoft.Extensions.Configuration;
```

    then create configuration variable and with controller use Dependency injection. then ->

```C#
var test = configuration.GetValue<bool>("DisplayNewBookAlert");
```

<-- we can indicate what data type we are receiving - <bool> , in example above

2.  To use GetValue in View file --> we use:
    these two lines below are needed to use GetValue in View file, ( to get data from appsettings.json)

```C#

@using Microsoft.Extensions.Configuration

@inject IConfiguration _configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).

```

Then we use-->

```C#
<p>@(_configuration.GetValue<bool>("DisplayNewBookAlert"))</p>
```

........................................................................................................................................................

### Different options how to read ConnectionString from appsetttings.json --> see BookStoreContext.cs file

...........................................................................................................................................................................................................

# Read configuration using GetSection method from appsettings.json

We use this when in appsettings.json file we have an object -->

```C#
"infoObj": {
    "key1": "value 1",
    "key2": "value 2",
    "key3": {
      "key 3 obj 1": "value 3 object"
    },
    "key4": "value 4"
  }
```

and we need to use many values from this appsettings.json object

```C#
configuration.GetValue<string>("infoObj:key1"); //to receive -value 1
configuration.GetValue<string>("infoObj:key2"); //to receive -value 3
configuration.GetValue<string>("infoObj:key3"); //to receive -value 3 object
configuration.GetValue<string>("infoObj:key4"); //to receive -value 4
```

- In example above we use - "infoObj" all the time
  We don't want to write the same code (Do not repeat yourself), to get the values from appsettings.json-->
  Then we use GetSection method

```C#
var newBook = configuration.GetSection("infoObj"); //passing the name of my key
//all the keys of "infoObj" will be available in newBook

//then we use new variable --> newBook
newBook.getValue<string>("key1");  //we don't write -"infoObj" anymore, will receive -value 1, as a result

```

..............................................................................................................

# Binding Configuration to objects using Bind method appsettings.json

- this allow to bind all proporties of some object in appsettings.json file to a particular model class

* allow appsettings.json object to bind a particular Model obect

How to bind two objects: (--> See appsetings.json, HomeController and Model/AlertConfig.cs)

1. We create new Model class, the property names and data type in new Model class must be the same as in appsettings.json object

2. Then we can Bind objects In Controller action method-->

```C#
var newBookAlert = new AlertConfig(); //create new object using AlertConfig class model and assigning it to new variable -> newBookAlert
configuration.Bind("NewBookAlertObj", newBookAlert);  //In Bind method we pass 2 parametrs - > first (key of object in appsettings.json), second (object of instance - just created new object -> it is a Model that we created to bind with appsettings.json object)


bool isDisplay = newBookAlert.DisplayNewBookAlert; //now we can acces all the properties in appsetiings.json of NewBookAlertObj object using newBookAlert variable
```

3. Or we can Bind objects In View -->

```C#
//we put on the top of the file
@{
    var newBook = new AlertConfig();
    _configuration.Bind("NewBookAlertObj", newBook);
}

//and then we use newBook - to access all properties of NewBookAlertObj from appsettings.json file
<p>@newBook.DisplayNewBookAlert</p>

```

..............................................................................................................

# Identity Core ( responsible for LogIn, SignUp, Passwords, Registration, security in the app and other features)

- It is a universal framework to provide security to any .NET app(can be used Blazor,Razor, ASP.NET CORE MVC and other framework that are available in .NET)
- Common framework for all .NET app
- It is NOT only limited to SignUp / SignIn but provide lots of feature that are requires for security management
- this framework provides all required tables to work with Authentication and Authorisation automaticaly, we don't need to create any extra table by ourself, everething is created automaticaly

Identity core features:

- Common framework for all .NET app
- All required tables are generate automaticaly
- Register a user
- Login
- Change Password
- Forgot Password
- User validation (check is it valid user or not)
- Password validation
- Password hashing
- Multi factor authentication (Ex - 2f authentication) <--user will be locked out if he is entering wrong password
- Lockout (Block user on "n" wrong attemts), we can block user after 'n' attemts of entering password
- External Identity (Google,Facebook, Microsoft, Twiter etc.), LogIn with Google account
- And much more

To start working with Identity Core framework we need to install --> (and all other dependent packages will install automatically in our app)

```C#
Microsoft.aspnetcore.identity.entityframeworkcore
```

### To connect Identity core package we need:

1. In Program.cs file , (line 111)-->

```C#
app.UseAuthentication();  //enable authentication connection using middleware, to use passwords ,LogIn,SignUp etc.
```

2. In Program.cs file , (line 40)-->

```C#
   builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>(); //to work With Identity Core we need to configure Identity to work with database
```

- AddIdentity<IdentityUser,IdentityRole>()
- IdentityUser <--is a table that already build in Identity framework, to work with a user we insert this table
- identityRole <--is a table that already buildIn in Identity framework, to work with roles
- to connect or to work with our database we write--> .AddEntityFrameworkStores<BookStoreContext>();
- BookStoreContext <--our database name

Also, In Program.cs file (Line 71) -->

```C#
builder.Services.AddScoped<AccountRepository, AccountRepository>();  //to work with dependency injections. this allow us to use Identity framework, use usernames, passwords, etc.
```

3. In BookStoreContext.cs file we inherit from -->

```C#
public class BookStoreContext : IdentityDbContext  // <--will create all needed tables for users and security automatically in our database
```

4. Then we add migrations, (add changes to our database)-->

```c#
dotnet ef migrations add (AnyMigrationsName)
```

5. To make update our database, we write -->

```c#
dotnet ef database update

```

6. Views/ Account/Signup.cshtml
7. AccountRepository.cs
8. AccountController.cs

.........................................................................................................................

# Add columns to AspNetUsers table (Add new properties to standard AspNetUsers table)

- Using Identity framework creates AspNeUsers table,(table for User Registration) and this table has already build in properties, such as: Id, UserName, PhoneNumber, Email and others.
- If we want to add some more other properties to this table such as: Name,LastName, dateOFBirth, and other we -->

we can inherit IdentityUser class from other class and add custom properties in that new class:

1. In Model folder we create a class which will inherit IdentityUser Class properies
2. New created class --->(ApplicationUser.cs) we add new properties to AspNetUsers table (such as: Name, LastName,Dob etc.)
3. Need to update, Change all the places where we used IdentityUser Class with ApplicationUser:

   - In Program.cs -->line 40

   ```C#
    builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

   //We change IdentityUser --> ApplicationUser
   ```

   - In BookStoreContext.cs --> line 27

   ```C#
   public class BookStoreContext : IdentityDbContext

   //We change IdentityDbContext --> IdentityDbContext<ApplicationUser>
   ```

   -In AccountRepository.cs -->line 45 , line 24, line 35

   ```C#
   var user = new IdentityUser(){...}

   //We change IdentityUser --> ApplicationUser


   private readonly UserManager<IdentityUser> _userManager;
   //We change IdentityUser --> ApplicationUser

   public AccountRepository(UserManager<IdentityUser> userManager){
   _userManager = userManager;

   //We Change IdentityUser --> ApplicationUser
   }
   ```

4. dotnet ef migrations add (NameOfMigration) <--add new properties to database
5. dotnet ef database update <-- update AspNetUsers table, with new added properties to it
6. assign new properties in AccountRepository -->

```C#
  public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel){

        var user = new ApplicationUser(){
            Email = userModel.Email,
            UserName = userModel.Email,

            //Add columns to AspNetUsers table
            FirstName = userModel.FirstName,
            LastName = userModel.LastName
        };
```

....................................................................................................................

# Configure the password compexity in Identity Core (We identify what symbols password must contain in SignUp process)

- Example-> Password must have 5 symbols, 1 Capital letter, 1 special symbol etc.

- By default Identity framework is configuration these settings for us:

1. Password must be at least 6 characters
2. Password must have at least 1 non alphanumeric character
3. Password must have at least 1 lowercase ("a" - "z")
4. Password must have at least 1 uppercase ("A" - "Z")

These Default Password settings can be changed. By following code:

- In Program.cs file -->line 50

```C#
//Configure the password complexity (User Registration password configuration)
builder.Services.Configure<IdentityOptions>( options=>{
//here we configure all the settigs for Identity framework,
//if we need to configure settings for pasword --> update settings for password
options.Password.RequiredLength = 5;
options.Password.RequiredUniqueChars = 1;
options.Password.RequireDigit = false;
options.Password.RequireLowercase = false;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequireUppercase = false;
});
```

..........................................................................................................................................

# LogIn & LogOut (-->See AccountController, Models/SignInModel. Models/SignUpUserModel.cs, AccountRepository, Views/Account/Login and Signup.cshtml)

............................................................................................................................................

# Authorize atribute (will allow to use certain action methods only for Loged in users)

- For example if user not loged in he cannot excess --> Add new book page
- We Implement some security, only loged in user allowed to add new book to database

To create this functionality we must:

1. in Program.cs we adding middleware-->

```bash
app.UseAuthorization();
```

2. We use Authorize atribute in Controller (in our case in BookController.cs --> line 78)

- Only Loged In Users will be able to access this AddNewBook action method

```C#
[Authorize]  //we add Authorize Attribute to action methods wich you want be accessable only for Loged In User
public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0){

// passing English language as default to our form  -->in return View(model)
var model = new Book(){
    LanguageId = 1
};

// here we get all languages from database , Language Table
// and passing the data in ViewBag
ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id","Name");  //under the hood --> Id- value property(in our case =1), Name - Text property(in our case =English)  -> <option value="1" > English </option>

ViewBag.Category = new List<string>(){
"programming","animals", "technology", "sports"
};

    // by default we passing isSuccess = false to the View page --> AddNewBook
    // and create variable int bookId = 0 and by default we passing it to View page -->AddNewBook
    ViewBag.IsSuccess = isSuccess;
    ViewBag.BookId = bookId;
    return View(model);
}
```

- we can use Authorize Attribute in multiple places
- we can use it in action method level
- ```C#
  [Authorize] Attribute is available in Microsoft.AspNetCore.Authorization
  ```

3. In case if you want to secure all action methods in Controller then we need to use --> [Authorize] Attribute in Controller level

```C
namespace Project_MVC_BookShop2.Controllers
{
   [Authorize]

    public class BookController : Controller
    {
      //some code here
    }
}

```

- if you use [Authorize] Attribute in Controller level that means all action methods will be available only for Loged in users

4. if user not loged In and pressed AddNewBok we can redirect user to certain page, (in our case -> login page):

- In Program.cs file we write (--> line 65 )

```C#
builder.Services.ConfigureApplicationCookie(config =>{
    config.LoginPath = "/login";
});

```
````
