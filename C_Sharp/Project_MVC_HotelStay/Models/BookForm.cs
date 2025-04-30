using System.ComponentModel.DataAnnotations;

namespace Project_MVC_HotelStay.Models;

public class BookForm
{

    [Key]
    public int Id { get; set;}
    [Required(ErrorMessage = "Please enter the Name")]
    public string Name { get; set;} = "";
    public string? Desciption { get; set; }
}