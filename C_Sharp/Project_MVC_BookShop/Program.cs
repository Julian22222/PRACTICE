using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using Project_MVC_BookShop.Repository;  //import BookRepository

using System.Collections.Immutable;
// this file is an entry point of application
// main file, without this file none will work

using Project_MVC_BookShop.Data;  //connect MyappDbContext ,from Data folder
using Microsoft.EntityFrameworkCore;  ////import installed nuget package (if you installed any nuget packages , you need this to use packages here)-> AddRazorRuntimeCompilation();

// builder allow to create our app by small parts
var builder = WebApplication.CreateBuilder(args);      // createBuilder -creating a host, is main in deployment of our app,

// Add services to the container.
// Add services to the container.
// Services - method to connet all different services for our app, after dot
// in here we adding services with name-> AddControllersWithViews, to make application aware that we going to use MVC patern ( Model, View, Controller)
//AddRazorRuntimeCompilation -update server automatically, <--Razor(ViewEngine) will compile, convert all C# and HTML on View page into HTML code only
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Uncomment this code to disable Client side validation(only in Development mode), add it to the existing chain
//  .AddViewOptions(option =>{
//     option.HtmlHelperOptions.ClientValidationEnabled = false;
// });



// you can add Nuget Packages -> dotnet add package << PackageName >>  /Example-> dotnet add package System.Text.Json
//  all Nuget Packages you can find in SOLUTION EXPLORER (LEFT MAIN BAR in the bottom , under dependencies)

builder.Services.AddScoped<BookRepository, BookRepository>();  //to work with dependency injections


// you can add Nuget Packages -> dotnet add package << PackageName >>  /Example-> dotnet add package System.Text.Json
//  all Nuget Packages you can find in SOLUTION EXPLORER (LEFT MAIN BAR in the bottom , under dependencies)

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
// options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //inject DB context into our app

//we can define the same connection string (insted of puting string in BookStoreContect.cs we put it here) and removing -> protected override void OnConfiguring metod from BookStoreContect Class
// builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStore;User ID=sa;Password=julik3322J!"));
builder.Services.AddDbContext<BookStoreContext>(); //we tell to our application that we use BookStoreContext class (Also, this needs for dependency injection)


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

app.UseHttpsRedirection();  //redirection from http ->to https
app.UseStaticFiles();   // UseStaticFiles -> located in folder wwwroot, allow to work fith files in wwwroot(css, photos, pictures)

app.UseRouting();   // routing connection, -> needs for endpoint connection

app.UseAuthorization();   // authorization connection


// endpoints connection, all your pages registration from Controllers folder,
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();  //run an app, after all have been added to request pipeline
