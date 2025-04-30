# BOOTSTRAP

Connect the Bootstrap to your project – go to [bootstrap.com](https://getbootstrap.com/docs/5.3/getting-started/introduction/)

And copy paste the bootstrap link to your project

# Grid section

It has 3 Levels – container, rows and columns. Wrap all your content to container first, then put rows and columns inside the container

```C#
<div class=”container”>
    <div class=”row”>
        <div class=”col”>
            //some other blocks
        </div>

    </div>
</div>
```

1. Container

```C#
//Container class:
<div class=”container”> //container by deault- depending from the screen size automatically gives padding,


//Container class has different styles:
<div class=”container-fluid”>, <div class=”container-sm”>, <div class=”container-md”>, <div class=”container-lg”>, <div class=”container-xl”>, <div class=”container-xxl”>


//Example:
<div class=”container-md”> // if the screen size is smaller than medium, it will not apply container class to that block. It will switches to be 100% width, taking all the width of the screen.

//Example:
<div class=”container-fluid”> //fluid container, fill the full screen – 100% of the screen width

```

2. Column

```C#
//Column class:
<div class=”col”> //will take full width of the row by default, and 50px height


//by default it will fill full width of the row, taking on account how many columns are in the row
//Example of 2 col in container
//If we add 3 col, it will share full width of the row, each col will take 33% of the row etc.

<div class=”container”>
    <div class=”row”>
        <div class=”col”></div>   //These 2 columns, will share full width of the row, each column will have 50% of the row having a gap between columns
        <div class=”col”></div>   //These 2 columns, will share full width of the row, each column will have 50% of the row having a gap between columns
    </div>
</div>
```

Row class in the bootstrap is using flexbox under the hood. Bootstrap’s grid is based on a 12 column system. Every single row in bootstrap has 12 hard-coded column size.

```C#
<div class=”col-6”> // will take only 50% of the row
```

```C#
//Example1
<div class=”container”>
    <div class=”row”>
        <div class=”col”></div> //by default, if there is no attributes to col (col-6), it will take the rest width – in this case will take 25%
        <div class=”col-6”></div>    //this column will take 50% of the row width, this column takes 6 column width space of the 12 columns
        <div class=”col”></div>   ////by default, if there is no attributes to col (col-6), it will take the rest width –in this case will take 25%
    </div>
</div>


//Example2
<div class=”container”>
    <div class=”row”>
        <div class=”col”></div>   //by default, if there is no attributes to col (col-6), it will take the rest, remaining width/space,  equally together with other class=”col” – in this case 12,5%
        <div class=”col”> </div>   //by default, if there is no attributes to col (col-6), it will take the rest width, equally together with other class=”col” - in this case 12,5%
        <div class=”col-6”> </div>    //this column will take 50% of the row width, this column takes 6 column width space of the 12 columns
        <div class=”col-3”> </div>   //this column will take 25% of the row width, this column takes 3 column width space of the 12 columns
     </div>
</div>


//Example3
//if you don’t specify col width – it will take remaining width in the row
//you can think that there is 13 rows in this example: col-1, col-10, col, col.
//col-1 takes 1 column width space of the 12 columns
// col-10 takes 10 column width space of the 12 columns
// 2 col – takes the remaining space of the row – 0.5 column for each col

<div class=”container”>
    <div class=”row”>
        <div class=”col-1”></div>   //this column will take 1/12 of the row width, this column takes 1 column width space of the 12 columns
        <div class=”col-10”></div>   // this column will take 10/12 of the row width, this column takes 10 column width space of the 12 columns
        <div class=”col”></div>   //by default, if there is no attributes to col (col-6), it will take the rest, remaining width/space,  equally together with other class=”col”
        <div class=”col”></div>   //by default, if there is no attributes to col (col-6), it will take the rest, remaining width/space,  equally together with other class=”col”
    </div>
</div>

```

```C#
<div class=”col-auto”>  //It will automatically adjust the column size depending of content inside (some text for example) to fit in the col, if there is nothing in that container it will be no column


<div clas=”col-lg-4 col-8” >  //It will make 4 columns at large screen and any other screen that is smaller large screen will have col-8, (8 columns)
<div clas=”col-xl-2 col-lg-4 col-8” >   // (col-xl-2) It will have 2 columns at extra large size screen, (col-lg-4) - 4 columns at large screen and lower screens will have 8 columns (col-8)
//when you have few screens (xl, lg, etc.) content adjustment you need to indicate last property -> col-4 or any other number, otherwise it will take full screen by default.
//Example: <div clas=”col-xl-2 col-lg-4 col-8” >   if we don’t mention col-8 in the end, when we will change screen to medium or small , this column will take full row, by default = col-12


//If you have more than 12 columns in the row, it will wrap onto next line
<div class=”container”>
    <div class=”row”>
        <div class=”col-4”></div>
        <div class=”col-10”></div>
     </div>
</div>


//If we want to put 2 columns and make bigger gap between the columns use – offset
<div class=”container”>
    <div class=”row”>
        <div class=”col-3”></div>
        <div class=”col-3 offset-3”></div>   //offset-3 means it will make a 3 columns gap between your col, (3 columns between first and second col )
    </div>
</div>
```

3. Row

```C#
//Example 1, if you want to have 2 columns in the row, in every single row, then we need to define that on the row class. All this columns will be separated 2 columns per row

<div class=”container”>
    <div class=”row row-cols-2”>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
    </div>
</div>


//Example 2
<div class=”container”>
    <div class=”row row-cols-2 row-cols-lg-4”>  //will have 4 columns in each row for large screens and above, lower screens than large will have 2 columns for each row
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
    </div>
</div>



// We can make gaps between rows
//to join divs together can use gy-0 and gy-0, (no gap)
// use g-3 , will use gap on all directions, on X and Y axis

<div class=”container”>
    <div class=”row row-cols-2 gy-2 gx-5”>  //gy – means gap in Y axis (vertically) – 2px,  gx- means gap in X axis – 5px (horizontally)
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
        <div class=”col”></div>
    </div>
</div>
```

Also, can make nested rows and columns

# Tables

```C#
//Example 1
<div class=”container”>
    <div class=”table”>   //make table design, with spaces and underlines, separating different rows
        <thead>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Age</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Kyle</td>
                <td>Cook</td>
                <td>27</td>
            </tr>
            <tr>
                <td>Sally</td>
                <td>Thorn</td>
                <td>13</td>
            </tr>
            <tr>
                <td>Jim</td>
                <td>Smith</td>
                <td>48</td>
            </tr>
        </tbody>
    </div>
</div>



//usually that is enough to put class=”table” but you can make extra customization – colours
// <div class=”table table-dark”> will make table in black colour
//<div class=”table table-striped”> – will stripe the table (make every 2nd line grey colour)
//<div class=”table table-striped-columns”>    will separate all columns and rows with the line
//<div class=”table table-hover> will make hover effect
//<div class=”table table-sm”> make table smaller
//<div class=”table-responsive” > if you have big table with lots of data to store in the table, this will allow you to scroll, location of the div - <div class=”container”><div class=”table-responsive”><table class=”table”> ….
//<div class=”table-responsive-lg” > scroll will appear only on screens smaller than large screen
////<div class=”table-group-divider” >  //makes thick line,
//<div class=”table table-bordered”> - will add borders to your table, //<div class=”table table-borderless”> - will remove all borders from your table


//Example 2
<div class=”container”>
    <div class=”table table-primary table-striped”>   // make table in blue colour
        <thead>
            <tr class=”table-success” >  //tr=table row, table-success  - will make this row in green colour
                <th>First name</th>  //table head
                <th>Last name</th>
                <th>Age</th>
            </tr>
        </thead>
        <tbody table-group-divider > // will make thick line, separating table head from table body
            <tr class=”table-active”> //will highlight this row in grey colour
                <td class=”table-danger”>Kyle</td> //td = table data, table-danger  - will make this slot red
                <td>Cook</td>
                <td>27</td>
            </tr>
            <tr>
                <td>Sally</td>
                <td>Thorn</td>
                <td>13</td>
            </tr>
            <tr>
                <td>Jim</td>
                <td>Smith</td>
                <td>48</td>
            </tr>

        </tbody>
    </div>
</div>
```

# Forms

```C#
<div class=”container”>
    <form>
        <label for=”email” class=”form-label”>Email</label>
        <input type=”email” id=”email” class=”form-control” >  //” id=”email” links this input with label where – for=”email”
        <button>Submit</button>
    </form>
</div>

```

```C#
//Range
<div class=”container”>
    <form>
        <label for=”email” class=”form-label”>Email</label>
        <input type=”range” id=”email” class=”form-range” >  //use type=”range” and class=”form-range” otherwise it won’t work
        <button>Submit</button>
    </form>
</div>
```

```C#
//Select
<div class=”container”>
    <form>
        <label for=”email” class=”form-label”>Email</label>
        <select class=”form-select”> //will make dropdown options 100% width
            <option>1</option>
            <option>2</option>
        </select>
         <button>Submit</button>
    </form>
</div>
```

```C#
//checkbox
<div class=”container”>
    <form>
        <div class=”form-check form-switch”>  // gives us a styling, can add a switcher
            <input type=”checkbox”class=”form-check-input” id=”email” />  //gives us a styling
            <label for=”email” class=”form-check-label”>Email</label>  //gives us a styling
        </div>
        <button>Submit</button>
    </form>
</div>
```

```C#
//Input-group
<form>
    <div class=”input-group”> //connects all blocks inline that are inside
        <div class=”input-group-text”>$</div>
        <input type=”number” class=”form-control” />
        <button class=”btn btn-primary”>+</button>
    </div>
</form>
```

```C#
//Make green tick when the field is field correctly
<div class=”container”>
    <form novalidate>  //novalidate override default behaviour, when if input not valid it show a message
        <div class=””form-floating>
            <input type=”email” id=”email” class=”form-control” placeholder=”Place Email here” required>
            <label for=”email” >Email</label>
            <div class=”invalid-feedback”>Invalid email</div>  //show error messages if input field incorrectly
            <div class=”valid-feedback”>correct email</div> //show message
        </div>
        <button>Submit</button>
    </form>
</div>


//in the bottom of the document
<script>
const form =document.querySelector(“form”)
form.addEventListener(‘submit’, e => {
if(!form.checkValidity()){
e.preventDefault()
}
form.classList.add(‘was-validated’)
})
</script>
```

# Buttons

```C#
<a class=”btn btn-primary” href=”#” > Test </a>  //will look as a button
<button class=”btn btn-link”> TEST </button>  //will look like a link

<button class=”btn btn-primary btn-sm”  > Test </button>  //make small button
```

# Alerts

```C#
<div class=”container”>
    <div class=”alert alert-success alert-dismissible fade show” role=”alert” >  // role=”alert” – will allow to close alert block, fade show- allow to alert fade in and fade out
        Alert
        <button class=”btn-close” aria-label=”close” data-bs-dismiss=”alert”  ></button>   // data-bs-dismiss=”alert”   - tells to bootstrap what to close when we click the “X” button
    </div>
</div>
```

# Cards
