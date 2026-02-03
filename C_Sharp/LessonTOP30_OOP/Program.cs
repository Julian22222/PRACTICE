using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP30_OOP
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
            Car ferrari = new Car();

            ferrari.Age = 25;
            ferrari.HorsePower = 471;
            // Console.WriteLine($"Age:{Age}, HorsePower: {HorsePower}");

            ferrari.ShowInfo();

            ferrari.BecomeOlder(5,50);

        //    Console.WriteLine($"Age:{Age}, HorsePower: {HorsePower}");


        }
    }

    class Car {
        string _name;
        public int HorsePower;
        public int Age;

        private float _maxSpeed;

        public void ShowInfo(){
            Console.WriteLine($"Age of the car is - {Age}");
        }

        public void BecomeOlder(int years, int runAwayHorses){
            Age+=years;
            HorsePower-=runAwayHorses;
        }
    }
}
