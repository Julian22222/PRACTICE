using System;

namespace Lesson4_Classes
{
    class Simple
    {
        private string _name;
        private string _model;
        private int _weight;
        
        public Simple(string name, string model, int weight){
            _name = name;
            Model = model;
            Weight = weight;
        }

        // public string Name {get; set;}
        

          public string Name {
            get{
                return _name;
            }
          set{
            _name = value;
          }
          }

        public string Model {get;set;}
        public int Weight {get;set;}

        public void Walk(){
            System.Console.WriteLine($"{_name} Robot walking");
        }



    }
}