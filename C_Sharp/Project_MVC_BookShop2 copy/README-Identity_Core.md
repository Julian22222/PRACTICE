[Authentication Docs](<https://www.csharpschool.com/blog/ASP-NET%20Core%202-2%20For%20Beginners%20(Part%2010):%20Forms%20Authentication>)

# Identity Core Framework needs for App Security

### Used for:

- Authentication
- Authorization
- LogIn
- Log Out
- Change password
- Forgot password
- etc.

# Identity Core ( responsible for LogIn, SignUp, Passwords, Registration, security in the app and other features)

- It is a universal framework to provide security to any .NET application (can be used Blazor, Razor, ASP.NET CORE MVC and other framework that are available in .NET)
- Common framework for all .NET app
- It is NOT only limited to SignUp / SignIn but provide lots of feature that are requires for security management
- this framework provides all required tables to work with Authentication and Authorisation automaticaly, we don't need to create any extra table by ourself, everething is created automatically
- Identity nuget package install AspNetUsers table with some already build in properties. If we want to add some more properties to AspNetUsers table (such as FirstName,LastName, etc.) we need to create new Model (in our case --> ApplicationUser.cs). All properties from AspNetUser table are used and filled when the user is register in our website

### Identity core features:

- Common framework for all .NET app
- All required tables are generate automaticaly
- Register a user
- Login
- Change Password
- Forgot Password
- User validation (check is it valid user or not)
- Password validation
- Password hashing (It is Security point - you can't store your plain password in your database table), convert user password to hashed password
- Multi factor authentication (Ex - 2f authentication) <--user will be locked out if he is entering wrong password
- Lockout (Block user on "n" wrong attemts), we can block user after 'n' attemts of entering password
- External Identity (Google,Facebook, Microsoft, Twiter etc.), LogIn with Google account etc.
- And much more

# To start working with Identity Core framework we need to install --> (and all other dependent packages will install automatically in our app)

```C#
Microsoft.aspnetcore.identity.entityframeworkcore
```

### To connect Identity core package we need:

1. In Program.cs file , (line 199)-->

```C#
app.UseAuthentication();  //enable authentication connection using middleware, to use passwords ,LogIn,SignUp etc.
```

2. In Program.cs file , (line 60)-->

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

3. In MyBookStoreWebDbContext.cs file we inherit from -->

change from --> BookStoreContext : DbContext

```C#
public class BookStoreContext : IdentityDbContext  // <--will create all needed tables for users and security automatically in our database
```

4. Then we add migrations, (add changes to our database)-->
   If we change anything in Data Folder files we need to do add migrations and update database

```c#
dotnet ef migrations add (AnyMigrationsName)
```

5. To make update our database, we write -->

```c#
dotnet ef database update

```

6. Create Controller --> in our case AccountController.cs
7. Create Model Class (Template that User will fill when signing up )--> in our example --> SignUpUserModel.cs
8. Views/ Account/Signup.cshtml
9. AccountRepository.cs

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

These Default Password settings can be changed. We can customize default password settings --> By following code: (--> See Program.cs file -->line 70)

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

- We can implement Authorize atribute after we implemented SignUp, Log In and Log Out functions

- For example if user not loged in he cannot excess --> Add new book page
- We Implement some security, only loged in user allowed to add new book to database
- If user not loged in then it will be no permission to access some action method, and by clicking on that action method will show --> page NOT FOUND. To solve this error--> we need to use this code to redirect user to certain page if he is not LogedIn

- In Program.cs file we write (--> line 84 )

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

2. We use Authorize atribute in Controller (in our case in BookController.cs --> line 85)

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
      public async Task <IActionResult> Login(SignInModel signInModel, string returnUrl){ //returnUrl <-- comes from URL query parametr that are created when you are clicking on some page, without LogIn
        if(ModelState.IsValid){

        var result = await _accountRepository.PasswordSignInAsync(signInModel);


        if(result.Succeeded){ //If logIn is Successful do this code --> (result.Succeeded == true)


         if(!string.IsNullOrEmpty(returnUrl)){  //if returnUrl exist in URL query then we return this returnUrl page
            return LocalRedirect(returnUrl);   //returnUrl <--used to return user to correct page after login
        }


        return RedirectToAction("index", "Home");  //return Home/index View Page, if User Loged In but there is no return URL in URL query
        }


        ModelState.AddModelError("", "invalid credentials"); //if LogIn is not Seccessful, show this message

        // ModelState.Clear(); // clear the ModelState

        }
        return View(signInModel);  ////if result is not successful we return a View and pasing - signInModel (User wasn't found in the database)
    }
```

- In Views/ Shared / \_LoginInfo.cshtml -->
- here we set returnUrl for Log In button, needed if we on any page and then you click on LogIn button, after LogIn it will retrn you to the same page where you were before

```C#
<a class="btn btn-outline-primary" asp-action="Login" asp-controller="Account"  asp-route-returnUrl="@Context.Request.Path">Login</a>

//we need to access current page path and we can do that easy by using --> @Context

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
