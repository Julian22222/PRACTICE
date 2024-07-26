## Overview of this Project

In this Project we are using dropDown menu from different resources: from controller hard code and from database. We have options where we get data from controller and get data from database for dropDown menu when we adding a book.(In AddNewBook View)

- in our App can be only 1 Context file therefore:
- To work with local database --> we create a BookStoreContext.cs file (this is database name) --> in Data/ BookStoreContext.cs (Copy all content from README-BookStoreContext.md to BookStoreContext.cs)
- BookStoreContext ////BookStore <--can be any name, this is a name of our database
- BookStoreContext ////Context <--must be always in the end of name of our class

- To work with AZURE SQL DATABASE --> we use MyBookStoreWebDbContext (this is database name) -in Data/MyBookStoreWebDbContext.cs
- to work with WEB AZURE SQL DB, change all BookStoreContext --> to MyBookStoreWebDbContext ( In Program.cs, Repository )
- we use Azure Data Studio (an instrument to control SQL Server ). Alco, can be SQL Server management studio, MySQL workbench

- Keyboard shortcuts:
  ( prop + Tab --> use in Class, will create public int MyProperty{get;set;})
  ( ctor + Tab --> create constructor for a class)

Also,

Everithing you change in Classes in Data folder (Database) --> we need to update the database using:

1. ```bash
   dotnet ef migrations add (AnyMigrationsName) //to add changes to database
   ```

2. ```bash
     dotnet ef database update  //to update database
   ```

- Add New Book --> in NavBar , we are filling the Form and we have dropdown options:

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

   dotnet ef migrations add init  //<--Example

   ```

2. ```bash
      dotnet ef database update  //to update database
   ```

- Server side validation is written in Model class/ Model folder

  ..............................................................................................................

## To quickly add missing namespace in VS Code- Just use CTRL+. / or ctr + space on the word with the red underline. No need to install other extensions.

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

# How to install nuget packages(Entity Framework Core)

In terminal we write:
dotnet add package (PackageName)

..............................................................................................................

# Main Locations in different folder

- Data folder --> we keep all data for database here.
- Data/ Model classes --> in these classes no point to use optional proporties, because it is converted from Model Class (As example: public string? Language { get; set; })
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
4. Testing

- launchSettings.json file:
  · Is only used on the local development machine.(doesn't work on production server, when deploying )
  · Is not deployed.
  · Contains profile settings.

  The environment for local machine development can be set in the Properties\launchSettings.json file of the project. Environment values set in launchSettings.json override values set in the system environment.

  (to apply Environment)The app's environment can't be changed while the app is running.We need to stop the app, then change Environment in Proporties/ launchSettings.json and run the app again

  If you don't use - 'IIS Express' (Windows only) server then you use Kestrel server

- Shared / \_Layout.cshtml --> Here we put common code for all pages. Provides common structure to other Views. (It is a template, basic layout - these elements will be shown on all pages.)
  Aslo, here we have all meta tags, css links, bootstrap, js links connections.
  -Generaly if you are creating any View that is common to our app we start the name of the file with underscore

- wwwroot folder --> it is contetnt root foleder or folder for static files, here we have CSS, JS, Img folders <--all extras that we want to show to our user. Also we have different frameworks - Bootstrap, jQuery, JS libraries. To work with static files we write --> middleware -> app.UseStaticFiles(); (line 161) in Program.cs file

- Views / \_ViewImports.cshtml --> here we can connect additional libraries and tag helpers which will be added to all View pages. Also, we can add namespaces here and all View files will have access to this namespaces, therefore we don't need to write them in each View file (example --> @using Project_MVC_BookShop2.Models )

- Views /\_ViewStart.cshtml -->if you are planning to create common View inside our application usually we use underscore before the View name. this is a common file. To dont write the same code in each View file we -> we put this code here -->template for all View files.

....................................................................................................................

# ViewBag (--> See BookController and AddNewBook.cshtml)

- ViewBag is used to pass any data from action method to View and we can display this data on View
- This type of data binding is known as loosely binding. If you passing the data by using Viewbag and we using that data in the View (data binding with the View)<--it is loosly binding
- Strongly binding is in controller when we pass the data with --> return View(data); to View. Located in controller action method
- We can pass any type of data in ViewBag
- ViewBag use dynamic property (because Razor renders any data type automatically when we use ViewBag, ether it is int or string or anything else)

- First we need to create a new property in ViewBag and then assign some data to it:

```bash
ViewBag.PropertyName = Data; //<--Data can be any type of data
```

- Since ViewBag is a server side code hence to use it on view, we need to use razor syntax i.e. @

```bash
@Viewbag.PropertyName //<--to use ViewBag in View file, use the same Property name as we assign the property in action method
```

- ViewBag.Num = 123; <-- ViewBag can have any data type and have any property name
- Viewbag.MyString = "Hello it is my string";
- We can use many ViewBags in action method but property name must be different
- Viewbag works on dynamic type but it not give any compile time error(when some property name is wrong or if there is some error)
- The scope of ViewBag is to current action method to view

### To use anonymous data (Data object in ViewBag)

1. In the Controller file we write on the top of the file-->

```bash
using System.Dynamic;
```

2. in action method we write -->

```C#
dynamic data = new ExpandoObject();
data.Id = 1;  //<--assigning data
data.Name = "Jack"; //<--assigning data

ViewBag.Data = data; //<--assigning data to Viewbag property

// or ViewBag.Type = new Book(){Id =5; Author = "This is Author", Title= "IT"}; <--Here we can use Book Model as an object
```

2. In View we write C# code on the top of the file

```C#
@{
dynamic data = ViewBag.Data
}
```

3. Then we can fetch all the properties in the HTML tag in View file by

```C#
@data.Id  //<--will give us 1
@data.Name  //<--will give us Jack
```

..............................................................................................................

# ViewData

- is used to pass any data type from action method to view and we can display this data on view (the same as Viewbag)
- work in key -value principle (not dynamic principal as Viewbag)
- This type of data binding is known as loosely binding
- ViewData use ViewDataDictionary type
- we create a new key in ViewData and then assign some data to it --> viewData["Title"] = 123;
- use razor syntax in View --> @ViewData["Title"]
- ViewData is usually used for a Title of different page in the browser , The title showed and related to current page (Also we put code in the \_Layout.cshtml file (line 7))

### Use ViewData with object data type

1. In Controller we write

```C#
ViewData["book"] = new Book(){Author = "Rob", Id = 2};
```

2. in View we write on the top of the file

```C#
@{
   var bookInfo = ViewData["book"] as Book; //<--assign what type is the data type, (Book data type), we don't need it in Viewbag because it works in dynamic principle
}
```

3. In View file in Html tags we write

```C#
<h2>@bokInfo.Id</h2> //<--wil give 2
<p>@bookInfo.Author</p> //<--will give Rob
```

.......................................................................................................................

# How data travel through the files

### When we addidng some Data to the database using form (--> From AddnewBook.cshtml)

1. Filled data from the form go to controller, acion method --> (--> See BookController.cs)

```C#
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method (this is Attribute routing)
public async Task<IActionResult> AddNewBook(Book book){  //<--book is data from the form

///code ...

}
```

2. Then we pass data from controller action method to Repository --> (--> See BookController.cs)

```C#
[HttpPost]
public async Task<IActionResult> AddNewBook(Book book){

///code ...

int id = await _bookRepository.AddNewBook(book);  //<-- using Repository method we pass data from the form further down to the Repository

}
```

3. In Repository we interact with database (-->See BookRepository.cs)

```C#
public async Task<int> AddNewBook(Book model){ //<--model is data which is comming from --> await _bookRepository.AddNewBook(book)
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

//when pressing submit all form will go to Controllers/BookController, AddNewBook
 <input type="submit" value="Add book" class="btn btn-primary" />

</form>

```

2. In Controller, action method -->

```C#
[HttpPost]  //<--above needed action method (attribute)
public async Task<IActionResult> AddNewBook(Book book){...}
```

### When we passing id from URL to the database to find element (--> From BookController.cs)

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

# Work with Database

- To make connection to our database we have to connect our app to databse using Entity Framework Core
- Entity Framework Core (EF Core) connects Asp.Net Core App and Database
- EF Core is a middle service, works between Asp.net and DB
- EF Core is Microsoft's official technology to interact with relational database
- EF Core is Microsoft's official technology to interact with relational databse
- EF Core can be used not only in Asp.net core app but also in any .NET technology - Mobile app, Windows app, Console.app other app in .NET
- EF Core can work with lots of databases:

  - SQL Server
  - MySQL
  - Cosmos db
  - Etc.

- EF Core features:

  - O/RM (object-relational mapper) (Database table convert to classes and opposit way, column convert to Properties and oposite way)
  - Open-source (entire code can be found in Git Hub repository)
  - Lightweight
  - Extensible (many extentions and nuget packages)
  - Support Async

EF Core approach:

- Code first
- Db first

All the packages of this Entity Framework Core are available in nuget (official website of all the packages)
To install --> dotnet add package PackageName

#### To work with SQL Server database: (we need to install)

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer //<-- this package has dependancy of Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Relational and will instal them automatically
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

- By creating Data folder and BookStoreContext.cs class --> then by using these commands it will create database and create tables:

1. ```bash
    dotnet ef migrations add AnyMigrationsName  //<--to add changes to database
   ```

2. ```bash
      dotnet ef database update  //to update database
   ```

# How to connect your project to SQl Server Database.(used for local database and Azure SQL database)

1. We create new folder with any name (in our case) Data folder. Inside Data folder we create new class to use it for database --> Books.cs
2. Create BookStoreContext.cs file in Data folder ( data connection to database)

- BookStoreContext ////BookStore <--can be any name, this is a name of our database
- BookStoreContext ////Context <--must be always in the end of name of our class

3. After creating Data folder with all needed files and content we can create --> MIGRATIONS FOLDER
4. To create Migrations folder, we write -->

```C#
# creating database with tables, Class proporties converts to table columns.
# Also, can add new proporties to the table in database
# use this command when add something in Classes in Data folder(Database)

dotnet ef migrations add (AnyMigrationsName)  //add changes and create databases with tables

dotnet ef migrations add init  //<--Example

```

- Migrations is used to create tables in DB, Migrations folder has info about added or deleted columns, tables in the DB

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

for development environment we use normal library files for Production environment we use min version library files

Example of local (build in bootstrap):

```c#
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script> // min version file
```

- CDN - (stands for )-> Content Delivery Network --> (get the libraries from internet)
  benefit of using CDN - it loads the file based on your geography location, increase performance of application
  With CDN you can get any library from internet.

[Click here to view all CDN libraries](https://cdnjs.com/libraries)

Example of CDN:

```C#
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
```

(-->See \_Layout.cshtml file)

Bootstrap web page --> [Click Here](https://getbootstrap.com/docs/5.3/getting-started/introduction/)

- To use Bootstrap libarary, in \_Layout.cshtml file in the head part we put -->

```C#
  <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />

  or CDN link of main bootstrap link
```

(CDN bootstrap css link)[https://getbootstrap.com/docs/5.3/getting-started/introduction/#cdn-links]

- Then in View files we can use Bootstrap css  
  ...............................................................................................................

# Partial views (Similar to React JS components)

- it is simple cshtml file that can be inserted anywhere in any View.
- It helps to separate (break up) the long code into small parts and insert it with partial views. (Similar to React components - components with certain code , that can be inserted anywhere,simplify the code).
- Helps to remove duplicate code from app.
- If we compare with Partial View, In ViewComponent file we can connect to database and get the data from database (-->See Components/ TopBooksComponent.cs , line 27, line 37)
- In one application we can use as many partial views as we want, there is no limit of partial views

How to use it-->
partial view is a common code therefore we put it in --> Views/Shared/ (NameOfPartialView).cshtml (Example--> Views/Shared/header.cshtml )
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

- is similar to partial views but much more powerful, don't use model binding, only depend on the data provided when calling into it. ViewComponent is a special feature that is used to render some data(view +data) on a view file without actually being the part of HTTP life cycle. It is not part of Http life cycle.

- If we compare with Partial View, In ViewComponent file we can connect to database and get the data from database (-->See Components/ TopBooksComponent.cs , line 27, line 37)

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

```C#
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

- We can define a unique URL(route) for each resource., All the routes should be unique with combination of URL + Http method

- We can define multiple routes for one resource, We can't define same route for multiple resources

When client type something in Browser (URL) and hit enter it goes to the server and URL hit controller. Request contains - URL that we are passing in our browser and type(of our request) -GET,POST,PUT, DELETE

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

   ```C#
   app.MapControllerRoute(
   name: "AboutUs",
   pattern: "about-us",  //<--new route
   defaults: new {controller ="Home" , action= "AboutUs"}
   )
   ```

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

Don't need to write in URL --> localhost:5167/home/aboutus
Inserting in URL --> localhost:5167/about-us <-- will gives us View page AboutUs

To pass some parametrs in route we add --> /{ParametrName}:

```C#
[Route("about-us/{id?}")]  //<--if we use curly brackets then this variable can be different, to pass some parameters in Attribute routing
public IActionResult AboutUs( int id) //<--here in id we will get id from URL that client typed
{
return View();
}
```

# Different writings in Routing

```C#
[Route("about-us")]
[HttpGet] //<--method, this action method will handle nly Get request
public IActionResult AboutUs()
{
return View();
}
```

or (the same)

```C#
[Route("about-us")][HttpGet] //<--method, this action method will handle nly Get request
public IActionResult AboutUs()
{
return View();
}
```

or (the same)

```C#
[HttpGet("about-us")] //<--method, this action method will handle nly Get request
public IActionResult AboutUs()
{
return View();
}
```

# Route constraints

Route constraints, allow us to specify the data type of parametr (For example: [Route("about-us/{id?}") <-- id must be a number only not a string or other data type])

1. in Routing constraints you can define the type of your parameters -->

```C
[Route("about-us/{id? : int}")]
```

id: int <-- id will definately must be an int ( other data types: string,decimal,float and other)

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
   [Click](https://github.com/dotnet/aspnetcore/tree/main/src/Http/Routing/src/Constraints)

- name : bool
  ................................................................................................................................

# DEPENDENCY INJECTION

- Our Repository classes and Context class must be used with Dependency injection !!!!!!!!
- Also, we use UserService Class (to get Logge-in User Id in any controller or service)
  -And Claims Class (to save logged-in User details,)

- after registration our services (Repository classes and Context clas) then we can use them enywhere in our application (--> services registration --> See Proram.cs line 92-94)

- We can use Dependency injection with iterfaces and without interfaces.
  -->see BookRepository (without interface)
  -->see LanguageRepository - ILanguageRepository and BookController.cs (interface)

- If we not useing Dependency incection we can have some problems between Controllers and Repositories(Services). When you make some changes in Repository data using controller or other files where this Repository was used the data can't be changed in all files (you neeed update all the files where you used this Repository ).

- But using Dependency Injections, if we use the Repository(Service) in different controllers or files and we need to make some changes in Repository data in one of the files then it will be allowed to do that. and it will be changed in all the files.

- Asp.Net Core provides the build-in support for Dependency Injection(DI)

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

- using interfaces we can pass data from Repository(database info) straight away to View file (--> see AboutUs View file.)

- It is not mandotory to pass Repository class to controller (then we create an object from that Repository class in controller) and then pass this created object from Repository class to View.
  We can straight away pass only Interfacese from Repository class to View (create in View object from Repository class) and then use it in View
  --> see AboutUs View file. (Can't pass BookRepository class because it doesn't have own Interface) --> can pass only Interfaces!!!.

...........................................................................................................................................................

# How to read the data from different variables in appsettings.json file

- Using configuration service (IConfiguration) allow us to get get any data from appsettings.json in our application (--> See ContactUs.cshtml View file)
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

```C#
@inject Microsoft.Extensions.Configuration.IConfiguration _configuration //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).

OR

//this example we use with GetValue method
@using Microsoft.Extensions.Configuration
@inject IConfiguration _configuration  //we can dirrectly read appsettings.json file from View using this injection, (don't need to use Controllers or other files).


<p>@_configuration["Name"]</p>

<p>@_configuration["infoObj:key1"]</p>
```

- appsettings.json is used when you want to provide default values that can apply to all environments.
- appsettings.{environment}.json can provide non-secret default values for each environment.
- User secrets is used for local dev to provide secret values and let the dev change other values (maybe tinker with log settings, for example) without having to change any of the source controlled files.
- Environment variables are used to provide secrets when deploying the app

#### The order in which configurations are loaded is significant, because they are being combined into one dictionary. If there are duplicate keys then the last value loaded will be the one used. In the case of the default host, the load order is:

1. appsettings.json
2. appsettings.{Environment}.json
3. secrets.json (if in Development environment specifically)
4. Environment variables
5. Command line arguments

# User Secrets file (used only for my local setting secrets, works only on my machine, not for a web for eveyone) <-- uncreapted file

To keep sensative data - User Id, Passwords, ConnectionString etc.

1. Download nuget package -> Microsoft.Extensions.Configuration.UserSecrets
2. Then right click on (ProjectName).csproj file of your project(<--located in very bottom of the list of all files of our Project) --> then click Manage user secrets

#### secret.json (file contains)

```C#
//We dont write as usual obects in secret.json file
"ConnectionStrings": {
   "DefaultConnection": "Server=.;Database=BookStore;TrustServerCert..........",
   "WebConnection": "Server=tcp:mybookstoredb.database.windows ...."



   //Insed of example above it should be written the following
   //We should write -->
 "ConnectionStrings:DefaultConnection": "Server=.;Database=BookStore;Tru...",
  "ConnectionStrings:WebConnection": "Server=tcp:mybookstoredb.d...;"

}
```

To have accessto User Secrets in our code --> we use IConfiguration

### Azure Key Vault (we use it in Web to keep secrets files)

................................................................................................................................................................................................................

# Accessing appsettings.json using GetValue method

With this approach we can define the data type that we accessing in appsettings.json --> it can be boolean, string and other (with previous options we always get string data type from appsettings.json file)
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
configuration.GetValue<string>("infoObj:key2"); //to receive -value 2
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
newBook.GetValue<string>("key1");  //we don't write -"infoObj" anymore, will receive -value 1, as a result
//in brackets we use only the name of the property

```

..............................................................................................................

# Binding Configuration to objects using Bind method appsettings.json

- this allow to bind all proporties of some object in appsettings.json file to a particular model class

* allow appsettings.json object to bind a particular Model obect

How to bind two objects: (--> See appsetings.json, HomeController (line 64) and Model/AlertConfig.cs)

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

- It is a universal framework to provide security to any .NET application (can be used Blazor,Razor, ASP.NET CORE MVC and other framework that are available in .NET)
- Common framework for all .NET app
- It is NOT only limited to SignUp / SignIn but provide lots of feature that are requires for security management
- this framework provides all required tables to work with Authentication and Authorisation automaticaly, we don't need to create any extra table by ourself, everething is created automatically
- Identity nuget package instal AspNetUsers table with some already build in properties. If we want to add some more properties to AspNetUsers table (such as FirstName,LastName, etc.) we need to create new Model (in our case --> ApplicationUser.cs). All properties from AspNetUser table are used and filled when the user is register in our website

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

1. In Program.cs file , (line 165)-->

```C#
app.UseAuthentication();  //enable authentication connection using middleware, to use passwords ,LogIn,SignUp etc.
```

2. In Program.cs file , (line 58)-->

```C#
   builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>(); //to work With Identity Core we need to configure Identity to work with database. BookStoreContext <-- is name of our database
```

- AddIdentity<IdentityUser,IdentityRole>()
- IdentityUser <--is a table that already build in Identity framework, to work with a user we insert this table
- identityRole <--is a table that already buildIn in Identity framework, to work with roles
- to connect or to work with our database we write--> .AddEntityFrameworkStores<BookStoreContext>();
- BookStoreContext <--our database name

Also, In Program.cs file (Line 94) -->

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

- Using Identity framework creates AspNeUsers table,(table for User Registration) and this table has already build in properties, such as: Id, UserName, PhoneNumber, Email and others. --> in Data/MyBookStoreWebDbContext.cs we inherit from IdentityDbContext --> // public class BookStoreContext : IdentityDbContext

- If we want to add some more other properties to this table such as: Name,LastName, dateOFBirth, and other -->

In Model folder we create a class (--> ApplicationUser.cs) and inherit from IdentityUser class.
Here in this Model class (-->in our case it is ApplicationUser.cs ) we can add extra properties to AspNetUsers table (table for user data when doing registration)

in this class we add custom properties in that new class:

1. In Model folder we create a class which will inherit IdentityUser Class properies
2. New created class --->Models/ (ApplicationUser.cs) we add new properties to AspNetUsers table (such as: Name, LastName,Dob etc.)

3. We add new fields for a SignUp form in Models/SignUpModel.cs and add new fields in Views/Account/SignUp.cshtml

4. in Context class --> MyBookStoreWebDbContext (line 29) we put -->
   public class MyBookStoreWebDbContext : IdentityDbContext<ApplicationUser>

5. Need to update, Change all the places where we used IdentityUser Class with ApplicationUser:

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

6. dotnet ef migrations add (NameOfMigration) <--add new properties to database
7. dotnet ef database update <-- update AspNetUsers table, with new added properties to it
8. assign new properties in AccountRepository -->

```C#
private readonly UserManager<ApplicationUser> _userManager;  //UserManager is used for Sign Up , create variable for SignUp, to interact with database's AspNetUsers table

private readonly SignInManager<ApplicationUser> _signInManager;  //SignInManager is used for Sign In, create variable for SignIn, to interact with database's AspNetUsers table


  public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager){
        _userManager = userManager;  //using _userManager -> we have acces to AspNetUsers table, when SignUp
        _signInManager = signInManager; //using _signInManager -> we have acces to AspNetUsers table, when SignIn
    }

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

- By default Identity framework is configurating these settings for us:

1. Password must be at least 6 characters
2. Password must have at least 1 non alphanumeric character
3. Password must have at least 1 lowercase ("a" - "z")
4. Password must have at least 1 uppercase ("A" - "Z")

These Default Password settings can be changed. By following code:

- In Program.cs file -->line 67

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

# LogIn & LogOut (-->See AccountController, Models/SignInModel. Models/SignUpUserModel.cs, AccountRepository, Views/Account/Login and Signup.cshtml, Views/Shared/ \_Logininfo.cshtml)

............................................................................................................................................

# Authorize atribute (will allow to use certain action methods only for Loged in users)

- For example if user not loged in he cannot excess --> Add new book page
- We Implement some security, only loged in user allowed to add new book to database
- If user not loged in then it will be no permission to access some action method, and by clicking on that action method will show --> page NOT FOUND. To solve this error--> we need to use this code to redirect user to certain page if he is not LogedIn

- In Program.cs file we write (--> line 82 )

```C#
builder.Services.ConfigureApplicationCookie(config =>{
    config.LoginPath = "/login";  //<--refirect User to Login page
});

```

To create this functionality we must:

1. in Program.cs we adding middleware--> ()

```C#
app.UseAuthentication();  <-- must always be above Authorization to work correctly!!!
app.UseAuthorization();  <-- must always be below Authentication to work correctly!!!
//ONLY IN THIS ORDER

```

2. We use Authorize atribute in Controller (in our case in BookController.cs --> line 79)

- Only Loged In Users will be able to access this AddNewBook action method

```C#
[Authorize]  //we add Authorize Attribute to action methods which you want be accessable only for Loged In User
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

```C#
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

4. if user not loged In and pressed AddNewBok (but AddNewBook action method has Authorization attribute ) we can redirect user to certain page, (in our case -> login page):

- In Program.cs file we write (--> line 82 )

```C#
builder.Services.ConfigureApplicationCookie(config =>{
    config.LoginPath = "/login";
});

```

# Return Url

When we use [Authorize] Attribute and not loged In user press Add New Book it will redirect him to Login page (see above -previous lesson).
And if you want to return user to AddnewBook page after he Loged in we need:(remebers the url that user pressed - but the access was denied for unauthorized users and after user login it return that page that user clicked before)

- We write in controller (In AccountController.cs):

```C#
      [Route("login")]  //Attribute routing
      [HttpPost]
      public async Task <IActionResult> Login(SignInModel signInModel, string returnUrl){
        if(ModelState.IsValid){

        var result = await _accountRepository.PasswordSignInAsync(signInModel);


        if(result.Succeeded){ //If logIn is Successful do this code --> (result.Succeeded == true)


         if(!string.IsNullOrEmpty(returnUrl)){  //if returnUrl exist then we return this returnUrl page
            return LocalRedirect(returnUrl);   //returnUrl <--used to return user to correct page after login
        }


        return RedirectToAction("index", "Home");  //return Home/index View Page
        }


        ModelState.AddModelError("", "invalid credentials"); //if LogIn is not Seccessful, show this message

        // ModelState.Clear(); // clear the ModelState

        }
        return View(signInModel);  ////if result is not successful we return a View and pasing - signInModel (User wasn't found in the database)
    }
```

- In Views/ Shared / \_LoginInfo.cshtml -->

```C#
<a class="btn btn-outline-primary" asp-action="Login" asp-controller="Account"  asp-route-returnUrl="@Context.Request.Path">Login</a>

//we need to access current page path and we can do thar easy by using --> @Context

```

..............................................................................................................

# If can check is the user Loged In or not by using SignInManager

->> See \_Logininfo.cshtml

...................................................................................................................

# Claims

- By useng Claims we can show full name of logged-in user on the UI
- Claims are provided by Identity framework
- In very simplest term, Claims are the storage, and in that storage we can save whatever info that we want --> as example: store all details about Logged-in User . We can save all details about logged-in user in Claims -->(FirstName, LastName, any other details from user profile). And we can use this Claims anywhere in my application

- To work with the Claims we add new file --> we can add this file anywhere in our app
- As example we created Folder --> Helpers and there we add a file --> ApplicationUserClaimsPrincipalFactory.cs

--> See that ApplicationUserClaimsPrincipalFactory.cs class to make Claims

- Then we need to register new Services in main Program.cs file (line 103), this is telling to our app that we use Claims (UserClaimsPrincipalFactory Class)

- Now we can use the Claims to show the values in the UI (--> See Views/Shared/Logininfo.cshtml)

................................................................................

# Get the Id of logged-in user in controller and services

- using this technique we can get Loged In user Id in Controller, repositories, services or anywhere in this app.
- To handle User Id we create one more Service in this app, the same way as we cretaed Repository folder

1. We create folder --> Service in the root of our App.
2. in this Service folder we create new Class --> UserService.cs
3. in this class we create few action methods
4. to work with Id we need HttpContext, eather we can use HttpContect directly into controller class or we can use HttpContext in UserService class
5. --> See Service/UserService.cs
6. now we need to register our service in our application in Program.cs main file (line 96)
7. Controllers/HomeController.cs in Index action method --> we get loged-in User Id

```C#
 var userId = _userService.GetUserId();
  Console.WriteLine(userId);  //<--will show UserId
```

### how to check is the User logged-In or Not in our app

1. we use the same approach
2. in UserService.cs file we add new action method

```C#
 public bool IsAuthenticated(){
            return _httpContext.HttpContext.User.Identity.IsAuthenticated; //<--action method to check is the User Logged-In or not
        }
```

- this action method return - true or false, depending is the user logged-in or not

3. --> Se Controllers/HomeController.cs in the Index action method (line 49-50)

.........................................................................................................

# Change Password

- we are using Identity framework to use change password functionality

- to change password we need old password -> currentPassword and NewPassword

1. We create new Model in our Models folder --> Models/ChangePasswordModel.cs

- here we create few properties -> CurrentPassword, NewPassword, ConfirmNewPassword

2. In Controllers/AccountController.cs we add new action method to change password

```C#
//this method dispay the page
 [Route("change-password")]
    public IActionResult ChangePassword(){
        return View();
    }


  //this method handle the submit btn
    [HttpPost("change-password")]
     public async Task <IActionResult> ChangePassword(ChangePasswordModel model){

        if(ModelState.IsValid){
            var result = await _accountRepository.ChangePasswordAsync(model);  //<--invoke action method from AccountRepository

            if(result.Succeeded){ //<--if password has been updated successfully do this code
            //if password has been updated successfully then we need to display some msg in UI for the user , using ViewBag
            ViewBag.IsSuccess = true;


                ModelState.Clear();  //<--clear ModelState
                return View();
            }


            //if result.Succeeded ==false, password hasn't been updated, there is some problem and we need to dispay that errors to the user in UI
            foreach(var error in result.Errors){
                ModelState.AddModelError("",error.Description);
            }


        }
        return View(model); //<--if ModelState is not valid it will return View, (the same page)
    }
```

3. Create new link to Change passord page in -->Views/Shared/ Logininfo.cshtml (line 19)

4. Create View page of ChangePassword --> Views/Account/ChangePassword.cshtml

5. Repository/AccountRepository.cs -->

- here we create action method to interact and make changes in database table

```C#
//Change Password action method
    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model){

        var userId = _userService.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);

  //go to defenition of --> ChangePasswordAsync , to see what parametrs it takes
       return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    }
```

# AREA

- if you don't want to keep your files in common Models, Controllers and Views folder, Area - allow to add models, controllers and views in separate folder for complitely separate feature to
  our app (keep it separately)

- to handle this type of situation is ASP.NET Core we have a concept of AREA
- AREA is a feature that allow to create completely separate folder structure within this application
- each AREA has its own folder structure
- if i want to create one AREA in this application then it will have one folder for Controllers, one folder for Models and one folder for Views.
- By using MVC foder we can add and keep our files complitely separate from main Controllers,Models and Views folders
- As example, if we want to add admin functionality to application, and i want to keep this separate from other files we should create AREA feature, it will have own layout and own files

- To work with AREA i need to write in terminal

1. In terminal of our app

```C#
//to install the dotnet-aspnet-codegenerator tool
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 7.0
dotnet tool install -g dotnet-aspnet-codegenerator --version 7.0

//to add Area folder with all the features to our app
dotnet-aspnet-codegenerator area AreaNameToGenerate
```

- This commands will create Areas folder at the root level of our app with all the features (--> Create Models, Controllers, Views folders in Area folder + ScaffoldingReasMe.txt file at the root level of our app)
- This allow to keep your files separately

2. Create files with any names in Controller , Models and Views folder (-->See Areas folder)

- for a navigation in this app we are using Routing, if we want to navigate to particular controller or file in this app then we need to tell to our application that we ae using these files , we have to tell to our app that we added new Area and this is the Route structure for this particular Area. to enable Area routing in ASP.NET Core we have to :

3. in Areas/Admin/Controllers/ HomeController.cs

```C#
namespace Project_MVC_BookShop2.Areas.Admin.Controllers;

[Area("admin")] //Admin <--here we defined only the name of the area, no Routing been defined
[Route("admin/[controller]/[action]")]  //Routing to our page
public class HomeController : Controller
{
//some logic
}
```

4. In Programm.cs we add (from ScaffoldinReadMe.txt file, it is located at the root of the our app, in the bottom)

```C#
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name : "MyAreas",
        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
```

- If you want to create new Area, you create separate folder in Areas folder with their own Controllers, Models, Views folders

- to return back from Admin page to our main controller and action methods(--> See Areas/Admin/Views/Home/index.cshtml file)

```C#
//we use asp-area=""
<a asp-area="" asp-controller="Home" asp-action="Index">Go bak to Home page</a>
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

```

```

```

```

```
