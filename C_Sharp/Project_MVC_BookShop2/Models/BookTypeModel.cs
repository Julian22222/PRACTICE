// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   //to use server side validations attributes


namespace Project_MVC_BookShop2.Models
{
    public class BookTypeModel
    {
        public int Id { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public string Description { get; set; } ="";


    }
}