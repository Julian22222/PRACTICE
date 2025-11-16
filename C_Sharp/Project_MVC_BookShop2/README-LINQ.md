C# doesn’t have a built-in map() method on collections. Instead, it uses LINQ (Language Integrated Query), and the equivalent method is "Select"

✅ C# does not have built-in map(), filter(), or find() etc. methods like JavaScript arrays do, instead C# has LINQ methods.

✅ It uses LINQ methods: Select(), Where(), and FirstOrDefault() — which serve the same purpose.

- C# provides LINQ (Language Integrated Query), which offers equivalent functionality through extension methods on collections such as IEnumerable<T> and List<T>.

Let’s break it down clearly

```JS
// Comparison: JavaScript vs. C#

  JavaScript        |     C# (LINQ)               |   Description
                    |                             |
  filter()          |   Where()                   |   Returns all elements that match a condition
  	find()          |  First() / FirstOrDefault() |   Returns one element that matches a condition
    map()           |   Select()                  |   Transforms each element in a sequence.
   some()           |   Any()                     |   Check any match, return bool
  every()           |   All()                     |   Check all match, return bool
  reduce(fn, init)  |   Aggregate(init, fn)       |   Reduce / accumulate, return single value

  // visual “cheat sheet” of JS vs. C# LINQ method mappings, other methods can be searched
```

```C#
//JavaScript — filter
const nums = [1, 2, 3, 4, 5];
const even = nums.filter(n => n % 2 === 0);
console.log(even); // [2, 4]

//C# — Where
var nums = new List<int> { 1, 2, 3, 4, 5 };
var even = nums.Where(n => n % 2 == 0);
Console.WriteLine(string.Join(", ", even)); // 2, 4

//So ✅ filter() in JS ≈ Where() in C#.
```

```C#
//JavaScript — find
const nums = [1, 2, 3, 4, 5];
const firstEven = nums.find(n => n % 2 === 0);
console.log(firstEven); // 2

//C# — FirstOrDefault
var nums = new List<int> { 1, 2, 3, 4, 5 };
var firstEven = nums.FirstOrDefault(n => n % 2 == 0);
Console.WriteLine(firstEven); // 2

//✅ find() in JS ≈ FirstOrDefault() (or First()) in C#.
```

```C#
//💡 Example Comparison

/////////////JavaScript
const nums = [1, 2, 3, 4, 5];

// map
const doubled = nums.map(n => n * 2);

// filter
const evens = nums.filter(n => n % 2 === 0);

// find
const firstEven = nums.find(n => n % 2 === 0);



//////////////C#
var nums = new List<int> { 1, 2, 3, 4, 5 };

// map → Select
var doubled = nums.Select(n => n * 2);

// filter → Where
var evens = nums.Where(n => n % 2 == 0);

// find → FirstOrDefault
var firstEven = nums.FirstOrDefault(n => n % 2 == 0);
```

# LINQ stands for Language Integrated Query

It’s a powerful feature in C# that allows you to write queries directly in your code to work with collections of data like arrays, lists, XML, databases, and more — all in a very readable and concise way.

What LINQ does:

Lets you filter, sort, group, and transform data collections easily.
Makes querying data feel like writing SQL but directly inside your C# code.
Works with any data source that implements IEnumerable<T> or IQueryable<T>.

```C#
//Basic example of LINQ:
//Imagine you have a list of numbers, and you want only the even numbers:

using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    static void Main(){

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        // LINQ query to get even numbers
        var evenNumbers = numbers.Where(n => n % 2 == 0);

        foreach (var num in evenNumbers){
            Console.WriteLine(num);
        }
    }
}

//Output =  2,4,6


//Key LINQ methods:
-  .Where() — filter elements  .Select() — transform elements
-  .OrderBy(), .OrderByDescending() — sort elements
-  .First(), .FirstOrDefault() — get first element(s)
-  .ToList() — convert query result to a list
```
