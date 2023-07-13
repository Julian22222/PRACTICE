using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP34_OOP_IS_A
{
   class Program
    {
        static void Main(string[] args)
        {

  
        Knight warrior1 = new Knight(100,5,10);
        Barbarian warrior2 = new Barbarian(100, 1, 7, 2);

        // TakeDamage is accessible from Knight and Barbarian classes
        warrior1.TakeDamage(500);
        warrior2.TakeDamage(250);

        Console.Write("Knight: ");
        // showInfo is accessible from Knight and Barbarian classes
        warrior1.ShowInfo();

           Console.Write("Barbarian: ");
        // showInfo is accessible from Knight and Barbarian classes
        warrior2.ShowInfo();



        }
    }

    class Warrior{

        // protected- allow to access to this variable from classes that inherit this class 
        // but no one can't change it from main block by rewriting the variable or change variable
        // Health = 10000, can't rewrite the variable
        protected int Health;
          public int Armor;
        public int Damage;

// constructor of base class
        public Warrior(int health, int armor, int damage){
        Health = health;
        Armor = armor;
        Damage = damage;
        }

        public void TakeDamage(int damage){
            // damage - has small letters ,
            // damage comes as an argument to this function
            Health -= damage - Armor;
        }

        public void ShowInfo(){
            Console.WriteLine(Health);
        }

    }

//   class Knight inheritance from Warrior class
    class Knight : Warrior{

    //   invoking constructor from Warrior class(base class)
    //   : base  <-means, we are calling constructor from base class( Warrior)
    //  base(health,armor,damage)  <-these are arguments that we want to pass to base constructor( constructor in Warrior class)

//  public Knight(int health, int damage) : base(health, 5 ,damage){ }  <- armor = 5, and in the first brackets we don't mention - int armor
        public Knight(int health, int armor, int damage) : base(health,armor,damage){ }

        public void Prey(){
        Armor +=2;
        }
    }

//   class Barbarian inheritance from Warrior class
    class Barbarian : Warrior{

        public int AttackSpeed;
        public Barbarian(int health, int armor, int damage, int attackSpeed) : base(health,armor,damage * attackSpeed){
            AttackSpeed = attackSpeed;
         }

        //   public Barbarian(int health, int armor, int damage) : base(health,armor,damage){ }

        public void Shout(){
            Armor -=2;
            Health +=10;
        }
    }
}
