[Microsoft ASP.NET Notes](https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-8.0)

# Static access Modificator

- if we use --> "Static" in filed or method that means thta method or field will belong to Class in general but not to its object.
- We can access the field or method through the Class without creating an object from that Class.
- Static classes are used as containers for static members like methods, constructors and others.

Check examples on my React_Asp.Net_WebAPi project.
Mappers file has static classes.
These Static classes are used in Controllers to change some objects to DTO objects

[-->One of the Mapper file Here<--](https://github.com/Julian22222/React_Asp.Net_WebAPI/blob/main/api/Mappers/ItemMappers.cs)

[-->One of he Controller file Here<--](https://github.com/Julian22222/React_Asp.Net_WebAPI/blob/main/api/Controllers/ItemController.cs)

```C#
//Example

public static class MyCollege{   //MyCollege is static container for other static class fileds, methods and constructors

//static fields
public static string collegeName;
public static string address;

//static constructor
static MyCollege(){
    collegeName = "ABC College";
}

//static method
public static void CollegeBranch(){
    Console.WriteLine("Computers");
}

//static method
public static void CollegeTime(){
    Console.WriteLine("Morning time");
}


}
```

```C#
static void Main(){

int Num = 7;

    Console.WriteLine("Hello");
    Console.WriteLine($"My number is {Num}");
}
```

```C#
static void Main(){

//static method, function MyArray
static int[] MyArray (int arr, int index, int value){
//some code..
}

}
```

```C#
class User {

public static int Identifications {get; set;}
public int Identification {get; set;}
}



//from other file we can interact with User field-->
User.Identifications = 10; //interact with this field through the class without creating am object from this User class
```

# Arrays

- We can't add new elements to arrays (single-dimensional arrays and two-dimensional arrays)

```C#
//different syntax of array use

int [] cucumbers = new int [5];  //<-- we can indicate the length of the array
int [] array = new int [3]{3,6,8};  //<-- indicate length of he array and numbers in the array
int [] numbers = {0,1,2,3,4,5,6};  //<-- put numbers in the array

int [,] array2 = new int [2,3];   //<-- two-dimensional arrays, has 2 lines and 3 columns
int [,] numbers2 = {{2,3,5},{4,6,8}};   //<-- two-dimensional arrays, has 2 lines and 3 columns
int [,] array2 = new int [2,3]{{2,3,5},{4,6,8}};  //<--two-dimensional arrays, has 2 lines and 3 columns

```

# Collections (Dynamic arrays)

- Programming object which contains the same or different data type elements.
- Collections allow to refere to these elements, add new elements and extract the elements from the collection

## List Collection ( Collection First type array)

- it is very similar to usual array
- more often is used in C#

```C#
List <int> numbers = new List <int>();  //<--creating empty List, which can have only numbers (data type)--> <int>
List <int> numbers = new List <int>(3);  //<-- can indicate the length of the List, empty List
List <int> numbers = new List <int>(){2,5,7,9};  //<--adding elements to the List

//Also there are convinient functions to work with List
numbers.Add(12);   //<-- will add 12 to our List, will add number to the end of our List
numbers.AddRange(new int[]{4,6,8,43});   //<-- add few element to the List
numbers.RemoveAt(3);  //<--will remove element with index 3
numbers.Sort();   //<--will sort numbers or strrings by ascendent order
numbers.Reverse();  //<--will turn around all List
numbers.Remove(6);  //<-- will remove number 6
numbers.Clear();  //<-- will clear all the List, will make it empty
numbers.IndexOf(43);  //<--will give index of number 43
numbers.Insert(1,37);  //<-- 1 -is index , 37 - is number, we are inserting number 37 into index 1, all others elements will be shifted to other indexes
//also contain other functions ...
```

## Queue (Collection Second type array)

- use FIFO algorithm, first in firs out

```C#
Queue <string> patients = new Queue <string>();
patients.Enqueue("Alex");  //<-- add Alex to the Queue
patients.Enqueue("Mark");
patients.Enqueue("Kevin");
patients.Dequeue();  //<-- will give an ellemnt and removes first element from the Queue (Alex)
patients.Peek();  //<-- will show first element in the Queue, we can check who is next in the queue
```

## Stack (Collection Third type array)

- use LIFO algorithm, last in firs out

```C#
Stack <int> numbers = new Stack <int>();
numbers.Push(1);  //<--adding an ellemnt to the Stack
numbers.Push(5);

numbers.Peek();  //<--will show first element in the Stack, we can check who is next in the queue -->(5)
numbers.Pop();  //<-- removes first element from the Stack
```

## Dictionary (Collection Fourth type array)

- similar to objects in JavaScript
- more often is used in C#
- contains keys and values

```C#
Dictionary <string,string> countriesCapitals = new Dictionary <string,string>();  //<-- empty Dictionary

countriesCapitals.Add("Australia","Canberra");  //<-- adding elements to our Dictionary, key = Australia, value = Canberra
countriesCapitals.Add("Turkey","Ankara");
countriesCapitals.Add("England","London");
Console.WriteLine(countriesCapitals["Australia"]);   //<-- will give Canberra

if(countriesCapitals.ContainsKey("Australia")){   //<-- if Dictionary contains a key = Australia, will show its value
   Console.WriteLine(countriesCapitals["Australia"]);
}

foreach(var item in countriesCapitals){
    Console.WriteLine($"Country - {item.Key}, Capital - {item.Value}");   //<-- will show keys and values of all elements
}


foreach(var key in countriesCapitals.Keys){
    Console.WriteLine($"Country - {key}");   //<-- will show only keys of all elements
}

foreach(var value in countriesCapitals.Values){
    Console.WriteLine($"Country - {value}");   //<-- will show only values of all elements
}

countriesCapitals.Remove("Turkey");  //<-- will delete element Turkey from Dictionary


//also can use different data types
Dictionary <int,string> countriesCapitals = new Dictionary <int,string>();
countriesCapitals.Add(1,"Canberra");
Console.WriteLine(countriesCapitals[1]);  //<-- will show Canberra

```

# Razor Syntax -standards how to use C# code in View file

- Everithing starts with --> @ for C# code in View file.
- We can have Single Line Syntax:

```C#
@expressin

@DateTime.Now

@DateTime.Now.ToString("dd-M-yy")

@YourC#Variable

//also we can make addition of two numbers, or add two strings together
@(3 + 5)  //<--will give us 8
```

- or we can use MultiLine Syntax:

```C#
@{
    //your code goes here
    int a = 10;
    int b = 20;
    int c = a + b;
}
```

-How to write Email on View:

```bash
username@domain.com  //<--automatically understands when we use C# code and when email
```

- Escape sequence: (use twite tag for example in View)

```C#
@@twiterAccount
```

# Conditional Statements(If, Else ,Else If , Ternary operator, Switch etc.)

```C#
@if(true){
    //some code
    <h1>Hello from IF Block</h1>
}
```

```C#
@{
    int a =12;
}

@if(a > 5){
    a++;  //<--C# code. but in If block we don't need to use @ because we are still in C# context, here we increasing a +1
    <h3> Hello from If block and value of a = @a</h3> //<--here we use @ to access a variable in View tag (When we need to define C# code in HTML )
}
else if(a == 10){
    <p>A is equal 10 </p>
}
else
{
<h2> Hello from Else Block</h2>
}
```

#### Ternary operator

```C#
@{
    int a =10;
    int b =0;
}

<h2>Ternary - @(a==10 ? b= 10 : b=5) </h2>

<h3> Value of B = @b </h3> //<--will give 10
```

### Switch

```C#
@{
    int a =10;
}

@switch(a){
    Case 1: <h1>I am 1</h1>
    break;
    Case 2: <h1>I am 2</h1>
    break;
    Case 3: <h1>I am 3</h1>
    break;
    Case 4: <h1>I am 4</h1>
    break;
    default: <h1>I am default</h1>
    break;

    }

```

# Loops(For ,ForEach, while, switch etc)

```C#
while(age>2){
    Console.WriteLine("Hello");
    age--;
}
```

```C#
switch(dayOfTheWeek){
    case "Monday" : ConsoleWriteLine("First day of the week");
    break;
    case "Tuesday" : ConsoleWriteLine("Second day of the week");
    break;
    case "Wednesday" :
    case "Thursday" : ConsoleWriteLine("Relax day");
    default: ConsoleWriteLine("This day doesn't exist");
    break;
}
```

```C#
@for(int i=0; i<5; i++){
    <h2>Hello from loop 5 times with index @i</h2>
}
```

```C#
@{
var list = new List<int>(){1,2,3,4,5};
}

@foreach (var item in list){
    <h1> @item </h1>
}
```

```C#
int [] numbers = new int []{2,3,4,5,6};

@for(int i=0; i<numbers.Length; i++){   //<-- with Array use Length instead of Count
    Console.WriteLine(numbers[i]);
}
```

```C#
List <int> nmbers = new List <int>(){2,5,8,9};

@for(int i=0; i<numbers.Count; i++){   //<-- with List use Count instead of Length
    Console.WriteLine(numbers[i]);
}
```

# Chain Methods in ASP.NET

```C#
//if we relationship between 2 tables, we can use Method -> Include(). Where Genre is a property from other table
public IActionResult Index (){
    var books = _context.Books2.Include(m => m.Genre).OrderBy(m => m.Title).ToList();

    return View(books);
}
```

# Convert

```C#
Convert.ToInt32(Value to convert to Int32 goes here)  <-- convert to int

Convert.ToSingle(5) /2  <-- convert 5 to float (5.0),  and devide on 2 --> 2.5
//if 5 is in int data type and 2 is in in data type --> 5/2 == 2, because one of the numbers must be in float or double data type to get correct result -->5/2==2.5

//double data type example --> 5.7 or 5.7d
//float data type example --> 5.7f

Convert.ToBoolean(Value to convert to bool goes here)
```

# Overload

......notes from notebook

..........

# Other / mini file and normal file version of libraries

- In wwwroot / lib we have normal version and mini file version of files
  Example: bootstrap-grid.css(normal, non mini file version) and bootstrap-grid.min.css (mini file verison)
- If we use mini file version in the app --> then we won't be able to debug our application in the browser, because an entire code is less than in normal version.
- If we use normal or non mini file version in the app --> the size of the file is larger than mini file version, then it can give a performance issues in the production environment
- Both files have thair avantages and disadvantages, that means we should render these file based on the environment.
- In Development environment we should render ( we should use) --> normal or non mini file version, in case we want to debug our app.
- In Non Development environment (Staging, Production,testing environment) we should use mini file version , to increase the performance of our app.

# return RedirectToAction

- AJAX or JavaScript when submitting the form can cause redirection issues

```C#
// Normally if we don't use AJAX;
// What happens:
// The browser sends a POST request to the server.
// The server responds with a 302 Redirect (from RedirectToAction(...)).
// The browser automatically follows the redirect to the GET version of AddNewBook.
// This works without any JavaScript.
```

- What happens in an AJAX form submission?
  AJAX (Asynchronous JavaScript and XML) uses JavaScript to send the form data without refreshing the page. Example:

```C#
$('#bookForm').submit(function(e) {
    e.preventDefault(); // stop the form from submitting the normal way

    $.ajax({
        url: '/Book/AddNewBook',
        method: 'POST',
        data: $(this).serialize(),
        success: function(response) {
            console.log("Server responded:", response);
        }
    });
});
```

- Why this breaks the redirect:

AJAX captures the response within JavaScript.

Normally your controller returns RedirectToAction(...), it's a 302 redirect, but AJAX does not follow redirects like a browser would.

Instead, the AJAX call just receives the raw response, and your JavaScript needs to decide what to do next.

So instead of navigating to the new page, nothing happens — unless you explicitly do it.

- How to handle redirection in AJAX?

If you're using AJAX, and still want to redirect the user after success, you must do it manually in JavaScript:

```C#
// Server side:
// Return a JSON object with a redirect URL instead of RedirectToAction:
return Json(new { redirectUrl = Url.Action("AddNewBook", new { isSuccess = true, bookId = id }) });



// Client side:
// In your JavaScript:
success: function(response) {
    if (response.redirectUrl) {
        window.location.href = response.redirectUrl; // manually redirect
    }
}
```

# Environment variables

- in View files we use (and in \_Layout.cshtml file as well) -->
- environment tag helpers can have different attributes

```C#
<environment include="Production">
<h2>Production<.h2>
</environment>
```

or

```C#
<environment names="Production,Staging">
    <h3>Production and Staging.</h3>
</environment>
```

or

```C#
<environment exclude="Develoment">
    <h3>Production..</h3>
</environment>
```

- In Classes or Controllers we use -->

```C#
private readonly IWebHostEnvironment _webHostEnvironment;

//constructor
public BookController(IWebHostEnvironment webHostEnvironment){
_webHostEnvironment = webHostEnvironment;
}

//in action method
if(_webHostEnvironment.IsDevelopment){
  // some code
} <-- if it envionment Development do some code
```

# Different Tag Helpers

- Tag Helpers enable server-side code to participate in creating and rendering HTML elements in Razor files (in View files)

1. Anchor Tag Helpers (asp-action='', asp-controller="") (--> See -Header.cshtml file)

```C#
//we put original controller name and action name, not Route attribute names
 <a class="nav-link text-dark" asp-controller="book" asp-action="GetAllBooks" >All Books</a>
```

2. Image tag Helpers -> (asp-append-version="true") (--> See -Header.cshtml file)

```C#
 <img src="~/img/Cat.png" width="60" height="60" asp-append-version="true" />


//in _Layout.cshtml
<link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
```

3. Environment Tag Helpers -> (<environment names="Development></environment>") (--> see \_Layout.cshtml file)

```C#
<environment include="Development">
 <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
  <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</environment>

<environment names="Production,Staging">
 <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
  <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</environment>

<environment exclude="Development">
 <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
  <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</environment>

```

4. Link Tag helpers -> if CDN not working use buil in file, (--> Layout.cshtml file in the head of file)

```C#
<link rel="stylesheet"
href="http://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js"
asp-fallback-href="~/lib/bootstrap/dist/js/bootstrap.js"
asp-fallback-test-property="sr-only"
asp-fallback-test-value="position"
integrity="absolute"
crossorigin="anonymous"></script>
```

5. Form Tag Helpers -> ( asp-for="") (-. See in AddNewBook.cshtml)

```C#
//asp-for="Title"  input element for Title property for our Book Model, asp-for="" <-- all properties will be available over here from the Model that we imported on the top of the file -AddNewBook.cshtml
<label asp-for="Title" class="control-lebel"></label>
<input asp-for="Title" class="form-control"/>

//to show an error msg when field in the form is not valid (use only--> span tag )
<span asp-validation-for="Title" class="text-danger"></span>
```

................................................................................................................

# Server side (Model) Validation (--> See Models/ Book.cs and AddNewBook.cshtml)

- Located in Model folder, using attributes
- ModelState.IsValid <-- return bool (true or false if Form is filled correctly or not, line 108 in BookController.cs)

# Validation summary (--> See AddNewBook.cshtml, line 55)

- we can choose what errors to display,
- We can add extra error messages by using --> (in BookController.cs, line 169,170)

```C#
<form method="post" >

//inside form tag we use asp-validation-summary="All"
<div asp-validation-summary="All" class="text-danger"></div>
```

# Data Type atribute (in Model Class)

- indicate what type of data you must enter in Input filed (--> See Models / SignInModel.cs )
- inputing datatype can be passwod, email, calendar etc
- See Models/ Book.cs file (line 16, 17, 18)

# Client side validation

- needs to don't hit the server with each request, (when the form filled incorrectly), --> validation taking place on the server(in server side validation)
- needs to prevent possibility of changing data in database by sending some strange requests by user to the server
- to work with client side validations we need some libraries: (the order of libraries is importnt)

1. jquery.js
2. jquery.validate.js
3. jquery.validate.unobtrusive.js

- If we use these 3 libraries in our app we enable client side validation automatically from server side validation.

- To use these libraries we import them in `C# _Layout.cshtml ` in the bottom of the file
- to use these libraries in our app --> we can use build in libraries from wwwroot/lib/ or we can use CDN
- to disable client-ide validation (if you want to debug your code) --> (--> See Program.cs file, line 47)

# Ajax Form

- If you want to use AJAX form in our app then we need some libraries: (get them from build in libraries or from CDN)

1. jquery.js
2. jquery.unobtrusive-ajax.js

- To use these libraries we import them in `C# _Layout.cshtml ` in the bottom of the file
- In AddNewBook.cshtml file --> in Form tag we put --> <form method="post" data-ajax="true" > <--This allow to work with ajax in our form
- Also, you can use different ajax functions using --> data-ajax attributes -->

```C#
<form method="post" data-ajax="true" data-ajax-complete="myComplete" data-ajax-success="mySuccess">
```

- ```C#
    data-ajax-loading="#myLoader"
  ```
  <-- use id="myLoader" in bootstrap piece of code, line 23-29 in AddnewBook.cshtml file, show loading picture when uploading

# Form

- A Form has various input options for user to get data easily:
- Text box
- Text Area,
- Calendar,
- Radio button,
- Checkbox,
- Dropdown,
- Number,
- Email,
- etc

# Redirects

[Click Here](https://www.scholarhat.com/tutorial/mvc/return-view-vs-return-redirecttoaction-vs-return-redirect-vs-return-redirecttoroute)

# ORM, What is an ORM – The Meaning of Object Relational Mapping Database Tools

Object Relational Mapping (ORM) is a technique used in creating a "bridge" between object-oriented programs and, in most cases, relational databases

You can see the ORM as the layer that connects object oriented programming (OOP) to relational databases.

## What is an ORM Tool?

An ORM tool is software designed to help OOP developers interact with relational databases. So instead of creating your own ORM software from scratch, you can make use of these tools

```C#
// As example: instead of writing a request to database  -->

"SELECT id, name, email, country, phone_number FROM users WHERE id = 20"

// Using ORM we can simply write --> users.GetById(20)
```

So the code above does the same as the SQL query. Note that every ORM tool is built differently so the methods are never the same, but the general purpose is similar.

Most OOP languages have a variety of ORM tools that you can choose from.

## Popular ORM Tools for .NET

- Entity Framework is a multi-database object-database mapper. It supports SQL, SQLite, MySQL, PostgreSQL, and Azure Cosmos DB.

## Popular ORM Tools for Typescript

- Prisma

## Advantages of Using ORM Tools

Here are some of the advantages of using an ORM tool:

- It speeds up development time for teams.
- Decreases the cost of development.
- Handles the logic required to interact with databases.
- Improves security. ORM tools are built to eliminate the possibility of SQL injection attacks.
- You write less code when using ORM tools than with SQL.

## Disadvantages of Using ORM Tools

- Learning how to use ORM tools can be time consuming.
- They are likely not going to perform better when very complex queries are involved.
- ORMs are generally slower than using SQL.
