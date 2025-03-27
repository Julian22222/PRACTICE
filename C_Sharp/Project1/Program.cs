// main file, without this file none will work

using Project1.Repository;


// builder allow to create our app by small parts
var builder = WebApplication.CreateBuilder(args);     // createBuilder -creating a host, is main in deployment of our app,

// Add services to the container.
// Services - method to connet all different services for our app, after dot
// in here we adding services with name-> AddControllersWithViews
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


//Our Repository classes and Context class must be used with Dependency injection !!!!!!!!
builder.Services.AddScoped<CarRepository, CarRepository>();


var app = builder.Build();  //creating our web app



// Configure the HTTP request pipeline.
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

app.Run();
