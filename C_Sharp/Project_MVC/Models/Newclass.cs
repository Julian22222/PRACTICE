using System.Globalization;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Project_MVC.Models
{
    public class Newclass
    {
        public bool IsActive { get; set; }

    public Newclass(bool isactive){
       IsActive = isactive;
    }
    }

}