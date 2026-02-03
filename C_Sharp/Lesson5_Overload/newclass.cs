using System;

namespace Lesson5_Overload
{
    class newclass
    {


// overload methods - you can make methods with the same name but it will receive different parametr numbers or different paramtr types

        public static void Multiply(int a, int b){
            int res = a * b;

           Console.WriteLine("Result:" + res); 
        }


          public static void Multiply(float a, float b){
            float res = a * b;

           Console.WriteLine("Result:" + res); 
        }


         public static void Multiply(int a){
            int res = a * 7;

           Console.WriteLine("Result:" + res); 
        }


        
    }
}