# Generics

Generics in TypeScript are a powerful feature that help you write reusable, flexible, and type-safe code. Let me break down what generics are, why you need them, and how/where to use them.

# What are Generics?

- Generics allow you to define functions, classes, or interfaces that work with any data type, while still maintaining strong type safety. Instead of specifying concrete types upfront, you specify a placeholder type that gets replaced with a specific type when the code is used.

# Why Use Generics?

1. Reusability: Write a function or class once, and use it with different types without rewriting code.
2. Type Safety: Maintain strict typing while keeping flexibility.
3. Avoid any: Using any defeats TypeScript’s purpose. Generics keep the benefits of static typing.
4. Better API Design: Users of your functions/classes get accurate types inferred, reducing bugs and improving developer experience.

# Where and How to Use Generics

1. Generic Functions

If you have a function that works the same way for different types, use generics.

```JS
// Example: Identity function that returns what it receives

function identity<T>(arg: T): T {
  return arg;
}

// Usage

const num = identity<number>(42);     // num is number
const str = identity<string>('hello'); // str is string
//Why: Without generics, you'd have to write multiple functions or use any and lose type safety.
```

2. Generic Interfaces

Useful when defining data structures or APIs that work with multiple types.

```JS
interface Box<T> {
  contents: T;
}

const box1: Box<string> = { contents: 'hello' };
const box2: Box<number> = { contents: 123 };
```

3. Generic Classes

When you build data structures or utilities that operate on various types.

```JS
class Stack<T> {
  private items: T[] = [];

  push(item: T) {
    this.items.push(item);
  }

  pop(): T | undefined {
    return this.items.pop();
  }
}


const numberStack = new Stack<number>();
numberStack.push(10);
const popped = numberStack.pop();  // TypeScript knows popped is number | undefined
```

4. Generic Constraints

You can restrict generics to certain types, e.g., only types with specific properties.

```JS
interface Lengthwise {
  length: number;
}

function logLength<T extends Lengthwise>(arg: T): T {
  console.log(arg.length);
  return arg;
}

logLength('hello');   // Works, string has length
logLength([1, 2, 3]); // Works, arrays have length
// logLength(123);    // Error, number has no length property
```

# Summary

```JS
       Use Case                                   |            Example                   |          Why Use Generics?
                                                  |                                      |
Functions that work with any type                 |   function identity<T>(arg: T): T    |   Reusability & type safety
Interfaces representing generic data structures   |   interface Box<T>                   |   Flexibility in data types
Classes implementing generic containers           |   class Stack<T>                     |   Reusable data structures with type safety
When you want to constrain types                  |    <T extends SomeType>              |   Restrict generics to compatible types
```

- Use generics when your code works with different types but shares the same logic.
- They help keep your code DRY, safe, and maintainable.
- Avoid any by preferring generics for flexibility with type safety.

# Generic functions

```JS
const identity = <T>(arg: T): T => {
  return arg;
};

//or

const identity = <T>(arg: T): T => arg;

// function keyword starts the declaration.
// identity is the function name.
// <T> declares a generic type parameter T.
// (arg: T) means the function accepts an argument of type T.
// : T after the parentheses means the function returns a value of type T.


function func<T>(item:T){
return item
}

const mi = func(34)
const mu = func("hey")
console.log(mi)
//console.log(mu)
```

Generic classes in TypeScript that show how generics make your classes flexible and type-safe.

1. Simple Generic Stack Class

A stack data structure that can hold elements of any type:

```JS
class Stack<T> {
  private items: T[] = [];

  push(item: T) {
    this.items.push(item);
  }

  pop(): T | undefined {
    return this.items.pop();
  }

  peek(): T | undefined {
    return this.items[this.items.length - 1];
  }

  size(): number {
    return this.items.length;
  }
}

// Usage:
const numberStack = new Stack<number>();
numberStack.push(10);
numberStack.push(20);
console.log(numberStack.pop()); // 20


const stringStack = new Stack<string>();
stringStack.push('hello');
console.log(stringStack.peek()); // 'hello'
```

2. Generic Key-Value Pair Class

Stores a key and value of potentially different types:

```JS
class KeyValuePair<K, V> {
  constructor(public key: K, public value: V) {}

  display(): void {
    console.log(`Key: ${this.key}, Value: ${this.value}`);
  }
}


// Usage:
const pair1 = new KeyValuePair<number, string>(1, "One");
pair1.display(); // Key: 1, Value: One

const pair2 = new KeyValuePair<string, boolean>("isActive", true);
pair2.display(); // Key: isActive, Value: true
```

3. Generic Repository Class (for data handling)

A class simulating a repository that works with any type of entity:

```JS
class Repository<T> {
  private items: T[] = [];

  add(item: T): void {
    this.items.push(item);
  }

  getAll(): T[] {
    return this.items;
  }
}


// Usage:
interface User {
  id: number;
  name: string;
}


const userRepository = new Repository<User>();
userRepository.add({ id: 1, name: "Alice" });
console.log(userRepository.getAll());
```

# Why Use Generic Classes?

- They let you create reusable data structures or utility classes.
- You get full type safety for stored/manipulated data.
- They prevent code duplication for different types.
