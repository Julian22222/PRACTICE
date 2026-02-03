using System;

namespace LessonTOP42_ComputerClub
{
    class Computer
    {
        
        // variable declaration
        private Client _client;
        private int _minutesRemaining;
        public int PricePerMinute {get; private set;}
        
        public bool IsTaken{
            get{
                return _minutesRemaining > 0; //return true or false
            }
        }

        // constructor
        public Computer(int pricePerMinute){
        PricePerMinute = pricePerMinute;
        }

        // method
        public void BecomeTaken(Client client){
        _client = client;
        _minutesRemaining = _client.DesiredMinutes;
        }

        // method
         public void BecomeEmpty(){
            _client = null;
        }

        public void SpendOneMinute(){
            _minutesRemaining--;
        }

        public void ShowState(){
            if(IsTaken){
                Console.WriteLine($"Computer is engaged , remained {_minutesRemaining} minutes");
            }else{
                Console.WriteLine($"Computer is free, price per minute: {PricePerMinute}");
            }
        }

    }
}