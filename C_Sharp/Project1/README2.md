# How to use Selectors

- jQuery use the same selector logic ass in CSS to interact with certain HTML element.

```C#
//in CSS we use

.logo {  //<--class Selector
  position: absolute;
  margin-left: -10%;
}

body .icons h4 {   ////<--Selector
  text-decoration: underline;
  text-transform: uppercase;
}

section {   ////<--tag Selector
  width: 70%;
  text-align: center
}

#block {   ////<--id Selector
  width: 70%;
  text-align: center
}

//this way you can indicate any attribute and its value, use quotes
img[height='150']{  //<-- attribute selector, img tag, where height="150" - attribute --> <img height="150" src="..." alt=".." >
 width: 70%;
 text-align: center
}

img[src^='img/']{ //<-- image src atrribute path should start with src="img/" , it can have any path after "img/"
 width: 70%;
}

img[src$='.png']{  //<-- image src atrribute path should end with src=".png"
 width: 70%;
}

a[href*='#']{  //<-- in <a> tag can have # in href attribute -> in any place, doesn't mater
 width: 70%;
}
```

```C#
//Selectors in jQuery
//The same Selector

$(".logo")  //<-- class Selector
$("section") //<-- tag Selector
$("#block")  //<-- id Selector
$("body .icons h4")  //<-- nested Selector
$('img, a');  //<-- allow to choose different elements -> images and anker tags(links) together
$('img[alt]');   //<-- if alt attribute exists in image tag then we can work with this element, doesn't metter what value has alt atrribute
$(.icons img + h2)  //<--interact with h2 that are located after img (only one element) in icons class

$('img[height=150]')  //<-- attribute selector, no quotes in jquery to use attribute selector
$('img[src^=img/]')  //<-- image src atrribute path should start with src="img/" , it can have any path after "img/"  . No quotes in jquery
$('img[src$=.png]')  //<-- image src atrribute path should end with src=".png" , No quotes in jquery
$('a[href*=#]')     //<-- in <a> tag can have # in href attribute -> in any place, doesn't mater, No quotes in jquery

$('menu li:even')  //<-- will get only even elements from this, (keep in mind that elements starts from index zero)
$('menu li:odd')  //<-- will get odd elements

$('img:not(.logo img)') //<-- get all img tags apart from --> .logo img
```

```C#
//Selectors in jQuery

$(".icons div")  //<-- interact with all div tags inside icons class


$(".icons > div")  //<-- if we want to get only child elements from icons container
//example
<div class="icons">
    <div>  //<--child
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
        <div>....</div>
    </div>

    <div>  //<--child
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
    </div>

    <div>  //<--child
        <img src="..." alt="">
        <h2>....</h2>
        <p>...</p>
    </div>

```

# How to use jQuery

```C#
jquery();

//or

$();

```
