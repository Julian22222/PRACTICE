using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP29_COLLECTION_DICTIONARY
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
        Dictionary<string,string> countriesCapitals = new Dictionary<string, string>();

        countriesCapitals.Add("Australia", "Canberra");   //add country(key) and its capital(value)
        countriesCapitals.Add("Turkey", "Ankara");
        countriesCapitals.Add("England", "London");

        countriesCapitals.Remove("Turkey");  // Delete element with key -Turkey


        Console.WriteLine(countriesCapitals["Australia"]);  //Canberra

        if(countriesCapitals.ContainsKey("Australia")){
               Console.WriteLine(countriesCapitals["Australia"]);  //Canberra
        }
      

        foreach(var item in countriesCapitals){
            Console.WriteLine($"Country - {item.Key}, Capital - {item.Value}");
        }



        }
    }
}
