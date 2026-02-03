using System.ComponentModel.DataAnnotations;

namespace Project1.Models{

public class Car{

// [Key] //create a Primary Key
//Database --> automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
public int Id { get; set; }


[Required][Display(Name ="Brand of the car")]
public string Name { get; set; } = "";

[Required][Range(0, 99999.99, ErrorMessage = "Price Should be between 0 and 99999.99")]
public decimal Price { get; set; }


[Required]
[DataType(DataType.Date)]
[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
public DateTime Year { get; set; }


[Required][Display(Name ="Type of fuel")]
//When applied to a Name property, the error message created by the preceding code would be "Name length must be between 6 and 8."
public string FuelType { get; set; } = "";

[Required][Display(Name ="Is the car available for viewing or test drive?")]
public bool IsAvailable { get; set; }

        internal object ToJson()
        {
            throw new NotImplementedException();
        }
    }

}