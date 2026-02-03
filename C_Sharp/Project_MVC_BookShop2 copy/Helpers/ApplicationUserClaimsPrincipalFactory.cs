using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;  //to use UserClaimsPrincipalFactory class, we inherit from this class
using Microsoft.Extensions.Options;
using Project_MVC_BookShop2.Models;  //to use ApplicationUser

namespace Project_MVC_BookShop2.Helpers
{

    //we inherit from UserClaimsPrincipalFactory class
    //right click on --> UserClaimsPrincipalFactory --. Go to definition
    //ApplicationUser <-- created class to add extra properties to AspNetUsers Table
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>
    {
        
        //Constructor
        //userManager <--Created name
        //roleManager <--Created name
        //options <--Created name
        // calling base constructor --> : base(userManager, roleManager, options)  <--passing all 3 parammeters
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options) {
// UserClaimsPrincipalFactory --Go to defenition




        }

        //new method -> GenerateClaimsAsync
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);  //<--store data in variable, variable can have any name, here in user we have all details about logged-in user
            //here we can add as many claims as we need for logged-in User
            //new Claim <--add Claim class, right click --> Claim ---> Go to Definition
            //UserFirstname --> is the name of our Key (Can have any name), then we can use this key in the View to get the --> user.FirstName (Logeininfo.cshtml line 16)
            //user.FirstName --> this is a value
            identity.AddClaim(new Claim("UserFirstname", user.FirstName ?? "")); //<-- if there is no user.FirstName (user not logged-in we show "" or can be null)
            identity.AddClaim(new Claim("UserLastName", user.LastName ?? "")); //<-- adding LastName to the Claims
            return identity;
        }
    }
}