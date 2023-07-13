using System;

// allow to read files using -> File.ReadAllLines( location of the file)
using System.IO; 

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP24_HEALTHBAR
{
   internal class Program
    {
        static void Main(string[] args)
        {
            // removes cursor from console
            Console.CursorVisible =false;

            // char [,] map = null;

            // string [] file = File.ReadAllLines("map.txt");

            // Console.WriteLine(file[0]);  //return all first line

            string path = "map.txt";


            // Convert string map to char map to be able to make changes in the map. String type is immutable
            char [,] map = ReadMap(path);

            // starting position of the @
            int packmanX = 1;
            int packmanY = 1;

            int score = 0;

            // Will always refresh the data in the brackets while is true
            while(true){

            // clear all data from the console before each loop
            Console.Clear(); 


            // change map color in console
            Console.ForegroundColor = ConsoleColor.Blue;
            // show map in the console
            DrawMap(map);

            // change @ color in console
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(packmanX, packmanY); //line -column positon
            Console.Write("@");

            // score block
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(50,0);
            Console.Write($"Score: {score}");


            ConsoleKeyInfo pressedKey = Console.ReadKey();

            HandleInput(pressedKey, ref packmanX, ref packmanY, map, ref score);
            }

        }

        private static char[,] ReadMap(string path){
            string [] file = File.ReadAllLines(path);

            // assign max length in x axis(width) and y axis(height) for map/assign how many lines and how many columns are in two-demention  map array
            char[,] map = new char[file[0].Length, file.Length];

            for (int x =0; x < map.GetLength(0); x++){
                for (int y = 0; y < map.GetLength(1); y++){
                    map[x,y] = file[y][x];
                }
            }
            return map;
        }

        private static void DrawMap (char[,] map){

            
                for (int y = 0; y < map.GetLength(1); y++){ //line
              for (int x =0; x < map.GetLength(0); x++){  //column
                  Console.Write(map[x,y]);
                }
                Console.Write("\n");
            }

        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int packmanX, ref int packmanY, char[,] map, ref int score){
            // make @ to move when we pres up, down,left,right arrow
            // if(pressedKey.Key== ConsoleKey.UpArrow){
            //     packmanY -= 1;
            // }else if(pressedKey.Key== ConsoleKey.DownArrow){
            //      packmanY += 1;
            // }else if(pressedKey.Key== ConsoleKey.LeftArrow){
            //      packmanX -= 1;
            // }else if(pressedKey.Key== ConsoleKey.RightArrow){
            //      packmanX += 1;
            // }

           int[] direction = GetDirection(pressedKey);
//  direction[1] -> moving up and down, (y axis)
//   direction[0] _> moving left and right (x axis)

// packmanX -is current position on x axis 
// direction -posibility to move left,right, up,down
           int nextPackmanPositionX = packmanX + direction[0];
           int nextPackmanPositionY = packmanY + direction[1];

           char nextCell = map[nextPackmanPositionX,nextPackmanPositionY];

           if( nextCell == ' ' || nextCell == '.' ){
            packmanX = nextPackmanPositionX;
            packmanY = nextPackmanPositionY;

            if(nextCell =='.'){
                score++;
                // when you collect the -> . it will leave empty on the map
                map[nextPackmanPositionX, nextPackmanPositionY] = ' ';
            }
           }
           

        }

        private static int[] GetDirection (ConsoleKeyInfo pressedKey){

             // check that @ can't cross borders -> #
            //can make different checks 
            // if(packmanX < 0 ){ packmanX =0; }

            int[] direction = {0,0};


//  direction[1] -> moving up and down, (y axis)
//   direction[0] _> moving left and right (x axis)
              if(pressedKey.Key== ConsoleKey.UpArrow){
                direction[1] =-1;
            }else if(pressedKey.Key== ConsoleKey.DownArrow){
                  direction[1] =+1;
            }else if(pressedKey.Key== ConsoleKey.LeftArrow){
                 direction[0] =-1;
            }else if(pressedKey.Key== ConsoleKey.RightArrow){
                  direction[0] =+1; 
            }
            return direction;
        }

        // private static int GetMAxLengthOfLine(string[] lines){
        //     int maxLength = lines[0].Length;

        //     foreeach(var line in lines){
        //        if(lines.Length > maxLength){


        //     maxLength = lines.Length;

        //        }
        //     return maxLength;   

        //     }
          
        // }

    }
}
