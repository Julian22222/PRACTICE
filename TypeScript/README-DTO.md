# DTO with TypeScript

The choice between classes, interfaces, or types for DTOs in TypeScript often depends on your project style, complexity, and needs.

1. Interfaces or Types for DTOs (Most Common)

Why?

- DTOs are typically simple data shapes without behavior.
- Interfaces and types are lightweight and great for defining data contracts.
- They don’t add runtime JavaScript code (only compile-time types).

Used for:

- API request/response typing
- Data validation layers (often paired with libraries like class-validator or zod)

2. Classes for DTOs (Less Common but Useful)

Why?

- If you want to add validation, transformation, or methods on DTOs.
- If you use libraries like class-transformer or class-validator, which rely on class decorators and runtime metadata.
- When you want DTOs to exist at runtime (interfaces/types are erased after compilation).

Used for:

- Server-side validation in frameworks like NestJS
- Automatic serialization/deserialization

```JS
//Example with types/ Interfaces
//Use types, or similar syntax with Interfaces

type Item2 ={
  Name: string,
  Age: number,
  Address: string
}

type Item3 ={
  Name: string,
  Age: number
}

const GetDto =(item: Item2): Item3=>{  // Item3 returning data type
return {
  Name : item.Name,
  Age : item.Age
}
}

//OR

function GetDto(item: Item2): Item3 {
  return {
    Name: item.Name,
    Age: item.Age
  };
}


const per: Item2 = {Name:"Andy", Age: 26, Address: "kikolo"}
const test22 = GetDto(per);
console.log(test22) // { "Name": "Andy", "Age": 26 }
```

```JS
//Example with Classes
//Use Classes

//1. Define your full data class (like Item2):

class Person {
  Name: string;
  Age: number;
  Address: string;

  constructor(name: string, age: number, address: string) {
    this.Name = name;
    this.Age = age;
    this.Address = address;
  }


  // Method to get DTO
  toDTO(): PersonDTO {
    return new PersonDTO(this.Name, this.Age);
  }

}


//2. Define your DTO class (Item3 equivalent):

class PersonDTO {
  Name: string;
  Age: number;

  constructor(name: string, age: number) {
    this.Name = name;
    this.Age = age;
  }
}

const person = new Person("Andy", 26, "kikolo");

// Get DTO (only Name and Age)
const dto = person.toDTO();

console.log(dto);  // Output: PersonDTO { Name: 'Andy', Age: 26 }
```
