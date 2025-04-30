using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Project_MVC.Interfaces;  //connect IElementsCategory interface
using Project_MVC.Models;   //connect Category class

namespace Project_MVC.mocks
{

    // this class will implement interface IElementCategory
    public class MockCategory : IElementsCategory
    {

       public IEnumerable<Category> AllCategories {

        // here you can return data from database
        get {
            return new List<Category>{
                new Category {categoryName="entertainment", desc="spend your time palying"},
                new Category {categoryName="study", desc="time for self-development"}
            };
        }
       }

    }
}