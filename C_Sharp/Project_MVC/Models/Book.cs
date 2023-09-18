using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Project_MVC.Models
{
    public class Book
    {
        // [Key] //automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
        public int Id { get; set; }

        // [Required]
        public string Name { get; set; }

        public string Author { get; set; }

       

       




        // constructor
        public Book()
        {
            
        }
        // prop + tab  --> quick btns for get,set
        //ctor + tab --> constructor


    }
}