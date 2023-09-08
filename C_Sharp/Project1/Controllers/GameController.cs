using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Views.Home; //User class connection

namespace Project1.Controllers
{
    public class GameController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
    }
}