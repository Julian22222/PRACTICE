using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP14_GAME
{
   class Program
    {
        static void Main(string[] args)
        {


 Random rand = new Random();

// will create random health , damage and armor for fighter 1
 float health1 = rand.Next(90,100);
 int damage1 = rand.Next(5,20);
 int armor1 = rand.Next(25,65);

 float health2 = rand.Next(80,150);
 int damage2 = rand.Next(20,40);
 int armor2 = rand.Next(65,100);

 Console.WriteLine($"Fighter 1 - {health1} health, {damage1} damage, {armor1} armor");
 Console.WriteLine($"Fighter 2 - {health2} health, {damage2} damage, {armor2} armor");

 while( health1 > 0 && health2 >0 ){
    health1 -= Convert.ToSingle(rand.Next(0,damage2 +1)) / 100 * armor1 ;
    health2 -= Convert.ToSingle(rand.Next(0,damage1 +1)) / 100 * armor2 ;

    Console.WriteLine("Fighter 1 health: " + health1);
    Console.WriteLine("Fighter 2 health: " + health2);
 }

 if(health1 <=0 && health2 <=0){
    System.Console.WriteLine("It is a Draw");
 }else if(health1 <= 0){
    System.Console.WriteLine("Fighter 1 is died");
 }else if(health2 <= 0){
    System.Console.WriteLine("Fighter 2 is died");
 }



        }
    }
}
