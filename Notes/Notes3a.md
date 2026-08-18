# REACT JS NOTES

# React Input

<input type="hidden" />

type="radio"
type="text"
type="number"
type="time"
type="submit"
type="password"
type="tel"
type="url"
type="week"
type="range"
type="button"
type="search"
type="reset"
type="email"
type="month"
type="checkbox"
type="date"
type="datetime-local"
type="file"
type="image"
type="color"

# 🔥 REACT BASIC INFO

- DOM (the Document Object Model)- it is a programming interface for HTML and XML documents
- DOM represents web page to allow programm amend, change document structure, style and content.
- DOM represents document as nodes and objects. Therefore different programming languages can connect to this web page.
- The DOM is an object-oriented representation of the web page, which can be modified with a scripting language such as JavaScript.
- The DOM has a tree structure, tree starts from root node which is

```JS
document.documentElement<html>
```

and then it branches out.
The Virtual DOM is a lightweight copy of the real DOM that React uses to improve performance.

- browsers can't read JSX, JSX must be converted to JavaScript using tools like Babel

- DOM relatively slow in refreshing due to tree structure, which is needed recursion.
- The Virtual DOM is a lightweight copy of the real DOM that React uses to improve performance.
- React gives opportunity to developers to work with virtual DOM. React creates a lightweight, in-memory representation of the UI.
- The Virtual DOM (VDOM) is a lightweight, in-memory representation of the real DOM.
- Virtual DOM (VDOM) which is used by React is refreshing much quicker and effective, because he doesn't use recursion to go through all nodes and branches( manually traversing through the parent and child nodes), but it uses Virtual DOM to compare with previous version of VDOM and updates only those nodes that have been changed.
- The Virtual DOM is a "copy" of the real DOM kept in memory. When state changes, React creates a new Virtual DOM tree. It compares it to the old one (this is called Diffing).

- React allow us to use syntax mix of JavaScript and HTML which is called JSX (JavaScript XML / JavaScript extension), JavaScript can be implemented using curly brackets --> {}
- React JSX can render strings, numbers, booleans (ignored), arrays, and React elements, but cannot render plain objects directly.
- Browsers cannot read JSX directly, so tools like Babel transpile it into standard React.createElement() calls.

```JS
//Why objects cause an error?

const obj = { name: "Alex" };

function App() {
  return <h1>{obj}</h1>;
}

//React will throw an error like:
//Objects are not valid as a React child
//This happens because React doesn’t know how to display a plain object.


//How to render objects correctly. You must convert objects into renderable values, such as:

//1️⃣ Access object properties
<h1>{obj.name}</h1>

//2️⃣ Convert object to a string
<h1>{JSON.stringify(obj)}</h1>

//3️⃣ Map over object values
{Object.values(obj).map(value => (
  <p key={value}>{value}</p>
))}
```

- Components allow to separate our app on independant and reusable blocks of code. All components names must begin with Capital Letter (PascalCase) to separate them from HTML tags (because they use the same syntax when theya are rendering)

```JS
<div />, <span /> //<-- HTML elements

<MyComponent />  //<-- Components
```

- Also, components create parent-child relationship and show child components -->
  App
  - Header
  - Nav
  - Content //<--parent has 2 child
    - Welcome Page //<--first child of Content
    - Sidebar //<--second child of Content

- onClick = {function} //<-- function that will be invoked after click
- onMouseEnter = {mouseOver function} //<-- function will be invoked after hover on that field

- lazy loading -> Loading components only when needed.

# 🗂️ Basic React file structure

- public (FOLDER) //<-- The public folder is used to store static files (you can keep here -🖼️ Images (logos, icons), 📄 index.html, 🌐 Favicon (favicon.ico), 📜 Manifest files, 🔊 Audio / video files)
  - favicon.ico
  - index.html ////<--MAIN, ROOT FILE, STARTING FILE

- src (FOLDER)
  - App.css //<--css file,styles
  - App.js //<-- where you start your DOM tree, first component, all others components are child components
  - index..css
  - index.js //<-- connect App.js to render + root, here we can make visualisation of React element inside DOM's root node
  - components //<-- folder, where you keep all components for your website

```JS
//Files in public can be accessed directly by URL:
<img src="/logo.png" alt="Logo" />

//Or using an environment variable (CRA):
<img src={process.env.PUBLIC_URL + "/logo.png"} />
```

.....................................................................................................

# ➕ Creating new REACT apps

```JS
//in terminal we put
// my-app-name <-- any name of our app, must be only in small letters

npx create-react-app my-app-name
```

- this command will install Reat app with all default recomended settings, will install required dependencies, create new git repo and initial commit if one doesn't exist. Also, we need to delete all unnecessary files and code.

- Before you start creating your app you need to think about app design and component location
  Tools: - Excalidraw - Figma - Miro

# 📦 Most common snippets /The short letters used to quickly create a function component in React.js

```JS
1️⃣ rfce + Tab
//Creates a React Function Component with export

import React from 'react'

function ComponentName() {
  return (
    <div>ComponentName</div>
  )
}

export default ComponentName
```

```JS
2️⃣ rfc + Tab
//Creates a React Function Component (without export)
```

```JS
3️⃣ rafce + Tab
//Creates a React Arrow Function Component with export

const ComponentName = () => {
  return (
    <div>ComponentName</div>
  )
}

export default ComponentName
```

```JS
4️⃣ rafc + Tab
//Creates a React Arrow Function Component (without export)
```

# ⛓️‍💥 Different types of writing React components

## React components can be either functional or class based.

- Difference between functional and class components? - Functional components are simple JavaScript functions and use hooks (like useState). Class components use ES6 classes and lifecycle methods.
- Class components: Use ES6 classes and extend React.Component. They use this.state and lifecycle methods like componentDidMount

Developers mostly use functional components, not class-based components.

Why developers prefer functional components:

    ✅ Simpler and cleaner syntax
    ✅ Use Hooks (useState, useEffect, etc.) instead of lifecycle methods
    ✅ Less boilerplate code
    ✅ Easier to read, test, and reuse logic
    ✅ Better performance optimizations (conceptually simpler)

[ --> Components <-- ](https://react.dev/reference/react/Component)

1. Function Component

```JS
import Header from "./components/Header";  //<--importing other component in here, to use it in this component


const App =()=>{

    const listItems = ["One", "Two", "Three", "Four"];
    //some other code related to this component

    return(
        //<--all return code must be in one <div> tag or using <> </> blocks
        <div>

            <h1>Hello World</h1>
            <ul>
                {listItems.map((item)=>( //<--inserting JavaScrit to HTML using curly brackets
                    //<--always use key={...} with <li> tags, keys must be unique and key must be assign to each ellement in the array, keys help React identify which items have changed, are added or are removed. Most often you would use your data as keys. Index can't be used in the keys due it is changing when you adding or removing items in the array.

                    //to help React app identify each item in the list when we loop --> listItems.map((item)=>{...})
                    <li key={item}>
                        {item}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default App;  // <-- export this component, always write this in the bottom of the file component,
//then we can import App from "./App.jsx" in other component, write this on the top of the file component


//In the bottom of component we can use ->
//export default ComponentName (to exposrt component)  ->   import ComponentName from "./ComponentName" (to import component in different file)
//OR
//export const ComponentName = () => {
//  return <div>ComponentName</div>
//}
// --> import { ComponentName } from "./ComponentName"
```

2. Class component

- To create a class based component we will create a Javascript class that inherits from Component using the extends keyword.
- Constructor --> When writing the constructor for an inherited class the first thing we must to do is to call the parent class constructor using the super keyword. This will call the React.Component constructor allowing us to inherit all of the functionality of that class. Our constructor will be invoked with the props of our component and one of the things super(props) will do is attach the props to the instance of our class on this.props.
- Our constructor will be invoked by React and we will not need to instantiate the class ourselves so no need for the new keyword as this will be handled by React.
- Render --> In functional components we would return some JSX to be rendered. As we are now in a class we must define a method called render to achieve the same purpose. This method is named by React and here we will return the JSX for our components UI. Note that to access the props we need to use the this keyword in order to access the instance properties
- State --> Class based components can be used to hold values in state. We can hold state values on a property of state and update those values with an inherited method called setState

- this.state --> In our constructor we can set the initial value of our components state. As we can only have a single value in state this value is an object which can hold multiple properties.

- this.setState --> When extending from the React.Component class we inherit the setState method. This can be used to update the components state following the same rules as the functions returned by useState. We can pass a new state object or pass a function that is invoked with the current state.

```JS
import { Component } from 'react';

// class App extends React.Component { <--the same, if we don't import import { Component } from 'react';
class App extends Component {

 this.state = {            //<-- state values , update these values with method called setState,  can have a single value in state which can hold multiple properties
    age: 42,
  };

  handleAgeChange = () => {
    this.setState({
      age: this.state.age + 1
    });
  };
 //some code here

  render() {
    return (
        <div>
            <h2>Hello World , {this,props.name}</h2>
            <button onClick={this.handleAgeChange}>
            Increment age
            </button>
            <p>You are {this.state.age}.</p>
        </div>
            );
  }
}
```

Example 2

```JS
import React from 'react';

class Greeting extends React.Component {
  constructor(props) {
    super(props); // this.props = props
  }

  render() {
    return (
      <section>
        <p>Hello, nice to meet you {this.props.name}</p>
      </section>
    );
  }
}

// App.js
const App = () => {
  return (
    <div>
      <Greeting name='Paul' />  //<--passing name="Paul to Greeting component"
    </div>
  );
}
```

###### The same component in Functional and Class example

```JS
//Functional component

const CounterWithHooks = () => {
  const [count, setCount] = useState(0);

  return (
    <div>
      <h1>Count: {count}</h1>
      <button onClick={() => setCount((currCount) => currCount + 1)}>Add one</button>
    </div>
  );
};
```

- Class components are used more often
- When we use Class component we don't use hooks. When we write --> extends React.Component we are inherit method setState (useState in Functional components). Using setState for updating the state of the component the same way as useState in Functional components.

```JS
//Class component

class Counter extends React.Component {
  constructor(props) {
    super(props);                     //<-- this allowing us to inherit all of the functionality of that class

    // set initial state, here we can use different states
    this.state = {
      count: 0,
    };
  }

  render() {
    return (
      <div>
        <h1>Count: {this.state.count}</h1>

        // update the state using this.setState
        <button
          onClick={() =>
            this.setState((currState) => {
              return { count: currState.count + 1 };
            })
          }
        >
          Add one
        </button>
      </div>
    );
  }
}
```

Example 4
Class component

```JS
import React from 'react';

classApp extends React.Component{
//Inside 1 component we have all methods and variables that we needed

helpText = "Help text";    //<-- to interact with this variable use--> this.helpText

render(){
    return(
        <div className="Name">
            <Header title="This is my header" />             //<-- passing title properties to Header component
            <h1>{this.helpText}</h1>
            <input placeholder={this.inputClick} onMouseEnter={this.mouseOver} />

            //If we want to interact with variables inside this class always use --> this.  before variable that you want to use (this.helpText)

            <p> {this.helpText === "Help text!" ? "Yes" : "No" } </p>
        </div>
    );

inputClick(){console.log("Clicked")}    //<-- to interact with this method --> this.inputClick
mouseOver(){console.log("Mouse over")}   //<-- to interact with this method --> this.mouseOver


class Header extends React.Component{
    render(){
        return(
            <header> {this.props.title}</header>   //<-- use this.props.title , that was passed in App class
        );
    }
}
}
}

export default App;
```

Example 5

- when using a Class component , we can create our own methods(functions) inside a Class

```JS
class Counter extends React.Component {
  constructor(props) {
    super(props);


    this.state = {
      count: 0,
    };


    // bind incrementCounts value of this to the correct instance of Counter
    this.incrementCount = this.incrementCount.bind(this);
  }

  // define a custom method
  incrementCount() {
    this.setState((currState) => {
      return {
        count: currState.count + 1,
      };
    });
  }

  render() {
    return (
      <div>
        <h1>Count: {this.state.count}</h1>
        // pass the method as a handler
        <button onClick={this.incrementCount}>Add one</button>
      </div>
    );
  }
}
```

# ⚠️ StrictMode

What is a "Strict Mode" in React? <React.StrictMode> is a tool for highlighting potential problems in an application. It renders components twice in development to help find side-effect bugs.

# 🛣️ React Router

Server-side routing

- The server has view for every single route of our app.
- user navigates to /about , the browser sends a GET request to /about and our server responds wih corresponding view.

Client-side routing

- In React app, the server provides a single HTML file and a bundle of JavaScript. The rendering of the applicatiohappens on client-side.
- Because all the views of our application are already in the browser, we don't need to make GET request to get different views.
- We can use a route, a librarythat catches the changes in the URL and renders different components accordingly.
- HTTP requests still happen in the background, but not for displaying vievs, just for getting or sending data to servers.

# 🛣️ USING REACT ROUTER

- it will give navigation illusion , it will track all changes in URL and renders corresponding component

```JS
npm i react-router-dom
```

Example

```JS
import {BrowserRouter, Routes, Route} from 'react-router-dom';

const App=()=>{

return(
    <BrowserRouter>      //<-- BrowserRouter must be wrapped around ll our entire app
        <div className="App">
            <h1>My App</h1>

            {/* Everithyng outside of <Routes> -->  <h1>My App</h1>  will be rendered(show) on each page, most often Navbar is used here */}

            <Routes>               //<--Routes component takes a number of Route components as children
                {/*   <Route path="/topics" element={<Topics /> } />  <-- if in URL will be /topics path then react will render component with tha name Topics*/}
                <Route path="/" element={<Home /> } />            ///<-- these are all available Routes in our App, Route takes a props of path
                <Route path="/topics" element={<Topics /> } />    ///<-- these are all available Routes in our App, Route takes a props of path
                <Route path="/about" element={<About /> } />      ///<-- these are all available Routes in our App, Route takes a props of path
            </Routes>
        </div>
    </BrowserRouter>
);
};
```

# 🛣️ React Default Routes (if user uses any URl that not existing in our app -> it will show some page that we will create)

react-router provide a path atribute --> "\*" , it means that it will show certain page if user uses any page that is not assigned or not existing

```JS
<Route path="*" element={ErrorPage} />

//ErrorPage <-- we create separate component for error page, if the route is missing in our code then it will show ErrorPage component and it will be displayed to the user
```

# 🚸 Props (shorten name from properties)

- Often we need to pass data between/ through components
- Props are key-value pairs
- We can pass any data from parent component to the it's child component but backwards we can't do it(from child to parent we can't pass any data)

```JS
const App = ()=>{  //<--parent App component
    return (
        <div>
            <Sum num1={2} num2={3} />  ///<-- passing props to App child component
        </div>
    );
};
export default App;



const Sum =(props)=>{
    console.log(props); //<--{num1:2,num2:3}

    return(
        <p>
            {props.num1} + {props.num2} = {props.num1 + props.num2}  //<--use curly bracket for JavaScript in React
        </p>
    );
};
export default Sum;
```

```JS
//How do you pass a function as a prop?
//You define a function in the parent and pass it just like a variable:

<Child handleClick={myFunction} />.
//This allows the child to "talk back" to the parent.
```

# 🚸 Passing state to child components (passing state using props)

- We can pass any data from parent component to the child component through - props
- props are imutable, and read-only.

```JS
import {useState} from "react";

const App=()=>{
const [todos, setTodos] = useState(["code1","code2","code3"]);

const clearTodos =()=>{
    setTods([]);
};

return(
    <div>
        <Header />
        <button onClick={()=>clearTodos()} > Clear All Todos </button>

        <TodoList todos ={todos} />  //<--passing todos ={todos} to TodoList child component as props

    </div>
)
};


//Header Component
const Header= ()=>{
return <h1> My todo List </h1>
};


//TodoList Component
const TodoList =(props)=>{  //<-- we receive the props here todos, that we passed
    return(
        <ul>
            {props.todos.map((todo)=>{
                return <li key={todo}> {todo} </li>
            })}
        </ul>
    );
};
```

- Example

```JS
const Todos =()=>{

const [todos,setTodos] =useState([
{id:1,text: 'eat'},
{id:2, text: "sleep"},
{id:3, text: "react"},
{id:4, text: "sleep"},
{id:5, text: "react"}
]);

let reactCount = 0;

todos.forEach((todo)=>{  //<--count how many react words we have in our todos
if(todo.text === "react"){
    reactCount++;
}
});

return(
    <div>
        <h1>My todo List</h1>
        <p>How many react words I have to do: {reactCount} </p>
        <ul>
            {todos.map({id, text})=>{
                <li key={id}> {text} </li>
            }}
        </ul>
    </div>
);

};
```

# 🚸 Children Props

- It is a special prop (props.children) that allows you to pass components or elements as data to other components, allowing for "Composition".
- It is a special built-in React prop that represents whatever you put between a component’s opening and closing tags. It contains everything inside component tags. Used for composition, not configuration, Makes components flexible and reusable.
- children is a special React prop that represents whatever is nested inside a component.
- children props allow you to create reusable wrapper components without knowing in advance what content they will contain. Common use cases: Layout components (Page, Container), UI wrappers (Card, Modal, Container components), Higher-order UI components
- children props is very flexible. It can be A string: A number, A React element, Multiple elements (array), null or undefined, Even a function (render props)
- children props lets you build reusable components that wrap dynamic content.
- You use children when you want the Modal to be generic, reusable, and flexible instead of hard-coded.

```JS
//Simple explanation

<MyComponent>
  Hello World
</MyComponent>

// Inside MyComponent, "Hello World" is available as:
// props.children

// Here:
// props is a parameter
// React will automatically pass it when rendering the component
// You do not create props yourself.
```

```JS
//Basic example

function Card(props) {
  return <div className="card">{props.children}</div>;
}

//Usage:
//<h2> and <p> are passed as children, Card renders them using props.children
<Card>
  <h2>Title</h2>
  <p>This is content</p>
</Card>

//Rendered output:

<div class="card">
  <h2>Title</h2>
  <p>This is content</p>
</div>
```

```JS
//Destructuring children
//Common pattern:

function Card({ children }) {
  return <div className="card">{children}</div>;
}
```

```JS
//Real-world example: Modal component
//Modal.jsx

function Modal({ isOpen, onClose, children }) {
  if (!isOpen) return null;

  return (
    <div className="overlay">
      <div className="modal">
        <button onClick={onClose}>Close</button>
        {children}
      </div>
    </div>
  );
}

////////////////////////////////////////
//Using the Modal in a page
function App() {
  const [open, setOpen] = React.useState(false);

  return (
    <>
      <button onClick={() => setOpen(true)}>Open Modal</button>

      <Modal isOpen={open} onClose={() => setOpen(false)}>
        <h2>Delete Account</h2>
        <p>Are you sure you want to delete your account?</p>
        <button>Confirm</button>
      </Modal>
    </>
  );
}
```

What’s happening (real usage explanation)

- Modal doesn’t know what content it will show
- The content (h2, p, button) is passed as children
- This makes Modal reusable anywhere in the app

```JS
//With children (flexible & reusable ✅) - with children props you can pass to Modal different compoents. Make it reusable by passing different content

function Modal({ isOpen, onClose, children }) {
  if (!isOpen) return null;

  return (
    <div className="modal">
      <button onClick={onClose}>✕</button>
      {children}
    </div>
  );
}

//Usage:
<Modal isOpen={open} onClose={close}>
  <h2>Delete Account</h2>
  <p>Are you sure?</p>
  <button>Confirm</button>
</Modal>
```

Think of Modal like a box 📦

The box stays the same
You can put anything inside
Without children, you’d need:

A different box for every item

# 🪝 Hooks (useState, useEffect, useParams, useSearchPramas, useNavigate, etc.)

- React Hooks - Functions that let you use state and lifecycle features in functional components.
- Hooks were introduced to reuse logic and avoid complex class components.
- All hooks starts with word --> use
- Our function should be named with the value of state they update --> setName, setCount etc. if we create state for name then we need to call it --> const [name,setName] = useState("")

- Only call Hooks at the top level of the component (declare all Hooks on the top)
- Only call Hooks from React function (Hook must be inside the function of the React component )

# 🪝 useState - React State

- To make our apps dynamic they must keep track of some information that cnahges. This concept reffered to as state.
- We use state to track any data that changes i our app
- React gives us the useState hook, which allow us to change state variable and then React will update our UI amd then render HTML --> new data will appear on our UI (new data will show on webpage)
- A good rule of thumb to follow --> place your state as low as possible, but as high as necessary. (you need to plan in what component you need to have which state, state must be located in correct component in the tree to pass all necessary data to lower components and not to high in the tree to do not make props drilling - by passing props through all the components down to correct component)
- State is a built-in object used to store component data that can change.
- State is local and mutable; props are external and read-only.
- useState - is a hook that allows functional components to manage state. When state changes the component re-renders
- useState is a Hook that allows you to add state to a functional component. It returns an array with two values: the current state and a function to update it.
- Why is state update sometimes "asynchronous"? React batches state updates to improve performance. If you update the state and immediately console.log it, you might see the old value.

```JS
import {useState} from 'react';  //<-- import useState from Reacr library to this component to use it in here

const App =()=>{
    // useState('Paul'); <-- useState takes 1 argument- initial value for the state
    // [Name, setName] <-- 2 elements
    // name <-- the current state value, Name = 'Paul'
    // setName <-- a function to update the state value, we can change the Name ="Paul" only using this function
    const [name, setName] = useState('Paul');
    console.log(name); //<--Paul

    return(
        <div>
            <h3>Hello {Name}</h3>

            <button onClick={()=>setName('Izzi')}> Say Hi to Izzi </button>  //<--by clicking on the button we change the Name="Paul" to Name = "izzi" using setName function
            //later we will use (event)=>{setName(event.target.value)}
        </div>
    );
};

export default App;
```

- Somethimes new state value will be dependant on the current value. As easy example --> when we create counter

```JS
import {useState} from 'react';  //<-- import useState from Reacr library to this component to use it in here

const App =()=>{
    const [count, setCount] = useState(0);  //<--count = 0 , from begging

return{
    <div>
        <h1>Count: {count}</h1>  //<-- Count: 0, zero is dynamic number

        <button onClick={()=>setCount((currCount)=>{  //<-- currCount === count , In here
            return currCount +1;
            }) }> Button +1 </button>  //<-- when we press this btn -> counter +1
    </div>
};
};
```

- Never mutate the State, always when you want to change the state you must do it using setState function
- From example above, we can change count state only using setCount function. We can't mutate the count and write count = 1. USE setCount!!!

Another Exanple 1

```JS
const [todos,setTodos] = useState(["clean", "wash", "cook", "eat", "sleep", "react"]);
const [task,setTask] = useState("");

function createTodo(){
    setTodos(oldTodos=>{   //<--oldTodos === todos
        return [...oldTodos,task]
    });
}
```

- Example 2

```JS
import {useState} from "react";

const App=()=>{
const [count, setCount] = useState(0);

const incrementCount=(increment)=>{  //<-- parametr will be passed with the function,
    setCount((currCount)=>{
        return currCount + increment;
    });
};

return(
    <div>
        <h1>Count; {count}</h1>
        <button onClick={()=>incrementCount(1)}> +1 Button </button>  //<--Invoke incrementCount by clicking the button
        <button onClick={()=>incrementCount(-1)}> -1 Button </button>
    </div>
);
};
```

- Example 3

```JS
import {useState} from "react";

const App=()=>{
const [todos, setTodos] = useState(["day1","day2","day3"]);

const addTodo = ()={
    setTodos((currTodos)=>{
        return [...currTodos, `day ${currTodos.length}`];  //<--adding new days to our array
    })
};

return(
    <div>
        <button onClick={()=>addTodo()}> Add Todo </button>

        <ul>
            {todos.map((todo)=>{
                return <li key={todo}>{todo} </li>
            })}
        </ul>
    </div>
);


};
```

- We CAN'T Do THIS -->

```JS
setTodos((currTodos)=>{
    currTodos.push('day ${currTodos.length}') //<--WE can't use push
    return currTodos;
    })
```

# 🪝 useEffect - Data Fetching (control of page rendering)

- useEffect - A hook for performing side effects like API calls. useEffect run after the component renders
- When we send a request to api/users with GET method --> to see all users , then it should show all users only once when web page is uploaded.

To do so we use the useEffect hook and it takes 2 arguments:

- effect to run a function (containing the logic of our side-effect)
- dependencies --> an array of variables. When any variable in the array change value , the effect will run
- useEffect With []: Runs once after the first render (Mounting).
- Without []: Runs after every single render.

-->

```JS
useEffect(()=>{},[]);

//From example above we see 2 arguments:
// ()=>{} <-- This is effect, first argument
// ,[] <--dependencies, second argument
```

Example

```JS
import {useEffect} from 'react';   //<--import useEffect to our app to use it here

const Users=()=>{
const [users, setUsers] = useState([]);

useEffect(()=>{
fetch('http://...../api/users').then((data)=>{  //<-- fetch all users from api, will show all users when the component is first rendered, (upload web page), and fetch will return users array (promise)
    setUsers(data.json());   ///<--here we assign all fetched users to our useState
});
},[]);  //<--for this effect there is no deppendencies therefore this array is empty. It means that request will run only once, in first render of this component and never again.

//if array is not empty, it will run this effect on each component rendering --> this wil case limitless componenr rendering cycle.


return(
<ul>
    {users.map(user=>(
        return(
            <li key={user}> [user] </li>   //<-- render each user
        );
    ))}
</ul>
);
};
```

### Data fetching

- Fetch API is based on promise (asynchronisity)
- To make requests in React we need to use - http client, axious (npm package), etc
- By default request will be with GET method

1.

```JS
//request with GET method
fetch('http://itunes.apple.com/search?term=beyonce').then((res)=>{res.json()}.then((data)=>{console.log(data)}));
```

2.

```JS
//request with POST method , we pass request and method, headers,body as a second option argument to fetch.

fetch('http://www.example.com/api/people',{
method: 'POST',
headers: {
    'Content-Type': 'application/json',
},
body: JSON.stringify({name:'Paul'}),
}).then((res)=>{res.json()}).then((data)=>{console.log(data)})
.catch((err)=>{
    console.log(err);
});
```

### 🪝 useEffect dependencies

- when we don't put square brackets at all --> Infinite loop (constattly fetching and updating the page, by running this useEffect)

```JS
//EXAMPLE

useEffect(()=>{
fetchUsers().then((usersFromApi)=>{
setUsers(usesFromApi);
});
} ////IF WE DON"T PUT square brackets HERE
);
```

Dependencies - it is variable that React is checking , depending on this it will invoke this useEffect function and update the user list from API
Everytime when component is rendering React comparing each value in the useEffect dependency array with value that it was in the dependency array on previous render. If any of this values are different --> it will invoke the useEffect function. If values are the same, React won't invoke useEffect function.

```JS
import {useEffect, useState} from 'react';
import {fetchUsersById} from '../.../api.js';  //<--fetch the user (URL link) from different file

const UserProfile=({userId}=>{    //<--{userId} is comming to this controller from --> /api/users/:userId (from fetchUsersById file)
const [user,setUser] = useState({});
};

useEffect(()=>{
    fetchUserById(userId).then((userFromApi)=>{
    setuser(userFromApi);
    });
},[userId]);    //<--each time when userId is changed it will case --> Render with new users (webpage refershes)


return(
    <section>
        <h2>{user.username}</h2>   ///<--render the users profile
    </section>
);
);
```

# 🪝 useParams - get the parameters from URL

1.

```JS
import { useParams, Link } from "react-router-dom";


//Parametric endpoints - useParams (in Route) , The syntax for a parametric endpoints is the same as in express, we use a ":" in our path, followed by the parameters name --> <Route path="/topics/:topic_id" element={<SingleCard/>} />

//and then inside SingleCard component we put --> const {topic_id} = useparams();
//topic_id will return an object containing the url params

const {topic_id} = useParams();

fetch(`http://www.sampleapis.com/wines/${topic_id}`).then((data)=>data.json()).then((collection)=>{console.log(collection)})
.catch((err)=>{
    console.log(err);
});

//useParams hook helps to get dynamic params from URL -> for example:
// <Route path="/topics/:topic_id" element={<SingleCard/>} />    <-- useParams hook will get the value of - topic_id
```

```JS
//Full working example inside SingleCard

import { useParams } from "react-router-dom";
import { useEffect } from "react";

const SingleCard = () => {
  const { topic_id } = useParams();

  useEffect(() => {
    fetch(`https://api.sampleapis.com/wines/${topic_id}`)
      .then((res) => res.json())
      .then((collection) => {
        console.log(collection);
      })
      .catch((err) => {
        console.log(err);
      });
  }, [topic_id]);

  return <div>Check console for data</div>;
};

export default SingleCard;
```

2.  Also, we can use 2 useParams

```JS
    <BrowserRouter>      //<-- BrowserRouter must be wrapped around ll our entire app
        <div className="App">
            <NavBar />
            {/* Everithyng outside of <Routes> -->  <h1>My App</h1>  will be rendered(show) on each page, most often Navbar is used here */}

            <Routes>               //<--Routes component takes a number of Route components as children
                {/*   <Route path="/topics" element={<Topics /> } />  <-- if in URL will be /topics path then react will render component with tha name Topics*/}
                <Route path="/wine-shop" element={<Home /> } />            ///<-- these are all available Routes in our App, Route takes a props of path
                <Route path="/wines/:type/:id" element={<SingleCard /> } />    ///<-- these are all available Routes in our App, Route takes a props of path
            </Routes>
        </div>
    </BrowserRouter>



SingleCard.jsx

const SingleCard=()=>{
    const [eachCard, setEachCard] = useState({});

    const {type,id} = useParams();    //<--read 2 useParams from URL

    useEffect(()=>{
        fetch(`http://api.sampleapis.com/wines/${type}/${id}`)
        .then((topicData)=>{topicData.json()})
        .then((data)=>{setEachCard(data)});
    },[]);
}

```

3.

```JS
//request with DELETE method , we pass request and method, headers,body as a second option argument to fetch.
import { useParams, Link } from "react-router-dom";


const { _id } = useParams();  //<--read user_Id from URL, user_Id must have the same name from App.js -->   <Route path="/wines/:_id" element={<SingleCard />} />


fetch(`http://www.example.com/api/people/${_id}`,{
method: 'DELETE',
headers: {
    'Content-Type': 'application/json',
},
}).then((res)=>{
    alert('Your user has been deleted');
})
.catch((err)=>{
    console.log(err);
});

```

# 🪝 useSearchParams - to handle Queries (get the query parametrs from URL )

- it uses for reading and changing URL queries
- Another feature is to add queries to the end of our endpoints to provide additional functionality.
- Usually this is used in search strings or optional filtering options.
- React router will also parse any queries and make them available via the useSearchParams hook.
- if we make request to /topics?sort_by=title , in this occasion we will receive an access to requests, which are called searchParams
- queries are optional and adding to an endpoint doesn't change the path. Therefore our path will still be the same -->

```JS
<Route path="/topics" element={<Topics />} />
```

Example 1 - ( this is reading URL queries )
request to --> /topics?sort_by=title

```JS
const Topics=()=>{
    let [searchParams, setSearchParams] = useSearchParams();   //<--it uses and returns an array containing 2 values, the same as we have in useState
    console.log(searchParams);                               //<-- URLSearchParams{}

    const sortByQuery = searchParams.get('sort_by');         //<-- get method in searchParams will return key value --> "title", If the request doesn't exist it will return - null

    <section>
        <h2>Topics</h2>
    </section>
};
```

Example 2 - ( this is changing URL queries)

```JS
const App=()=>{
    let [searchParams,setSearchParams] = useSearchParams();

    function handleSubmit(event){
        event.preventDefault();  //<--do not update the page

        let params = serializeFormQuery(event.target);

        setSearchParams(params);
    };

    return(
        <div>
            <form onSubmit={handleSubmit} >
                //some form to submit
            </form>
        </div>
    );
};
```

Example 3 - (Update URL queries)

```JS
<Link to="/topics?sort_by="title" > Topics </Link>

const Topics=()=>{
    let [searchParams, setSearchParams] = useSearchParamas();

    const sortByQuery = searchParams.get('sort_by');  //<--"title"
    const orderQuery = searchParams.get('order');   //<--"title"

    const setSortOrder = (direction)=>{
        const newParams = new URLSearchParams(searchParams);   //<--copy existing queries to avoid mutation
        newParams.set('order', direction);          //<-- set the order query

        setSearchParams(newParams);
    };

useEffect(()=>{
    //fetch new data based on the queries
},[sortByQuery,orderQuery]);

return(
    <section>
        <h2>Topics </h2>
        <button onClick={()=>setSortOrder('asc')}> Ascending </button>
        <button onClick={()=>setSortOrder('desc')}> Descending </button>
    </section>
);
};
```

```JS
//in this example:
//const [searchParams, setSearchParams] = useSearchParams();

searchParams → read values from the URL (like sortBy)
setSearchParams → update the URL query string

setSearchParams is used to update the URL query string so the URL stays in sync with UI state, enabling persistence, shareability, and browser navigation support.
```

Why you need setSearchParams

1️⃣ Reading is not enough

searchParams.get("sortBy");

This only reads from the URL.

If the user:

changes sorting
clicks a filter
selects pagination
👉 you must write back to the URL, otherwise:

the URL won’t reflect UI state
refresh / share link won’t work correctly
That’s what setSearchParams is for

```JS
Update the value (THIS is where setSearchParams is used)
function handleSortChange(value) {
  setSearchParams({ sortBy: value });
}

// Now the URL becomes:
/products?sortBy=rating
```

```JS
// Real-world pattern (recommended)
// Preserve existing query params
// ⚠️ This is important

function handleSortChange(value) {
  setSearchParams(prev => {
    prev.set("sortBy", value);
    return prev;
  });
}

// This:
// updates sortBy
// keeps other params (page, filter, etc.)
```

# 🪝 useNavigate - Navigating Programmatically

- react-router-dom in React Router will provide access to useNavigate hook, which allow navigate programatically , for example after form is submitted.

The navigate function has 2 different argument types:

- A string, which represent the "to" value (the same as with <Link to="/path" >)
- A number, which represents how many steps forward (positive number) or backwards (negative number) you'd like to go in the browser's history (e.g navigate(-1 is the same as clicking the back button))

THIS IS NOT A REPLACEMENT FOR <Link> components!!!

Example

```JS
import {useNavigate} from 'react-router-dom';

function MyComponent(){
    const navigate = useNavigate();

    function handleSubmit(e){
        e.preventDefault();
        doSomething()
        .then(()=>{
            navigate('/somewhere/else');
        })
        .catch((err)=>{
            console.log(err);
            //handle error
        });
    };


    return(
        <form onSubmit={handleSubmit} >
            <label> Some msg </label>
            <input />
        </form>
    );
};
```

# Controlled Components (in React)

### This example showing uncontrolled component

```JS
const App =()=>{

const [list,setList] = useState(["Bread","Strawberries","Chocolate"]);

return(
<div>
    <ItemAdder setList={setList} />  //<--passindprops to child components
    <ShoppingList items= {list} />
</div>
);
};


const ShoppingList =(props)=>{
return(
    <ul>
        {prop.items.map((item)=>{
            return (
                <li key={item}>
                    <p>{item}</p>
                </li>
            );
        })}
    </ul>
);
};


const ItemAdder=()=>{
return(
<div>
    <form>
        <label> Add new Item:
        <input />  //<-- this input allow to put some data,but it is inserting data to React, to read the data we need to return to DOM, this is called uncontrolled component
        </label>
        <button type="submit"> Addd item </button>
    </form>
</div>
);
};
```

### This example showing controlled component

A component is controlled if:

- its value is set with a prop.
- changes to that value are handled by React

```JS
const ItemAdder =()=>{

const [newItem, setNewItem] = useState("");

return(
    <form>
        <label>
            Add a new item:

            //Each controlled component must have --> value={...} and onChange={....}, if any of this block will be missing then we can't change the useState or it will have constant values
            //value={newItem} <--input data will be available in newItem, everything that will be typed in the input will be available in newItem
            //onChange={(event)=>setNewItem(event.target.value)}  <--onChange event is trigered every time when input value is changed

            <input value={newItem} onChange={(event)=>setNewItem(event.target.value)} />  //<--everything user write in this input will be inserting to newItem useState
        </label>
        <button type="submit" > Add item </button>
    </form>
);
};
```

#### Multiple elements in Controlled Component

- Example 1

```JS
const [formData, setFormData] useState({
name: "",
lastname: "",
email: "",
phone: ""
});

const onChange=(e)=>{
    setFormData((prev)=>{                  //<--will write new data using setFormData
        let prevData = {...prev};          //<-- creating a copy of previous value from formData using spread operator and adding this value to prevData variable
        prevData[`${e.target.id}`] = e.target.value;     //<--asign new value , key = value

        return prevData;
    });
};


<input type="text" value={formData.name}     //<-- all inserted text will go to formData.name in useState
onChange={onChange}                          //<--we invoke onChange function on any text change in the input
id="name"                                    //<-- id must be the same as in formData useState object
placeholder="name" />

<input type="text" value={formData.lastname} onChange={onChange} id="lastname" placeholder="lastname" />
<input type="email" value={formData.email} onChange={onChange} id="email" placeholder="email" />
<input type="phone" value={formData.phone} onChange={onChange} id="phone" placeholder="phone" />

```

- Example 2

```JS
const [data,setData] = useState({
userEmail: "",
title: "",
progress: ""
});

const handleChange =(e)=>{

const {name, value} = e.target;    //<--destructuring name and value from e.target
setData(data=>(
    ...data, [name] : value      //<--creating a copy of previous value from data using spread operator and adding new key value pair to the data useState
));
};

<input type="email" required name="userEmail" value={data.userEmail} onChange={handleChange} />
<input type="text" required maxLength={30} name="title" value={data.title} onChange={handleChange} />
<input type="range" min="0" max="100" required name="progress" value={data.progress} onChange={handleChange} />
```

# 🎉 Event Handlers

in React.js, onClick, onSubmit, and onChange are event handler props (often called event handlers for short).

- it is a function that triggers in response to user actions (onClick, onChange, onSubmit, etc)
- uses camelCase

```JS
const headleChange =()=>{
  console.log("Button clicked");
}

//Can invoke the function using these examples:
<input type="text" onChange={headleChange} />
//OR
<input type="text" onChange={()=>headleChange() } />
```

```JS
//Option 1 (✅ Recommended)
<input type="text" onChange={headleChange} />

// Why this is better:

// React automatically passes the event object to headleChange
// No unnecessary function creation on each render
// Cleaner and idiomatic React


function headleChange(e) {
  console.log(e.target.value);
}

// ✔ Best practice
// ✔ Better performance
// ✔ Access to the event
```

```JS
// Option 2 (⚠ Works, but usually not needed)

// What’s different:

// You are creating a new function on every render
// The event object is NOT passed unless you do it explicitly
// Usually unnecessary unless you need extra logic or parameters
// If you want the event here, you must write:



<input type="text" onChange={(e) => headleChange(e)} />

<input
  type="text"
  onChange={(e) => headleChange(e, "username")} />
```

# 🗒️ Forms

```JS
const ItemAdder =({setList})=>{

const [newItem, setNewitem] = useState("");

const handleSubmit =(event) =>{

    event.preventDefault();    //<--prevent the form default behavious, won't refresh the web page and won't go to another web page, by invoking this function


    setList((currList)=>{             ///<-- add new item to our list in App
        return [newitem, ...currList];
    });

    setNewItem("");            //<-- reset the input to be empty, input conteiner will become empty after submitting data
};


return(
<form onSubmit={hadleSubmit}>  //<--after pressing submit button, the form will be sent --> this will invoke the  hadleSubmit function

    <label>
        Add a new Item:
        <input value={newItem} onChange={(event)=>setNewItem(event.target.value)} />
    </lable>
    <button type="submit"> Add item </button>         //<--submit button
</form>
);
};
```

### ⭐ Most Common used EVENT HADLERS

- onClick
- onChange
- onSubmit
- onFocus
- onBlur
- onKeyDown
- onMouseEnter / onMouseLeave
- onScroll
- and many others

# 🔗 Link tags VS. anker tags

- when use `<a>` tags, usually it uses to connect web pages togther, but by default it will upload new page --> which defeats one of the purposes of React and single page applications.
- If you need a full page reload (e.g., for external links or to reset the entire application state), use the standard HTML `<a>` tag instead of `<Link>`.

```JS
<a href="/your-path">Click here</a>
```

- Therefore we use `<Link>` to don't upload entire page. For example using `<Link>` tags are useful in NavBar component, NavBar component will be outside `<Routes>` and inside `<BrowserRoutes>`
- `<Link>` component is designed specifically to prevent full page refreshes. It enables client-side routing, which updates the URL and changes the view without fetching a new HTML document from the server, making your app feel faster and more seamless.

```JS
<nav>
    <Link to="/" > Home </Link>
    <Link to="/about" > About </Link>
</nav>
```

- Alos, we can wrap <Link> tag aroun any quantity child elements-->

```JS
<ul>
    <li>
        <Link to="/topics/1" >
            <h2> Topic Id 1 </h2>
            <img src="http://....." alt="topic1" />
            <p>Topics 1's tag link </p>
        </Link>
    </li>
</ul>
```

# 🌐 React Context

- useContext - a hook to access global state without prop drilling
- When we passing data through out the components in React, we hold our values in state and pass them between components using props. When we have big and complicated App , we can use state in few/ many components on various levels of nesting in our component trees. Therefore state is hold on higher levels (in App.js) and then passing down through different components - this is called props drilling.
  -The Context API is designed for state that needs to be shared by multiple components, or for state that can be considered global
- React offers a solution --> Context API. Context API makes data and state global (accessible everywhere) in our App.
- Usually in Context is used : Loged in users, themes (swith between light and dark theme) or language settings.

Example 1

```JS
//we create separate component --> Context.jsx
//Context.jsx

import {createContext} from 'react';
export const ThemeContext = createContext();  //ThemeContext <-- can be any name, Also Context can be created using --> React.createContext()


...................................................................
Theme.js  //can be app.js or index.js
const ThemeProvider =(props)=>{

const [theme,setTheme] = useState('light');


return(
<ThemeContext.Provider value={{theme,setTheme}} >    //ThemeContext -->is the same name from line 1279.  theme and setTheme will be available everywhere, globaly in our App
{props....}
</ThemeContext.Provider>
);
};
......................................................................


ToggleTheme,js

import {useContext} from 'react';
import Context from "./Context";

const ToggleTheme=()=>{
const {theme,setTheme} = useContext(ThemeContext);  //<--here we can use these data (theme,setTheme), ThemeContext name must be the same from line 863

const toggleTheme = () => {
    setTheme((currTheme) => {
      return currTheme === 'light' ? 'dark' : 'light';
    });
};

return (
    <button onClick={toggleTheme} className={`button__${theme}`}>
      Change theme
    </button>
  );
};


export default ToggleTheme;
```

...................................................................................

- Context can be created using --> React.createContext() . To Create Context can be passed some starting values, when we don't use Provider. This can be useful when writing front and tests for our components

Example 2 (-->See App.js file)

```JS
//We are passing a value prop to the Context. Provider and this value will be available to all the children of that provider. Thereforewe should wrap all our app in Providers on high level to make all values to be accessible everywherein in our app

<Context.Provider value={value} >    //<--Context is a name from the file where we assign the data, in our case ThemeContext
                                    //{value} <-- these are values of all props that will be accessible everywhere in our App
    <Header />    //<--This is child of Context.Provider
    <NavBar />    //<--This is child of Context.Provider
    {/* //some code here.. */}
</Context.Provider>

```

# UI Behaviours and Optimistic Rendering

Updating UI -->

- Making a fresh request we eill get the most up to date data which is static until a new request is made. We should understand and be familar with this concept when we are updating the data using POST, PATCH,PUT, DELETE etc. request.
- In particular, when API's (server) being used by multiple users will cause multiple updates at the same time. --> When you give "Like" on the page and at the same time some more users are giving "Like" on the same page --> You want to see +1 "Like" , but not all the "Likes" that have been done at the same time , under the same post or picture.

#### Refreshing data Vs. Local updates

- When user posting something , put "LIKE" , leave a comment etc. In these case user interface(UI) should show the effect of what was changed or added , notifying the user that his changes were applied to the server.
- Good rule of thumb is to show the user the result of their actions.
- Here we don't want to show actual, real report how many messages we have, comments number,number of Likes, how many posts waas send to the server, etc. at the moment. But we want to show that the user was made an inpact by pressing some button. Therefore we change last number +1 to show the effect from pressin the button.

#### Optimistic rendering

- Optimistic rendering is the technique of rendering UI updates without confirmation of success from the back end based on the assumption that the request will succeed. There are several requests to a server that won't require any kind of server side validation and will be successful the vast majority of the time. Think of features such as clicking a like button or up-voting a users post. Facebook's servers will process a huge number of requests to like posts with almost all of them being successful.

- No matter how fast the servers are however it will take some amount of time to process the request. In order to improve our UX we can assume that the request will succeed and give our user immediate feedback that their request has succeeded whilst it is processed in the background. When the request is eventually successful, we have no need to perform further updates, so can stay quiet.

- It should be clear that this approach should be avoided in some circumstances - financial transactions, for examples, should never give the indication that something has happened when it hasn't, or vice versa. But there may be advantages in some cases to adopting the optimistic rendering approach

Example

```JS
//optimistic approach
//At this point, as developers, we need to decide what we are trying to present here: a factual report of how many messages have been sent at that moment, or the impression that a user has had an impact by pressing the button. If it is the latter, then seeing the number increment by one might be more effective. This is demonstrated below:

const [voteIncrementCounter, setVoteIncremntCounter] = useState(0);
const [err,setError] = useState(null);

const handleIncrement=()=>{

    fetch(`http://...../${article_id}/comments`)
    .then((data)=> data.json())
    .then((res)=>{setVoteIncremntCounter(res)});

    setVoteIncremntCounter((currentLikes)=>currentLikes + 1);   //<--this will show +1 Like on the Page straightaway, without uploading to the server

    fetch(`http://...../${article_id}/comments`,{   //<--this part posting changes to the database, uploading + 1 like to the server
    method: 'PATCH',
    headers: {"Content-type":"application/json; charset=HTF-8"},
    body: JSON.stringify({//HERE WHAT WE ARE POSTING TO THE SERVER, votes: +1})
    })
    .then((res)=>res.josn())
    .catch((err)=>{setError(err)});  //Error handling

    //Error handling - is always a chance of things going wrong, even if it's something like the network connection cutting out. We will need to handle these errors and give our users appropriate feedback. It may be enough to simply undo the change you made so that the user can try again or you may need to keep some err state to give your user more detailed feedback.
    // We use catch block --> .catch((err)=>console.log(err));
});


<button onClick={handleIncrement} > Like  </button>   //<-- by pressing this Like btn , it will invoke --> handleIncrement function

{err ? <p>{err}</p> : null} //if there is an error then it will show the error, if no error will not show anything
```

#### Optimistic approach

- By adopting an optimistic approach we assume that the request will be successful. In this case, we don't need the api response to perform our UI update and the server will correctly process this request in the vast majority of cases. This allows us to perform the UI update immediately whilst the api call is processed in the background. This gives us a much more responsive UX and gives the user feedback that their actions are happening immediately.
- Both setState and the API request trigger simultaneously
- Both actions will always trigger
- Cannot accurately represent the database

#### Non-optimistic Approach

- It's waiting for the response to the PATCH request before updating the UI.
- setState only happens after the API response
- Can accurately represent the database at the time of response

# Loading patterns

- when working with http requests, all requests are asynchronios and it takes some time to get the response back -->in these cases it will show to user that the web page is loading
- loadind speed depends from user internet connection and traffic volume going through our server. Therefore while we waiting for a response from the server it is good practice to show to the user that the page is loading.

```JS
//Example

import {useEffect} from 'react';

const User=()=>{
const [users, setUsers] = useState([]);
const [isLoading,setIsLoading] = useState(true);  //isLoading === true from the start, it will case the render to show --> Loading ... message

useEffect(()=>{
fetch('http://www.example.com/api/users').then((res)=>{res.json()}).then((usersFromApi)=>{setUsers(usersFromApi)}).catch((err)=>{console.log(err)});

setIsLoading(false);  //<--when we get the succesfull (fullfilled) response back and get all the users we setIsLoading === false,
},[]);

return(

if(isLoading){                 //<--if isLoading === true it will show message --> Loading ...
return <p> Loading ...</p>
}else{                          //if isLoading === false it will show all users
return <ul>
{users.map(user=>{
//render each user
})}
</ul>
};

);
};
```

...........................................................................................................................

# Client Side vs Server Side Errors

Server side validation and client side validation

- Sometimes errors that can occur could be dealt with before a request is sent to a backend - client side. Sometimes the only time you can know something is wrong is when the server responds with an error - server side.

- Client Side (Post information like body is missing, The user use URL that is not existing etc. )

- Server Side (the server isn't working, URL is wrong, incorrect Id that user requesting, the user not authorised to access a resource etc.)

# Sorting and other mothods + spread operator

- When render a list of array items or we need to sort some array in React we must not mutate original array (Don't mutate state)
- To avoid array mutation we need to create a copyb of this array before we start sort, map etc. Therefore we use spread operator --> [...items] <--as example, it will copy all the elements from current array to new the array (create a shallow copy of the array) , whech we can change and use further.

Example of mutation

```JS
cosnt List=()=>{

const [items, setItems] = useState(['banana', 'apple']);

return(
<div>
   <ul>
        {items.map((item) => (
          <li key={item}>{item}</li>
        ))}
      </ul>
      <button
        onClick={() => {
          setItems((items) => items.sort()); // This is bad
        }}
      >
        Sort alphabetically
      </button>

{/* use example below*/}
    <button
        onClick={() => {
        setItems((items) => [...items].sort());  // <-- here we use spread operator and don't mutate the state, we create a shallow copy of the array
        }}
        >Sort alphabetically
    </button>
</div>
);
};
```

Also we can use 2 buttons, have we want to sort this items list (in alphabetic order or the other way around)
In this example items and the sort value are stored in useState and are updated independently. --> This is not ideal
clicking the "Sort buttons" doesn't trigger an update to the items array, (array list not changing for a user -UI is not changed)

```JS
const List = () => {
  const [items, setItems] = useState(['banana', 'apple','pear', "pineapple"]);
  const [sort, setSort] = useState('a -> z');

  return (
    <div>
      {/* render items */}
      <p>Sorted {sort}</p>
      <button
        onClick={() => {
          setSort('a -> z');
          setItems((items) => [...items].sort());  //<-- will sort items order in alphabetical order
        }}
      >
        Sort a to z
      </button>
      <button
        onClick={() => {
          setSort('z -> a');
          setItems((items) => {
            return [...items].sort((a, b) => {
              return b.localeCompare(a);               //<-- will sort items order in "Z" --> "A" order
            });
          });
        }}
      >
        Sort z to a
      </button>
    </div>
  );
};
```

This example uses useEffect and when a user clicks a button it will trigger an update to the items array

```JS

const List = () => {
  const [items, setItems] = useState(['banana', 'apple', "lemon"]);
  const [sort, setSort] = useState('a -> z');

  useEffect(() => {
    if (sort === 'a -> z') {
      setItems((items) => [...items].sort());
    } else {
      setItems((items) => {
        return [...items].sort((a, b) => {
          return b.localeCompare(a);
        });
      });
    }
  }, [sort]);

  return (
    <div>
      {/* render items */}
      <button onClick={() => setSort('a -> z')}>Sort a to z</button>
      <button onClick={() => setSort('z -> a')}>Sort z to a</button>
    </div>
  );
};
```

this example with --> API requests, calls

```JS
const List = () => {

  const [items, setItems] = useState("");
  const [isLoading, setIsLoading] = useState(true);


useEffect(() => {
    setIsLoading(true);   //<--until we don't get the response back from the server --> loading state = true, and i t will show Loading... message

    api
    .getItems({ sort })
    .then((items) => {
      setItems(items);
      setIsLoading(false);     //<-- here we recive the response and setIsLoading = false, removes loading message from UI
    })
    .catch((err) => {
      // error handle
    });
}, [items]);    //when items in useState is changing it will update the UI items list



  return (
    <div>
    <ul>
        {items.map(item)=>{
            <li key={item}>
                {item}
            </li>
        }}
    </ul>

      <button onClick={() => setItems([...items,apple])}>Add Apple</button>    //<--when we change items array it will trigger useEffect
    </div>
  );
};
```

# Pagination

- API's usually has lots of data, could be more than 1000 elements (books for example)
- Imagine how big the response would be if you could make a request to get all books from this server
- There are many different ways that RESTful APIs can do this. A few of the common options are:
  - Page numbers ( The client requests a particular "page" of results:)

  ```JS
  /items?p=1    <--responds with the first 10 items

  /items?p=2    <--responds with items 11 -> 20

  ?_limit=10 and ?limit=10 //are both valid URLs, but only one will work depending on how the server is implemented.
  ```

  - Limit / Offset (The client provides how many items they want to receive and where the server should start)

  ```JS
  /items?limit=10    <---responds with the first 10 items

  /items?limit=10&start=11       <--responds with items 11 -> 20

  ```

  - Cursor (By providing an order, and an id for the last item that was received, the API figures out the next set of items the client should receive)

  ```JS
    /items?last_viewed_id=123
  ```

UI strategies how to show lost of data (1000 elements) on the page from the server resposne:

- A "load more" button
- "Next" & "Back" buttons
- Page number buttons
- Infinite scroll (new "pages" of the data are loaded when the user scrolls close to the buttom of hte list)

##### "Next" & "Back" buttons

- We need to track what is the current page state in --> useState. User must update the current page by clicking buttons, and we need to make sure that user can't request not existing pages by switching off particular buttons

```JS

<button
  onClick={() => {
    setPage((currentPage) => currentPage - 1);
  }}
  disabled={page === 0}                             //<-- if page === 0 , this button (Previous Page btn) will be disabled
>
  Previous Page
</button>
<button
  onClick={() => {
    setPage((currentPage) => currentPage + 1);
  }}
  disabled={PAGE_LENGTH * page >= totalCount}         //<-- The total number of pages can be calculated using the total count of items and the number of items per page. Because the total count could change, it would be good to keep this information in state and update it each time that more data is requested.
>
  Next Page
</button>
```

Whenever the current page is updated in state, it can trigger a new request for a specific page of data:

```JS
useEffect(() => {
  api.getItems({ page }).then(({ items, total_count }) => {
    setItems(items);
    setTotalCount(total_count);
  });
}, [page]);
```

##### Infinite Scroll

- There are many ways to implement an infinite scroll. Sometimes necessary to set up a scroll event listener on the window, or some other scrollable element. If this is the case, it will be important to ensure the event listener is removed from the page when the component is unmounted. To do so, ensure a "cleanup" function is returned from the useEffect that removes the event listener:

```JS
useEffect(() => {
  function handleScroll() {
    // check whether user is close to bottom of page
    // if so, fetch new "page" of data and add to state
  }

  window.addEventListener('scroll', handleScroll);

  return () => {
    window.removeEventListener('scroll', handleScroll);
  };
}, []);
```

# Styling in React

- When designing the style for our App we need to think about different screen sizes (phones,iPads, computers, laptops)
- it is easier to start your design for mobile screen and then chnage your app for bigger screens
- in CSS we can make a request for screen size and depending from the size components will be placed differently

```JS
@media only screen and (max-width:600px){   //screen size from 0 - 600px, or min-width:600px <-- from 600px and more
    body {
        background-color: lightblue;
    }
}
```

# Lifecycle methods

- In order to perform side effects such as api calls in class based components we have a number of lifecycle methods we can use
  - componentDidMount - Invoked immediately after the component is first rendered.
  - componentDidUpdate - Invoked whenever props change, setState or forceUpdate are called
  - componentWillUnmount - Invoked immediately before a component is removed from the page

```JS
//componentDidMount in Functional component

useEffect(() => {
  fetch('https://space-facts.herokuapp.com/api/planets')
    .then((res) => res.json())
    .then((data) => setPlanet(data.planets));
}, [])
```

```JS
//componentDidMount in Class component

import React from 'react';

class Planets extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      planets: [],
    };
  }

  componentDidMount() {
    fetch('https://space-facts.herokuapp.com/api/planets')
      .then((res) => res.json())
      .then((data) => {
        this.setState({ planets: data.planets });
      });
  }

  render() {
    return (
      <section>
        <h2>Planets</h2>
        <ul>
          {this.state.planets.map((planet) => {
            // render each planet...
          })}
        </ul>
      </section>
    );
  }
}
```

```JS
//componentDidUpdate in Functional component
//often have dependencies when the effect should be re-rander --> [planetId]
//by adding a variable to the dependency array React will compare the current value against the value on the previous render to see if they differ.

const SinglePlanetWithHooks = ({ planetId }) => {
  const [planet, setPlanet] = useState({});

  useEffect(() => {
    fetch(`https://space-facts.herokuapp.com/api/planets/${planetId}`)
      .then((res) => res.json())
      .then((data) => setPlanet(data.planet));
  // if planetId changes between renders, re-run the effect
  }, [planetId]);

  return (
    <section>
      <h2>{planet.name}</h2>
      {/* render the rest of the planet info */}
    </section>
  );
};
```

```JS
//componentDidUpdate in Class component
// to achieve similar effect in a class based component we can use the componentDidUpdate lifecycle method.This method is invoked with some arguments in order for us the make the comparison between renders.--> 0: the previous render's props, 1: the previous render's state

//We can then compare the two to see if the planetId has changed re-fetch the data if it has.

//Just as with useEffect dependencies be careful how this comparison is made to avoid infinite loops. Notable prevProps and prevState are objects and prevProps !== this.props will always be true as they are both objects and have different references.


class SinglePlanet extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      planet: {},
    };
    this.fetchPlanet = this.fetchPlanet.bind(this);
  }

  componentDidMount() {
    fetchPlanet();
  }

  componentDidUpdate(prevProps, prevState) {              //<-- 0: the previous render's props, 1: the previous render's state
    if (prevProps.planetId !== this.props.planetId) {
      fetchPlanet();
    }
  }

  fetchPlanet() {
    fetch(
      `https://space-facts.herokuapp.com/api/planets/${this.props.planetId}`
    )
      .then((res) => res.json())
      .then((data) => {
        this.setState({ planet: data.planet });
      });
  }

  render() {
    return (
      <section>
        <h2>{planet.name}</h2>
        {/* render the rest of the planet info */}
      </section>
    );
  }
}

```

# Semantic tags

- We should use semantic tags more often and avoid genearal tags --> such as div ,span
- semantic tags, helps to navigate through the web page nad improve SEO --> main, section, article, footer, header, aside, nav

# Accessibility

- our App or web page must be accesible and easy to use for majority of people, including disable people, people with visual impairments and with occasions with bad internet connections, dot't have all the accesories for computer.

### Why do we need to care about accessibility?

- to make sure everyone can access and enjoy a product we build. It's simply the right and most empathetic thing to do. Additionally ,there are legal obligations to have at least some basic accessibility in place.
- There are guidelines that we can follow to help write accessible code. these guidelines are called the Web Content Accessibility Guidelines (WCAG) --> also you can find it on MDN web-site.

#### The 4 principles ( The guidelines each belongs to one of 4 principles)

- Perceivable - users can ,are able to see/read/ hear content
- Operable - users are able to navigate to their own preferences
- Understandable - users are able to understand content and content behaves as expected
- Rebust - users can use variety of devices

#### Conformance level

There are 3 levels of accessability:

- A (essential) <-- it is a bare minimum. This means making sure that your site doesn't have clashing colours and all inputs have labels,for example.
- AA (ideal) <-- is a level most developers should aim for and is a level for the legal obligation
- AAA (specialized) <-- is usually when you are providing specialist support e.g your site could be explored with any keyboard device imaginable.

Helpful tools ( apps which help us to create accessibility)
It can be npm packages or browser extensions

- Lighthouse (Chrome browser extension)
- aXe (npm package and browser extension)
- Wave (browser extension)

#### Area ( Accessibility Rich Internet Application)

- using additional labels or linking content together to better show user what is going on a page
- use Area tags if you know what they are doing
- Area--> will read all the text from the web page

```JS
<button area-label="Like"> Heart </button>  //<-- extra label , which here says 'like' ,s the user knows what the button does
```

# React Custom Hooks

- Custom Hooks are one of the most powerful ideas in modern React.
  They let you extract reusable logic from components and share it cleanly.
- A custom hook in React is your own hook — a reusable function that starts with use and uses other React hooks to share logic between components.
- Custom Hook is a custom hook is simply a JavaScript function that: Uses React hooks (useState, useEffect, etc.), Starts with the word use, Encapsulates reusable logic. And It does NOT return JSX as React component.
- custom hook it is reusable functions that use hooks
- A custom hooks - are function, They reuse logic, not UI, They can use other hooks, They make code cleaner and reusable
- In React we have been using some in built hooks to add functionality to our components. React also supports writing our own custom hooks to extract component logic into re-useable functions.
- Custom hook is a function, whose name starts with "use" and can call other hooks within itself.
- When we are calling hooks from within our components we must follow the rules of hooks and we must do the same with our custom hooks.
- Custom hooks work exactly like components, the major difference being that they are not responsible for rendering data. Where our components will usually return some JSX (or potentially null) our custom hooks can return data in any form we would like.
- Custom Hooks Exist to Reuse logic, Making components cleaner, Improving readability & testability

```JS
                  Custom hook vs component

Custom Hook                |    Component

Reuses logic               |    Reuses UI
Returns values/functions   |    Returns JSX
Starts with use            |    Starts with capital letter
```

Custom Hook = reusable stateful logic extracted from components. It lets you reuse logic, not UI

### Why custom hooks exist?

Without custom hooks, you might repeat logic like:

- fetching data
- form handling
- window resize tracking
- authentication logic
- Custom hooks let you write that logic once and reuse it everywhere.

### When to create a custom hook

Create one when:

logic is reused in multiple components
a component becomes too large
you want to separate logic from UI

### With a custom hook

```JS
function useCounter(initialValue = 0) {
  const [count, setCount] = React.useState(initialValue);
  const increment = () => setCount(c => c + 1);

  return { count, increment };
}

//Usage:
function Counter() {
  const { count, increment } = useCounter(0);

  return <button onClick={increment}>{count}</button>;
}

//🔹 Key rules of custom hooks
//✅ Must start with use
//✅ Can use other hooks in your custom hook->
//useState,useEffect,useContext,useRef,useReducer, etc
```

```JS
//Real-world example: useFetch  <-- custom hook

function useFetch(url) {
  const [data, setData] = React.useState(null);
  const [loading, setLoading] = React.useState(true);

  React.useEffect(() => {
    fetch(url)
      .then(res => res.json())
      .then(data => {
        setData(data);
        setLoading(false);
      });
  }, [url]);

  return { data, loading };
}

//Usage:
function Users() {
  const { data, loading } = useFetch("/api/users");

  if (loading) return "Loading...";
  return <pre>{JSON.stringify(data)}</pre>;
}
```

```JS
//🧱 Basic Custom Hook Example
//useCounter.js
import { useState } from "react";

function useCounter(initialValue = 0) {   //<- custom React hook called useCounter
  const [count, setCount] = useState(initialValue);

  const increment = () => setCount(c => c + 1);   //<- this hook has a increment function
  const decrement = () => setCount(c => c - 1);   //<- this hook has a decrement function
  const reset = () => setCount(initialValue);    //<-- this hook has a reset function

  return { count, increment, decrement, reset };
}

export default useCounter;

///////////////////////////////////////////////////////////////////
//Import useState
//Create the custom hook

function useCounter(initialValue = 0) {

//This defines a function named useCounter
//initialValue = 0 means:
//If you don’t give a starting number, it starts at 0
//Example:
useCounter()     // starts at 0
useCounter(10)   // starts at 10


// Create state for the counter
const [count, setCount] = useState(initialValue);

//count → the current number
//setCount → function to change the number
//React remembers count between renders

//Increase the counter
const increment = () => setCount(c => c + 1);

//This increases the number by 1
//c is the current value
//Using c => c + 1 is safe and always correct
//In simple words:
//“Take the current number and add 1”


//Decrease the counter
const decrement = () => setCount(c => c - 1);

//This decreases the number by 1
//In simple words:
//“Take the current number and subtract 1”


//Reset the counter
const reset = () => setCount(initialValue);
//This sets the number back to the starting value
//Example:
useCounter(5) → reset() → count becomes 5


//Return everything
return { count, increment, decrement, reset };
//This gives other components access to:
//the number (count)
//the functions to change it

////////////////////////////////////////////////////////////////////
//Usage

function Counter() {
  const { count, increment, decrement } = useCounter(10);  //<-- use custom hook, destructuring functions from it

  return (
    <>
      <p>{count}</p>
      <button onClick={increment}>+</button>
      <button onClick={decrement}>-</button>
    </>
  );
}
```

```JS
//📦 Custom Hook for API Calls (Very Common)
//useFetch.js

import { useState, useEffect } from "react";

function useFetch(url) {   //<-- create custom hook - useFetch
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    let isMounted = true;

    fetch(url)
      .then(res => res.json())
      .then(data => {
        if (isMounted) {
          setData(data);
          setLoading(false);
        }
      })
      .catch(err => {
        if (isMounted) {
          setError(err);
          setLoading(false);
        }
      });

    return () => {
      isMounted = false;
    };
  }, [url]);

  return { data, loading, error };  //returning states from custom hook
}

export default useFetch;

//then you can import this hook to your component
```

# React Fragment

- React fragments (<></> or <React.Fragment></React.Fragment>) are super useful.
- React components must return a single parent element. If you don't want to add extra <div> nodes to the DOM (which can mess up CSS styling or accessibility), you use Fragments to group children.
- This can mess with CSS styling or layout (especially with flex/grid).

```JS
function MyComponent() {

  return (
    <>
      <h1>Hello</h1>
      <p>Welcome to my app</p>
    </>
  );
}
```

# React.js + Bootstrap

- Install Bootstrap via npm (best for React) in your React project root.

```JS
npm install bootstrap
```

- Import Bootstrap CSS in your project. Open src/index.js or src/main.jsx (depending on your setup) and add:

```JS
import 'bootstrap/dist/css/bootstrap.min.css';
```

bootstrap minified version - for Production:

- ✅ Loads faster
- ✅ Smaller bundle size
- ✅ Better performance
- ✅ Standard for production apps

Non-minified: for -> Learning , debugging, development

```JS
bootstrap/dist/js/bootstrap.bundle.js
```

This version:

- ✅Is readable
- ✅Has comments
- ✅Is easier to debug

```JS
(Optional) Import Bootstrap JS (for modals, dropdowns, tooltips)
If you need Bootstrap’s JavaScript features:
npm install @popperjs/core
Then in index.js or main.jsx:
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
```

⚠️ Remember: use className, not class.

```JS
//Alternative: Use CDN (quick but not ideal)
//Good for small demos, not recommended for production.
//Add this to public/index.html:

<link
  href=https://cdn.jsdelivr.net/npm/bootstrap/dist/css/bootstrap.min.css
  rel="stylesheet"
/>

<script src="https://cdn.jsdelivr.net/npm/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
```

# useMemo hook

- useMemo remembers the result of a calculation and only recalculates it when its dependencies change.
- usually is used for some calculations - sorting and filtering, It is fast and No fetch
- Works similara to useEffect - render the page when state are changing in dependency array.
  Is used in Home.jsx compoent - line 53
- the page does re-render when state are changing in dependency array, but it does NOT refetch, and only the necessary work runs
- useMemo hook cause page re-renders when sorting or filtering changes (states in useMemo -> in dependency array)

# useRef

- useRef is a Hook that lets you persist a mutable value across renders without causing a re-render, and it’s commonly used to reference DOM elements.
- useRef creates a mutable object whose .current value persists between renders without triggering a re-render when it changes.
- useRef is a React Hook that provides a persistent, mutable reference that survives re-renders and can be used to access DOM elements or store values without triggering a re-render.

```JS
//Basic syntax
const ref = useRef(initialValue);

//Returns an object: { current: initialValue }
//Updating ref.current does not re-render the component
```

```JS
//Common use cases
//1️⃣ Accessing DOM elements (most common)
import { useRef } from "react";

function InputFocus() {
  const inputRef = useRef(null);

  const focusInput = () => {
    inputRef.current.focus();
  };

  return (
    <>
      <input ref={inputRef} />
      <button onClick={focusInput}>Focus</button>
    </>
  );
}

//👉 Here, useRef lets you directly interact with the DOM.
```

```JS
//2️⃣ Storing a value that doesn’t trigger re-renders
function Timer() {
  const countRef = useRef(0);

  const increment = () => {
    countRef.current += 1;
    console.log(countRef.current);
  };

  return <button onClick={increment}>Increment</button>;
}

// Value persists across renders
// UI doesn’t update when it changes
```

```JS
//3️⃣ Keeping previous values
function Example({ value }) {
  const prevValue = useRef(value);

  useEffect(() => {
    prevValue.current = value;
  }, [value]);

  return <p>Previous: {prevValue.current}</p>;
}
```

```JS
                              useRef vs useState

Feature                    useRef    |   useState

Triggers re-render         ❌ No     |  ✅ Yes
Mutable                    ✅ Yes    |  ❌ No
Persists across renders    ✅ Yes    |  ✅ Yes
Use for UI updates         ❌ No     |  ✅ Yes
```

Important rules

    useRef value is stable between renders
    Changes to .current do not cause re-renders
    Can be used in function components only

# Old code

```JS
//filter
  if (value.wineType === "Red") {
    value.wineList = value.wineList.filter((element) => {
      return element.wine === "Red";
    });
  }


 if (value.SortBy === "year" && value.order === "asc") {
    value.wineList.sort((a, b) => {
      return Number(a.year) > Number(b.year) ? -1 : 1;
    });
  }
```

```JS
//NavBar
{items.map((item) => {
          return item === "home" ? (
            <li key={item} className="NavBar">
              <NavLink
                to="/Wine-Shop"
                className="NavBar-link"
                onClick={handleHome}
              >
                {" "}
                {item}
              </NavLink>
            </li>
          ) : item === "requests" ? (
            <li key={item} className="NavBar">
              <NavLink to="requests" className="NavBar-link">
                {item}
              </NavLink>
            </li>
          ) : (
            <li key={item} className="NavBar">
              <NavLink to="basket" className="NavBar-link">
                {item}
              </NavLink>
            </li>
          );
        })}
```

```JS
const allComments = [
  {user: "John", region: "London", comments: "adadada"},
  {user: "Mike", region: "Stockport", comments: "dadada"},
  {user: "Adam", region: "Manchester", comments: "ytyty"}
]

//destructurisation of the items
allComments.map(({ user, region, comments }, index) => (
    {user}
    {region}
    {comments}
  }

```
