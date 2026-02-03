using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Project_MVC.Models;  //import Category class

namespace Project_MVC.Interfaces
{
    public interface IElementsCategory
    {
        
        // function
         IEnumerable<Category> AllCategories {get;}


    }
}