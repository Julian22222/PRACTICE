using System;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;   //connect [Display(Name="Nickname")] block 

namespace Project1.Models
{
    public class Contact
    {
        // to change different name for any of the fields( change Name to Nickname)
        [Display(Name = "First name")]

        // Validation (info checker)
        // this field will be obligatory to field
        // ErrorMessage="...." - allow to change error text to show to user
        [Required(ErrorMessage ="Please insert your Name" )]

        // to check inserted length, max length of this input is 15 ,then show msg
        [StringLength(15, ErrorMessage ="Your name must contain less than 15 symbols")]


        // variable declaration
        public string Name {get;set;}


          // to change different name for any of the fields( change Name to Last name)
          [Display(Name = "Last name")]

          [Required(ErrorMessage ="Please insert your surname" )]
          public string Surname {get;set;} ="";

          

          [Display(Name = "Your contact number :")]
          [Required(ErrorMessage = "A phone number is required.")]
          [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
          [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
          public string PhoneNr {get;set;}

          [Required(ErrorMessage ="Please insert your email" )]
          [EmailAddress(ErrorMessage ="Please insert your email" )]
          public string Email {get;set;}

          [DataType(DataType.DateTime)]
          [Display(Name = "Date amd time (if required)")]
          public DateTime Date { get; set; }


          [Required(ErrorMessage ="Please insert your message" )]
          [StringLength(50, ErrorMessage ="Your name must contain less than 50 symbols")]
          public string Message {get;set;}
          

    }

    internal class DateTypeAttribute : Attribute
    {
    }
}