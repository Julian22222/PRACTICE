using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  //will allow to inherit from ViewComponent

namespace Project_MVC_BookShop2.Components
{
    public class TestBooksViewComponent : ViewComponent   //inherit from ViewComponent, ///ViewComponent <--must be always in the end of name of our class
    //TestBooksViewComponent <-- can have any name, and is followed by ViewComponent sufix.
    {

        //InvokeAsync <-- name of the method
        // <IViewComponentResult>  <-- return data type, we use only this data type in View Component
        public async Task<IViewComponentResult> InvokeAsync(){
            return View();
        }
    }
}