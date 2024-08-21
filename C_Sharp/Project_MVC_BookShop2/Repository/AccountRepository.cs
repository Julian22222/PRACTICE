using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;   //Can you Task with async await , and to use Task
using Microsoft.EntityFrameworkCore;  //to use ToListAsync method, SaveChangesAsync(), FindAsync(id); and other asyn methods
using Project_MVC_BookShop2.Models;  //Book class import connection
using Project_MVC_BookShop2.Controllers;   //BookControllers methods connection
using Project_MVC_BookShop2.Data;  //import BookstoreContext database and Books class from Data folder
using Microsoft.AspNetCore.Identity; // to use <IdentityResult>
using Project_MVC_BookShop2.Service;  //to work with userManager, signInmanager <-- these needed to save save User details

namespace Project_MVC_BookShop2.Repository;

public class AccountRepository
{   

//UserManager - is buildin in Identity framework, needs for SignUp 
//SignInManager is buildIn in Identity framework, needs for LogIn, Log Out, and we can check if User Loged In or Not 
//These 2 Manager are very importan to work with user details and to handle Authentication and Authorisation


//IdentityUser <--It is a User Class (is buildin in Identity framework)
//_userManager <-- is a name of User Class (can be any name)

//  private readonly UserManager<IdentityUser> _userManager;  <--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table.
    private readonly UserManager<ApplicationUser> _userManager;  //UserManager is used for Sign Up, Change password and etc. (all operations that are specific for particular user --> these are available in this _userManager ), Here we creating variable for SignUp and ChangePassword, to interact with database's AspNetUsers table

// ApplicationUser --> is User Class that we created, where we added extra porperties to standart AspNetUsers table
    private readonly SignInManager<ApplicationUser> _signInManager;  //SignInManager is used for Sign In and Sign Out. Here we creating variable for SignIn and SignOut, to interact with database's AspNetUsers table

    private readonly UserService _userService;

    // constructor, here we use dependency injection, application will resolve AccountRepository automatically
    // because we have written the code in our -Program.cs file -> (line 50) -> 
    //builder.Services.AddScoped<AccountRepository, AccountRepository>();
    public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, UserService userService){
        _userManager = userManager;  //using _userManager -> we have acces to AspNetUsers table, when SignUp
        _signInManager = signInManager; //using _signInManager -> we have acces to AspNetUsers table, when SignIn
        _userService = userService;  //geting userId if the User LoggedIn
    }

  //  public AccountRepository(UserManager<IdentityUser> userManager){
    //     _userManager = userManager;
    // }   <---use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table



    //action method
    //IdentityResult <--return data type
    public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel){

        //  var user = new IdentityUser(){   //<--IdentityUser is buid-in Class in Identity Core
        // Email = userModel.Email,
        // UserName = userModel.Email,
        // } <--use this code if we use standard AspNetUsers table, if we don't add any properties to AspNetUsers table
        

        //assign all inserted data from Client to --> user variable (filled Form from Signup.cshtml page) 
        var user = new ApplicationUser(){  //ApplicationUser <-- is our created Model Class
            Email = userModel.Email,
            UserName = userModel.Email,

            //Added columns to AspNetUsers table from ApplicationUser Model Class
            FirstName = userModel.FirstName,
            LastName = userModel.LastName
        };

        //CreateAsync <-- method that are provided by UserManager,
        //CreateAsync --> Go to Definition (to check what variables it takes)
       var result = await _userManager.CreateAsync(user, userModel.Password);  //interacting with UserManager, AspNetUsers table (create mew User --> SignUp), Adding new User to Database
       
       return result;
    }





    //SignIn method
    public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel){ //SignInModel (first)<--data type, signInModel(secnd)<-- data from AccountController(that came from LogIn Form)
        //here we use a method that is available in SignInManager
        //PasswordSignInAsync --> Go to Definition (to check what variables it takes)

     var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false); //checking data in database's AspNetUsers table, is the data exists (false --> how many incorrect atepts for lockout)
    //signInModel.RememberMe <--check box in Sign in Form, (can be true or false)

    return result;
    }



    //SignOut method,--> will logOut the user
    public async Task SignOutAsync(){
     await _signInManager.SignOutAsync();
    }




    //Change Password action method
    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model){

        var userId = _userService.GetUserId();  //<--Geting Id of Logged-in user
        var user = await _userManager.FindByIdAsync(userId);

//go to defenition of --> ChangePasswordAsync , to see what parametrs it takes
       return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    }
    


}
