using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;   //connect [Display(Name="Nickname")] block , to use Requred block and Display block-> []

namespace Project_MVC.Models
{
    public class Contact
    {
          // to change different name for any of the fields( change Name to Nickname)
          // this is atribute
        [Display(Name = "Nickname")]

        // Validation (info checker)
        // this field will be obligatory to field
        // ErrorMessage="...." - allow to change error text to show to user
        [Required(ErrorMessage ="Please insert your name" )]

        // to check inserted length, max length of this input is 15 ,then show msg
        [StringLength(15, ErrorMessage ="Your name must contain less than 15 symbols")]


        // variable declaration
        public string Name {get;set;}


          // to change different name for any of the fields( change Name to Last name)
          [Display(Name = "Last name")]

          [Required(ErrorMessage ="Please insert your surname" )]
          public string Surname {get;set;}

          [Required(ErrorMessage ="Please insert your age" )]
          public int Age {get;set;}

          [Required(ErrorMessage ="Please insert your email" )]
          [EmailAddress(ErrorMessage ="Please insert your email" )]
          public string Email {get;set;}

          [Required(ErrorMessage ="Please insert your message" )]
          [StringLength(50, ErrorMessage ="Your name must contain less than 50 symbols")]
          public string Message {get;set;}
    }
}