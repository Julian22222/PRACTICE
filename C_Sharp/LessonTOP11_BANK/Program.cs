using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP11_BANK
{
   class Program
    {
        static void Main(string[] args)
        {
        
      
      float money;
      int years;
      int percent = 4;

      Console.Write("Insert amount of money that you want to put to the Bank for percentage: ");
      money = Convert.ToSingle(Console.ReadLine());

      Console.Write("For how many years do you want to keep money in the bank? ");
      years = Convert.ToInt32(Console.ReadLine());

      for( int i = 0; i < years; i++){
        money += money /100 * percent;
        Console.WriteLine("This year " + money);

        Console.ReadKey();
        // Console.ReadKey(); - NotSupportedException to press any key to get another result
      }
 


        }
    }
}