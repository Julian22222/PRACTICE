using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
// using Project1.Models;
// using Project1.Views.Home; //User class connection
using Project1.Models;  //Contact class connection

namespace Project1.Controllers
{
    public class ContactsController : Controller
    {
        
        public IActionResult Index()
        {
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
            ModelState.AddModelError("", "Please check your data");
            return View("Index");
        }
    }
}

