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

-->/// this is basic package

2. ```bash
   Microsoft.EntityFrameworkCore.Relational
   ```

   -->/// package to work with ralational database

3. ```bash
   Microsoft.EntityFrameworkCore.SqlServer
   ```

   -->/// package to work with Sql server. use in Program.cs or in Data/MyBookStoreWebDbContext.cs files

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
