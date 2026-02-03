using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP35_GETER_SETTER
{
   class Program
    {
        static void Main(string[] args)
        {
        //    iMovable car = new iMovable();  //cannot create new object from interface
        iMovable car = new Car();
        iMovable humane = new Human();
            
        }
      
        }

        // interfaces are similar to classes but you cannot create new object from interfaces
        // class can inherit many interfaces
        interface iMovable
        {
            void Move();
            void ShowMoveSpeed();
        }

        interface iBurnable{
            void Burn();
        }

         // class can inherit many interfaces
        class Car : Vihicle, iMovable, iBurnable
        {
            public void Move(){

            }

            public void ShowMoveSpeed(){

            }

            public void Burn(){

            }
        }

        class Vihicle{

        }

        class Human : iMovable{
            public void Move(){

            }

            public void ShowMoveSpeed(){

            }
        }
    }

