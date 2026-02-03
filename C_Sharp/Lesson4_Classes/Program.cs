// using System;
using System.Collections.Generic;

namespace Lesson4_Classes
{
    class Program
    {
        static void Main()
        {
            Robot bot = new Robot();
            bot.Name = "Bot";
            bot.Weight = 800;
            bot.cordinates = new byte[] {0,0,0};

            Console.WriteLine($"Name: {bot.Name}, Weight: {bot.Weight}");
            Console.WriteLine(new string('_',25));



            Killer killer = new Killer();
            killer.Name = "Killer";
            killer.Weight = 1000;
            killer.cordinates = new byte[] {0,0,10};

            Console.WriteLine($"Name: {killer.Name}, Weight: {killer.Weight}");
            killer.printValues();

            Console.WriteLine(new string('_',25));


            Simple basicRobot = new Simple("Petre","M400", 500);
            Console.WriteLine(basicRobot.Name);
             Console.WriteLine(basicRobot.Model);
             Console.WriteLine(basicRobot.Weight);
             basicRobot.Weight = 200;
            Console.WriteLine(basicRobot.Weight);
            basicRobot.Walk();

            Console.WriteLine(new string('_',25));




            Robot bot1 = new Robot();
            bot1.Weight = 1000;
            Console.WriteLine(bot1.Weight);

            Console.WriteLine(new string('_',25));





            Simple [] robotsArray = { new Simple("A","1",100), new Simple("B","2",105),new Simple("C","3",110)};
            foreach(var robot in robotsArray){
                System.Console.WriteLine($"Name of the robot: {robot.Name}");
            }
            
            Console.WriteLine(new string('_',25));



            NewRobot Smart = new NewRobot("Smart", "S1",200);
            Console.WriteLine(Smart.Name);
            Console.WriteLine(Smart.Model);
            Smart.Walk();

            Console.WriteLine(new string('_',25));


            List <Simple> robots = new List <Simple> ();
            robots.Add(new Simple("Alex","B1",10));
             robots.Add(new Simple("John","B2",20));
              robots.Add(new Simple("Mick","B3",25));
               robots.Add(new Simple("Ben","B4",30));

               foreach( Simple obj in robots){
                Console.WriteLine(obj.Name);
               }


        }
    }
}


