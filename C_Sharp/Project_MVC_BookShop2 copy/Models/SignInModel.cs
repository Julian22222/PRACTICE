// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   //to use server side validations attributes
using Microsoft.AspNetCore.Http;    //to use IFormFile (special data type to hold information about uploaded files)




namespace Project_MVC_BookShop2.Models
{
    public class SignInModel
    {

         // This is a class for Log In
        

        // Data anatations
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)] //assign specific type for a field(Password,Date,DateTime, Currency, EmailAddress,CreditCard, PhoneNumber,Time,Upload and others,some attributes are not working in MVC)
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}