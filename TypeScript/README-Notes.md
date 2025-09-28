# TypeScript can't be run directly from terminal

VS Code automatically use JS. Therefore we need:

1. tsx library --> npm install tsx (this will install tsx library locally in our project). but the terminal won’t find tsx directly. Therefore to run the typescript file if we installed tsx localy - we use:

```JS
npx tsx mytypescript.ts
```

2. If you want to run -> tsx filename.ts , without npx. Install tsx globally :

```JS
npm install -g tsx

tsx --version

tsx filename.ts
```

# Common Hotkeys to Create React Component Template

```JS
npx create-react-app my-react-ts --template typescript    // my-react-ts  <- name of the React project folder

cd my-react-ts
npm start
```

```JS
rfc + Tab  - React Functional Component template

rafce + Tab - React Arrow Function Component with export

rcc + Tab - React Class Component

rfce + Tab - React Functional Component with export

```

# Basic Data Types

JS is a language with dynamic types, can change variable types

To define the data types in JS we use operator --> typeof

in JS there is 7 basic Data Types:

- number
- string
- boolean
- null
- underfined
- object
- Symbol //ES6

TypeScript is a high-level programming language that adds static typing with optional type annotations to JavaScript. It is designed for developing large applications and transpiles to JavaScript.

[ --> TypeScript Playground <-- ](DYUwLgBAtgngdgVyhAvBArAKFo5aBEAZgE4mH6aYDGA9nAM42gB0wNA5gBQ5ICUltBpFgA1AIbAAXAG84YqCEkR6YYgEs47ADQQx7RRFwAjEMR30QADwD8Sles0BfVBGmY5CiEvwApGgAs4fC1MPRAvCAAmAHZMR0pRCWYLSxd8KAkQCmo6RhY2LkTgXiA)

# Run Typescript file

- Option 1
  install npm package --> ts-node

```JS
ts-node script.ts  //run the code
```

this will run node and perform typescript compilation and start script.

- Option 2

  1. Compile first (manualy)

  ```JS
  tsc index.ts
  ```

  2. node index.js

  ```JS
  node index.js
  ```

# React + TypeScript

```JS
npx create-react-app my-react-ts --template typescript    // my-react-ts  <- name of the React project folder

cd my-react-ts

npm start
```

# Data types in TypeScript - boolean, number, string

```JS
let isCompleted: boolean = false;

let myNum: number = 7;

const name: string = "John";

const greetUser = (): void =>{
    alert("Hello, nice to see you!");
}

let obj: { first: string } = { first: "hello" };

//Array
let list: number[] = [1,2,3];
or
let list: Array<number> = [1,2,3];  //Generic type
```

# Tuple Type

```JS
//can contain multiple data types

let x: [string, number];
x= ["hello", 10];
//or
let y: [string, number] = ["goodbuy", 43];
```

# Any Type

```JS
//to use ANY TYPE is not save, can cause errors because it is not tracking the data type
//better to avaid to use Any type
//any - can be any data type, but this approach doesn't allow to reveal TypeScript potential

let y: [any, any] = ["hello", 45];
let z: Array<any> = [10, "hello"];

let notSure: any = false;  //easily can change to any data type
notSure = true;
notSure = 42;
notSure = "hello";
```

# Enum Type (mix of object and array)

- enum types are used often
  -enum is always public by default. Enums are always accessible. There is no public, private, or protected modifier for enum. Enum inside a class Not allowed at all.
- enum is typically used when you have a fixed set of related values, like directions, statuses, roles, etc
- Fixed set of named values
- No inheritance from enum
- don't use enums if you need dynamic values or runtime evaluation

🔸 Key Features:

- Automatically assigns numeric or string values.
- Used mainly as labels for constants.
- Enforces valid values at compile time.
- Good for things like:
  -Statuses (enum Status { Pending, Approved, Rejected })
  - Roles (enum Role { Admin, User, Guest })
  - Flags (enum Permission { Read = 1, Write = 2, Execute = 4 })

Use an enum when:

- You have a fixed list of possible values.
- You want type safety and readable code.
- You need a clean alternative to magic strings or numbers.

```JS
enum OrderStatus {
  Pending,
  Processing,
  Shipped,
  Delivered
}

// Enforces correct value
function updateStatus(status: OrderStatus) { ... }
```

```JS
//here each element has their own index
enum Directions{
    Up,
    Down,
    Left,
    Right
}

Directions.Up;  //0
Directions.Down;  //1
Directions[2];   //Reverse enum - //Left.  Reverse enum works only with numbers, and indexes


//can make custom index for enum elements
enum Directions{
    Up =2,
    Down = 4,
    Left = 6,
    Right
}

Directions.Up;  //2
Directions.Down;  //4
Directions.Right;  //7, next index number
Directions[2];   //Reverse enum - //Up.  Reverse enum works only with numbers, and indexes


//Custom name for keys, use const before enum for resources mimimization and optimization, don't compile enum if not called
enum links {
    youtube = "http://youtube.com/",
    facebook = "https://facebook.com/"
}

links.youtube  //http://youtube.com/

//////////////////
//To use an enum in other files, you must export it
export enum Color {
  Red,
  Green,
  Blue
}

//in other file:
//import { Color } from './Color';
//const c: Color = Color.Green;
```

# Never Type

```JS
//Is used when function return Error, and in infinite loop

//Function return Error
const msg = "hello";
const error = (msg: string): never => {
    throw new Error(msg);
};


//Function infinite loop example
const infiniteLoop = (): never =>{
    while(true){
        //code
    }
}
```

# Object Type

```JS
const create: {name: string, age: number} = {name: "Jack", age: 34};


//Multiple types for one value
const myFunc = (item: object | null): void =>{};  //receives an argument - item, which can be either object type or null
myFunc({obj: 1});

let id: number | string;  //can receive number type or string type;
id = 10;  //valid
id ="43";  //valid


//Example 2
let user: any = {
    name: "Ben",
    age: 30,
};

user = "test"; //can easyly change object type when is any type, not very good practice


//Example 3 - correct way to define object type
let user: {name: string, age: number} = {
    name: "Ben",
    age: 30,
};

user = "test";  // Error, user = object type
//When we gave strong typesation and its properties - Now our object is protected uncontrolled changes, uncontrolled object expansion


//Example 4
//If you want to expand your object
let user: {name: string, age: number, nickName?: string} = {  //nickName?: string - is optional property
    name: "Ben",
    age: 30,
};

user.nickName = "strong";
```

# Type Aliases

A type alias is a custom name for a type (primitive, union, intersection, object, etc.).

Types allow to:

1. Improve Readability and Intent

```JS
// This makes the function signature clearer than just using string

type Email = string;
type Age = number;

function sendEmail(email: Email) { ... }
```

2. To Represent Union or Intersection Types

```JS
// It prevents invalid values and documents the allowed states.
type Status = "success" | "error" | "loading";

function setStatus(status: Status) { ... }
```

3. To Reuse Complex Object Structures

```JS
//Avoids repeating the same object structure everywhere.
type User = {
  id: number;
  name: string;
  email: string;
};

function getUser(): User { ... }
function saveUser(user: User) { ... }
```

4. To Make Code DRY and Flexible

```JS
//If you change the structure in one place, it's updated everywhere the alias is used
//This creates reusable patterns using generics.

type APIResponse<T> = {
  data: T;
  success: boolean;
  error?: string;
};

const response: APIResponse<string[]> = {
  data: ["one", "two"],
  success: true
};
```

5. To Alias Existing Types for Meaning

```JS
// Adds semantic meaning without changing behavior.
type UUID = string;
type Timestamp = number;
```

When Not to Use Type Aliases:

- Don’t use type aliases for class-like behavior — use interface or class instead.
- For purely object shapes that will be extended or implemented, prefer interface (see below)

Use type when:

- You need to alias anything, not just objects.
- You want to define union, intersection, or tuple types.

Some common examples and notes of Type Aliases:

```JS
//with "type" - we can define data types

//We’ve been using object types and union types by writing them directly in type annotations. This is convenient, but it’s common to want to use the same type more than once and refer to it by a single name.

//A type alias is exactly that - a name for any type. The syntax for a type alias is: creating a class to use it many times in your code
type Point = {
  x: number;
  y: number;
  h?: string;  //optional property, can be used with other functions
  getpass?: () => string;   //optional property, can be used with other functions
};

function printCoord(pt: Point) { //use template with data type that we indicated previously
  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);
}
printCoord({ x: 100, y: 100 });


function printCoord(pt: Point) { //use template with data type that we indicated previously
  console.log("The coordinate's x value is " + pt.h);
  console.log("The coordinate's y value is " + pt.y);
}
printCoord({ x: 100, y: 100, h: "hello" });


//Example 2
type Name = string; //ustom type creation
let id: Name; //Apply custom type
id = "Hello World" //valid
id = 10;  //Error




//You can actually use a type alias to give a name to any type at all, not just an object type. For example, a type alias can name a union type:

type ID = number | string;



type UserInputSanitizedString = string;



function sanitizeInput(str: string): UserInputSanitizedString {

  return sanitize(str);

}


// Create a sanitized input
let userInput = sanitizeInput(getInput());


// Can still be re-assigned with a string though
userInput = "new input";

```

# Functions

- Need to describe proporty types for variables that are passing as an argument to the function and also can give a type of function returning data type
- If variable can have few types it is called - Multiple argument types
- Default argument s also can be added
- can add optional argument - not mandatory argument

```JS
//example 1
const createPassword =(name: string, age: number | string)=>{ //age property can be either number or string type
    return `my name is: ${name} and and I am ${age} years old`
};


//example 2
const createPassword =(name: string = "Max", age: number | string = 20)=>{
    return `my name is: ${name} and and I am ${age} years old`
};

createPassword(); //my name is Max and and I am 20 years old


//example 3
const createPassword =(name: string, age?: number | string)=>{ //age? - is optional, can pass argument or leave it.
    return `${name} ${age} `
};

createPassword('Max');  //valid, we can pass only a name to our function
createPassword('Max', 12);  //valid


//REST
const createSkills = (name, ...skils)=>{
    `${name}, my skills are ${skills.join()}`
};

//REST type
const createSkills = (name: string, ...skils: Array<string>)=>{ //skilss it is an Array of skills that we are passing after Jack
    `${name}, my skills are ${skills.join()}`
};

//Call function with REST argument
createSkills("Jack", "JS", "ES6", "React"); //"Jack, my skills are JS, ES6, React"



//If you want to indicate of data type that function returning
const createPassword = (name: string, age: number | string): string =>{  //...): string indicate of returning type, return type is a string
    `${name}${age}`
}

//If function not return any data, we need to use -> void
const greetUser =():void =>{
    alert("Hello, nice to see you");
};



//example 4
//when we create variable and then we want to assign a function to this variable

//Function variable type
let myFunc: (firstArg: string) => void;

function oldFunc(name: string): void {
    alert(`Hello ${name}, nice to see you`);
}

myFunc = oldFunc;

```

# Classes

```JS
//example 1
class User {
    name: string;  //creating a variable in the class
    age: number;
    nickName: string;

    constructor(name: string, age: number, nickName: string){ //received variables and assigning them to class variables when initialization/ createing an object from the Class
        this.name = name;
        this.age = age;
        this.nickname = nickName;
    }
}

const jack = new User("Jack", 22, "beast");  // {name: "Jack", age: 22, nickName: "beast"}


//There are 4 access modificators
//- public (goes by default, if not defined any modificator) - easy access to this method or property
//- private (can access only through current class, inside the class)
// - protected ( can access only in inherited classes)
//- readonly (can read only this property or method)


//Example 2
//can give default value to the classes
class User {
    name: string;
    age: number = 20;  //age default property
    nickName: string = "WEEEGE";   //nickName default property

    constructor(name: string){ //don;t need to assign age and nickName, they go by default
        this.name = name;
    }

    getPass(): {
        return `${this.nickName}${this.age}`
    }
}
const jack = new User("Jack");  // {name: "Jack", age: 20, nickName: "WEEEGE"}
jack.getPass();  //WEEEGE20


//Example 3 - convinient Typescript option, shorten option
class User {
    constructor(public name: string, public age: number, public nickName: string){}  //Each property Must have access modificators, to be able to use it

    getPass(): {
        return `${this.nickName}${this.age}`
    }
}

//Example 4 - using accessors -> getters and setters
class User {

    constructor(public name: string, private age: number, public nickName: string){}  //Each property Must have access modificators, to be able to use it

    getAge(): {
        return `${this.age}`
    }

    setAge(age: number){ //valid method
        this.age = age;
    }

    set myAge(age: number){  //valid property
        this.age = age;
    }
}

const tom = new User("Tom", 22, "huh");
tom.getAge();  //22
tom.age;  //Error
tom.setAge(34);  //age = 34;
tom.myAge = 28; //age = 28;
```

# Static properties defined in the Class

- Static properties - properties that can be accesses without creating an object from current Class
- To call static property - we use Class and static property

```JS
class User {

    static secret = 12345;  //static property

    constructor(public name: string, public age: number, public nickName: string){}

    getPass(): string {
        return `${this.name} have scret ${User.secret}`;
    }

}

//Example of call static property
User.secret; //12345
```

# Inheritance

- use extends

```JS
class User {

    private nickName: string = "Fire"
    static secret = 12345;  //static property

    constructor(public name: string, public age: number){}

    getPass():string {
        return `${this.name} have scret ${User.secret}`;
    }
}


class Bill extends User{
   name: sting = "Bill";  //this is default name for Bill's Class

   constructor(age: number){
    super(name, age);  //this calling parent constructor -> User class constructor
   }

    getPass():string {
        return `${this.age}, ${this.name} have scret ${User.secret}`;
    }
}

const max = new User("Max", 20);  // //{name: Max, age: 20}
const bill = new Bill(34);  //{name: Bill, age: 34}
```

# Abstract Class

- You can't create an object from abstract Class, it is just a template.
- It is an Interface which approximately describes how other classes should look like which will be inherited from this abstract class.
- other classes can iherit all properties and methods from abstract class
- First you must create a common class (using extends) from abstract class, and then you can use it to create an object

```JS
abstract class User {

    constructor(public name: string, public age: number){}

    greet(): void {
        console.log(this.name);
    }

    abstract getPass(): string;  //the logic of this method should be described in extended classes, it is abstract method
}


class Max extends User {
    constructor(public name: string, public age: number, public nickName: string){  //nickName is additional property for this class
        super(name, age);  //properties from parent class
    }

    getPass(): string {
        return `${this.name}`
    }
}

const max = new Max("Max", 22, "Fire");
```

# Namespaces & Modules

- allow to use variables, methods, functions etc. from other files. It goes to global scope
- use export - to make that variable accessable from anywhere
- Modules - new, simple version how to export and import properties between files
- Namespaces (nowadays used rerely)- Old version how to export and import properties between files

```JS
//Modules
//Import/ Export (ES6 Modules)
//Don't use namespace

//File "Utils.ts"
export const SECRET: string = 'Tom';
export const getPass = (name: string, age: number): string => `${name}${age}`;

//File "Customers.ts"
import {getPass, SECRET } from "./Utils";

const myPass = getPass(SECRET, 31);  //"Tom31"
```

```JS
//Namespaces - Example 1 - Define namespace and use that properties in the same file

namespace Utils {    //this encapsulate all properties inside
    export const SECRET: string = '12345';  //export
    const PI: number = 3.14;  //don't have access to this property

    export const getPass = (name: string, age: number): string => `${name}${age}`;

    export const isEmpty = <T>(data: T): boolean => !data;
};

const myPass = Utils.getPass("Max", 22);    //"Max22"
const isSecret = Utils.isEmpty(Utils.SECRET);   //"false"

//constant with the same name outside namespace
const PI = 3;  //valid, no Errors


//Namespaces - Example 2 - use 1 namespace in different files

//File "Utils.ts"
namespace Utils {
    export const SECRET: string = '12345';

    export const getPass = (name: string, age: number): string => `${name}${age}`;
};


//File "Customers.ts"
/// <reference path="Utils.ts" />    //<--Import Utils properties

const myPass = Utils.getPass("Max", 11);  //"Max11"
```

# Interfaces

- An interface declaration allowing to give a data type to your variables, functions etc.: (similar to type)
- Helps to describe object form or give a template how it should look like

```JS

interface Point {
  x: number;
  y: number;
}


function printCoord(pt: Point) {
  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);
}
printCoord({ x: 100, y: 100 });
```

- Type aliases and interfaces are very similar, the key distinction is that a type cannot be re-opened to add new properties vs an interface which is always extendable.

![pic1](https://github.com/Julian22222/PRACTICE/blob/main/TypeScript/IMG/pic1.JPG)

```JS
//here we use interface and type to describe object structure

    //can be used for any data types including basci data types
    type User {
      name: string;
      age: number;
    }

//much more powerful tool with more capabilities, has more additional options where to use.
//Inteface can be used in extends or impliments -> can be inherited and extended by other interfaces
interface User {
  name: string;
  age: number;
}
```

```JS
//interface example 1
//can use optional properties

interface User {
  name: string,
  age: number,
  position?: string,    // optional property
}

const tom: User = {  //create object based on Interface
    name: "Tom",
    age: 32,
}


//Example 2 - use "readonly modifier"
interface User {
  readonly name: string,   //<-- can't be changed
  age: number,
  position?: string,    // optional property
}

const tom: User = {  //create object based on Interface
    name: "Tom",
    age: 32,
}

tom.age = 12;  //valid
tom.name = "Ben";  //Error


//Example 3 - if want to extend an object from this interface, and we don't know how many and what properties will be added
interface User {
  name: string,
  age: number,
  [propName: string]: any,   // can be any type, typescript not controlling its type
}

const tom: User ={
    name: "Tom",
    age: 32,
    nickName: "webKing",   //interface don't controll this property
    justTest: 'test'        //interface don't controll this property
}


//Example 4 - use Interface to implement another class, Interface can be used fo extends and Implements to describe a class
interface User {
  name: string,
  age: number,
  getPass(): string,
}

//class creation based on interface "User"
class Jack implements User {
  name: string = "Jack",
  age: number = 31,
  nickName: string = "webKing";  //<-- not in interface

  getPass(){
    return `${this.name}${this.age}`
  }
}

//Example 5, create class from multiple Interfaces
interface User {
  name: string,
  age: number,
  getPass(): string,
}

//second interface
interface Pass {
    getPass(); string;
}

//class created ffrom 2 different Interfaces
class Kim implements User, Pass {
    name: string = "Kim";
    age: number = 22;

    getPass(){
        return `${this.name}${this.age}`
    }
}

//Example 6, Interface extends
interface User {
  name: string,
  age: number,
}

//Interface extends
interface Admin extends User {
    getPass(): string;
}


class Kim implements Admin {
    name: string = "Kim";
    age: number = 22;

    getPass(){
        return `${this.name}${this.age}`
    }
}

```

# Generic Types (common types)

- Allow to create components to work with different data types

```JS
//Example of useing "any"  -- Error cases
const getter = (data: any): any => data;  //can receive any data type

getter(10);  //10
getter("test"); //test
getter(10).length;  //underfined - no such method when use number
getter("test").length;  //4
```

```JS
//if we receive a string type in the function then we return a string type, etc.
//for this reason we use Generic Types
//T - can be any letter - means data type, usually use -> T for Type
// T - we assign this T to our argument in the function and then reurning the same data type
//dynamically manipulate of data types

//ES6 syntax
const getter = <T>(data: T): T => data;   //getting argument type,and return this type from the function

//ES5 syntax
const getter<T>(data: T): T {
    return data;
}

getter(10).length;  //in this case typeScript will show an Error meesage that saying it doesn  number doesn't have - length method,
```

```JS
//Example 2 - we can control what data type we sent to our function
const getter = <T>(data: T): T => data;

//Define type in function calling
getter<number>(10).length;  //<number> we indicate what data type our function will receive
getter<string>("test").length;  //<string> we indicate what data type our function will receive
```

```JS
//normal class
class User {
    constructor( public name: string, public age: number){}

    public getPass(): string {
        return `${this.name}${this.age}`
    }
}




//Generic class
//If we want that our class has more flexability and ability to receive different data types
class User<T> {
    constructor(public name: T, public age: T){}

    public getPass(): string {      //all the data type will automatically converts to sting type
        return `${this.name}${this.age}`
    }
}

const dick = new User("Dick", "33");  //passing 2 strings
const max = new User(123, 345);  //passing 2 numbers

//now our Class can receive different data types
dick.getPass();   //"Dick33"
max.getPass();  //"123345"
```

```JS
//Generic class with different argument type, if we need to pass string and number
//function can receive arguments with different data types

class User<T, K> {  //define one more Generic type
    constructor(public name: T, public age: K){}

    public getPass(): string {      //all the data type will automatically converts to sting type
        return `${this.name}${this.age}`
    }
}

const dick = new User("Dick", 33);  //passing a string and number to a function, therefore we use <T, K> generic types
```

```JS
//Example 3 - we can define if we want to see one of received function arguments should be a specific data type
//For example if second argument we must have a number, we can make a restrictions for generic Type - K, that it can receive only numbers
//For this cases we need to use - extends

class User<T, K extends number> {  //restrictions for generic Type - K, can be only a number
    constructor(public name: T, public age: K){}

    public getSecret(): number {
        return this.age*3;  //method can be performed only with age =number, therefore we added restrction for generic Type - K
    }
}

//In this case, TypeScript has very good controll on all stages
```

# Decorators

- Decorators - Ability of TypeScript to add annotations and meta program syntax for declaring classes and functions

- Decorator - is an ordinary function, it can be attached to a class declaration, an accessor method, a property or a parameter.

- Decorators wrap the decorated entity and modify its behavior.

```JS
class User {
    constructor( public name: string, public age: number){}

    public getPass(): string {
        return `${this.name}${this.age}`
    }
}
```

```JS
//Base structure of Decorator

const logClass = () => ();
```

```JS
//There is one simple rule- as the only argument the class decorator function must accept the constructor of the decorated entity

//Class Decorator
const logClass = (constructor: Function) => {  //this logClass decorator recives constructor and return everything into console
    console.log(constructor);   //Result of call: Class User{}
}

@logClass   //<--If you want to Use or Apply decorator for class
class User{
    constructor( public name: string, public age: number){}

    public getPass(): string {
        return `${this.name}${this.age}`
    }
}
```

In general there are 4 main Decorator types:

- Class
- Property
- Method
- Accessor

////////////////////////////////////////////////////////////////////////////

```JS
// Parameter type annotation, declare what types of parameters the function accepts.

function greet(name: string) {
  console.log("Hello, " + name.toUpperCase() + "!!");
}


//You can also add return type annotations. Return type annotations appear after the parameter list:
function getFavoriteNumber(): number {
  return 26;
}


//Much like variable type annotations, you usually don’t need a return type annotation because TypeScript will infer the function’s return type based on its return statements

//If you want to annotate the return type of a function which returns a promise, you should use the Promise type:

async function getFavoriteNumber(): Promise<number> {
  return 26;
}


// The parameter's type annotation is an object type
function printCoord(pt: { x: number; y: number }) {
  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);
}

printCoord({ x: 3, y: 7 });



//Optional Properties
//Object types can also specify that some or all of their properties are optional. To do this, add a ? after the property name:
function printName(obj: { first: string; last?: string }) {
  // ...
}
// Both OK
printName({ first: "Bob" });
printName({ first: "Alice", last: "Alisson" });


//Let’s write a function that can operate on strings or numbers:
function printId(id: number | string) {
  console.log("Your ID is: " + id);
}
// OK
printId(101);
// OK
printId("202");


function welcomePeople(x: string[] | string) {
  if (Array.isArray(x)) {
    // Here: 'x' is 'string[]'
    console.log("Hello, " + x.join(" and "));
  } else {
    // Here: 'x' is 'string'
    console.log("Welcome lone traveler " + x);
  }
}


function greet(person: string, date: Date) {
  console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}
greet("Maddison", new Date());
```

[--> Video <--](https://www.youtube.com/watch?v=MtO76yEYbxA&list=PLNkWIWHIRwMEm1FgiLjHqSky27x5rXvQa&index=2)
