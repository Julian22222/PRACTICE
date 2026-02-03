[ASP.NET Docs](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&WT.mc_id=dotnet-35129-website&tabs=visual-studio-code)

[ASP.net docs](https://www.csharpschool.com/blog?tag=asp.net+core+2.2)

[--> Install VSCode or VS to work with ASP.NET <--](https://www.youtube.com/watch?v=qBTe6uHJS_Y&t=9s)

To work with in ASP.NET MVC Core with C# we need VS Code extensions:

- C# Dev Kit (by Microsoft)
- C# Extensions (by JosKreativ)
- and others from picture below.

![pic2](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic2.jpg)

![pic3](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic3.jpg)

![pic4](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic4.jpg)

![pic5](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic5.jpg)

Before you start your project - install all needed NUGET PACKAGES!!!

## Overview of this Project

In this Project we are using dropDown menu from different resources: from controller hard code and from database. We have options where we get data from controller and get data from database for dropDown menu when we adding a book.(See --> AddNewBook View)

- in our App can be only 1 Context file therefore:
- To work with local database --> we create a BookStoreContext.cs file (this is database name) --> in Data/ BookStoreContext.cs (Copy all content from README-BookStoreContext.md to BookStoreContext.cs)
- BookStoreContext ////BookStore <--can be any name, this is a name of our database
- BookStoreContext ////Context <--must be always in the end of name of our class

- To work with AZURE SQL DATABASE --> we use MyBookStoreWebDbContext (this is database name) -in Data/MyBookStoreWebDbContext.cs
- to work with WEB AZURE SQL DB, change all BookStoreContext --> to MyBookStoreWebDbContext ( In Program.cs, Repository )
- we use Azure Data Studio (an instrument to control and interact with SQL Server, it is used more often ). Also, can be SQL Server management studio (SSMS) or MySQL workbench (these last 2 options --> used rarely)

#### To start new app--> write in terminal

1. in terminal --> dotnet new list
2. In terminal --> dotnet new mvc
3. dotnet watch run

### Small but important things

- Keyboard shortcuts:
  ( prop + Tab --> use in Class, will create public int MyProperty{get;set;})
  ( ctor + Tab --> create constructor for a class)

- To quickly add missing namespace in VS Code - Just use CTRL + . / or ctr + space on the word with the red underline. No need to install other extensions.

- We use different classes with the same properties in Data folder and in Model folder. Because we can keep the logic separate. Properties in Data classes are representing columns in database table. If in future you have any change in this entity class (in Data folder) then you need to update property in Model class , then in the controller and then in database. And this might be a problem . Therefore we keep these clases separately - Model classes separate and entity clases separate (Classes in Data folder).

Also, Everithing you change in Classes in Data folder (Database) --> we need to update the database using:

1. ```C#
      dotnet ef migrations add (AnyMigrationsName) //<-- this command create Migrations folder in the App and add changes to database whern update the properties or tables in DB,
   ```
   - ef --> stands for Entity Framework
2. ```C#
     dotnet ef database update  //<-- this command update database. Using connection string in the APP--> creates tables / updates data in SSMS
   ```

- Add New Book --> in NavBar , we are filling the Form and we have dropdown options:

- BookType dropdown menu <-- is coming from database, (they not hardcoded in the View) - types: children, fiction, non-fiction, mystery, biography, fantasy
- Category dropdown menu <-- is coming from Controller (See --> BookController.cs) <--hardcoded options

- We create new class for dropdown -->BookType, in Data folder and in Models folder
- We Create connection, relationship between two tables - Books table and BookType table in Data folder--> (--> See Data/Language.cs file)

In Books class we use property-->

```C#
//BookType <--first BookType it is data type (BookType table)
//BookType <-- second BookType it is a name of property

public BookType BookType { get; set; }
```

- For each table in databse we create Class in Data folder

- Each table has their own Repository file with the logic related to that table

- Everithing you change in Data folder files (Database) --> we need to update the database using:

1.

```C#
   dotnet ef migrations add (AnyMigrationsName) //to add changes to database

   dotnet ef migrations add init  //<--Example

```

2.

```C#
      dotnet ef database update  //to update database
```

- Server side validation is written in Model class/ Model folder

[Example of Server Side Validation in the Model Class](https://www.c-sharpcorner.com/UploadFile/cda5ba/adding-custom-validation-in-mvc/)

3. If we already have some data in the DB (We added many items to the DB and we want to delete everithing from DB) and we want to clear all the data from DB:

- Delete Migrations folder in VS Code, in your project
- Go to Azure Data Studio app --> Delete all used tables for your project in the DB
- Then add migrations and update the DB

```C#
 dotnet ef migrations add (AnyMigrationsName) //to add changes to database + add migrations folder to the project

dotnet ef migrations add init   //<--Example
```

```C#
      dotnet ef database update  //to update database
```

4. If we want to add tables or/and new properties to the Database. Doesn't matter Local database and/or to Web Application (to Azure portal) the process is the same:

### use Connection string full path, it allows to connect with that Database which is mentioned in the connection string and allows to add tables and properties that we indicated in Data folder/Context DB connection file and Data/Models --> to that DB that is mentioned in connecion string.

### Key Vault --> doesn't have connection string full path, therefore can't add needed tables and properties to the correct Database.

### Process step by step how to add tables and properties to the DB:

- We will update only that database that is mentioned in ConnectionString full path --> ConnectionString can be written in User Secrets file /or Variable environment /or in appsettings.json (DON'T KEEP WEB CONNECTION STRING in the appsettings.json --> only localhost connection string). Example below-->

```C#
//Connection string from User Secrets file / appsettings.json file (For example)
"ConnectionStrings:DefaultConnection": "Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!"
```

```C#
"ConnectionStrings:DefaultConnection":"Server={Server Name};Initial Catalog={Database Name};Persist Security Info=False;User ID={DB User Name};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
```

- Then we use that connection in Data/ Context Database connection file, or in Program.cs file (depends where do do you want to connect to the DB)-->

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
```

and

```C#
//For PRODUCTION ENVIRONMENT WE DON'T HAVE ACTUAL CONNECTION STRING HERE WITH KEY VAULT, THEREFORE IT CANNOT UPDATE THE WEB DATABASE IN THE AZURE, WE HAVE TO PUT --> FULL CONNECTION STRING PATH FROM USER SECRETS file or FROM ENVIRONMENT VARIABLES TO CONNECT / UPDATE / ADD ALL THE TABLES AND PROPERTIES (FOR A FIRST TIME WHEN YOU CONNECT TO THE WEB DB or YOU WANT TO ADD SOME PROPERTIES OR/AND UPDATE THE FUNCTIONALITY OF THE APP)


//THIS EXAMPLE WILL NOT WORK, WILL NOT ADD ALL NEEDED TABLES AND PROPERTIES TO CORRECT DATABASE,
//WILL NOT CONNECT / UPDATE / ADD NEW PROPETIES,FUNCTIONS , (WHEM YOU CONNECT FIRST TIME TO WEB DB USE FULL CONNECTION STRING PATH),
//KEY VALT DOESN'T PROVIDE FULL CONNECTION STRING PATH
if(builder.Environment.IsProduction())
{
var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

var credential = new ClientSecretCredential(keyVaultDirectoryID.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());

builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);

builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString()));
}
```

- Instead of KEY VAULT OPTION we need to add -->

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //Where DefaultConnection will be full Connection string path to Web DATABASE FROM USER SECRETS FILE /ENVIRONMENT VARIABLES, TO ADD ALL THE TABLES AND PROPERTIES TO WEB APPLICATION DB
}
```

```C#
//Full Connection String path

"Server= api of your database here;Database=NameOfYourDatabase;TrustServerCertificate=true;User ID=sa(userName);Password=PasswordForDatabase"
```

- Then in terminal we put

```C#
dotnet ef migrations add (AnyMigrationsName) //to add changes to database + add migrations folder to the project

dotnet ef migrations add init   //<--Example

//init <--name of migration file, this name should be different for DEVELOPMENT -when we create local Database, adding folders with properties and for PRODUCTION environment- when connecting to Cloud (AZURE PORTAL)

//when we want to create Mirgations folder:
  //- for loacal database, we write -> dotnet ef migrations add init, and then dotnet ef database update
  //- if we want to crate/ add migration files for for Cloud (AZURE PORTAL), we write different name ->  dotnet ef migrations add init2 (For example) and then dotnet ef database update

  //These migration file names must be different for local and cloud DB, the will be located in Migrations folder.


//firts time when we put these 2 commands above we create database name as well
```

```C#
dotnet ef database update  //to update database
```

- Once we added all needed tables and properties to the Web DB ,Then we can put KEY VAULT option back for Production environment,as it is
- AND leave Development option as normal , with connection string to local databse

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //change DefaultConnection variable to be a full connection string path to local DB
}
```

- Then -->

# To Deploy ASP.Net web application in Azure (preparing and creating all resources to deploy)

### First option how to Deploy your project to Azure

- go to --> [Azure Portal](https://portal.azure.com/) and Create App Services

- install Azure App Service extension and Azure Account extension in VS code

- To deploy new app or deploy new changes, go to correct folder of our app write in terminal-->

  ```C#
  dotnet publish -c Release -o ./bin/Publish
  ```

- In VS Code our app, Right click the bin\Publish folder and select Deploy to Web App... (pic below)

![pic8](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic8.jpg)

- Then automatically Comand Palette will appear (Terminal Search bar on the top ) --> Choose your Project name there (pic below)

![pic9](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic9.jpg)

- first time in Comand Palette --> LogIn through Comand Palette

- if Comand Palette is blank and doesn't show any options --> close VS Code and open again

- Once the deployment is finished, click Browse Website to validate the deployment

### Second option how to Deploy your project to Azure

- go to --> [Azure Portal](https://portal.azure.com/) and Create App Services
- install Azure App Service extension and Azure Account extension in VS code
- From the Left Menu bar find Azure tab in VS Code
- Open correct Subscription (Where is your project)--> Right Click on correct App Services (Name of your project that you are deploying) --> from the menu choose --> Deploy to Web App.
- during first deployment I need select Browse (where is my Application compiles)
  [Check here](https://www.youtube.com/watch?v=4BwyqmRTrx8&t=363s)

..............................................................................................................

## To quickly add missing namespace in VS Code --> Just use CTRL+. / or ctr + space on the word with the red underline. No need to install other extensions.

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

// --version 7.0  <-- must match with .NET version that you are using in this app
```

................................................................................

# How To migrate from ASP.NET Core MVS Project from .NET 7.0 to .NET 8.0

[Migrate from ASP.NET Core in .NET 7 --> to .NET 8](https://learn.microsoft.com/en-us/aspnet/core/migration/70-80?view=aspnetcore-7.0&tabs=visual-studio-code)

[-->EXAMPLE HERE<--](https://www.youtube.com/watch?v=Bq7nSZfyBrg)

- delete --> bin and obj folders
- update your .NET 7.0 to .NET 8.0 in your project Project_MVC_BookShop2.csproj file: -->
  - Project_MVC_BookShop2.csproj file (this file located in root folder in the bottom)
  - <TargetFramework>net7.0</TargetFramework> //<-- change this line to net8.0
  - all the nuget packages with .net version 7.0 change them to 8.0 --> <PackageReference Include="microsoft.aspnetcore.identity.entityframeworkcore" Version="7.0" /> (Example)
- Then build your project and run application

# How to Check what .NET version we are using

- Go to Project_MVC_BookShop2.csproj file (located in the bootom of the root Project Folder)
- Then Check line 4 -->

```C#
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
```

# Easiest way to work with Nuget Packages and Install packages from Nuget Package

- Download NuGet Gallery //<--by pcisco , from VS Code extensions

#### USE Nuget Gallery-->

- In Command Palette put (ctr + shift + p )-->

```bash
NuGet:Open NuGet Gallery
```

- add in serch nuget package that you want to download
- add your .NET version for a package and press "+" in api.csproj section
- to delete some packages we can press "-" in api.csproj section

#### Another option how to download Nuget packages

- To install nuget packages with different version , not the latest version we put: (EXMAPLE). We put in terminal -->

```C#
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0
```

#### Another option, How to install nuget packages (can be any nuget package)

Nuget packages can be installed through -->VSCode-->View --> Command Palette
if --> Nuget Package Manager extension was installed on VS Code.

- write in the Palette --> Nuget Package, click it and then write your Nuget package name to download

or

In terminal we write:
dotnet add package (PackageName)

..............................................................................................................

# Main Locations in different folder

- Data folder --> we keep all data for database here.
- Data/ Model classes --> if we use optional proporties in these classes, if there is no data in that property it will == null (As example: public string? BookType { get; set; })
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

  Repository is a place where you can get, post, edit, delete the data in database.
  We use Repository class methods in BookController

- In View , when we fill the Form (it is completing through Model class -from Models folder) , then in BookRepository we convert Model data to Database Model(Model class from Data folder)

- Components folder --> we use for ViewComponent
  Also we use Views/Shared folder to display ViewComponent View

- Properties / launchSettings.json --> here we keep global settings of our app, here we have PORT, URL of our website (local ports). We have 2 PORTS -if one is engaged application will use second PORT.
  we can change App Environment here:

1. Development
2. Production
3. Staging
4. Testing

- launchSettings.json file:

  - Is only used on the local development machine.(doesn't work on production server, when deploying )
  - Used for the development environment only
  - The settings in the this file can be overridden by environment variables or command-line arguments (in Program.cs file)
  - Is not deployed.
  - Contains profile settings.

  The environment for local machine development can be set in the Properties\launchSettings.json file of the project. Environment values set in launchSettings.json override values set in the system environment.

  (to apply Environment)The app's environment can't be changed while the app is running.We need to stop the app, then change Environment in Proporties/ launchSettings.json and run the app again

  If you don't use - 'IIS Express' (work only on Windows) server then you use Kestrel server

  - IIS is Reach server and Powerful server
  - Kestrel server allow to run DotNet Core on Linux and MacOs
  - Kestrel --> is a Web Server, Cross platform, open Source, Works with Reverse Proxy Servers
  - It is recommended to use other famous and powerful web servers like IIS, Apache, or NGINX as a reverse proxy server when run Kestrel for public websites. because Kestrel is very basic light weight server which is fast but can't manage all the server performance.Therefore Kestrel can use other Powerful web servers such as IIS, Apache, or NGINX to help process all the requests and tasks then validate and pass it to Kestrel. (Kestrel server has no -> Security,management, no support for windows authentication, no support for port sharing , no https logs, no http redirect rules etc.)

- Views/Shared / \_Layout.cshtml --> Here we put common code for all pages. Provides common structure to other Views. (It is a template, basic layout - these elements will be shown on all pages.). We need let View know that we are using some certain layout-->\_Layout.cshtml file, therefore we need to write in each View file on the top, or indicate it in the Views/\_ViewStart.cshtml file -->

```C#
  @{
    Layout = "NameOfTheLayout";

    //or
    Layout = "~/Views/Shared/_Layout.cshtml"
    }
```

Aslo, here we have all meta tags, css links, bootstrap, js links connections.
Generaly if you are creating any View that is common to our app we start the name of the file with underscore

- wwwroot folder --> it is contetnt root foleder or folder for static files, here we have CSS, JS, Img folders <--all extras that we want to show to our user. Also we have different frameworks - Bootstrap, jQuery, JS libraries. To work with static files we write --> middleware -> app.UseStaticFiles(); (line 161) in Program.cs file

- Views / \_ViewImports.cshtml --> here we can connect additional libraries and tag helpers which will be added to all View pages. Also, we can add namespaces here and all View files will have access to this namespaces, therefore we don't need to write them in each View file (example --> @using Project_MVC_BookShop2.Models )

- Views /\_ViewStart.cshtml -->We need let the View know what is the Layout file name. if you are planning to create common View layout inside our application usually we use underscore before the View name. this is a common file. To don't write the same code in each View file we -> we put this code here -->template for all View files.

\_ViewStart.cshtml View file is executed before other Views

```C#
@{
  Layout = _Layout;
}
```

- Migrations folder appears only after

  ```C#
  dotnet ef migrations add (AnyMigrationsName) //<-- this command create Migrations folder in the App and add changes to database whern update the properties or tables in DB,

  dotnet ef database update  //<-- this command update database. Using connection string in the APP--> creates tables / updates data in SSMS
  ```

  Migrations use Entity Framework to add tables and its properties to DB, which we indicated in Data folder/DB context file (MyBookStoreWebContext.cs) and Data/Models.

  init <--name of migration file, this name should be different for DEVELOPMENT -when we create local Database, adding folders with properties and for PRODUCTION environment- when connecting to Cloud (AZURE PORTAL)

  when we want to create Mirgations folder:

  - for loacal database, we write -> dotnet ef migrations add init, and then dotnet ef database update
  - if we want to crate/ add migration files for for Cloud (AZURE PORTAL), we write different name -> dotnet ef migrations add init2 (For example) and then dotnet ef database update

These migration file names must be different for local and cloud DB, the will be located in Migrations folder.

firts time when we put these 2 commands above we create database name as well

- Area folder -use to separate Controllers, views from main Controllers and Views folders

....................................................................................................................

# What is Interface?

- Interfaces is similar to Repository Classes,
- Interfaces are inherited by Repository Classes and have the same methods and arguments as -> Repository Class that inherits this Interface
- Interfaces have only Method names with arguments but all logic of these methods are in Repository Class
- Interfaces are easier to implement and use in Application
- Then we use Interfaces to interact with DB in different classes, we import Interface
- To use Interfaces in ASP.NET Core MVC --> we need to register them in Program.cs file with Dependency injection

```C#
//example
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
```

# Claims Vs. Roles

Roles:

All we needed for app are User and Admin. You can easily add more complicated roles by inserting them into role table and adding annotations to controllers to prevent people from accessing certain endpoints. You could also add logic into controllers via RoleManager for more complicated scenarios

- Roles are more generic and broad (old school)
- if our App become more complex, for exapmple if we have 20 different roles and each time when we need to get the Role --> we need to get it from DB and that is limitation with Roles. To get the Role we have to hit the Database and DB should respond

This is wnhen Claims come in ...

Claims:

- Claims are everything, like a tag associated with the User
- Claims don't require DB and they are very flexible (new shool)
- Claims don't use DB and give much more flexibility (this makes Claims much better than Roles)
- Microsoft has moved away from Roles

#### Claims path (For Claims we used Services and Interface)

- Claims is almost like Roles --> Key -valie pairs of things that are going describe what the User does and what he can't do
- when User Submits his LogIn Form with Username , email etc. when LogIn--> it sends JWT to the Server
- And if LogIn data is correct --> User is Authenticated --> then We get the access to all the values in the DB related to this User, that we have created such as (User info(as example) --> username, email, is User loggedIn, etc.) --> (See Service/TokenService.cs line 35) <-- set User values that we have access to
- And all these data will be associated with this User
- We will have an object with all these User's data --> (See Service/TokenService.cs line 34) <--indicate the User prperties
- And we can use this object --> use this HttpUser.Context all throughout the app as long as the User is LogedIn. Object is created -->(See Service/TokenService.cs line 35)
- We get all User data once when User LogIn we don't need to hit the server every time

The concept of the ClaimPrincipal is almost like a valet, like Authentication valet <-- it is an object that holds all information about this User

- In our case we create ClaimPrincipal object with values that we have created in --> Service/TokenService.cs line 35, This values we can use to identify the User and express what the User can and can't do withing your app. Very similar to the Role( in Data/ApplicationDBContext.cs file)--> but more flexible

....................................................................................................................

# ViewBag (--> See BookController and AddNewBook.cshtml)

- ViewBag is a type object
- ViewBag is limited to the current request only.
- ViewBag is used to pass any temporary data from action method to the View and we can display this data on View
- ViewBag is valid only between the controller and the view that it returns — it does not persist/not working across redirects or RedirectToAction.
- ViewBag, ViewData, and ModelState only persist within the same request, but a Redirect option triggers a new HTTP request, which wipes ViewBag, ViewData, and ModelState out.
- ViewBag is typically used for passing non-critical, display-related data (e.g., dropdown list items, status messages).
- This type of data binding is known as loosely binding. If you passing the data by using Viewbag and we using that data in the View (data binding with the View)<--it is loosly binding
- Strongly binding is in controller when we pass the data with --> return View(data); to View. Located in controller action method
- We can pass any type of data in ViewBag
- ViewBag use dynamic property (because Razor renders any data type automatically when we use ViewBag, ether it is int or string or anything else) to pass temporary data from a controller to a view during a single request-response cycle.
- Disadvantage of this method --> we will not get any errors if we misspell the ViewBag property in the View file --> (Example if we put in View file --> @Viewbag.Numm)
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

4. Another way how to pass AnonymousObjects

- Also ViewBag can be passed from action method to View and then we can loop through all the ellement -->

```C#
//in View file

@foreach (Employee item in ViewBag.employees){  //<--Employee is a Class, ViewBag.employees was passed from action method (it consist of a List of Employee)
  //code
  <p>@item.Id<p>     //<--properties from Employee Class
   <p>@item.Name<p>   //<--properties from Employee Class
}
```

# Viewbag scope and limitations

When you use RedirectToAction, Redirect, or other redirect methods:

- ViewBag, ViewData, and ModelState can be passed only with – return View() option.
  If use RedirectToAction("ShowCars", "Home"); - This starts a new request, which means all that per-request data (ViewBag, ViewData, ModelState) is lost unless you persist it some other way.
- The framework starts a new request, which means the original ViewBag data is lost.
- This is because ViewBag data is not stored in TempData, session, or cookies, so it doesn’t persist beyond the current request.

##### If you need to preserve/pass data across a redirect, use one of the following:

1. TempData – persists for one additional request (ideal for redirects).

```C#
TempData["Message"] = "Car Deleted Successfully";

return RedirectToAction("ShowCars", "Home");
```

```C#
//Then in your ShowCars view (or controller action), you can retrieve it like this:
//In the View (Razor):

@if (TempData["Message"] != null){
  <div class="alert alert-success">@TempData["Message"]</div>
}

//OR in the controller (if needed):
var message = TempData["Message"] as string;
```

2. Query String / Route Values – pass small amounts of data explicitly via the URL. (not recommended for sensitive info)

```C#
return RedirectToAction("ShowCars", "Home", new { message = "Car Deleted Successfully" });
//Then read Request.Query["message"] in the target action/view. But TempData is cleaner and safer for most cases.
```

Example 1

```C#
//1. Example of passing Data with Query String
//A query string allows you to pass small pieces of data through the URL. It's easy to use, but keep in mind that it’s visible to the user and has length limitations.

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Passing data via query string
        return RedirectToAction("Details", new { id = 1, message = "Hello from query string!" });
    }



    public IActionResult Details(int id, string message)
    {
        // Receiving data from the query string
        ViewBag.Message = message;
        return View();
    }
}



//Generated URL Example:
//When you call the RedirectToAction in the Index action, the URL will look like:
/Home/Details?id=1&message=Hello%20from%20query%20string%21


//In the Details View (e.g., Details.cshtml):
<h2>Details View</h2>
<p>Message: @ViewBag.Message</p>
```

Example 2

```C#
//2. Example of Passing Data with Route Values
//Route values allow you to include data as part of the URL path, instead of as a query string. It's cleaner and often used in RESTful routing.

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Passing data via route values
        return RedirectToAction("Details", new { id = 123, message = "Hello from route values!" });
    }



    public IActionResult Details(int id, string message)
    {
        // Receiving data from route values
        ViewBag.Message = message;
        return View();
    }
}


//Route Configuration
//If you're using default routing ({controller=Home}/{action=Index}/{id?}), this will work without extra configuration. But, you can configure custom routes in Startup.cs or Program.cs.

//In the Details View (e.g., Details.cshtml):
<h2>Details View</h2>
<p>Message: @ViewBag.Message</p>

// Example URL with Route Values:
/Home/Details/123/Hello%20from%20route%20values%21
```

Summary:

- Query String: The data is passed after the ? in the URL. You can see the data in the URL itself. Example: /Home/Details?id=1&message=Hello
- Route Values: Data is passed as part of the URL path. Example: /Home/Details/123/Hello

Both methods allow you to pass data without using ViewBag or TempData. Just remember that query strings are visible and should not be used for sensitive data, and route values often look cleaner but are also exposed in the URL.

..............................................................................................................

# ViewData (very similar to ViewBag but has different features)

- ViewData use ViewDataDictionary type
- is server side code
- is used to pass any data type from action method to view and we can display this data on view (the same as Viewbag)
- work in key -value principle (not dynamic principal as Viewbag)
- This type of data binding is known as loosely binding
- we create a new key in ViewData and then assign some data to it --> ViewData["Title"] = 123;
- use razor syntax in View --> @ViewData["Title"]
- ViewData is usually used for a Title of different page in the browser , The title showed and related to current page (Also we put code in the \_Layout.cshtml file (line 7))
- Disadvantage of this method --> we will not get any errors if we misspell the ViewData key in the View file --> (Example if we put in View file --> @ViewData["Titleeee"])

### Use ViewData with object data type

1. In Controller we write

```C#
ViewData["book"] = new Book(){Author = "Rob", Id = 2};
```

2. in View we write on the top of the file

```C#
@{
   var bookInfo = ViewData["book"] as Book; //<--assign what type is the data type, (Book data type), we don't need it in Viewbag because it works in dynamic principle

   //or , another example
   @foreach(var item in ViewData["AllUsers"] as List<User>){  //for complex type data we tell to compiler what data we are expecting from ViewData ,  where --> User is a Model Class
    //lopping Users
   }
}
```

3. In View file in Html tags we write

```C#
<h2>@bokInfo.Id</h2> //<--wil give 2
<p>@bookInfo.Author</p> //<--will give Rob
```

- Also we can pass some Object with ViewData from action method --> ViewData["book"] = new Book(){Author = "Rob", Id = 2}; to View file

```C#
// in View file

@{
  var MyData = @ViewData["book"] as dynamic;
}

<p>@MyData.Author</p>
<p>@MyData.Id</p>
```

.......................................................................................................................

# How data travel through the files

### When we addidng some Data to the database using form (--> See AddnewBook.cshtml)

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
- Entity Framework Core (EF Core) connects ASP.NET Core App and Database
- EF Core is a middle service, works between ASP.NET and DB
- EF Core is Microsoft's official technology to interact with relational database
- EF Core can be used not only in ASP.NET core app but also in any .NET technology - Mobile app, Windows app, Console.app other app in .NET
- EF Core can work with lots of databases:

  - SQL Server
  - MySQL
  - Cosmos db (it is No SQL DB)
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

Migrations use Entity Framework to add tables and its properties to DB, which we indicated in Data folder/DB context file (MyBookStoreWebContext.cs) and Data/Models.

#### To work with SQL Server database: (we need to install)

[All available EntityFrameworkCore nuget packages To work with different DB is HERE](https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli)

- ```C#
  Microsoft.EntityFrameworkCore
  ```

- ```C#
  Microsoft.EntityFrameworkCore.Relational
  ```

- ```C#
  Microsoft.EntityFrameworkCore.SqlServer //<-- this package has dependancy of Microsoft, EntityFrameworkCore, Microsoft.EntityFrameworkCore.Relational and will instal them automatically
  ```

- ```C#
  Microsoft.EntityFrameworkCore.Tools
  ```

- ```C#
  Microsoft.EntityFrameworkCore.Design //<-- needs for EntityFrameWorkCore migrations,
  ```

- By creating Data folder and BookStoreContext.cs class --> then by using these commands it will create database and create tables:

1. ```bash
   dotnet ef migrations add AnyMigrationsName  //<--to add changes to database
   ```

2. ```bash
      dotnet ef database update  //to update database
   ```

# How to connect your project to SQl Server Database.(used for local database and Azure SQL database)

### For Local DB

1. First of all you need to install and configure Microsoft SQL Server in your VS Code --> Check the video
   [How to Install and Configure Microsoft SQL Server](https://www.youtube.com/watch?v=EbePHFUE9sI)

https://totatca.com/ttc-144/

or

[YouTube video](https://www.youtube.com/watch?v=GBboALYvvuE)

[Microsoft Docs](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver16&tabs=ubuntu2004)

2. Create folder Data in the root of your project
3. Create all required files in Data folder, such as --> Models(Entity classes), Database connection file(in our case it is BookStoreContext.cs file).
   Database connection file should contain DB connection string or connection string can be added to Program.cs file.
4. (If needed use this step) In Azure Data Studio app--> Create New Connection --> fill Server name, User Name, Password, Database name from Microsoft Azure Database
5. We can Create new DB or add changes to existing DB or update the existing DB --> write in terminal

```C#
dotnet ef migrations add NameOfTheMigration  //<-- create migrations folder (with Up and Down methods in our App)
dotnet ef database update  //<-- will connect to DB, DataBase appear in Azure Data Studio App
```

5. It will create BookStore database on your local server--> in Azure Data Studio, with all tables that you have indicated in the Database connection file (from Data folder)

### For hosted DB - in Azure portal

(go to step 2 if you have already Installed and configured Microsoft SQL Server for local Database)

1. Install and configure Microsoft SQL Server in your VS Code --> Check this video
   [How to Install and Configure Microsoft SQL Server](https://www.youtube.com/watch?v=EbePHFUE9sI)

https://totatca.com/ttc-144/

or

[YouTube video](https://www.youtube.com/watch?v=GBboALYvvuE)

[Microsoft Docs](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver16&tabs=ubuntu2004)

2. Create hosted Database in Azure Portal (create all these services in --> Azure Portal --> create subscriotions, Resource group, App Services, SQL database (Here you add the name of your Database), SQL Server, Key Vault, App registration)
3. We create new folder with any name (in our case) Data folder. Inside Data folder we create new class to use it for database --> Books.cs
4. Create DatabaseNameContext.cs file in Data folder ( data connection to database).
   The name must be the same as you created in the Auzre Portal, SQL database. NameOfTheDataBase + Context.cs <-- syntax of the database connection file name in our app.

- BookStoreContext ////BookStore <--can be any name, this is a name of our database
- BookStoreContext ////Context <--must be always in the end of name of our class, suffix

5. In Azure Data Studio app--> Create New Connection --> fill Server name, User Name, Password, Database name from Microsoft Azure Database
6. After creating Data folder with all needed files and content we can create --> MIGRATIONS FOLDER
7. To create Migrations folder, we write -->

```C#
# creating database with tables, Class proporties converts to table columns.
# Also, can add new proporties to the table in database
# use this command when add something in Classes in Data folder(Database)

dotnet ef migrations add (AnyMigrationsName)  //add changes and create databases with tables, create Migrations Folder in our App, Create all the tables and columns

dotnet ef migrations add init  //<--Example

```

- Migrations is used to create tables in DB, Migrations folder has info about added or deleted columns, tables in the DB

8. To make update to our database, we write -->

```C#

dotnet ef database update  //<-- will connect to DB, DataBase appear in Azure Data Studio App

```

9. To see All migrations commands, we write -->

```c#

dotnet ef migrations

```

10. To remove some proporties from table

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

[CDN bootstrap css link](https://getbootstrap.com/docs/5.3/getting-started/introduction/#cdn-links)

- Then in View files we can use Bootstrap css

#### jQuery is used for

- automatic client side validation from server side validation (--> See Views/Shared/\_Layout.cshtml file, line 77-88)
- using Scripts in the bottom of the View page --> allow to add some functions to the page that will be trigered, when page is loading, loaded or from other page behaviour (--> See Views/Home/Index.cshtml line 106 - 119)

#### Ajax is used for (similar to jQuery)

- when adding new book in Views/Book/AddNewBook.cshtml in line 40 we have <form> tag --> where we put

```C#
<form method="post" data-ajax="true" data-ajax-complete="myComplete" data-ajax-success="mySuccess" data-ajax-loading="#myLoader" asp-controller="Book" asp-action="AddNewBook">
```

and in the bottom of the page

```C#
@section scripts {
@* Here we can insert any scripts*@

    <script>
        function myComplete(data){ //<--name of the function is myComplete, to receive the data from your function we can pass some attributes--> (data)
        //this code wil execute , doesn't metter if the request is successful or fail.
            alert("I am from complete");
        }

        function mySuccess(data){  //<--name of the function is mySuccess

        //this code wil execute if the request is successful
            alert("I am from Success");
        }

        function myFailure(data){  //<--name of the function is myFailure

        //this code wil execute if the request was failed
            alert("I am from Failure");
        }
    </script>
}
```

Which allow to trigger different function depending from View Page behaviour. if it will successfuly post the form or fail or other.

...............................................................................................................

# Partial views (Similar to React JS components)

- it is simple cshtml file that can be inserted anywhere in any View.
- It helps to separate (break up) the long code into small parts and insert it with partial views. (Similar to React components - components with certain code , that can be inserted anywhere,simplify the code).
- Helps to remove duplicate code from app.
- If we compare with Partial View, In ViewComponent file we can connect to database and get the data from database (-->See Components/ TopBooksViewComponent.cs )
- In one application we can use as many partial views as we want, there is no limit of partial views
- The partial tag helper render content asynchronous

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
@Html.Partial("partialViewName", Model);

or

@await Html.PartialAsync("partialViewName", Model);  //<--synchronous method
//or
@{await Html.RenderPartialAsync("partialViewName", Model);}   //<--synchronous method
```

##### There are other partial tag helpers such as:

- model
- for (rarely used)
- view-data (used to assign a ViewData to pass to the partial view)

```C#
<partial name="somePartialName" for="Name" />  //<-- for tag helper doesn't work together with model tag helper


@ViewData["TestMsg"];
<partial name="somePartialName" model="@Model.Name" view-data="ViewData" />  //<-- can pass data from ViewData to the partial view
```

.................................................................................................................................................................

# ViewComponent

- is similar to partial views but much more powerful, don't use model binding, only depend on the data provided when calling into it. ViewComponent is a special feature that is used to render some data(view +data) on a view file without actually being the part of HTTP life cycle. It is not part of Http life cycle.

- If we compare with Partial View, In ViewComponent file we can connect to database and get the data from database (-->See Components/ TopBooksViewComponent.cs )

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

return View(books);
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
   Usually to get for instance AboutUs page we put in URL --> "/Home/AboutUs"
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

```C#
[HttpGet("{id:int?}")]
```

................................................................................................................................

# DEPENDENCY INJECTION (DI)

🔥 To understand this concept better lets make an example:

- In Repository/Services we can make some methods where will be all the logic.
- Then In Services we can make some methods where will be all the logic
- From controller we call this "Services" methods.
- TO don't instantiate an instance of "Service" in each method in Controllers we can use Dependency Injection.

#### What Classes must be used as Dependency Injection

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

[-->Click HERE<--](https://www.youtube.com/watch?v=vDJMzs1OYqo)

```C#
//These lifetime services is used in Program.cs file -->

builder.Services.AddScoped<BookRepository, BookRepository>();

//or with Interface
builder.Services.AddScoped<IBookTypeRepository, BookTypeRepository>();
```

In ASP.NET Core MVC, there are 3 different ways to register services with build-in dependency injection:

- Singleton (AddSingleton<>) <--Same instance/object for all entire application. The service is registerd with the first time they are requested and the same instance/object of the service will be used throughout the lifetime of the application. (When you change something you need to stop application, and rerun it againg to apply new changes). Singleton --> Creates only single object and keep the data of this object all the time while application is running.

```C#
//AddSingleton: This ensures that the CarRepository is only instantiated once during the lifetime of the application and is shared across requests.
//AddSingleton keeps the new data in local memory, only while the app is running, when the app is closed, the data will be lost. It keeps only original data = 10 cars in the local memory
```

- Transient(AddTransient<>) <-- A new instance/object of the service will be created every time it is requested.Every time when you use this service or call this service then new instance will be created and discarded when it is no longer needed. Will create new object every time when needed and discard the object when function has been executed. Works opposite way than Singleton

- Scoped (AddScoped<>) <-- These are created once per client request. instance will be the same for all http request. This means that the same instance/object of the service will be used throughout the entire request, but a new instance/object will be created for each subsequent request.

```C#
//Keep the data all the time, even if you close the application
```

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

```C#
//if use connection string in appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!"
  }
```

### In appsettings.json we can keep :

- ConnectionString to localhost only
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

# User Secrets file (used only for my local setting secrets, works only on my machine, not for a web for eveyone, works only in Development environment) <-- Not encrypted file(working with other people, not save, other people in your team can see the secrets can be shown in secret.json file)

- This file (secrets.json) is not stored with the rest of your project, it is on my machine only, located otside of Project folder

- Connection WebConnection string will not work in Production environment (From my secrets.json file)
- User secret file is located in separate Directory outside of Application Repository, outside of our main Project file, therefore it will never be uploaded to GiHub

To keep sensative data - User Id, Passwords, ConnectionString etc.

1. Download nuget package ->

```C#
Microsoft.Extensions.Configuration.UserSecrets
```

2. Then right click on (ProjectName).csproj file of your project(<--located in very bottom of the list of all files of our Project)-(step 1 from the pic below) --> then click Manage user secrets (step 2)

![pic10](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic10.jpg)

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

### Environment variables <-- not Encrypted, anyone in the team with access can see the variables. (Another way how to keep secrets for your app). Can be used in Production and Development Environment.

- Create .gitignore file and .env file --> put all secrets in .env.
- In Azure Portal put your secrets in Environment variables

### Azure Key Vault (we use it in Web to keep secrets files) <-- Encrypted file, after creating the Key Vault. Key Vault will keep all your variables and you can use them in passwords, connection strings etc. but you will never see the secrets again. Very save approach. Used in Production environmnet

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

# AREA

- if you don't want to keep your files in common Models, Controllers and Views folder, Area - allow to add models, controllers and views in separate folder for complitely separate feature (functionality) to our app (keep it separately)

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

- This commands will create Areas folder at the root level of our app with all the features (--> Create Models, Controllers, Views folders in Area folder + ScaffoldingReasMe.txt file at the root level of our app --> in the bottom of the project's root folder)
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
