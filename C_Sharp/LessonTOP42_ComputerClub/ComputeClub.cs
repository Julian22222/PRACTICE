using System;
// using System.IO;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace LessonTOP42_ComputerClub
{
    class ComputeClub
    {
        private int _money = 0;

        // list with price per minute -PricePerMinute, class Computer
        private List<Computer> _computers = new List<Computer>();
        private Queue <Client> _clients = new Queue<Client>();

        // constructor
        public ComputeClub(int computersCount){

            // random value for 1 minute use for different computers
            Random random = new Random();

            for(int i=0; i<computersCount; i++){
                _computers.Add(new Computer(random.Next(5,15)));
            }
            
            // invoke method 
            // 25 clients has been added to Queue 
            CreateNewClients(25, random);
        }

            // method
            public void CreateNewClients(int count, Random random){
                for (int i =0; i<count; i++){
                    // random money client has (100-250)
                    // random minutes that client what to use computer
                    _clients.Enqueue(new Client(random.Next(100,251),random));
                }

            }

            public void Work(){
                      while(_clients.Count > 0){
                    Client newClient = _clients.Dequeue();

                    Console.WriteLine($"The balance of computer club is {_money} pounds . Waiting new client");
                    Console.WriteLine($"You have new client and he wants to buy {newClient.DesiredMinutes} minutes.");
                    ShowAllComputerState();

                    Console.Write("\nYou are offering computer to a customer with number: ");
                   
                //    assign user input to variable userInput
                   string userInput = Console.ReadLine();

                    // int.TryParse will try to convert string -userInput, and gives us on way out
                    // variable ->computerNumber (as converted variable)
                    // we checking if input is correct (can be converted to int) then we do something, else we show msg - Wrong input, try again
                   if(int.TryParse(userInput, out int computerNumber)){
                    computerNumber -=1;

                    // we checking if number you inserted is in the list
                    if(computerNumber >= 0 && computerNumber < _computers.Count){


                        // checking if chosen computer has already engaged than we can't take it
                        if(_computers[computerNumber].IsTaken){
                            Console.WriteLine("You trying to take computer, that has already engaged.");
                        }else{
                            
                            // checking has user have enough money to pay for computer
                            if(newClient.CheckSolvency(_computers[computerNumber])){
                            Console.WriteLine($"User paid and sat down on computer: {computerNumber + 1}");
                            _money += newClient.Pay();
                            _computers[computerNumber].BecomeTaken(newClient);
                            }else{
                            Console.WriteLine("User hasn't enough money");
                            }


                        }

                    }else{
                        Console.WriteLine("You inserted wrong number. Try again!");
                    }



                   }else{
                    // we adding back 1 client, because for each iteration it remove 1 client.
                    CreateNewClients(1,new Random());
                    Console.WriteLine("Wrong input, try again!");
                   }


                    Console.WriteLine("to choose another customer ,press any key");

                    // to dont refresh all the time the list of computers
                    Console.ReadKey();
                    Console.Clear();
                    SpendOneMinute();
                }
             
              
            }

            private void ShowAllComputerState(){

                Console.WriteLine("\nList of all computers: ");
                for(int i=0; i < _computers.Count; i++){
                    Console.Write(i + 1 + " - ");

                    // ?????? ,invoke a method for all elements-> ShowState in Computer Class
                    _computers[i].ShowState();
                }
            }

            private void SpendOneMinute(){
                foreach (var computer in _computers)
                {
                    computer.SpendOneMinute();
                }
            }

    }
}