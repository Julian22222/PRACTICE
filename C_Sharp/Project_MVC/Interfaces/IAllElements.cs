using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Project_MVC.Models;  //import Element class

namespace Project_MVC.Interfaces
{
    public interface IAllElements
    {
         
        // IEnumerable - is a list type
        IEnumerable<Element> Elements {get;}
        IEnumerable<Element> getFavElements {get;set;}

        // function
        Element getObjectElement(int elementId);

    }
}