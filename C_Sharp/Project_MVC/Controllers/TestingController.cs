using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Controllers
{
    public class TestingController : Controller
    {

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Test1()
    {
        return View();
    }

    public IActionResult Test2()
    {
        return View();
    }

    public IActionResult Test3()
    {
        return View();
    }
    }
}