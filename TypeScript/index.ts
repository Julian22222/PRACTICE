let numberValue: number = 42;

console.log(numberValue);

let str: string = "Hello, TypeScript!";
console.log(str);

let isActive: boolean = true;
console.log(isActive);

let arr: number[] = [1, 2, 3, 4, 5];
console.log(arr);

let tuple: [number, string] = [1, "TypeScript"];
console.log(tuple);

let anyValue: any = "This can be anything";
console.log(anyValue);

function greet(name: string): string {
  return `Hello, ${name}!`;
}
console.log(greet("World"));

interface Person {
  name: string;
  age: number;
}
function displayPerson(person: Person): void {
  console.log(`Name: ${person.name}, Age: ${person.age}`);
}

let person: Person = { name: "Alice", age: 30 };
displayPerson(person);

enum Color {
  Red,
  Green,
  Blue,
}
let myColor: Color = Color.Green;
console.log(myColor); // Output: 1 (index of Green in the enum)

class Animal {
  constructor(public name: string) {}
  speak(): void {
    console.log(`${this.name} makes a noise.`);
  }
}
