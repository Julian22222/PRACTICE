using System;    //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims; //<-- to use FindFirstValue function


namespace Project_MVC_BookShop2.Service
{
    public class UserService
    {
        //to work with Id we need httpContext


private readonly IHttpContextAccessor _httpContext;  //<--creating httpContex variable
//Also we can use HttpContext class directly in Controller

        //constructor
        public UserService(IHttpContextAccessor httpContext)
        {
           _httpContext = httpContext;
        }



        //creating few action methods in this class
        // httpContext.HttpContext.User  <-- inside this User we need to get details from our Claims
        // User? --> if user not loged in to our app -> User = null, we use User? to handle null value. User is optional
        //then we use FindFirstValue method ( which is available from System.Security.Claims;)
        //ClaimTypes.NameIdentifier <-- key for the Claims
        //now we can use this action method everywhere in this application to get Id of loged-in user
        public string GetUserId(){
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        public bool IsAuthenticated(){
            return _httpContext.HttpContext.User.Identity.IsAuthenticated; //<--using this action method we can check is the User Logged-In or not
        }
    }
}