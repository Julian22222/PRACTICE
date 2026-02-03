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

/
//
///
///

[ --> Playground <-- ](https://dotnetfiddle.net/)

///
///
//
/

```C#
//////////////////////////////////// insert to Playground to work

using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System;       //using the System library in your project.Which gives you some useful classes like Console or functions/methods like WriteLine-> Console.WriteLine("Hello World!");
using System.Collections.Generic;  //allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
//using System.Linq;    //querying any type of data source
using System.Threading.Tasks;              //creating new threads for computation, aslo when use async-await operations, and to use Task
using System.IO;  // to use Path.Combine function
using System.Diagnostics;
```

```C#
class MyItem{
    public string name {get; set;}
    public int age {get; set;}
}

var ok = new MyItem {name ="Tom", age = 22};

int num = 7;

string str = "Hello World";
var modifiedStr = str.Substring(0,5);
Console.WriteLine(modifiedStr);

var you = (new {name="Adam", age=22});


string myStr = "123";
var pop = Convert.ToInt32(myStr);


var myList = new List<int>(){1,2,3,4};
myList.Add(5);

foreach(var item in myList){
    Console.WriteLine(item);
}


//Console.WriteLine("Hello World");

var items = new List<string>(){"one", "two", "three"};


foreach(var item in items){
 Console.WriteLine(item);
}


for (int i=0; i< items.Count; i++){
 Console.WriteLine(items[i]);
};



public class Item {
    public int Age {get; set;}
    public string Name {get; set;}
}



var Tony = new Item(){Age=12, Name="Tony"};

var Ben = new Item(){Age=15, Name="Ben"};

var list = new List<Item>(){
Tony, Ben
};


//Console.WriteLine(Tony.Name);

foreach(var item in list){
 Console.WriteLine(item.Name);
}

public class Item2:Item{  //class is extends another class, Item2 inherits all methods and properties from Item class
public string Address {get; set;}
}

var me = new Item2{
Age=22, Name ="Ant", Address = "PIZDA"
};

Console.WriteLine(me.Address);


int i = 2 + 2;
Console.WriteLine($"sum is {i}");
```

```C#
static void Main(){

int num = 7;

    Console.WriteLine("Hello");
    Console.WriteLine($"My number is {num}");
}
```

```C#


abstract class Temple{
    public string Name { get; set; }
    public int Tel { get; set; }
}

class Goo : Temple{
    public int Age { get; set; }
}


var cla = new Goo { Name = "Lee", Tel = 123455, Age = 22 };

Console.WriteLine($"{cla.Name}, {cla.Tel}, {cla.Age}");

Console.WriteLine(me.Address);
/////////////////////////////////////////////

abstract class Temple{
    public string Name { get; set; }
    public int Tel { get; set; }
    public abstract string Address { get; set; }
}


class Goo : Temple{
    public int Age { get; set; }
    public override string Address { get; set; } = "street";
}

var cla = new Goo { Name = "Lee", Tel = 123455, Age = 22 };
Console.WriteLine($"{cla.Name}, {cla.Tel}, {cla.Age}, {cla.Address}");



var myList = new List<int>(){1,2,3,4};

var modifiedList = myList.Select(x => x * 2).ToList();   //correct example
//Select(x => x * 2) is a LINQ projection that doubles each element.
// .ToList() materializes the result into a new list

var modifiedList = myList.Where(x => x % 2 == 0).ToList();  //Where method expects a predicate — a function that returns a bool

var modifiedList = myList.Where(x => x * 2).ToList();     //INCORRECT!!!!, WHERE expecting – Boolean result

int i = 2 + 2;

i




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

```C#
////functions in C#

//"Action" - is a delegate type for methods that return void and take no parameters.
Action myfunc = () => {
    Console.WriteLine("Hello!!!");
};

myfunc();  // Call the function

///////////////////////////////
// When you use Action in C#, you don’t need to explicitly specify the return type in the lambda because:

// Action is a predefined delegate that represents a method with no return value (void) and no parameters.
// So the compiler already knows the method returns nothing (void), and the lambda you write must match that.
// Func<T> = delegate for methods returning a value of type T.
///////////////////////////////////

///As method in the class
myfunction(){
 Console.WriteLine("Hello!!!");
}

myfunction();  // Call the method

///////////////////////////////////

//function in C#
Func<string> myfunc = () => {  //<string> <-- return a string
    return "this is a string";
};

// Calling the function
string result = myfunc();
Console.WriteLine(result);

// Func<string> means: a delegate that returns a string and takes no parameters.
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

# When constructor is called?

In ASP.NET Core MVC with C#, the constructor of a class is called when an instance of the class is created. In the context of an MVC application, constructors are called in different scenarios depending on the type of class:

- Controller Constructor is called every time when we requesting any of its methods (for example: HomeController method) . When an HTTP request is made to a specific URL (e.g., /Home/Index), the ASP.NET Core MVC framework will attempt to route the request to a controller action (e.g., Index action in the HomeController. The constructor is called for each incoming request of corresponding Controller)
- ASP.NET Core uses dependency injection (DI), The constructor is called when the service is requested. If the controller has dependencies that are registered in the Dependency Injection (DI) container (e.g., services, repositories), they are passed into the constructor automatically by the DI system.
- For models (classes representing data), the constructor is typically called when a new instance of the model is created

##### Why is the Constructor Called Every Time?

This is because, by default, ASP.NET Core MVC creates a new instance of the controller for each request, which ensures that the controller is stateless. Each request gets its own fresh instance of the controller, and its constructor is called each time.

However, the dependencies (services injected into the controller) may have different lifetimes:

- Transient: A new instance of the service is created every time it’s requested.
- Scoped: A new instance of the service is created for each HTTP request, but the same instance is reused within the scope of that request.
- Singleton: A single instance of the service is used throughout the entire application's lifetime

Even though the constructor is called for each request, services injected into the controller will be resolved according to their lifetimes in the DI container.

- When you request /Home/Index, the HomeController constructor is called first.

- If you then request /Home/About, the HomeController constructor will be called again to create a new instance of the controller.

##### Alternative: Controller Lifecycle with DI

If you want to avoid creating a new controller instance per request (which is uncommon and not recommended for most scenarios), you could technically make the controller itself a singleton, but that would introduce issues like shared state between requests, which goes against the principles of statelessness in web applications.

To summarize: Yes, the constructor is called every time an action on the HomeController is requested, because the controller is created fresh for each HTTP request.

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

# How script functions work with AJAX

```C#
//in View file

//when click this button it will call --> addToBasket(1, 'The Hobbit', 'J.R.R. Tolkien')
<button onclick="addToBasket(1, 'The Hobbit', 'J.R.R. Tolkien')">Add to Basket</button>



<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    function addToBasket(bookId, title, author) {
        var book = {
            Id: bookId,
            Title: title,
            Author: author
            // Add other fields if needed
        };

        $.ajax({
            type: "POST",
            url: '/YourControllerName/Basket',
            data: JSON.stringify(book),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    alert(response.message); // Or update a part of your page dynamically
                }
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    }
</script>




//In Controller
[HttpPost]
public JsonResult Basket([FromBody] Book book) //receive a book that we sent from ajax call(view file)
{
    _basketRepository.AddToBasket(book);
    return Json(new { success = true, message = "Book added to basket" });
}

```

# Ajax/JQuery options to pass data from cshtml page to controller

1. Content-Type & DataType

When sending JSON data or expecting JSON back, specifying these can avoid some common issues.

```C#
$.ajax({
    type: "POST",
    url: "/Home/Basket",
    contentType: "application/json; charset=utf-8",  // specify what you send
    dataType: "json", // specify what you expect back
    data: JSON.stringify({ id: bookId }),
    success: function(response) { /*...*/ }
});
```

2. Anti-Forgery Token (CSRF Protection)

ASP.NET Core usually requires an antiforgery token for POST requests for security.
You can include the token in your AJAX request headers.

```C#
var token = $('input[name="__RequestVerificationToken"]').val();

$.ajax({
    type: "POST",
    url: "/Home/Basket",
    headers: { "RequestVerificationToken": token },
    data: { id: bookId },
    success: function(response) { /*...*/ }
});

//Make sure your form includes:
@Html.AntiForgeryToken()
```

3. Using .done(), .fail(), .always() Promises Syntax

Instead of using success and error callbacks, you can use the jQuery Promise methods which can sometimes look cleaner and allow chaining:

```C#
$.ajax({
    type: "POST",
    url: "/Home/Basket",
    data: { id: bookId }
}).done(function(response) {
    alert(response.message);
}).fail(function() {
    alert("Error adding book to basket.");
}).always(function() {
    console.log("Request completed.");
});
```

4. BeforeSend and Complete Callbacks

Useful for showing loaders or disabling buttons before the AJAX call, and enabling them afterward.

```C#
$.ajax({
    type: "POST",
    url: "/Home/Basket",
    data: { id: bookId },
    beforeSend: function() {
        $("#loading").show();  // or disable button: $("#myBtn").prop('disabled', true);
    },
    complete: function() {
        $("#loading").hide();  // or enable button: $("#myBtn").prop('disabled', false);
    },
    success: function(response) { /*...*/ },
    error: function() { /*...*/ }
});
```

5. Global AJAX Event Handlers

If you want to handle AJAX events for the entire page (like global loading indicators):

```C#
$(document).ajaxStart(function(){
    $("#loading").show();
}).ajaxStop(function(){
    $("#loading").hide();
});
```

6. Timeout
   To prevent hanging AJAX calls:

```C#
$.ajax({
    type: "POST",
    url: "/Home/Basket",
    data: { id: bookId },
    timeout: 5000, // milliseconds
    success: function(response) { /*...*/ },
    error: function(xhr, status, error) {
        if (status === "timeout") {
            alert("The request timed out.");
        } else {
            alert("Error adding book to basket.");
        }
    }
});
```

7. Handling Complex Data / Multiple Parameters
   If you have complex objects or multiple parameters, serialize to JSON:

```C#
var book = {
    id: bookId,
    quantity: 2,
    notes: "Gift wrap"
};

$.ajax({
    type: "POST",
    url: "/Home/Basket",
    contentType: "application/json; charset=utf-8",
    data: JSON.stringify(book),
    success: function(response) { /*...*/ }
});
```

8. Using $.post() Shortcut

If you only need a simple POST request without extra configuration, jQuery provides a shorthand:

```C#
$.post("/Home/Basket", { id: bookId }, function(response) {
    alert(response.message);
}).fail(function() {
    alert("Error adding book to basket.");
});
```

##### Submit Form

```C#
$("Form").submit();   ß $("Form") — selects all <form> elements on the page (case-insensitive but usually lowercase is preferred: "form").

.submit() — triggers the form's native submit event, causing the form to post back to the server in the usual way (not AJAX).

.submit() is perfect when you want a full page refresh or server-rendered page response.
```

1. $("Form").submit();

This triggers a normal form submit, which sends the form data to the controller via a full page reload or redirect (classic POST/GET request).
Not AJAX. It submits the form traditionally.
If you want to send form data using AJAX instead (to avoid page reload), you usually do something like:

```C#
$("form").submit(function(event) {
    event.preventDefault();  // prevent normal form submit
    var formData = $(this).serialize(); // serialize form inputs into URL encoded string
    $.ajax({
        type: $(this).attr("method"), // POST or GET
        url: $(this).attr("action"),
        data: formData,
        success: function(response) {
            // handle success (update UI, show message, etc.)
        },
        error: function() {
            // handle error
        }
    });

});
```

2. Use .serialize() to grab all input fields in a form and send as URL-encoded string via AJAX

```C#
var formData = $("form").serialize();
```

3. Use FormData API to upload files asynchronously

```C#
var formData = new FormData(this);
```

4. Disable submit button to prevent multiple clicks

```C#
$("#btn").prop("disabled", true);
```

```C#
//Example: Sending entire form with AJAX (instead of $("form").submit() traditional)

$("form").submit(function(e) {
    e.preventDefault(); // stop traditional submit
    var formData = $(this).serialize();

    $.ajax({
        type: $(this).attr("method"),
        url: $(this).attr("action"),
        data: formData,
        success: function(response) {
            alert("Form submitted successfully");
            // update UI or do other things
        },
        error: function() {
            alert("Error submitting form");
        }
    });
});
```

## What is an ORM Tool?

An ORM tool is software designed to help OOP developers interact with relational databases. So instead of creating your own ORM software from scratch, you can make use of these tools

```C#
// As example: instead of writing a request to database  -->

"SELECT id, name, email, country, phone_number FROM users WHERE id = 20"

// Using ORM we can simply write --> users.GetById(20)
```

So the code above does the same as the SQL query. Note that every ORM tool is built differently so the methods are never the same, but the general purpose is similar.

Most OOP languages have a variety of ORM tools that you can choose from.

# Differences when you send a data to controller

Differences between method in Controller

```C#
[HttpPost]
public IActionResult Basket([FromBody] Book book){..}
//and

[HttpPost]
public async Task<IActionResult> AddNewBook(Book book){ ..}
```

1.

```C#
[HttpPost]
public IActionResult Basket([FromBody] Book book){..}

//This is a synchronous action method.
//It processes the request and returns a response in a blocking way
//Suitable for simple, fast operations (e.g., updating in-memory lists, minimal logic, etc.).
//[FromBody] tells ASP.NET Core to bind the incoming data from the request body (usually JSON in AJAX).- used when Data send via AJAX
//Only required when using application/json and model binding is ambiguous or from non-form sources.
```

2.

```C#
[HttpPost]
public async Task<IActionResult> AddNewBook(Book book){ ..}

//This is an asynchronous action method.
//It allows non-blocking I/O operations, such as database access, API calls, or file handling.
//Returns a Task<IActionResult> which enables await inside the method.
//This method is used when data is submitted via a FORM
```

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

# How to add Awesome icons In ASP.NET core MVC

[--> Awesome Icons <--](https://fontawesome.com/search?ic=free)

1. You can use CDN link

```C#
//add this link to _Layout.cshtml into <head> tag

<link href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.css' rel="stylesheet" />
```

Then you can insert Awesome Icons code anywhere in HTML

2. Using NuGet: Install the FontAwesome NuGet package into your project.

- Directly: Download the Font Awesome CSS file (e.g., all.min.css) and place it in your wwwroot/lib/font-awesome/css directory.

```C#
// If using NuGet or downloaded directly, add the following to your _Layout.cshtml or other layout file:
<link rel="stylesheet" href="~/lib/font-awesome/css/all.min.css" />
```

# You can change appsettings.json from controller

In ASP.NET Core MVC, you can modify appsettings.json properties at runtime, but there are a few things to keep in mind.

1. Read-Only Nature: The appsettings.json file is typically read at application startup and is usually considered read-only during runtime. If you want to modify values dynamically, you would need to update the file itself or use another approach like storing the configuration in a database, an external configuration service, or in-memory.
2. Approach 1: Modifying the Configuration in Memory
   You can modify in-memory configuration settings using IConfiguration and IConfigurationRoot. However, it won't change the actual appsettings.json file on disk.
   Here's an example of how to update the configuration in memory:

```C#
public class HomeController : Controller
{
    private readonly IConfigurationRoot _configuration;

    public HomeController(IConfiguration configuration){
        _configuration = (IConfigurationRoot)configuration;
    }


    public IActionResult ChangeSetting(){

        // Update a setting in memory (will not affect appsettings.json on disk)
        _configuration["AppSettings:MySetting"] = "NewValue";

        // If you want to use the updated value, you can fetch it like so:
        var updatedValue = _configuration["AppSettings:MySetting"];
        ViewData["UpdatedValue"] = updatedValue;

        return View();
    }
}
```

3. Approach 2: Modifying the appsettings.json File Directly
   If you want to modify the actual appsettings.json file on disk (i.e., persist changes between restarts), you can read the JSON file, modify it, and save it back. However, this is less common, as it might interfere with the application's initial configuration on startup.
   Here's an example:

```C#
using System.IO;
using Newtonsoft.Json.Linq;


public class HomeController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public HomeController(IWebHostEnvironment environment){
        _environment = environment;
    }

    public IActionResult ChangeSetting(){

        string filePath = Path.Combine(_environment.ContentRootPath, "appsettings.json");

        // Read the appsettings.json file
        var json = JObject.Parse(System.IO.File.ReadAllText(filePath));

        // Modify the value
        json["AppSettings"]?["MySetting"] = "NewValue";

        // Save it back to the file
        System.IO.File.WriteAllText(filePath, json.ToString());

        return View();
    }
}
```

Important considerations:

Thread Safety: Modifying the appsettings.json file at runtime might lead to issues with multiple instances or concurrent requests. Be cautious about potential race conditions.
Application Restart: Changes to appsettings.json might not take effect immediately unless you manually trigger a restart or reload the configuration. In ASP.NET Core, the configuration is usually reloaded when the app restarts.
File Permissions: Make sure the application has write permissions to the appsettings.json file, especially if you're running in a production environment.
Alternative Approaches

If you need to change configurations at runtime frequently, it might be a better idea to:

Store the configuration in a database and fetch it as needed.
Use a distributed configuration service like Azure App Configuration or Consul.
Use in-memory storage with dependency injection.
These approaches offer greater flexibility and control compared to modifying appsettings.json directly.
