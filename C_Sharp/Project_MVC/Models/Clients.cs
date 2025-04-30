using System.Globalization;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Models
{
    public class Clients
    {

        public static readonly List <Clients> clients = new List<Clients>(){
        new Clients("Emy","Morgen",17,"test@mail.com", "Hello"),
        new Clients("Tom","Teky",22,"testtt2@mail.com", "Hey"),
    };

        // [BindProperty]
        // public List<Clients> ExistingUser {get;set;}

        public string Name {get;set;}
        public string LastName {get;set;}
        public int Age {get;set;}
        public string Email {get;set;}
        public string Message {get;set;}

        public string ExperienceLevel { get; set; }
        

        public Clients(string name,string lastname, int age, string email, string message){
            Name = name;
            LastName = lastname;
            Age = age;
            Email = email;
            Message = message;
        }

        // public void exsitingList(){
        //     this.ExistingUser = clients;
        // }
    }
}