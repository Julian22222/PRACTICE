using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP32_OOP
{
   internal class Program
    {
        static void Main(string[] args)
        {

            bool isOpen = true;

        // creating tables separately - one by one 
        // Table table1 = new Table(1,4);
        // Table table2 = new Table(2,8);
        // Table table3 = new Table(3,10);

        // To see ShowInfo method for all tables we need to address separately to each table
        // table1.ShowInfo();
        // table2.ShowInfo();
        // table3.ShowInfo();



        // It is easier and better to create array of our tables, instead of creating each table one by one
        Table [] tables = { new Table(1,4), new Table(2,8), new Table(3,10) };


        while(isOpen){

        Console.WriteLine("Book your tables and seats in our Restaurant.\n");

        // iteration through each table
        for(int i=0; i<tables.Length; i++){
            tables[i].ShowInfo();
        }

            Console.Write("\nInsert table number to book: ");
            int desiredTable = Convert.ToInt32(Console.ReadLine()) - 1;  //index starts from 0
            Console.Write("\nInsert how many seats do you want to book: ");
            int desiredPlaces = Convert.ToInt32(Console.ReadLine());

            bool isReservationCompleted = tables[desiredTable].Reserve(desiredPlaces);

            if(isReservationCompleted){
                Console.WriteLine("Booking is completed");
            }else{
            Console.WriteLine("Booking failed, Not enogh places.");
            }

            Console.ReadKey();
            Console.Clear();
        }




        }
        class Table
        {
            public int Number;
            public int MaxPlaces;
            public int FreePlaces;

            public Table(int number, int maxPlaces){
                Number = number;
                MaxPlaces = maxPlaces;
                FreePlaces = maxPlaces;
            }

            public void ShowInfo(){
                Console.WriteLine($"Table number: {Number}, free seats:{FreePlaces} out of {MaxPlaces}");
            }

            public bool Reserve(int places){
                if(FreePlaces >= places){
                    FreePlaces -= places;
                    return true;
                }else{
                    return false;
                }
        }
            }
        

        
    
    }
}