using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonTOP33_OOP_HAS_A
{
   internal class Program
    {
        static void Main(string[] args)
        {
        
        // HAS-A link is used when objects are linked and used together, objects can interact with each other

        Performer worker1 = new Performer("Jack");
        Performer worker2 = new Performer("Mike");

        Task[] tasks = {new Task(worker1,"Order new banners"), new Task(worker2,"Study new programm") };

        Board schedule = new Board(tasks);
        schedule.ShowAllTasks();

        }
    }

    class Performer{
    // variable declaration - Name
    public string Name;

    // constructor
    public Performer(string name){
        Name = name;
    }
    }

    class Board{

        //using Task object in this class
        // creating array of our tasks 
        //variable declaration
        public Task [] Tasks;

        //constructor
        public Board(Task[] tasks){
            Tasks = tasks;
        }

        public void ShowAllTasks(){
            for (int i = 0; i < Tasks.Length; i++){
                // will show all tasks info
                Tasks[i].ShowInfo();
            }
        }

    }

    class Task{
        // using Performer object in this class , with the name -Worker
        //variable declaration
        public Performer Worker;
        public string Description;


        //constructor
        public Task(Performer worker,string description){
            Worker = worker;
            Description = description;
        }

        public void ShowInfo(){
            Console.WriteLine($"Responsible: {Worker.Name}\nTask description: {Description}.\n");
        }

    }

}