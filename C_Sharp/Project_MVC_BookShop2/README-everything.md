Raor Syntax -standards how to use C# code in View file

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

# Loops(For ,ForEach etc)

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

# Other / mini file and normal file version of libraries

- In wwwroot / lib we have normal version and mini file version of files
  Example: bootstrap-grid.css(normal, non mini file version) and bootstrap-grid.min.css (mini file verison)
- If we use mini file version in the app --> then we won't be able to debug our application in the browser, because an entire code is less than in normal version.
- If we use normal or non mini file version in the app --> the size of the file is larger than mini file version, then it can give a performance issues in the production environment
- Both files have thair avantages and disadvantages, that means we should render these file based on the environment.
- In Development environment we should render ( we should use) --> normal or non mini file version, in case we want to debug our app.
- In Non Development environment (Staging, Production,testing environment) we should use mini file version , to increase the performance of our app.

# Environment variables

- in View files we use (and in \_Layout.cshtml file as well) -->

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

1. Anchor Tag Helpers (asp-action='', asp-controller="") (--> See -Header.cshtml file)

```C#
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
