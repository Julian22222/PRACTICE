using System;

namespace Lesson4_Classes{

class Robot{

// variable declaration
private string name;
private int weight;
public byte[] cordinates;

public void printValues(){
    foreach(int el in cordinates){
        Console.WriteLine(el);
    }
}

// accessors -getters && setters
public int Weight{
    get{
        Console.Write("Result: ");
        return weight;
    }
    set{
        if(value < 1){
        weight = 1;
        }else{
            weight = value;
        }
    }
}

// short note accesors 
public string Name { get; set; }



}

}