using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lesson3_Read_Write_files
{
    class Program
    {
        static void Main()
        {
        //     Console.WriteLine("Please write a text: ");
        //     string text = Console.ReadLine();
            

        //     // Writing data
        //     // FileStream (class)-allow us to open a file for reading or writing 
        //     // OpenOrCreate - > will open file -info.txt or create if it doesn't exist
        //     // using - is a derective
        // using(FileStream stream1 = new FileStream("info.txt",FileMode.OpenOrCreate)){
        //     // creating byte array -> we can write file only with byte data type
        //     //converting string data type to byte
        //     // we put text variable to our ->info.txt file , from line that user will inserts
        //     byte[] myArray = System.Text.Encoding.Default.GetBytes(text);

        //     // stream1 - is an object
        //     // we write to -myArray, starting from index 0(how many elements we are skipping, in this case we are not skipping anything), 
        //     // insert all text - all myArray.Length(untill what ellemnt we are writing the file, in this case we are writing untill end, full file)
        //     stream1.Write(myArray, 0, myArray.Length);
        // }





        // Reading data
        // File - global class
        // OpenRead -> this method allow us to open and read the certain file 
        // FileStream (class)-allow us to open a file for reading or writing 
        using(FileStream stream2 = File.OpenRead("info.txt")){
            // we receive all the data from "info.txt" in bytes ,therefore we create array-> byte[] array
            // new byte[stream2.Length] -> we define a length for our array 
            byte[] arrayIt = new byte[stream2.Length];
            //stream2 - is an object
            // stream2.Read -> method 
            // arrayIt - is an array, we indicate where we store our data
            // read array - arrayIt,
         // starting from index 0(how many elements we are skipping, in this case we are not skipping anything), 
         // insert all text - all arrayIt.Length(untill what ellemnt we are reading the file, in this case we are reading untill end, full file)
            stream2.Read(arrayIt, 0 ,arrayIt.Length);

            // create variable that we will show in console - textFromFile
            // GetString - convert bytes data types to string type
            string textFromFile = System.Text.Encoding.Default.GetString(arrayIt);
            System.Console.WriteLine(textFromFile);  //show text from the file
        } 


        }
    }




}


