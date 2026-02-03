using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LessonTOP40_Abstract_Classes


{
    class Program
    {
        static void Main()
        {

        // Vehicle car = new Vehicle();  // will show an error , can't make objects from abstract classes
        // Vehicle car = new Car(); //can make this exsample

        Vehicle[] vehicles = {
            new Car(),
            new Train()
        }

        }
    }

    abstract class Vehicle{
    // variable declaration
    protected float Speed;

    // abstract method which has not realisation, depends from vehicle which will inherit this class
    public abstract void Move();

    // method with realisation
    public float GetCurrentSpeed(){
        return Speed;
    }

    }

    class Car : Vehicle
    {
    public override void Move(){
        Console.WriteLine("This a car and it's moving on the road");
    }
    }

     class Train : Vehicle
    {
    public override void Move(){
        Console.WriteLine("This a train and it's moving on the rails");
    }
    }
}

