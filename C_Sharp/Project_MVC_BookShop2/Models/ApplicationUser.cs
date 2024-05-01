// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   //to use server side validations attributes
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;    //to use IFormFile (special data type to hold information about uploaded files)
//to work with userManager, signInmanager <-- these needed to save save User details


namespace Project_MVC_BookShop2.Models
{
    public class ApplicationUser : IdentityUser //new class inherit all properties and methods from IdentityUser Class
    {
        //this class allow us to add extra properties to standart AspNetUsers table alredy build-in properties,
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // DateOfBirth is optional , it is not mandotary field
        public DateTime? DateOfBirth { get; set; }
    }
}