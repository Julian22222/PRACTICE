using System.Globalization;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Project_MVC.Models
{
    public class Newclass
    {
        public bool IsActive { get; set; }
public string String {get;set;}
 
// public Newclass(string str){
// String= str;
// }

//   public void info()
//     {
//         Console.WriteLine("Hellooooo");
//     }

// public void ShowText(string str)
//    {
//       Console.WriteLine(str);
//    }


static void Display(string message)        
    {            
        Console.WriteLine(message);        
    }

    }





}