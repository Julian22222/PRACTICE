# How to install nuget packages(Entity Framework Core)

All needed packages to start working with your App

In terminal we write:
dotnet add package (PackageName)

### To install nuget packages with different version , not the latest version we put: (EXMAPLE)

```C#
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0
```

### PackageNames:

1. ```bash
   Microsoft.EntityFrameworkCore
   ```

-->/// this is basic package, allow us to interact with database, Entity Framework Core it is middle service that connects our .NET Core app and DB, it can work with many databases: SQL Server, MySQL, Cosmos db etc.
allow to inherit DbContext, allow to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods

2. ```bash
   Microsoft.EntityFrameworkCore.Relational
   ```

   -->/// package to work with ralational database

3. ```bash
   Microsoft.EntityFrameworkCore.SqlServer
   ```

   -->/// package to work with Sql server. use in Program.cs or in Data/MyBookStoreWebDbContext.cs files (This package has relational dependency of first 2 packages, this package will automatically instal first 2 packages), use this package in MyBookStoreWebDbContext.cs and Program.cs
   (Database Providers,all Nuget packages to work with different DB)[https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli]

4. ```bash
   Microsoft.EntityFrameworkCore.Tools
   ```

   -->/// package to write queries to database

5. ```bash
   Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
   ```

   -->/// package to update automaticaly ViewEngine, Razor ViewEngine will compile amd render C# and HTML to HTML in any change in your code, we use this package in Program.cs, RuntimeCompilation should work only in Development Environment

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

11. ````bash
            Microsoft.Extensions.Configuration.UserSecrets
        ```
        -->///package to use User Secrets file, to keep sensative data - User Id, Passwords, ConnectionString etc.

    ````

12. ````bash
    Microsoft.aspnetcore.identity.entityframeworkcore
        ```
    -->///To start working with Identity Core framework we need to install this package  --> and all other dependent packages will install automatically in our app
    -->///if we want to use LogIn, SignUp, Passwords, Registration, security in the app and other features
    ````
13. ```bash
    Microsoft.AspNetCore.Authorization
    ```

````

-->//to use [Authorize] Attribute in Controllers, when only logedIn users can access certain pages (BookController line 79)
..............................................................................................................

# Different namespaces:

1. ```C#
@using System.Runtime.InteropServices.WindowsRuntime;
```
````

2. ```C#
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

-->to use IFormFile (special data type to hold information about uploaded files), allow to upload any file to our app, used in Model class (--> See Book Model in Models/Book.cs file)

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

    -->// to insert for LogIn, SignUp, Passwords, Registration, security in the app and other features in our app
    -->// to inherit from IdentityDbContext, in BookStoreContext.cs file

17. ```C#
    using System.Dynamic;
    ```

--->// to work with ViewBag ,passing an obect data type to View

18. ```C#
    using System.ComponentModel.DataAnnotations;
    ```

-->// to use server side validations attributes (or Model validation), used in Class Model

```

```

```

```
