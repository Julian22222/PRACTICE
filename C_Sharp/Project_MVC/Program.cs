using System;
using System.Collections.Immutable;
// this file is an entry point of application
// main file, without this file none will work

using Project_MVC.Data;  //connect MyappDbContext ,from Data folder
using Microsoft.EntityFrameworkCore;  ////import installed nuget package

// builder allow to create our app by small parts
var builder = WebApplication.CreateBuilder(args);     // createBuilder -creating a host, is main in deployment of our app,

// Add services to the container.
// Services - method to connet all different services for our app, after dot
// in here we adding services with name-> AddControllersWithViews
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyappDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyappDbConnectionString"))); //inject DB context into our app

// Configuration -> services that allow to acces to the data that we mentioned in appsettings.json 

var app = builder.Build();  //creating our web app



// Configure the HTTP request pipeline.- is a middleware(using app,... to go to next task, to handle reqests and responses)
// Configure - this method needs to connect all needed components to our app
//this part about ->info errors, show correct msg to user, in development and in production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();  //redirection from http ->to https

app.UseStaticFiles();  // UseStaticFiles -> located in folder wwwroot, allow to work fith files in wwwroot(css, photos, pictures)

app.UseRouting();    // routing connection, -> needs for endpoint connection

app.UseAuthorization();  // authorization connection

// endpoints connection, all your pages registration from Controllers folder,
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();  //run an app, after all have been added to request pipeline