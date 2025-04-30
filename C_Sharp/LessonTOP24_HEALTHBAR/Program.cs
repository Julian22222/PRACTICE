using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP24_HEALTHBAR
{
   internal class Program
    {
        static void Main(string[] args)
        {
            int health = 5, maxHealth =10;
            DrawBar(health,maxHealth, ConsoleColor.Green);

        }

        static void DrawBar( int value, int maxValue, ConsoleColor color){

            ConsoleColor defaultColor = Console.BackgroundColor;

            string bar ="";
// itteration will add our value ,one by one to the bar variable, colorful bar
            for (int i=0; i<value; i++){
                bar += " ";
            }
            Console.Write("[");
            // assigning color that we receive to the variable
            Console.BackgroundColor =color;
            // show bar
            Console.Write(bar);
            // return default console color 
            Console.BackgroundColor = defaultColor;

            bar ="";
// showing the rest of the empty bar, which is not colored - black , default color
            for (int i=value; i<maxValue; i++){
            bar += " ";
            }
    Console.Write(bar + "]");
        }
    }
}
