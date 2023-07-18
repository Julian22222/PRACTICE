using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LessonTOP41_Static


{
    class Program
    {
        static void Main()
        {
            // we can interact with static variables through class name -User
            // we assign =10 to Identifications
            User.Identifications = 10;

            // creating two objects - user1 and user2
            User user1 = new User();
            User user2 = new User();

            // invoke method ShowInfo in class user1 and user2
            user1.ShowInfo();
            user2.ShowInfo();
    

        }
    }

    class User {
        public static int Identifications;
        public int Identification;

        public User(){
            Identification = ++Identifications;
        }

        // method 
        public void ShowInfo(){
            Console.WriteLine(Identification);
        }

    }

   
}
