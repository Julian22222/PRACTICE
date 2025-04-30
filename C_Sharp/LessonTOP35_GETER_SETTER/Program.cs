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

            Renderer renderer = new Renderer();
            Player player = new Player(1, 2);

            
            renderer.Draw(player.X(),player.Y());
        
            
        }



        class Renderer{
        public void Draw(int x, int y, char character ='@'){
        Console.CursorVisible = false;
        Console.SetCursorPosition(x,y);
        Console.Write(character);
        Console.ReadKey(true); //remove message -> insert any key to continue...


        }

        }



        class Player{

            // you don't need to declere variables if you use short version of getters and setters
            // private int _x;
            // private int _y;



                public int X {get; private set;}
                public int Y {get; private set;}

                public Player (int x, int y){
                    X= X;
                    Y = Y;

                }


            // public int X{
            //     get{
            //         return _x;
            //     }
            //     // does't allow to set the value
            //     private set{
            //         _x = value;
            //     }
            // }

            //   public int Y{
            //     get{
            //         return _y;
            //     }
            //     // does't allow to set the value
            //     private set{
            //         _y = value;
            //     }
            // }

            // public Player(int x, int y){
            //     _x=x;
            //     _y=y;
            // }

            // public int GetPositionX(){
            //     return _x;
            // }

            //  public int GetPositionY(){
            //     return _y;
            // }
        }
    }
}
