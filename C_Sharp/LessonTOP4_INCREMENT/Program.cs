using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP4_INCREMENT
{
   class Program
    {
        static void Main(string[] args)
        {
    //   int i =0;
    //   i++;
    //   Console.WriteLine(i);


    int health;
    int armor;
    int damage;
    int percentConverter = 100;
    System.Console.WriteLine("Insert Health amount:");
    health = Convert.ToInt32(Console.ReadLine());
     System.Console.WriteLine("Insert Armor amount:");
    armor = Convert.ToInt32(Console.ReadLine());
     System.Console.WriteLine("Insert Damage amount:");
    damage = Convert.ToInt32(Console.ReadLine());

health-=damage * armor / percentConverter;

Console.WriteLine($"You got {damage} damage. You have {health} amount of health");
        }
    }
}
