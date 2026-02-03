using System;

namespace Lesson6_enum
{

// enum - means enumiration
// name -> Type
// it has all the types for our robots, which will be created from class Killer

enum Type { Enemy, Hero, Traitor}


    class Killer : Robot
    {

        public Type type;

        public Killer(string name,string model,int weight, Type type) : base(name,model,weight){
            this.type = type;
        }
        

        public void ShowType(){
            if(this.type ==Type.Hero){
            Console.WriteLine("He is a Hero");
            }
            else{
                Console.WriteLine("It is not a Hero");
            };
        }

    }
}