using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP12_GAME
{
   class Program
    {
        static void Main(string[] args)
        {
        // our health and damage that create to your enemy
      int playerHealth = 100;
      int playerDamage = 10;

        // enemy's health and damage
      int enemyHealth = 50;
      int enemyDamage = 15;

// iteration will run while playerHealth and enemyHealth is MemoryExtensions > 0
     while(playerHealth > 0 && enemyHealth > 0){
playerHealth -=enemyDamage;
enemyHealth -=playerDamage;


Console.WriteLine(playerHealth + " player's health");
Console.WriteLine(enemyHealth + " enemy's health");

     }
 
if(playerHealth <= 0 && enemyHealth <= 0){
Console.WriteLine("DROW");
}else if (playerHealth > enemyHealth){
Console.WriteLine("You WON");
 }else{
    Console.WriteLine("You Lost!");
 }


        }
    }
}
