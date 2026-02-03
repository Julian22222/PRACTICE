using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP38_Virtual_methods
{
   class Program
    {
        static void Main(string[] args)
        {

        NonPlayerCharacter[] characters = {
            new NonPlayerCharacter(),
            new Farmer(),
            new Knight(),
            new Child()
        };

        foreach(var charactrer in characters){
            charactrer.ShowDescription();
            System.Console.WriteLine(new string('-',40));
        }
      
        }
      
    }

    class NonPlayerCharacter{
        public virtual void ShowDescription(){
            Console.WriteLine("I am simple NPC , I can walk");
        }
    }

    // inherit all data,proporties and methods from base class - NonPlayerCharacter
    class Farmer : NonPlayerCharacter
    {
        public override void ShowDescription(){
            // will show base method (from NonPlayerCharacter) -ShowDescription msg
            base.ShowDescription();
            Console.WriteLine("Also I am Farmer and I can do hard work");

        }
    }

    class Knight : NonPlayerCharacter
    {
    public override void ShowDescription(){
         Console.WriteLine("I am Knight, I can fight");
    }

    }

    class Child : NonPlayerCharacter
    {

    }

}

