# TypeScript Examples

[ --> TypeScript Playground <-- ](https://www.typescriptlang.org/play/?#code/DYUwLgBAtgngdgVyhAvBArAKFo5aBEAZgE4mH6aYDGA9nAM42gB0wNA5gBQ5ICUltBpFgA1AIbAAXAG84YqCEkR6YYgEs47ADQQx7RRFwAjEMR30QADwD8Sles0BfVBGmY5CiEvwApGgAs4fC1MPRAvCAAmAHZMR0pRCWYLSxd8KAkQCmo6RhY2LkTgXiA)

```JS
const y = 12;
// console.log(isNaN(y));

let obj = { x: 0 };

{/* enum name must start with capital letter */}
enum Test {
Up = 1,
Down = 3,
Left = "three",
Right = "four"
}
{/* // console.log(Test)
//will give-->
//  "1": "Up",
//   "3": "Down",
//   "Up": 1,
//   "Down": 3,
//   "Left": "three",
//   "Right": "four" */}

enum Mo {
  hey,
  pay,
  day
}
{/* // console.log(Mo)
//will give-->
//"0": "hey",
// "1": "pay",
// "2": "day",
// "hey": 0,
// "pay": 1,
// "day": 2 */}



class Item {
  static table = "12345";  //can access to this value -> Item.table

  constructor(private name1: string, public age1:number) { }

  getValue(){
    return this.name1
  }

  setAge(age:number){
    this.age1 = age
  }
};

let student = new Item("Bob", 23)
 // console.log(student.getValue()) --> "Bob"


let peope = [
new Item("Jack", 21),
new Item("Tom", 25),
new Item("Ben", 23)
]

// for (let i=0; i<peope.length; i++){
// console.log(peope[i]);
// }


class Mynew extends Item {
  constructor(name1: string, age1: number, public family: boolean) {
    super(name1, age1);  // Pass the parameters to the parent constructor
  }

 getValue(){
  return `Hello ${this.age1}, this static file = ${Item.table}`
  }
}


const myName = new Mynew("V",45, true);
// console.log(myName.getValue()) -->  "Hello 45, this static file = 12345"


{/* ////////////////////////////////////////////////////////////////////// */}

let i: number = 9;

let m: {name: string, age?: number} = {name: "Eliot" };

let h: number[] = [1,2,3,4];
h.push(5);

type Ob = {name: string, size: number};
let n: Ob[] = [{name:"hdada", size:9}, {name:"kkda", size: 7}]
{/* // console.log(n) */}


/////////////////////////////////////////////////////////////////////////////////////////

// interface Iobj {name: string, lastname: string};
type MyObj = {
  name: string,
  lastname: string
};


const myfunc =(item: MyObj) =>{
  return `Hello my name is ${item.name} and lastname is ${item.lastname}`
}

const test = myfunc({name: "Tony", lastname:"rtrr"});
// console.log(test)  --> "Hello my name is Tony and lastname is rtrr


const hujF = (item: string | number)=>{  //function can receive string or number data type as arguments
  if(item != null && typeof(item) === "string"){
    return `Hello ${item}`
  }

return item+1
}


const bb = hujF("pizdjuk")
// console.log(bb)  --> "Hello pizdjuk"

//////////////////////////////////////////////////////////////////////////////////////////////////

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
    return `Hello`; //////Must be declered because it was abstract function in Template abstract class
  }
}

const hey = new Template2("Bill", 98)
// console.log(hey.finc1())  --> "Hello World! and Bill"


const myfeee:string = ()=> string;


interface Iomg {
  name: string,
  age: number
};

const yuh =(item: Iomg)=>{
  return item.name + item.age
}

const k = yuh({name:"billy", age: 23})
//console.log(k); --> "billy23"




class Beer implements Iomg {
  constructor( public name:string, public age: number, public address: string){}
}

const jug = new Beer("figo",33, "dede");
//console.log(jug)  --> {"name": "figo","age": 33,"address": "dede"}



interface Ikilo extends Iomg{
  huj: string;
}


class Huj implements Ikilo {
  constructor(public name: string,public age: number,public huj: string){}
}


let mojHuj = new Huj("pi", 23, "zopa");
{/* console.log(mojHuj) */}


{/* /////////////////////////////////       DTO in TypeScript ////////////////////////////*/}
type Item2 ={
  Name: string,
  Age: number,
  Address: string
}

type Item3 ={
  Name: string,
  Age: number
}

const GetDto =(item: Item2): Item3=>{
return {
  Name : item.Name,
  Age : item.Age
}
}

const per: Item2 = {Name:"Andy", Age: 26, Address: "kikolo"}

const test22 = GetDto(per);
console.log(test22)
{/* ///////////////////////////////////////////////////////////////////////////// */}


/////////////////////////////////////// Generic Types

const identity = <T>(arg: T): T => {
  return arg;
};

//or
// const identity = <T>(arg: T): T => arg;

function func<T>(item:T):T{
return item
}

{/* // function keyword starts the declaration.
// identity is the function name.
// <T> declares a generic type parameter T.
// (arg: T) means the function accepts an argument of type T.
// : T after the parentheses means the function returns a value of type T. */}

const mi = func(34)
const mu = func("hey")
{/* console.log(mi)
// console.log(mu) */}

{/* /////////////////////////////////////////////////////////// */}
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
