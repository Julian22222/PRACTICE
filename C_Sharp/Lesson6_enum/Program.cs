// using System;
using System.Collections.Generic;

namespace Lesson6_enum
{
    class Program
    {
        static void Main()
        {
            Robot bot = new Robot("Alex", "ddd",20);
            Console.WriteLine(bot.Name);

            Killer killer = new Killer("Boby","ggg",34, Type.Hero);
             Console.WriteLine(killer.Name);
             killer.ShowType();
            
            Console.WriteLine(new string('_',25));
           
           Killer enemy = new Killer("Rob","mm",21, Type.Enemy);
           System.Console.WriteLine(enemy.Name);
           enemy.ShowType();
          





            }
            



        


        }
    }

