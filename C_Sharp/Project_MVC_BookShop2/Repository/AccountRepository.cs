using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;   //Can you Task with async await , and to use Task
using Microsoft.EntityFrameworkCore;  //to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods
using Project_MVC_BookShop2.Models;  //Book class import connection
using Project_MVC_BookShop2.Controllers;   //BookControllers methods connection
using Project_MVC_BookShop2.Data;  //import BookstoreContext database and Books class from Data folder
using Microsoft.AspNetCore.Identity;  //to work with userManager, signInmanager <-- these needed to save save User details

namespace Project_MVC_BookShop2.Repository;

public class AccountRepository
{   

//UserManager - is buildin in Identity framework, needs for SignUp 
//SignInManager is buildIn in Identity framework, needs for SignIn 
//These 2 Manager are very importan to work with user details and to handle Authentication and Authorisation

//IdentityUser <--It is a User Class (is buildin in Identity framework)
//_userManager <-- is a name of User Class (can be any name)

//  private readonly UserManager<IdentityUser> _userManager;  <--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table
    private readonly UserManager<ApplicationUser> _userManager;  //UserManager is used for Sign Up 

    private readonly SignInManager<ApplicationUser> _signInManager;  //UserManager is used for Sign In 


    // constructor, here we use dependency injection, application will resolve AccountRepository automatically
    // because we have written the code in our -Program.cs file -> (line 50) -> 
    //builder.Services.AddScoped<AccountRepository, AccountRepository>();
    public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager){
        _userManager = userManager;
        _signInManager = signInManager;
    }

  //  public AccountRepository(UserManager<IdentityUser> userManager){
    //     _userManager = userManager;
    // }   <---use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table



    //action method
    //IdentityResult <--return data type
    public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel){

        //  var user = new IdentityUser(){ ...} <--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table
        
        var user = new ApplicationUser(){
            Email = userModel.Email,
            UserName = userModel.Email,

            //Add columns to AspNetUsers table
            FirstName = userModel.FirstName,
            LastName = userModel.LastName
        };

        //CreateAsync <-- method that are provided by UserManager
       var result = await _userManager.CreateAsync(user, userModel.Password);
       
       return result;
    }



    //SignIn method
    public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel){
        //here we use a method that is available in SignInManager

     var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false); //passing data to database
    
    return result;
    }

}
