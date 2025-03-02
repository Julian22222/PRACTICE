- using jQuery When we choose some element from HTML using Selector --> it creates jQuery object, and this object can be assign to variable

- in jQuery we can use chain functions --> $('.mainText p').hide(2000).show(2000);

```C#
var tagP = $('p');  //<-- can have 1 or many elements in our variable, depending how many p tags existst on our HTML page.
//tagP can have an array of <p> tag objects, if we have many p tags on the page.
//example --> [<p>,<p>,<p>,<p>]
```

# jQuery Methods

1. TEXT

```C#
//in HTML

<div class="mainText">
    <p>Your text is here</p>
</div>
```

```C#
//keep the text in this variable
var tagP = $('.mainText p').text(); //<--it will have text from <p> tag -> Your text is here

$('.mainText p').text('New text');  //<-- will change previous text on new text
```

2. HIDE and SHOW

```C#
$('.mainText p').hide();  //<-- will hide this element
$('.mainText p').hide(3000); //<-- can pass some time, time to hide the element
$('.mainText p').hide(3000, function(){
    //some code
});  //<-- also we can add argument-> function that will invoke after 3000 seconds passed

//the same additional argument function
$('.mainText p').hide(3000, myFunction()); //<-- function is declered before and here we are invoking that function

$('.mainText p').show();  //<-- will show this element
$('.mainText p').show(3000); //<-- can pass some time, time to show the element


var tagP = $('.mainText p');
tagP.hide().text('my new text').show(1000);  //<-- use variable to add a method, or we can add chain functions
```

3. WIDTH and HEIGHT

```C#
$('btn').width();  //<-- can have a width of any element
$('btn').width(150);  //<-- can change a width of any element


$('p').height();  //<-- can have a height of any element
$('p').height(250);  //<-- can change a height of any element
```

4. HTML

- We change code in HTML

```C#
<div class="mainText">
    <div>
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
        <div>....</div>
    </div>

    <div>
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
    </div>

    <div>
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
    </div>
```

```C#
$('.mainText').html(); //<-- we will get all HTML code of this mainText class block

$('.mainText').html('<b>new text</b><h2>my Header</h2>');  //<-- can change HTML code, adding any tegs here, put all tegs in one line!!!
```

5. fadeIn and fadeOut and fadeTo

- Allow to hide and show element smoothly

```C#
$('.mainText').fadeOut();   //<--smoothly hide
$('.mainText').fadeOut(2000, myFunction());   //<-- can add time, and function in arguments


$('.mainText').fadeIn();   //<--smoothly show

$('.mainText').fadeTo(4000, 0.5);  //<-- allow to hide elemnt until some level of transparency, -> 4 seconds and  0.5 (50% of transperancy)
$('.mainText').fadeTo(2000, 1);   //<-- to return the transparency back to normal/original view, to 100%
```

6. slideUp and slideDown

```C#
$('.mainText').slideUp(2000); //<-- hide element by sliding Up

$('.mainText').slideDown();  //<-- show element by sliding down
```

7. attr and removeAttr

- Allow to add and remove attributes to any HTML tags
- attributes are -> href, class, id, src, alt, title, etc.

```C#
//in HTML
<img src="img/logo.png" alt="pic" >
```

```C#
$('img').attr('src');  //<-- will get src attribute from the img, (we indicate attribute in the brackets -> ('src'))

$('img').attr('src', 'img/icon1.png');  //<-- this way we can add attribute or change existing one

$('img').attr('title', 'this is title');  //<-- when you drug the mouse over this element -will show this message

$('img').removeAttr('src');  //<-- remove src attribute in img tag
```
