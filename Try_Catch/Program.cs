using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Try_Catch
{
    class Program
    {
        static void Main()
        {
            // in here we use C Sharp as an example

        // during running some programms errors can occur, therefore we use -try catch.

        // catch block - allow us to process, or handle error and show 
        // required message to the user or developer, without shouting down the running programm.

        // in catch block we write down the error that we are trying to catch , and then we 
        // show a message that we want to show to the user, when this error occur.

        try{
            Console.WriteLine("Please insert your number: ");
            int num = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine($"Your number is {num}");

        }catch(FormatException){
            // if user will imput some letters -it will give us - FormatException error.
            // string can't be a -> int data type, 
            //in this case we show msg - You have entered wrong input

            System.Console.WriteLine("You have entered wrong input");
        }



        }
    }

}
