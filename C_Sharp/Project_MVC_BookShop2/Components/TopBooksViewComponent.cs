using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  //will allow to inherit from ViewComponent


namespace Project_MVC_BookShop2.Components
{
    public class TopBooksViewComponent : ViewComponent   //inherit from ViewComponent
    {

        //InvokeAsync method
        // <IViewComponentResult>  <-- return data type
        public async Task<IViewComponentResult> InvokeAsync(){
            return View();
        }
    }
}
