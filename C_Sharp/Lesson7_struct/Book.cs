using System;

namespace Lesson7_struct
{
    struct Book
    {
        // variable declaration
        private string title, author, intro;
        private int pages;

        // constructor
        public Book (string title, string author, int pages){
            this.title = title;
            this.author = author;
            this.pages = pages;
        }

        // method
        public void printValues(){
            Console.WriteLine(this.author + " wrote the book: << " + this.title + " >> with " + this.pages + " pages long.");
        }

        
    }
}