using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP18_ARRAY4
{
   class Program
    {
        static void Main(string[] args)
        {


int [] sectors = { 6, 28, 15, 15, 17};
 bool isOpen =true;

while(isOpen){  

    // Console.SetCursorPosition(0,18);

     for(int i = 0; i <sectors.Length; i++){
        Console.WriteLine($"Sector {i +1} has {sectors[i]} seats.");
}

// Console.SetCursorPosition(0,0);
Console.WriteLine("\nFlight Registration");
Console.WriteLine("\n1 - book your seats.\n2 - exit.\n ");
Console.Write("Enter command number: ");

switch(Convert.ToInt32(Console.ReadLine()))
{
    case 1:
    int userSector, userPlaceAmount;
    Console.Write("In Which sector do you want to book your place? ");
    userSector = Convert.ToInt32(Console.ReadLine())-1;
    if(sectors.Length <= userSector || userSector< 0){
        Console.WriteLine("This sector doesn't exist.");
        break;
    }

    Console.Write("How many places do you want to book? ");
    userPlaceAmount = Convert.ToInt32(Console.ReadLine());

    if(userPlaceAmount < 0 ){
    Console.WriteLine("Incorrect places amount.");
    break;
    }

    if(sectors[userSector] < userPlaceAmount){
        Console.WriteLine($"In sector {userSector} not enough spaces, there is {sectors[userSector]} spaces left.");
        break;
    }
    sectors[userSector] -= userPlaceAmount;
    Console.WriteLine("Your booking completed.");
    break;


    case 2:
    isOpen = false;
    break;
}

    }

    Console.ReadKey();
    Console.Clear();

        }
    }
}
