using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using Project_MVC_BookShop2.Repository;  //import BookRepository
using Project_MVC_BookShop2.Models;

using System.Collections.Immutable;
// this file is an entry point of application
// main file, without this file none will work

using Project_MVC_BookShop2.Data;  //connect MyappDbContext ,from Data folder
using Microsoft.EntityFrameworkCore;  //to work with database
using Microsoft.AspNetCore.Identity;  // LigIn, SignUp, Security 
using Azure.Identity;   //connects Key Vault
using Microsoft.Extensions.Configuration.AzureKeyVault;   //connects Key Vault
using Azure.Security.KeyVault.Secrets;  ////import installed nuget package (if you installed any nuget packages , you need this to use packages here)-> AddRazorRuntimeCompilation();
using Project_MVC_BookShop2.Service; //<-- to use UserService class, get User Id and is the user logged-in or not
using Project_MVC_BookShop2.Helpers;
using Microsoft.Rest.TransientFaultHandling; //<--to use ApplicationUserClaimsPrincipalFactory class, to add Claims

// builder allow to create our app by small parts
var builder = WebApplication.CreateBuilder(args);      // createBuilder -creating a host, is main in deployment of our app,

//the same thing as --> // var builder = WebApplication.CreateBuilder(args); 
// in this sample we can assign an environment variable
// var builder = WebApplication.CreateBuilder(new WebApplicationOptions
// {
//     EnvironmentName = Environments.Production
// });   



// Add services to the container.
// Services - this method allow to connect all different services for our app, after dot
// in here we adding services with name-> AddControllersWithViews, to make application aware that we going to use MVC patern ( Model, View, Controller)
//In Web API , we only need Models and Controllers, in this case use -> builder.Services.AddControllers(); (to check all the templates , we put in terminal --> dotnet new list)
//AddRazorRuntimeCompilation -update server automatically in any change of code, <--Razor(ViewEngine) will compile, convert all C# and HTML on View page into HTML code only
//by default,not installed(due to file size and not required when in production) --> Razor file compilation takes places only at build and publish times using the Razor SDK, but not at the runtime.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


///////////////Uncomment this code to disable Client side validation(only in Development mode), add it to the existing chain
//  .AddViewOptions(option =>{
//     option.HtmlHelperOptions.ClientValidationEnabled = false;
// });



// builder.Configuration.AddUserSecrets<Program>();  ///////connect User secrets for local settings, works only on my machine, not for a web for eveyone


// builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>(); //<--use this code if you are planning to use Usernames, Passwords, LogIn, SignUp in yur App (it has already build in properties), it has standard AspNetUsers table. if we don't want to add any extra properties to AspNetUsers table, to work With Identity Core we need to configure Identity to work with database
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<MyBookStoreWebDbContext>();  //<--use this code if you are planning to use Usernames, Passwords, LogIn, SignUp in yur App. Use this code if you are planning to ADD some properties to AspNetUsers table, ( we use Class ApplicationUser with added properties) to work With Identity Core we need to configure Identity to work with database
//AddIdentity <-- will get all the feature that are available in Identity framework core
//IdentityUser <--is a table that already build in Identity framework, to work with a user we insert this table
//ApplicationUser <-- created Model class with added properties to AspNetUsers table 
//identityRole <--is a table that already buildIn in Identity framework, to work with roles
//to connect or to work with our database we write--> .AddEntityFrameworkStores<BookStoreContext>();
//BookStoreContext <--our database name
//MyBookStoreWebDbContext <-- our database name


///////////Configure the password complexity (User Registration), we can customize default password settings -->
// builder.Services.Configure<IdentityOptions>( options=>{
// ////////here we configure all the settigs for Identity framework, 
// //////////if we need to configure settings for pasword --> update settings for password
// options.Password.RequiredLength = 5;
// options.Password.RequiredUniqueChars = 1;
// options.Password.RequireDigit = false;
// options.Password.RequireLowercase = false;
// options.Password.RequireNonAlphanumeric = false;
// options.Password.RequireUppercase = false;
// });



//we use [Authorize] Attribute in BookController--> if User is not logedIn and press AddnewBook it will redirect him to LogIn page
builder.Services.ConfigureApplicationCookie(config =>{
    config.LoginPath = "/login";  //<--redirect User to Login page
});


// you can add Nuget Packages -> dotnet add package << PackageName >>  /Example-> dotnet add package System.Text.Json
//  all Nuget Packages you can find in SOLUTION EXPLORER (LEFT MAIN BAR in the bottom , under dependencies)


//Our Repository classes and Context class must be used with Dependency injection !!!!!!!!
builder.Services.AddScoped<BookRepository, BookRepository>();  //registration of BookRepository services to work with dependency injections
builder.Services.AddScoped<IBookTypeRepository, BookTypeRepository>();  //to work with dependency injections, Here we used ILaguageRepository (interface), --> the code means: if we use ILanguageRepository then provide an objecy of LanguageRepository class
builder.Services.AddScoped<IBasketRepository, BasketRepository>();  //to work with dependency injections
builder.Services.AddScoped<AccountRepository, AccountRepository>();  //to work with dependency injections. this allow us to use Identity framework, use usernames, passwords, etc.
builder.Services.AddScoped<UserService,UserService>();  //to work with UserService class, to get Logge-in User Id in any controller or service
//in Dependency Injection we can use Interfaces or classes

//we can define the same connection string (insted of puting string in BookStoreContect.cs we put it here) and removing -> protected override void OnConfiguring metod from BookStoreContect Class
// builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!"));

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,ApplicationUserClaimsPrincipalFactory>(); //to save logged-in User details into Claims, then we can use them anywhere in our app


// Configuration -> services that allow to acces to the data that we mentioned in appsettings.json or to access our Secrets
//we need to use IConfiguration in Programm.cs to have access to secrets and appsettings.json data , we use--> builder.Configuration
if(builder.Environment.IsDevelopment())  //if Environment = Development --> do this code (it will show local dabase )
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options => //this helps our app understand that we use MyBookStoreWebDbContext database and also we can use database in depandency injection --> context
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //HERE WE GETTING DATA FROM LOCAL DATABSE

/////another syntax how to write the same connection
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  /<--make a variable with connection string
//options.UseSqlServer(connectionString);  /<-- connect to SQL Server
}




//Key Vault after sometime expires and won't connect to DB -->  Need to Renew Client Secret for KeyVault in Microsoft Azure
if(builder.Environment.IsProduction()) //if Environment = Production --> do this code, getting data from SQL server Azure Portal, using Key Vault
{
var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");             //getting info from appsettings.json
var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");           //getting info from appsettings.json
var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");  //getting info from appsettings.json
var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");    //getting info from appsettings.json

//this allow us to authenticate us in Azure ID, to prove that we are who we are, to access to resources that we want in Azure Portal   
var credential = new ClientSecretCredential(keyVaultDirectoryID.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());

//adding Azure Key Vault, use all our values to access Azure Key Vault
builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

//the tool that go and create that connectionString with Azure Key Vault
var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);


builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString())); //inject DB context into our app
//ProdConnection <-- is the name thet we created in Key Vault in Azure Portal to keep our Secrets in secret (our connection string to database) 
}


//Use this code if we connecting to the database from MyBookStoreWebDbContext class --> line 63-89, 
//No need this code If we connecting from Programm.cs file --> and we use connection in Development and Production ENV
//Our Repository classes and Context class must be used with Dependency injection !!!!!!!!
//This line register our database and we can use our database in Dependency injection --> context
// builder.Services.AddDbContext<MyBookStoreWebDbContext>(); //we are telling to our application that we use BookStoreContext class as DB connection (Also, this line needs to make dependency injection in different classes), with this example connection string must present in MyBookStoreWebDbContext class --> protected override void OnConfiguring method
//(the same) or write--> builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //<--we can put connection string over here as well, then delete all protected override method from MyBookStoreWebDbContext class


var app = builder.Build();  //creating our web app


// Configure the HTTP request pipeline.- is a middleware(using app,... to go to next task, to handle reqests and responses)
// Configure - this method needs to connect all needed components to our app
//this part about ->info errors, show correct msg to user, in development and in production

if (!app.Environment.IsDevelopment())  //if our environment = not development do  -->this code . Environment variables located in-> launchSettings.json (isProduction(), isStaging())
{
    app.UseExceptionHandler("/Home/Error");  // <-- will show this page in productions for common users, in case of any errors (except 404,400,500 etc.)
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// app.Environment.EnvironmentName  <--will gives us Environment variable value


// app.UseStatusCodePages();  <--putting unexisting route will give - Page Not Found, ususally this section is not used in production because it returns a message that isn't useful to users (Page not found, status code: 404)
//app.UseStatusCodePages(Text.Plain,"Status Code Page with formatted string : {0}");  //<-- UseStatusCodePages with the format string, can write any message and in brackets will show statusCode error number
//app.UseStatusCodePages(async context =>{ 
//if(context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
//{HERE WE GENERATE A CUSTOM 404 PAGE
// context.HttpContext.Response.ContentType = "text/html";
//await context.HttpContext.Response.WriteAsync("<html><body><h1>Page Not Found</h1></body></html>")
//}else if(context.HttpContext.Response.StatusCode == StatusCodes.Status500InternalServerError){.....the same code as above..}});




//////first option, declaring that we are going to use status code error pages
// app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}"); 
/////<--if the route don't exist--> here we can create our custom error pages, StatusCodeError <--it is a route, {0} <--shows status code number, it is placeholder for status code
/////Then we can create action method in HomeController for this error page, line 128 

//////second option, declaring that we are going to use status code error pages
app.UseStatusCodePagesWithReExecute("/StatusCodeError", "?statusCode={0}");
//here we use second argument






// app.UseHttpsRedirection();  //redirection from http ->to https
app.UseStaticFiles();   // UseStaticFiles -> located in folder wwwroot, allow to work with files in wwwroot(css, photos, pictures in wwwroot folder)

app.UseRouting();   // routing connection, -> needs for endpoint connection, allow to use different routes in controllers , should be writen before app.MapControllerRoute

app.UseAuthentication();  //authentication connection, Work together with Identity Core Framework -->to use passwords ,LogIn,SignUp etc.(Authentication must be above Authorization!!!! to work correctly)

app.UseAuthorization();   // authorization connection, Work together with Identity Core Framework 
//UseAuthentication, UseAuthorization must be above MapControllerRoute



// endpoints connection, all your pages registration from Controllers folder,
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    

//in this middleware ( --> app.MapDefaultControllerRoute()) already build in  --> pattern: "{controller=Home}/{action=Index}/{id?}");
//can be used instead of this middleware--> app.MapControllerRoute( ......); 
// app.MapDefaultControllerRoute();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name : "MyAreas",
        pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});



//use this instead -->app.MapControllerRoute( ..)
// // this will return a msg on URL -> / in this example we don't use Controllers and Models
// app.UseEndpoints(endpoints =>     //<-- endpoints -can be any name
// {
// endpoints.MapGet("/", async context =>
// {
//     await context.Response.WriteAsync("Hello world!");
// });
// });

// // or
// app.Use(async(context, next)=>{
// await context.Response.WriteAsync("Hello from middleware1 \n");
//await next.Invoke();       //<-- allow to invoke another middleware if we use any after this middleware
// });
// app.Use(async(context, next)=>{     //<-- next middleware
// await context.Response.WriteAsync("Hello from middleware2 \n");
// });


//app.Map("/map1", mapappbuilder => {     //<-- mapappbuiler -can be any name
//mapappbuilder.Run(async context =>{
//await context.Response.WriteAsync("Map1 \n");
//});
//});

app.Run();  //run an app, after all have been added to request pipeline, it is last line in Program.cs file

//whatever code writen beyond this middleware--> app.Run();  will never be executed