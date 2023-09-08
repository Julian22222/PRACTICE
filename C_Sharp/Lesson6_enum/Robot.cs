using System;

namespace Lesson6_enum
{
    class Robot
    {

        // private string _name;
        // private string _model;
        // private int _weight;

        public Robot(string name, string model, int weight){
        Name = name;
        Model = model;
        Weight = weight;
        }

        public string Name{get;set;}
         public string Model{get;set;}
         public int Weight{get;set;}

         public void GetInfo(){
            System.Console.WriteLine("Hello" + Name);
         }

        
    }
}