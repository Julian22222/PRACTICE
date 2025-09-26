# React + TypeScript

```JS
//in the component

import ( IProduct ) from '../Types/types.tsx'  //import model type

interface ProductProps {  //props that this component will receive
    product: IProduct
    onCreate: ()=> void   //function that doesnt return anythig (usually used for useState functions, when set a value)
}

const submit=()=>{
  ///some code

  onCreate();  //calling the function
}

export function Product ({product}: ProductProps){     //or export function Product (props: ProductProps){ ... {props.product.title} ....}
    return(
        <div>
            {product.title}

            <button onClick={submit}>click</button>
        </div>
    )
}



//types.tsx
export interface IProduct {
    id: number,
    title: string,
    price: number,
    description: string
    category: string,
    image: string
}

```

```JS
//use State - if details === true -> show component

{details &&
    <div>
        <p> {product.description}
    </div>
    }

```

```JS

const Card: FC<CardProps> = ({ width, height, children, variant, myFunc }) => {
  const [myNumber, setMyNumber] = useState(2);

  return (
    <div
      onClick={() => myFunc(myNumber)}
      style={{
        border: variant === CardVariant.outlined ? "1px solid red" : "none",
        background: variant === CardVariant.primary ? "lightgrey" : "",
        width,
        height,
      }}
    >
      {children}
    </div>
  );
};

export default Card;

////////////////////////////////The same component
export function Card: FC<CardProps> ({ width, height, children, variant, myFunc }) {
  const [myNumber, setMyNumber] = useState(2);

  return (
    <div
      onClick={() => myFunc(myNumber)}
      style={{
        border: variant === CardVariant.outlined ? "1px solid red" : "none",
        background: variant === CardVariant.primary ? "lightgrey" : "",
        width,
        height,
      }}
    >
      {children}
    </div>
  );
};

```

```JS
// What is a Functional Component (FC) in React with TypeScript?
// Here, Greeting is a functional component typed with FC<Props>, meaning it accepts props of type Props.


import React, { FC } from 'react';



type Props = {
  name: string;
};



const Greeting: FC<Props> = ({ name }) => {
  return <h1>Hello, {name}!</h1>;
};



//Why use FC?

-It automatically types the children prop, so if your component accepts children (e.g., <Greeting>children</Greeting>), it’s typed properly.

-It helps ensure your props are correct and TypeScript can check for you.

-Provides a cleaner and more concise syntax than class components.



//When and where to use it?

-Use  it Almost always, for all new React components. Use Functional Components almost everywhere! They are the standard way to build React components nowadays.

- For simple or complex UI parts, functional components are great.

- When you want to use React Hooks (like useState, useEffect), you have to use functional components because hooks don’t work in class components.

- If you want a component with props and maybe children, typing it with FC helps keep your code safe.





////Abstract Class
//static property in the class - static table = "12345";


//How to check data type if we receive multiple argument types in the function

function printId(id: number | string) {

  if (typeof id === "string") {

    // In this branch, id is of type 'string'

    console.log(id.toUpperCase());

  } else {

    // Here, id is of type 'number'

    console.log(id);

  }

}





function welcomePeople(x: string[] | string) {

  if (Array.isArray(x)) {

    // Here: 'x' is 'string[]'

    console.log("Hello, " + x.join(" and "));

  } else {

    // Here: 'x' is 'string'

    console.log("Welcome lone traveler " + x);

  }

}



//Optional Properties

Object types can also specify that some or all of their properties are optional. To do this, add a ? after the property name:

function printName(obj: { first: string; last?: string }) {

  // ...

}







// Type Aliases

//if you use the same type more than once and refer to it by a single name

type Point = {
  x: number;
  y: number;

};



// Exactly the same as the earlier example

function printCoord(pt: Point) {

  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);

}



//can be written this way as well:
type ID = number | string;



// Interfaces,  Interfaces VS  type
//An interface declaration is another way to name an object type:

interface Point {

  x: number;
  y: number;

}


function printCoord(pt: Point) {

  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);
}

printCoord({ x: 100, y: 100 });





// Non-null Assertion Operator (Postfix !)
TypeScript also has a special syntax for removing null and undefined from a type without doing any explicit checking. Writing ! after any expression is effectively a type assertion that the value isn’t null or undefined:

function liveDangerously(x?: number | null) {

  // No error

  console.log(x!.toFixed());

}





// Modules in TypeScript, how to import and export



// @filename: hello.ts

export default function helloWorld() {

  console.log("Hello, world!");

}



//use in different file
import helloWorld from "./hello.js";

helloWorld();





//example2
// @filename: maths.ts
export var pi = 3.14;
export let squareTwo = 1.41;
export const phi = 1.61;

export class RandomNumberGenerator {}

export function absolute(num: number) {

  if (num < 0) return num * -1;
  return num;

}



import { pi, phi, absolute } from "./maths.js";

console.log(pi);

const absPhi = absolute(phi);

```
