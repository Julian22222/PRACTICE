using System.Runtime.InteropServices.WindowsRuntime;
using System;       //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Linq;    //querying any type of data source
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using Microsoft.AspNetCore.Mvc;           //allow to use Routes , //importing to inherit Controller
using Project_MVC_BookShop2.Repository;    //BookRepository connection and methods - GetAllBooks and others
using Project_MVC_BookShop2.Models;        //SignUpUserModel class import connection
using Microsoft.AspNetCore.Mvc.Rendering;   //to use SelectList, SelectListItem, SelectListGroup, use Html partial views
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Project_MVC_BookShop2.Controllers;


public class AccountController : Controller
{

private readonly AccountRepository _accountRepository;


//controller
public AccountController(AccountRepository accountRepository){

_accountRepository = accountRepository;  //dependency injections, to work with Identity framework, (we can use this Depenedency injection because we wrote - line 50- in Program.cs)
}


    [Route("sign-up")]  //Attribute routing 
    public IActionResult Signup(){
        return View();
    }


    [Route("sign-up")]  //Attribute routing 
    [HttpPost]
    public async Task <IActionResult> Signup(SignUpUserModel userModel){

     if(ModelState.IsValid){
        
        //logic when post SignUp form
        var result = await _accountRepository.CreateUserAsync(userModel);




        if(!result.Succeeded){ //if it is false- means User has not been created in database, the we do this code( Here we check is the user was created in the database)

        //then we display all error messages
            foreach( var errorMessage in result.Errors){ //result.Errors <--has all errors messages

                ModelState.AddModelError("", errorMessage.Description); //assign all error msges to ModelState

            }
        return View(userModel);  //if result is not successful we return a View and pasing - userModel (User wasn't created in the database)
        }




        ModelState.Clear(); // clear the ModelState
     }   
     
        return View();  //if result is successful return View
    }






     [Route("login")]  //Attribute routing 
    public IActionResult Login(){
        return View();
    }


    [Route("login")]  //Attribute routing 
    [HttpPost]
    public async Task <IActionResult> Login(SignInModel signInModel, string returnUrl){  //string returnUrl <--used to return user to correct page after he LogedIn
        if(ModelState.IsValid){

        var result = await _accountRepository.PasswordSignInAsync(signInModel);


        if(result.Succeeded){ //If logIn is Successful do this code --> (result.Succeeded == true)


        if(!string.IsNullOrEmpty(returnUrl)){ //returnUrl <--used to return user to correct 
            return LocalRedirect(returnUrl);
        }




        return RedirectToAction("index", "Home");  //return Home/index View Page
        }


        ModelState.AddModelError("", "invalid credentials"); //if LogIn is not Seccessful, show this message

        // ModelState.Clear(); // clear the ModelState

        }
        return View(signInModel);  ////if result is not successful we return a View and pasing - signInModel (User wasn't found in the database)
    }


    [Route("logout")] //URL--> /Account/Logout --> will logOut the user
    public async Task<IActionResult> Logout(){
        await _accountRepository.SignOutAsync();
        return RedirectToAction("Index","Home");
    }    
} 



