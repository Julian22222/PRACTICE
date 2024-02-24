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

    private readonly UserManager<IdentityUser> _userManager;

    //constructor
    public AccountRepository(UserManager<IdentityUser> userManager){
        _userManager = userManager;
    }


    public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel){

        var user = new IdentityUser(){
            Email = userModel.Email,
            UserName = userModel.Email
        };

       var result = await _userManager.CreateAsync(user, userModel.Password);
       
       return result;
    }
}
