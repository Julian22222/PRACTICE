// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;

namespace Project_MVC_BookShop2.Models
{
public class ChangePasswordModel{

    [Required, DataType(DataType.Password), Display(Name = "Current password")]  //<--mandatory field, data type = password
    public string CurrentPassword { get; set; }

    [Required, DataType(DataType.Password), Display(Name = "New password")]
    public string NewPassword { get; set;}

    [Required, DataType(DataType.Password), Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "Confirm new password does not match")]
    public string ConfirmNewPassword { get; set;}
 
    }
}

