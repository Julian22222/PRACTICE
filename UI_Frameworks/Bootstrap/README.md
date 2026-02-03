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

```C#
<div class=”btn-group”> //will join all components inside this div together, usually used for buttons
    <button class=”btn btn-primary active” >Test1</Button>
    <button class=”btn btn-primary active” >Test2</Button>
</div>




<div class=”btn-group-vertical”> …</div>  // will join all components inside this div and vertical
```

# Different attributes

```C#
data-bs-toggle=”button”  // toggle between active state and off state for a button

//example
<button class=”btn btn-primary” data-bs-toggle=”button”>Test</Button>


//If you want to start with active state toggle button , give class of active , and an area-pressed attribute = true. It will active button state by default and then you can toggle between 2 states
<button class=”btn btn-primary active” data-bs-toggle=”button” aria-pressed=”true”>Test</Button>
```

```C#
// data-bs-“something” Attribute

You will notice a lot of Bootstrap components have this –> data-bs-“something” , that you can add. It is just a custom data attribute prefixed with “bs” , It allows you to hook this up to the JS portion of Bootstrap, to make this button toggle between these 2 states.
```

```C#
// role="alert" attribute
//Role attribute - it's critical for ensuring that users with disabilities, particularly those using screen readers, can properly understand and respond to the alert message. Screen readers can announce the content of the alert when it appears, helping users who rely on these tools.

<div class="alert alert-success" role="alert">Alert</div>
```

```C#
// class=”btn-close” – will style and add “X” to close this div

// data-bs-dismiss=”alert” – tells bootstrap what to close when we click this “X” button.  Adds functionality to the button. It tells Bootstrap's JavaScript to remove the alert element from the DOM when the button is clicked. By clicking this “X” button it will remove/close alert div

//aria-label="close" - attribute is used for accessibility purposes, It provides a label for screen readers, that help users with disabilities

//Example:
<button class=”btn-close” aria-label=”close” data-bs-dismiss=”alert” ></button>
```

# Links

```C#
<a hre=”#” class=”alert-link”> something</a>  //style link
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

```C#
<div class=”alert fade show”>  //make sure you add fade and show, to your class
```

# Cards

```C#
//Examples to style cards

class=”card-group”  //join cards together inside this div
class=”card”
class=”card-header”
class=”card-footer”
class=”card-img-top”
class=”card-img-bottom”
class=”card-img-overlay”  //text is overlaying over the image on the cards
class=”card-body”
class=”card-title”
class=”card-subtitle”
class=”card-text”
```

# Modal

```C#
//main structure of Modal
<div class=”container”>
    <div class=”modal”>
           <div class=”modal-dialog”>
                 <div class=”modal-content”>

                </div>
           </div>
    </div>
</div>
```

```C#
//you need these 3 different nested modal classes inside of each other, to make a Modal
//By its own it will not be shown, it needs a button to open the modal

<div class=”container”>
    <button class=”btn btn-primary” data-bs-toggle=”modal” data-bs-target=”#modal” >Open </button>   // data-bs-toggle=”modal” -saying I am toggling a modal to open and close it., data-bs-target=”#modal” – is a selector for our modal, it data-bs-target=”#modal” matches with the id attribute in   -  <div class=”modal” id=”modal”>
    <div class=”modal fade modal-sm” id=”modal” >   //fade - adds fade in and fade out effect, modal-sm – will make small modal window, can be modal-lg or modal-xl,
           <div class=”modal-dialog modal-dialog-centered modal-dialog-scrollable” >  // modal-dialog-centered – modal placed in page centre, modal-dialog-scrollable – will be scrollable if there is a lot info inside this modal section –in modal-body, class=”modal fade modal-fullscreen”  - modal will have full screen size, don’t need to indicate any modal-xl (as example) in <div> above, modal-fullscreen-md-down  -it uses full screen modal on screens below medium screen
            <div class=”modal-content”>
                <div class=”modal-header”>
                    Header
                    <button class=”btn-close” data-bs-dismiss=”modal”></button>
                </div>   //styling Modal
                <div class=”modal-body”> Body</div>
                <div class=”modal-footer”> Footer
                    <button class=”btn btn-primary” data-bs-dismiss=”modal”>Close</button>
                </div>
            </div>
        </div>
    </div>
</div>
```

# Collapse (drop down section)

```C#
<div class=”container”>
    <button class=”btn btn-primary” data-bs-toggle=”collapse” data-bs-target=”#row” aria-expanded=”false” aria-controls=”row”>Toggle< /button>   // data-bs-toggle=”collapse” – for dropdown, data-bs-target=”#row” – targeting the thing we want to collapse, it this case it is block with -> id=”row”, aria-expanded=”false” – startup position for dropdown section (closed dropdown), aria-controls=”row” should match with id=”row” in section below, aria-controls will not work with multiple elements in dropdown, because you can specify only one id in here, in our case with multiple elements drop together 2 elements

    //one element in collapse
    <div class=”row collapse” id=”row”>  // collapse -add for dropdown
        <div class=”col”>
            <div class=”box”>Section 1< /div>
        </div>
    </div>

    //can hav multiple elements
     <div class=”row collapse” id=”row”>  // collapse -add for dropdown
        <div class=”col”>
            <div class=”box”>Section 2< /div>
        </div>
    </div>

</div>
```

# NavBar

```C#
//Example 1
<nav=”navbar navbar-expand navbar-dark bg-dark”>   // navbar-dark – dark navbar line, navbar-expand – make navbar horizontal
    <div class=”container”>
        <a href=”#” class=”navbar-brand”>BRAND</a>  //can be replaced with Brand Icon
        <ul class=”navbar-nav”>
                <li class=”nav-item”>
                <a href=”#” class=”nav-link active” aria-current=”page” >Home</a>  //active - Home button will be slightly darker colour,
                </li>

                <li class=”nav-item”>
                <a href=”#” class=”nav-link”>Store</a>
                </li>
        </ul>
        <div class=”navbar-text”>Test</div> //can add section to right side of the NavBar section, but Navbar menu section will be shifted in the middle
    </div>
</nav>
```

```C#
//Example 2 – dropdown menu
<nav=”navbar navbar-expand-md navbar-dark bg-dark”>   // navbar-expand-md – make navbar horizontal on medium screen and higher, otherwise navbar will be vertically stacked
    <div class=”container”>
        <a href=”#” class=”navbar-brand”>BRAND</a>
        <button class=”navbar-toggler” data-bs-toggle=”collapse” data-bs-target=”#nav” aria-controls=”nav” aria-label=”Expand Navigation”>
            <div class=”navbar-toggler-icon”></div>    // class=”navbar-toggler-icon – create burger menu icon
        </button>   // data-bs-target=”#nav”-/targeting id=nav, aria-controls=”nav”></ - for screen readers, disable people- it points that id=”nav”
        <div class=”collapse navbar-collapse” id=”nav”>
            <ul class=”navbar-nav”>
                <li class=”nav-item”>
                    <a href=”#” class=”nav-link active” aria-current=”page” >Home</a>
                </li>

                <li class=”nav-item”>
                    <a href=”#” class=”nav-link”>Store</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
```

# Screen readers attribute difference

- role="" — What the element is
  Purpose: Defines what type of UI element the HTML element represents (e.g., a button, dialog, banner).
  Helps screen readers understand the purpose or behavior of the element.
  If the native HTML tag already has a semantic role (like <button>, <nav>, etc.), you often don’t need role

  ```C#
    <div class=”alert alert-success alert-dismissible fade show” role=”alert” >  // role=”alert” – will allow to close alert block, fade show- allow to alert fade in and fade out
        Alert
        <button class=”btn-close” aria-label=”close” data-bs-dismiss=”alert”  ></button>   // data-bs-dismiss=”alert”   - tells to bootstrap what to close when we click the “X” button
    </div>
  ```

- aria-label="" — What the element is called  
   Purpose: Provides a text label for screen readers when there’s no visible label.
  It does not define what the element is, only how it's announced.

  ```C#
  <button class=”navbar-toggler” data-bs-toggle=”collapse” data-bs-target=”#nav” aria-controls=”nav” aria-label=”Expand Navigation”>
  ...
  </button>
  ```

Rule of Thumb:

Use role when the HTML element doesn't describe its purpose clearly.
Use aria-label when the element needs a name for screen readers but doesn’t have visible text.

# Other things

```C#
//Background
<div class="card bg-dark">  //make backgroung dark colour, but the text is black as well, (can't see text on black background)
Hello
</div>

//to make text show on black background
<div class="card bg-dark text-white">  //or text-light
Hello
</div>

//or

<div class="card text-bg-dark"> //automatically give text correct colour depending from background colour
Hello
</div>
```

```C#
//Opacity
<div class="card bg-primary bg-opacity-25 text-opacity-50"> //will give 25% background apacity, text-opacity-50%
Hello
</div>
```

```C#
//make different colour of anker tag
<a href="#" class="link-danger"> Hi </a>  //will be in red colour
```

```C#
//Stack elements

//automatically elements will be verically stacked on top of each other
<div class="card-body">
    <div class="box"></div>
    <div class="box"></div>
    <div class="box"></div>
    <div class="box"></div>
</div>


//elements will be horizontally stacked up next to each other
<div class="card-body">
    <div class="hstack gap-3">  //horizontal stack, with gap 3 between elements
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
    </div>
</div>


//elements will be verically stacked on top of each other
<div class="card-body">
    <div class="vstack">  //vertical stack
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
        <div class="box" style="width:100%"></div>
    </div>
</div>
```

```C#
//borders

<div class="card border border-3 border-primary rounded-3 p-2 m-5">
    Hello
</div>

//border-primary - colour of the border,
//border-3 - width of the border,
//rounded-3 - round border corners,
//border - allow to add border class, to see other changes within border,
//rounded-pill - will make pill shape on ages,
//rounded-circle - will make circle or oval,
//rounded-top, rounded-bottom - rounding only top or bottom side,
//border-start - will give a border only on left side,
//border-opacity-50
//p-2 -padding
//p-0 - no padding
//pt-2 -top padding, pb - bottom
//ps-2 padding on the left side, pe- right side
//m-5 margin, mt, mb, ms, me - margin start, margin bottom, margin top, margin bottom
//m-lg-5 on large screen sizes and above margin 5
//m-auto
//p-auto
```

```C#
//Display property for elements

<div class="d-flex"> //flex box container
Hello
</div>

//d-none - will hide this element
//d-block - show element


<div class="d-block d-lg-none"> //hide this element on large screen size and above, but show on screen sizes smaller than large screen size
Hello
</div>


<div class="d-lg-block d-none"> //visible only on large and above screens
Hello
</div>
```

```C#
//Flexbox


<div class="d-flex justify-content-center"> //flex box container , justify-content-cnter - make all element in the center
    <div class="box" style="width:100px"></div>
    <div class="box" style="width:100px"></div>
    <div class="box" style="width:100px"></div>
    <div class="box" style="width:100px"></div>
</div>

//justify-content-end
//justify-content-start
//justify-content-between
//justify-content-around
//align-items-center
//flex-wrap
//flex-nowrap
//flex-column
```
