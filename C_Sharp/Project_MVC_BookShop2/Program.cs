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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Security.KeyVault.Secrets;  ////import installed nuget package (if you installed any nuget packages , you need this to use packages here)-> AddRazorRuntimeCompilation();


// builder allow to create our app by small parts
var builder = WebApplication.CreateBuilder(args);      // createBuilder -creating a host, is main in deployment of our app,


//the same thing as --> // var builder = WebApplication.CreateBuilder(args); 
// in this sample we can assign an environment variable
// var builder = WebApplication.CreateBuilder(new WebApplicationOptions
// {
//     EnvironmentName = Environments.Production
// });   


//to use IConfiguration in Programm.cs and have access to secrets and appsettings.json data
// var provider = builder.Services.BuildServiceProvider();
// var configuration = provider.GetRequiredService<IConfiguration>();


// Add services to the container.
// Add services to the container.
// Services - method to connet all different services for our app, after dot
// in here we adding services with name-> AddControllersWithViews, to make application aware that we going to use MVC patern ( Model, View, Controller)
//AddRazorRuntimeCompilation -update server automatically, <--Razor(ViewEngine) will compile, convert all C# and HTML on View page into HTML code only
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



///////////////Uncomment this code to disable Client side validation(only in Development mode), add it to the existing chain
//  .AddViewOptions(option =>{
//     option.HtmlHelperOptions.ClientValidationEnabled = false;
// });



// builder.Configuration.AddUserSecrets<Program>();  ///////connect User secrets for local settings, works only on my machine, not for a web for eveyone


// builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>(); //<--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table, to work With Identity Core we need to configure Identity to work with database
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<MyBookStoreWebDbContext>();  //<--use this code if we add some properties to AspNetUsers table, to work With Identity Core we need to configure Identity to work with database
//AddIdentity <-- will get all the feature thta are available in Identity framework core
//IdentityUser <--is a table that already build in Identity framework, to work with a user we insert this table
//identityRole <--is a table that already buildIn in Identity framework, to work with roles
//to connect or to work with our database we write--> .AddEntityFrameworkStores<BookStoreContext>();
//BookStoreContext <--our database name


//////////// //Configure the password complexity (User Registration)
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
    config.LoginPath = "/login";
});


// you can add Nuget Packages -> dotnet add package << PackageName >>  /Example-> dotnet add package System.Text.Json
//  all Nuget Packages you can find in SOLUTION EXPLORER (LEFT MAIN BAR in the bottom , under dependencies)



builder.Services.AddScoped<BookRepository, BookRepository>();  //to work with dependency injections
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();  //to work with dependency injections, Here we used ILaguageRepository (interface)
builder.Services.AddScoped<AccountRepository, AccountRepository>();  //to work with dependency injections. this allow us to use Identity framework, use usernames, passwords, etc.



// if(builder.Environment.IsDevelopment())  //if Environment = Development --> do this code
// {

// // builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
// // options.UseSqlServer(configuration["ConnectionStrings:WebConnection"])); //inject DB context into our app
// }


// if(builder.Environment.IsProduction()) //if Environment = Production --> do this code
// {

var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");


//this allow us to authenticate us in Azure Key Vault
var credential = new ClientSecretCredential(keyVaultDirectoryID.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());

//adding Azure Key Vault, use all our values to access Azure Key Vault
builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

//the tool that go and create that connectionString with Azure Key Vault
var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);


builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString())); //inject DB context into our app
// }

//we can define the same connection string (insted of puting string in BookStoreContect.cs we put it here) and removing -> protected override void OnConfiguring metod from BookStoreContect Class
// builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStore;User ID=sa;Password=julik3322J!"));
builder.Services.AddDbContext<MyBookStoreWebDbContext>(); //we tell to our application that we use BookStoreContext class (Also, this needs for dependency injection)


// Configuration -> services that allow to acces to the data that we mentioned in appsettings.json 

var app = builder.Build();  //creating our web app


// Configure the HTTP request pipeline.- is a middleware(using app,... to go to next task, to handle reqests and responses)
// Configure - this method needs to connect all needed components to our app
//this part about ->info errors, show correct msg to user, in development and in production

if (!app.Environment.IsDevelopment())  //if our environment = development do  -->this code . Environment variables located in-> launchSettings.json (isProduction(), isStaging())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





// app.UseHttpsRedirection();  //redirection from http ->to https
app.UseStaticFiles();   // UseStaticFiles -> located in folder wwwroot, allow to work fith files in wwwroot(css, photos, pictures)

app.UseRouting();   // routing connection, -> needs for endpoint connection

app.UseAuthentication();  //authentication connection, to use passwords ,LogIn,SignUp etc.(Authentication must be above Authorization!!!! to work correctly)

app.UseAuthorization();   // authorization connection

// endpoints connection, all your pages registration from Controllers folder,
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//in this middleware already build in  --> pattern: "{controller=Home}/{action=Index}/{id?}");
//can be used instead of this middleware--> app.MapControllerRoute( ......); 
// app.MapDefaultControllerRoute();

app.Run();  //run an app, after all have been added to request pipeline
