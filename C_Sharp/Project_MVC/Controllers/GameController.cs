using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_MVC.Models;
using Project_MVC.Models; //User class connection

namespace Project_MVC.Controllers
{
    public class GameController : Controller
    {

        public IActionResult Index()
        {
            
            return View();
        }
        
    }
}