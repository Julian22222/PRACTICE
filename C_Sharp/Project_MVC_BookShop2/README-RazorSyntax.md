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
