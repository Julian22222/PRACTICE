using System.ComponentModel.DataAnnotations;

namespace Project1.Models{

public class Car{

[Key] //automaticaly add an id as an identity column,don;t need to pass the value, it will creare it automatically
public int Id { get; set; }


[Required][Display(Name ="Brand of the car")]
public string Name { get; set; } = "null";


[Range(0, 999.99, ErrorMessage = "Price Should be between 0 and 999.99")]
public decimal Price { get; set; }


[DataType(DataType.Date)]
public DateTime Year { get; set; }


[StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
//When applied to a Name property, the error message created by the preceding code would be "Name length must be between 6 and 8."
public string FuelType { get; set; } = "Petrol";

}

}