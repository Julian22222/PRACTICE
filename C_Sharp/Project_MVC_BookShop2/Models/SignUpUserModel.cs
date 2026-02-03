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
    public class SignUpUserModel
    {

        // class for SignUP 

        //data anatations, display attributes

        [Required(ErrorMessage ="Please enter your name")]
        public string FirstName { get; set; }

    
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please enter your email")]
        [Display( Name ="Email address" )]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]  //email template
        public string Email { get; set; }


        [Required(ErrorMessage ="Please enter a strong password")]  //mandatory field
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]  //compare value with ConfirmPassword
        [Display( Name ="Password" )]  //show this line above input field
        [DataType(DataType.Password)]  //password template, type = password
        public string Password { get; set; }


        [Required(ErrorMessage ="Please confirm your password")]  
        [Display( Name ="Confirm Password" )]
        [DataType(DataType.Password)]  //password template
        public string ConfirmPassword { get; set; }
    }
}