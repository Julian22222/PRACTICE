// using System.IO;
// using System.Security.AccessControl;
using System;   //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;
using System.Linq;  //querying any type of data source
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   //to use server side validations attributes (or Model validation)
using Microsoft.AspNetCore.Http;    //to use IFormFile (special data type to hold information about uploaded files), allow to upload any file to our app, used in Model class


namespace Project_MVC_BookShop2.Models
{
    public class Book
    {

        // [DataType(DataType.Date)] //assign specific type for a field(Password,Date,DateTime, Currency, EmailAddress,CreditCard, PhoneNumber,Time,Upload and others, some attributes are not working in MVC)
        // [Display(Name ="Date")]
        // public string MyField{get;set;}


        // [Key] //automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
        public int Id { get; set; }

        // this is server side validation attributes(or model validation)
        [StringLength(100,MinimumLength =2)]  //max length= 100, and min length =5
        //[StringLength(100)]  <-- max length is 100 , minimum is 0
        //[StringLength(100, ErrorMessage ="Please put no more than 100 characters")] <--Costom erroe message specific for this atribute
        [Required(ErrorMessage = "Please enter the title of you book")] //mandatory field to field, custom msg if the field is not valid
        public string Title { get; set; }


        [Required] //mandatory field to field
        public string Author { get; set; }


        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }  //store Id of our language

        public string? Language { get; set; }

        
        [Required(ErrorMessage = "Please enter the total pages")] //mandatory field to field, custom msg if the field is not valid
        [Display(Name ="Total pages of book")]  //to update the text,(TotalPages will be update on Total pages of book)


 // when you put -> ? -> this field is  - not Required
        public int? TotalPages { get; set; }



        // IFormFile - it is a special data type(it allow to create a property where we can upload the file), this property will hold all the details about uploaded files / Uploded image, img name(file Name)
        [Display(Name ="Choose the cover photo of your book")]
        [Required] //<--mandatory field
        public IFormFile CoverPhoto { get; set; }

        // uploaded image path, from wwwroot/books/cover + File name. We don't need full path --> serverFolder from BookController
        public string? CoverImageUrl {get; set;}


        
       
        // IFormFile - it is a special data type(it allow to create a property where we can upload the file), this property will hold all the details about uploaded files / img (file Name)
        [Display(Name ="Upload your book in pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }

        // uploaded image full path
        public string? BookPdfUrl {get; set;}
       




        // constructor
        // public Book()
        // {
            
        // }
        // prop + tab  --> quick btns for get,set
        //ctor + tab --> constructor


    }
}