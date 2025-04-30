using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;    //need to -> inherit from Controller , allow to use Routes 
// using Project1.Models;
// using Project1.Views.Home; //User class connection
using Project_MVC.Models;     //Contact class connection
using Project_MVC.Models;      //Clients class

namespace Project_MVC.Controllers
{
    public class ContactsController : Controller
    {
        
        public IActionResult Index()
        {



    //     var clients = new List<Clients>(){
    //     new Clients("Emy","Morgen",17,"test@mail.com", "Hello"),
    //     new Clients("Tom","Teky",22,"testtt2@mail.com", "Hey"),
    // };


            return View();
        }




        // this method receive data by method post
        // our form has method post
        [HttpPost]

        // Contact class , with name contact(new object created from Contact class)
        // contact object will contain all info from user (from the form )
        public IActionResult Check(Contact contact)
        {

            // we check is the receiving data ok? or not 
            //if the data ok we do the code in curly brackets
            if(ModelState.IsValid)
            {


                // this document dosn't see - List of clients 
                // clients.Add(contact);



                // Here you put what you want to do , when user click button send (send the form)
                // we can connect to database, etc.

                // if the data is ok , we will rederect user to main page
                return Redirect("/");
            }

            // if data not ok we return Index page
            return View("Index");
        }
    }
}