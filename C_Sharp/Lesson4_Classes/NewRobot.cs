using System;

namespace Lesson4_Classes

// inheritance with contstructor
{
    class NewRobot : Simple
    {
        public NewRobot(string name, string model, int weight) : base(name, model, weight){}
        
    }
}