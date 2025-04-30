// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   //to use server side validations attributes


namespace Project_MVC_BookShop.Models
{
    public class Book
    {

        // [DataType(DataType.Date)] //assign specific type for a field(Password,Date,DateTime, Currency, EmailAddress,CreditCard, PhoneNumber,Time,Upload and others)
        // [Display(Name ="Date")]
        // public string MyField{get;set;}


        // [Key] //automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
        public int Id { get; set; }

        // this is server side validation attributes
        [StringLength(100,MinimumLength =2)]  //max length= 100, and min length =5
        [Required(ErrorMessage = "Please enter the title of you book")] //mandatory field to field, custom msg if the field is not valid
        public string Title { get; set; }


        [Required] //mandatory field to field
        public string Author { get; set; }


        public string Description { get; set; }
        public string? Category { get; set; }
        public string? Language { get; set; }

        
        [Required(ErrorMessage = "Please enter the total pages")] //mandatory field to field, custom msg if the field is not valid
        [Display(Name ="Total pages of book")]  //to update the text,(TotalPages will be update on Total pages of book)
        public int? TotalPages { get; set; }

       

       




        // constructor
        // public Book()
        // {
            
        // }
        // prop + tab  --> quick btns for get,set
        //ctor + tab --> constructor


    }
}