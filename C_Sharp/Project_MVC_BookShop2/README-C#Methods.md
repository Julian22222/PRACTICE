# C# Methods

- Instance methods: Methods you call on an instance (object) of a class.
  Example: myObject.ToString(), myList.Add(), myString.Substring()

- Static methods: Methods you call on the class itself, not on an instance.
  Example: Math.Sqrt(9), Console.WriteLine(), string.Join(...), Math.Abs(...)

- Extension methods: Special static methods that look like instance methods and can be called on existing types — like LINQ methods .Where(), .Select(), .ToList().
  These methods extend the capabilities of existing types without modifying their source code.

# Examples of common instance methods in C#:

```C#
Method         |                        Description                   |   Class/Type

.ToString()    |   Converts the object to its string representation   |   object (base class)
.Equals()      |   Compares two objects for equality                  |   object
.GetHashCode() |   Gets a hash code for the object                    |   object
.Add()         |   Adds an element to a collection                    |   e.g., List<T>
.Remove()      |   Removes an element from a collection               |   e.g., List<T>
.Substring()   |   Extracts part of a string                          |   string
.Trim()        |   Removes whitespace from the start/end of a string  |   string
.Count()       |   Gets the number of elements in a collection        |   LINQ extension method
```

# Often used methods for common C# data types like string, List<T>, int, and others.

1. String Methods

```C#
         Method                        |                  Description                     |   Example

.ToUpper()                             |  Converts string to uppercase                    |   "hello".ToUpper() → "HELLO"
.ToLower()                             |  Converts string to lowercase                    |   "HELLO".ToLower() → "hello"
.Substring(int startIndex, int length) |  Extracts part of the string                     |   "hello".Substring(1, 3) → "ell"
.Trim()                                |  Removes whitespace from start and end           |   " hello ".Trim() → "hello"
.Contains(string)                      |  Checks if string contains substring             |   "hello".Contains("ll") → true
.Replace(string old, string new)       |  Replaces substring with another                 |   "hello".Replace("l", "x") → "hexxo"
.Split(char[])                         |  Splits string into an array based on separators |   "a,b,c".Split(',') → ["a", "b", "c"]
.StartsWith(string)                    |  Checks if string starts with substring          |   "hello".StartsWith("he") → true
.EndsWith(string)                      |  Checks if string ends with substring            |   "hello".EndsWith("lo") → true
```

2. List<T> Methods (Generic List)

```C#
         Method             |                Description                              |   Example

.Add(T item)                |  Adds an element to the list                            |   myList.Add(5)
.Remove(T item)             |  Removes first occurrence of an element                 |   myList.Remove(5)
.RemoveAt(int index)        |  Removes element at specified index                     |   myList.RemoveAt(0)
.Count                      |  Gets number of elements (property, not method)         |   int count = myList.Count;
.Clear()                    |  Removes all elements                                   |   myList.Clear()
.Contains(T item)           |  Checks if element exists in the list                   |   myList.Contains(5) → true or false
.Insert(int index, T item)  |  Inserts element at given index                         |   myList.Insert(0, 10)
.IndexOf(T item)            |  Returns index of first occurrence, or -1 if not found  |   myList.IndexOf(5)
.Sort()                     |  Sorts the list in ascending order                      |   myList.Sort()
```

3. int (and other numeric types)

```C#
    Method/Operation      |                Description                      |   Example

.ToString()               |  Converts number to string                      |   123.ToString() → "123"
Parse(string)             |  Converts string to int                         |   int.Parse("123") → 123
TryParse(string, out int) |  Tries to convert string, returns bool success  |   int.TryParse("123", out var x)
Arithmetic operators      |  +, -, *, /, % for math operations              |   5 + 3, 10 / 2
Math.Abs(int)             |  Returns absolute value                         |   mMath.Abs(-5) → 5
Math.Max(int, int)        |  Returns larger of two                          |   Math.Max(3, 7) → 7
Math.Min(int, int)        |  Returns smaller of two                         |   Math.Min(3, 7) → 3
Math.Pow(double, double)  |  Power function (returns double)                |   Math.Pow(2, 3) → 8
Math.Round(double)        |  Rounds double to nearest integer               |   Math.Round(2.7) → 3
```

4. Other common types

DateTime (dates and times)

```C#
    Method                   |                Description               |   Example

DateTime.Now                 |  Current date and time                   |   DateTime now = DateTime.Now;
.AddDays(double)             |  Adds days to date                       |   now.AddDays(5)
.ToString(string format)     |  Converts date/time to formatted string  |   now.ToString("yyyy-MM-dd")
.Day, .Month, .Year          |  Gets components of the date             |   now.Day → day of month
.Compare(DateTime, DateTime) |  Compares two dates                      |   DateTime.Compare(d1, d2)
```

# Common and Useful Static Methods in C#

1. string class static methods

```C#
    Method         |                Description                                            |   Example

string.Join()     |  Concatenates elements of a collection into a string with a separator  |   string.Join(", ", new[] { "a", "b" }) → "a, b"
string.Format()   |  Formats strings with placeholders                                     |   string.Format("Name: {0}", "Alice") → "Name: Alice"
string.Concat()   |  Concatenates multiple strings                                         |   string.Concat("Hello", " ", "World") → "Hello World"
```

2. Math class

```C#
    Method      |                Description                       |   Example

Math.Abs()      |  Returns absolute value                          |   Math.Abs(-5) → 5
Math.Max()      |  Returns max of two values                       |   Math.Max(3, 7) → 7
Math.Min()      |  Returns min of two values                       |   Math.Min(3, 7) → 3
Math.Pow()      |  Returns base raised to exponent power           |   Math.Pow(2, 3) → 8
Math.Round()    |  Rounds to nearest integer                       |   Math.Round(2.7) → 3
Math.Sqrt()     |  Square root                                     |   Math.Sqrt(9) → 3
Math.Ceiling()  |  Smallest integer greater than or equal to value |   Math.Ceiling(2.3) → 3
Math.Floor()    |  Largest integer less than or equal to value     |   Math.Floor(2.7) → 2
```

3. Convert class

```C#
    Method           |                Description          |   Example

Convert.ToInt32()    |  Converts various types to int      |   Convert.ToInt32("123") → 123
Convert.ToString()   |  Converts various types to string   |   Convert.ToString(123) → "123"
Convert.ToDouble()   |  Converts to double                 |   Convert.ToDouble("12.34") → 12.34
Convert.ToBoolean()  |  Converts to bool                   |   Convert.ToBoolean("true") → true
```

4. DateTime class

```C#
    Method           |                Description               |   Example

DateTime.Now         |  Current date and time (property)        |   DateTime now = DateTime.Now;
DateTime.Today       |  Current date with time set to midnight  |   DateTime today = DateTime.Today;
DateTime.Parse()     |  Parses string to DateTime               |   DateTime.Parse("2025-07-09")
DateTime.TryParse()  |  Tries parsing string to DateTime safely |   DateTime.TryParse("invalid", out var dt)
```

5. Enumerable class (LINQ static methods)

```C#
    Method           |                Description              |   Example

Enumerable.Where()   |  Filters a collection by condition      |   myList.Where(x => x > 5)
Enumerable.Select()  |  Projects each element of a collection  |   myList.Select(x => x.ToString())
Enumerable.Join()    |  Joins two collections on keys          |   myList.Join(otherList, a => a.Id, b => b.UserId, (a, b) => new { a, b })
Enumerable.OrderBy() |  Sorts collection ascending             |   myList.OrderBy(x => x)
Enumerable.GroupBy() |  Groups elements by a key               |   myList.GroupBy(x => x.Category)
Enumerable.Sum()     |  Sums numeric elements                  |   myList.Sum(x => x.Amount)
```

# Summary:

- Static methods belong to the class itself, not instances.
- Useful for utility functions (Math, Convert, string formatting).
- LINQ methods are static methods in the Enumerable class but called like instance methods thanks to extension methods.

Enumerable.Join --> Joins two collections based on matching keys (like SQL JOIN) --> Match students to their scores
