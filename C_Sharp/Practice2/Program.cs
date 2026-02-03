using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// Create branch
app.Map("/health", (IApplicationBuilder branchBuilder) =>
{	
    // Terminal middleware
    branchBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Healthy");
    });
});

app.Map("/anotherbranch", (IApplicationBuilder branchBuilder) => 
{
    branchBuilder.UseStaticFiles();
    // Terminal middleware
    branchBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Terminated anotherbranch!");
    });
});

// Terminal middleware
app.Run(async context =>
{
    await context.Response.WriteAsync("Terminated main branch");
});

// app.UseMiddleware(async (context, next)=>{
//     await context.Response.WriteAsync("Hello from my first middleware");
//     await next();
// await context.Response.WriteAsync("Hello from my third middleware");
// });



// app.Use(async (context, next)=>{
//     await context.Response.WriteAsync("Hello from my second middleware");
// });

app.Run();
