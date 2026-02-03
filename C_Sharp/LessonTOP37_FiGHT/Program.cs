using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP37_FiGHT{
   class Program
    {
        static void Main()
        {

            // creating an array from class Fighter, array name - fighters
        Fighter[] fighters = {
            //adding each ellement to our fighters array
            new Fighter("John",500,50,0),
            new Fighter("Mark",250,25,20),
            new Fighter("Alex",150,100,10),
            new Fighter("Jack",300,75,5)
        };

        //variable declaration, to use it for index
        int FighterNumber;
       

        // itteration and showing their statistics, for each fighter
        for(int i=0; i<fighters.Length; i++){
           Console.Write((i+1) + " ");
            fighters[i].ShowStats();
        }

        // will make a dash line of 25 characters
        Console.WriteLine("\n**" + new string('-',25) + "**");
        

        Console.Write("\nChoose first fighter , insert first fighter number: ");
        // converting user input to the - int data type, and assign it to the FighterNumber variable
        FighterNumber = Convert.ToInt32(Console.ReadLine())-1;
        
        // find choosen fighter from our array with index ->fighters[FighterNumber]
        //assign it to the firstFighter variable
        Fighter firstFighter = fighters[FighterNumber];

        Console.Write("\nChoose second fighter , insert second fighter number: ");
        FighterNumber = Convert.ToInt32(Console.ReadLine())-1;
        Fighter secondFighter = fighters[FighterNumber];
         Console.WriteLine("\n**" + new string('-',25) + "**");

        // while itteration -> do code while condition in the barckets is true -> firstFighter.Health > 0 && secondFighter.Health > 0
         while(firstFighter.Health > 0 && secondFighter.Health > 0){
            
            firstFighter.TakeDamage(secondFighter.Damage);
            secondFighter.TakeDamage(firstFighter.Damage);

            firstFighter.ShowCurrentHealth();
            secondFighter.ShowCurrentHealth();
         }


  Console.WriteLine("\n**" + new string('-',25) + "**");

            if(firstFighter.Health > secondFighter.Health){
               Console.WriteLine($"Fighter {firstFighter.Name} won"); 
            }else{
                Console.WriteLine($"Fighter {secondFighter.Name} won"); 
            }
        }

        class Fighter{
            private string _name;
            private int _health;
            private int _damage;
            private int _armor;


            public int Health{
                get{
                    return _health;
                }
            }

            public int Damage{
                get{
                    return _damage;
                }
            }

            public string Name{
                get{
                    return _name;
                }
            }


            public Fighter(string name,int health, int damage, int armor){
                _name = name;
                _health = health;
                _damage = damage;
                _armor = armor;
            }

            public void ShowStats(){
                Console.WriteLine($"Figter - {_name}, Heath - {_health}, Damage - {_damage}, Armor - {_armor}");
            }

            public void ShowCurrentHealth(){
                Console.WriteLine($"{_name}, Health: {_health}");
            }
            public void TakeDamage(int damage){
                _health -= damage - _armor;

            }
        }

        }
        
    }
