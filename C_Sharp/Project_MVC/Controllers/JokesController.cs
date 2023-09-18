using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;

using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using Project_MVC.Models;   //connection of error
using Project_MVC.Models; //User class connection
using Project_MVC.Controllers;   //clients list connection
using Project_MVC.Data;

namespace Project_MVC.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: JOKES
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Joke.ToListAsync());
               return View();
        }
    }
}