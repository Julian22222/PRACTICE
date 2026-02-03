using Microsoft.AspNetCore.Mvc;
using Project_MVC_HotelStay.Models;

namespace Project_MVC_HotelStay.Controllers;

public class BookingController : Controller
{
    
    public IActionResult AddBooking(){
        return View();
    }


    [HttpPost]
     public IActionResult AddBooking(BookForm model){

        if(model.Desciption == null){
            ModelState.AddModelError("Desciption", "Please enter description");
        }

        if (ModelState.IsValid){

        return RedirectToAction("Index","Home");
        }

        ModelState.AddModelError("", "This is Error 1");

        return View(model);
    }

}
