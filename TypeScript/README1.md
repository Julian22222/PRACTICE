# TypeScript Examples

```JS
let obj = { x: 0 };


enum Test {
Up = 1,
Down = 3,
Left = "three",
Right = "four"
}



class Item {

  static table = "12345";

  constructor(private name1: string, public age1:number) { }

  getValue(){
    return this.name1
     }

  setAge(age:number){
    this.age1 = age
  }
};

let student = new Item("Bob", 23)
// console.log(student.getValue())


let peope = [
new Item("Jack", 21),
new Item("Tom", 25),
new Item("Ben", 23)
]

// for (let i=0; i<peope.length; i++){
// console.log(peope[i]);
// }


class myP extends Item {
  constructor(name1: string, age1: number, public family: boolean) {
    super(name1, age1);  // Pass the parameters to the parent constructor
  }

 getValue(){
  return `Hello ${this.age1}, this stati file = ${Item.table}`
  }
}


const myName = new myP("V",45, true);
// console.log(myName.getValue())




let i: number = 9;

let m: {name: string, age?: number} = {name: "Eliot" };

let h: number[] = [1,2,3,4];
h.push(5);

type ob = {name: string, size: number}
let n: ob[] = [{name:"hdada", size:9}, {name:"kkda", size: 7}]
// console.log(n)




// interface myObj {name: string, lastname: string};
type myObj = {name: string, lastname: string};



const myfunc =(item: myObj) =>{
  return `Hello my name is ${item.name} and lastname is ${item.lastname}`
}

// const test = myfunc({name: "tony", lastname:"rtrr"});
// console.log(test)


const hujF = (item: string | number)=>{
  if(item != null && typeof(item) === "string"){
    return `Hello ${item}`
  }

return item+1
}


const bb = hujF("pizdjuk")
// console.log(bb)



abstract class Template{
  species: string = "human";
  // name: string;
  // age: number
  constructor(public name:string, public age: number){
  // this.name = name;
  // this.age = age;
  }

  finc1(){
    return `Hello World! and ${this.name}`;
  }

  abstract func2(): string;
}



class Template2 extends Template{
  constructor(name:string, age: number){
    super(name,age)
  }

  func2(){
    return `Hello`;
  }
}

const hey = new Template2("Bill", 98)
// console.log(hey.finc1())


const myfeee:string = ()=> string;


const yuh =(item:omg)=>{
  return item.name + item.age
}


const k = yuh({name:"billy", age: 23})
console.log(k);


interface omg {name: string, age: number};



class Beer implements omg {
  constructor( public name:string, public age: number, public address: string){}

}



const jug = new Beer("figo",33, "dede");
console.log(jug)



interface kilo extends omg{
  huj: string;

}



class Huj implements kilo {
  constructor(public name: string,public age: number,public huj: string){}

}



let mojHuj = new Huj("pi", 23, "zopa");
console.log(mojHuj)
```

- Abstract classes can be extended by other classes.

- Abstract classes can implement interfaces

- class implement few interfaces

```JS
interface A {
  methodA(): void;
}

interface B {
  methodB(): void;
}

// Class implements both interfaces A and B
class MyyClass implements A, B {
  methodA() {
    console.log('methodA');
  }

  methodB() {
    console.log('methodB');
  }
}

/////////////////////////////

interface A {
  methodA(): void;
}

interface B {
  methodB(): void;
}

// Interface C extends both A and B
interface C extends A, B {}

class MyClass implements C {
  methodA() {
    console.log('methodA');
  }

  methodB() {
    console.log('methodB');
  }
}
```

- Interface can extend another interface, interfaces can extend one or many other interfaces to combine their members.

```JS
interface CanRun {
  run(): void;
}

interface CanJump {
  jump(): void;
}


interface Athlete extends CanRun, CanJump {
  compete(): void;
}
```
