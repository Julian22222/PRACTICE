# Modify Objects

```JS
//We add another property to the object

const ninja = {
name: "Naruto",
rank: "Genin",
weapon: "stars"
}

const newNinja = {
...ninja,  //make a copy of the object data
id: ninjas.array.length +1   //<-- added field id to the object
}
```

```JS
//Example 1
//Change one key in all the objects- Example from MySQL-database/app.js project

const formattedRows = rows.map((row) => ({
//converting database -serviceCheck property from 1/0 to true/false
//MySQL, SQLite, SQL Server, Oracle stores boolean values as 1 (true) and 0 (false) under the hood.
...row,
serviceCheck: Boolean(row.serviceCheck), // Convert 1/0 to true/false
}));
```

```JS
//Example 2
//If you want to remove some key from the object/ objects

const myArr = [{name:"John", age:22, password:"33333"},{name:"Tom", age:28, password:"34555"} ]

//here for example i want to remove password key from each object in the array

//You can use object destructuring to exclude the password key like this:
const modifiedArr = myArr.map(({ password, ...rest }) => rest);

//Explanation:
// { password, ...rest } destructures the object, taking out the password key and collecting the remaining properties in rest.
// rest becomes the new object without the password.

//Result:
[
  { name: "John", age: 22 },
  { name: "Tom", age: 28 }
]
```

```JS
//Example 2 - //If you want to remove some key from the object/ objects
//From  PRACTICE/Next_and_NestJS/my-app-next/src/config/auth.ts

const currentUser = users.find((user) => user.email === credentials.email); //if user exists return the user object, finding user with matching email

const { password, ...userWithoutPassword } = currentUser; //destructuring to exclude password from the user object
return userWithoutPassword as User; // ensure id is a string //returning a user object without password,
```
