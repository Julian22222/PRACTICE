using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP31_OOP
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
            Car ferrari = new Car("F40",471, 30, 317.0f);

            // if no data is passed in the brackets to the constructor , than overload will run
            Car audi = new Car();

            ferrari.ShowInfo();

            audi.ShowInfo();

            ferrari.BecomeOlder(5,50);

            ferrari.ShowInfo();  // data after BecomeOlder function


        }
    }

    class Car {
        public string Name;
        public int HorsePower;
        public int Age;

        public float MaxSpeed;

//Constructor
        public Car(string name, int horesPower, int age, float maxSpeed){

            // this.Name - will use Name value from this class
            // this.Name = name;  
            Name = name;
            HorsePower = horesPower;
            Age = age;
            MaxSpeed = maxSpeed;
        }

// overload - will ran this code if no data is passed in the brackets
        public Car(){
            Name = "Audi";
            HorsePower = 500;
            Age = 2;
            MaxSpeed = 250;
        }

        public void ShowInfo(){
            Console.WriteLine($"\nName of the car: {Name}\nNumber of Horse Power: {HorsePower}\nAge of the car: {Age}\nMax speed: {MaxSpeed}");
        }

        public void BecomeOlder(int years, int runAwayHorses){
            Age+=years;
            HorsePower-=runAwayHorses;
        }
    }
}