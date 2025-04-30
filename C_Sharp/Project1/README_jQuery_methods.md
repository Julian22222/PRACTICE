# Difference between 2 methods in JQuerry:

```C#
//Method 1
$('.buy-btn').click(function(){
    //code
}

//binds an event only to existing elements when the page is loaded. This works only for elements that already exist in the DOM when the page is loaded. If you dynamically add new .buy-btn elements to the page after the initial load (e.g., after an AJAX request or a dynamic update), the new elements won't have the click event bound to them.
```

```C#
//Method 2
$('#basket').on('click', '.cancel-btn', function(){
    //code
});

//uses event delegation, meaning it works for both existing and dynamically added elements inside the #basket container. It doesn't matter if the .cancel-btn elements exist when the page is first loaded — the event will still work for dynamically added buttons.
```

# Often used jQuery methods in <script> block

Summary:

These are just some of the most commonly used jQuery methods for DOM manipulation, animations, form handling, event handling, and AJAX requests. You can mix and match them based on your specific needs in the web development process.

jQuery is incredibly flexible, allowing you to perform complex actions with just a few lines of code. You can use these methods to make your web pages more dynamic and interactive.

1. Hiding an Element

Hides an element, making it invisible on the page.

```C#
// Hide an element with ID 'myElement'
$("#myElement").hide();
```

2. Showing an Element

Shows a previously hidden element.

```C#
// Show an element with ID 'myElement'
$("#myElement").show();
```

3. Toggling an Element's Visibility

This method toggles between hiding and showing an element.

```C#
// Toggle visibility of the element with ID 'myElement'
$("#myElement").toggle();
```

4. Changing CSS of an Element

You can change the CSS properties of an element directly using .css().

```C#
// Change the background color of an element with ID 'myElement'
$("#myElement").css("background-color", "blue");
```

5. Adding and Removing Classes

You can add, remove, or toggle classes of an element.

```C#
// Add a class to an element
$("#myElement").addClass("highlight");

// Remove a class from an element
$("#myElement").removeClass("highlight");

// Toggle a class on and off
$("#myElement").toggleClass("highlight");
```

6. Animating an Element

Use jQuery's .animate() to create smooth transitions. Here’s an example where we animate the height of an element.

```C#
// Animate the height of an element with ID 'myElement' to 200px over 1 second
$("#myElement").animate({

    height: "200px"

}, 1000);
```

7. Changing HTML Content

You can change the HTML content of an element using .html().

```C#
// Change the HTML content inside an element with ID 'myElement'
$("#myElement").html("<strong>New HTML content</strong>");
```

8. Changing Text Content

The .text() method is used to change or get the text content of an element.

```C#
// Set text of an element with ID 'myElement'
$("#myElement").text("This is the new text!");

// Get the text content of an element with ID 'myElement'
var text = $("#myElement").text();

console.log(text); // Outputs the text content of the element
```

9. Appends HTML to an Element

You can append HTML content inside an element using .append().

```C#
// Append a new paragraph to the element with ID 'myElement'
$("#myElement").append("<p>New paragraph appended!</p>");
```

10. Prepending HTML to an Element

You can prepend HTML content to the start of an element using .prepend().

```C#
// Prepend a new paragraph to the element with ID 'myElement'
$("#myElement").prepend("<p>New paragraph prepended!</p>");
```

11. Changing Input Values

Set the value of an input field using .val().

```C#
// Set the value of an input field with ID 'myInputField'
$("#myInputField").val("New value for input field");
```

12. Getting Input Values

Get the value from an input field.

```C#
// Get the value of an input field with ID 'myInputField'
var inputValue = $("#myInputField").val();

console.log(inputValue); // Outputs the value from the input field
```

13. Submitting a Form

Submit a form programmatically using .submit().

```C#
// Submit a form with ID 'myForm'
$("#myForm").submit();
```

14. Binding Events (Click, Hover, etc.)

You can attach various events like click, hover, change, etc.

```C#
// When button with ID 'btn1' is clicked, execute the function
$("#btn1").click(function () {

    alert("Button 1 clicked!");

});


// When mouse enters an element with ID 'myElement', change background color
$("#myElement").hover(

    function () {

        $(this).css("background-color", "yellow");

    },

    function () {

        $(this).css("background-color", "");

    }

);
```

15. Getting the Index of an Element

You can get the index of an element relative to its siblings.

```C#
// Get the index of the clicked button within its parent
$("#myButton").click(function () {

    var index = $(this).index();

    console.log("Button index is: " + index);

});
```

16. Traversing the DOM (Parent, Children, Siblings)

You can traverse the DOM to get related elements.

```C#
// Get the parent of an element
var parent = $("#myElement").parent();

console.log(parent);

// Get the children of an element
var children = $("#myElement").children();

console.log(children);

// Get the next sibling of an element
var nextSibling = $("#myElement").next();

console.log(nextSibling);
```

17. Chaining Methods

jQuery allows you to chain multiple methods together for cleaner code.

```C#
// Chain multiple jQuery methods together
$("#myElement").css("background-color", "blue").fadeOut(1000).fadeIn(1000);
```

18. Checking if an Element Exists

You can check if an element exists before manipulating it.

```C#
// Check if an element exists and then hide it
if ($("#myElement").length) {

    $("#myElement").hide();

} else {

    console.log("Element not found");

}
```

19. Preventing Default Action (like form submit)

Prevent the default action of an event (e.g., form submission).

```C#
// Prevent default form submission when button is clicked
$("form").submit(function (event) {

    event.preventDefault();

    alert("Form submission prevented!");

});
```

20. Using AJAX to Get Data

AJAX allows you to retrieve data from the server without refreshing the page.

```C#
// Make an AJAX request to get data from the server
$.ajax({

    url: '/get-data',

    type: 'GET',

    success: function (data) {

        console.log("Data retrieved: ", data);

    },

    error: function () {

        console.log("Error fetching data");

    }

});
```

### Full Example in Script Block:

Here’s an example that combines some of the methods listed above in a <script> block:

```C#
<script>

    $(document).ready(function () {

        // Show and hide buttons

        $("#btn1").click(function () {

            $("#btn2").hide(); // Hide button 2

            $("#btn3").show(); // Show button 3

            $('#result').text('Button 1 was clicked!'); // Change text content

        });



        // Change image source when button is clicked

        $("#btn2").click(function () {

            var figherImg = $(this).data("fighter-img"); // Get the custom data attribute

            $("#figterContainer1 img").attr("src", figherImg); // Change image source

        });



        // Animate an element

        $("#btn3").click(function () {

            $("#figterContainer1").animate({

                width: "500px",

                height: "500px"

            }, 1000);

        });



        // Bind event on a dynamically added button

        $(document).on('click', '#btn4', function () {

            alert('Dynamically added button clicked!');

        });



        // Dynamically add a new button

        $("#addButton").click(function () {

            $("body").append('<button id="btn4">Dynamically Added Button</button>');

        });

    });

</script>
```

1. .fadeIn() and .fadeOut()

Used to create fading animations for elements (making them appear or disappear).

```C#
// Fade in an element with ID 'myElement'
$("#myElement").fadeIn(1000);  // Fade in over 1 second

// Fade out an element with ID 'myElement'
$("#myElement").fadeOut(1000);  // Fade out over 1 second
```

2. .slideDown() and .slideUp()

These methods slide elements in and out vertically.

```C#
// Slide down an element with ID 'myElement'
$("#myElement").slideDown(1000);  // Slide down over 1 second

// Slide up an element with ID 'myElement'
$("#myElement").slideUp(1000);  // Slide up over 1 second
```

3. .delay()

Adds a delay before performing an action on an element.

```C#
// Add a 2-second delay before fading out the element
$("#myElement").delay(2000).fadeOut();
```

4. .focus() and .blur()

These methods are used to focus or remove focus from form elements.

```C#
// Focus on an input field with ID 'myInput'
$("#myInput").focus();

// Remove focus from an input field with ID 'myInput'
$("#myInput").blur();
```

5. .hover()

This method allows you to handle both mouse enter and mouse leave events.

```C#
// When the mouse enters or leaves an element
$("#myElement").hover(

    function() {

        $(this).css("background-color", "yellow"); // On mouse enter

    },

    function() {

        $(this).css("background-color", ""); // On mouse leave

    }

);
```

6. .on()

Used to bind event handlers to elements, including events like click, hover, change, etc.

```C#
// Bind a click event to a button with ID 'btn1'
$("#btn1").on("click", function() {

    alert("Button clicked!");

});

// Bind multiple events to the same element
$("#btn1").on("click mouseenter", function() {

    $(this).css("color", "red");

});
```

7. .off()

Removes event handlers that were previously attached using .on().

```C#
// Remove the click event handler from the button with ID 'btn1'
$("#btn1").off("click");
```

8. .each()

Iterates over a collection of elements and executes a function for each.

```C#
// Iterate through all paragraphs and change their text color
$("p").each(function(index) {

    $(this).css("color", "blue");

});
```

9. .prop()

This method is used to get or set properties of elements (e.g., checkboxes, radio buttons).

```C#
// Set the 'checked' property of a checkbox
$("#myCheckbox").prop("checked", true);

// Get the 'checked' property of a checkbox
var isChecked = $("#myCheckbox").prop("checked");

console.log(isChecked);  // true or false
```

10. .attr()

This method gets or sets attributes of an element (e.g., href, src).

```C#
// Get the 'href' attribute of a link
var href = $("#myLink").attr("href");

console.log(href);

// Set the 'src' attribute of an image
$("#myImage").attr("src", "new-image.jpg");
```

11. .val()

Gets or sets the value of form elements (input fields, select dropdowns, etc.).

```C#
// Get the value of an input field
var inputValue = $("#myInput").val();

console.log(inputValue);

// Set the value of an input field
$("#myInput").val("New Value");
```

12. .append() and .prepend()

These methods allow you to add new content inside an element.

```C#
// Append HTML content to an element with ID 'myElement'
$("#myElement").append("<p>New content added at the end</p>");

// Prepend HTML content to an element with ID 'myElement'
$("#myElement").prepend("<p>New content added at the beginning</p>");
```

13. .remove() and .empty()

These methods remove elements or their content from the DOM.

```C#
// Remove an element with ID 'myElement'
$("#myElement").remove();

// Empty an element, removing all its child elements
$("#myElement").empty();
```

14. .css()

Used to get or set CSS styles of an element.

```C#
// Get the background color of an element
var bgColor = $("#myElement").css("background-color");

console.log(bgColor);

// Set the background color of an element
$("#myElement").css("background-color", "green");
```

or another sample

```C#
$(document).ready(function() {
    $("#fighterImg").css({
        "position": "absolute",
        "right": "20px",
        "top": "50px"
    });
});
```

15. .siblings()

Finds all the sibling elements of the selected element.

```C#
// Get all siblings of the element with ID 'myElement'
$("#myElement").siblings().css("color", "blue");
```

16. .next() and .prev()

These methods return the next or previous sibling of an element.

```C#
// Get the next sibling of an element with ID 'myElement'
$("#myElement").next().css("background-color", "yellow");

// Get the previous sibling of an element with ID 'myElement'
$("#myElement").prev().css("background-color", "green");
```

17. .animate()

Animate specific CSS properties of an element over a given duration.

```C#
// Animate an element to move 200px right and 100px down
$("#myElement").animate({

    left: "200px",

    top: "100px"

}, 1000);
```

18. .scrollTop() and .scrollLeft()

These methods are used to get or set the scroll position of an element.

```C#
// Get the vertical scroll position of an element
var scrollPosition = $("#myElement").scrollTop();

console.log(scrollPosition);

// Set the vertical scroll position of an element
$("#myElement").scrollTop(100);  // Scroll to 100px from the top
```

19. .fadeTo()

Fades an element to a specified opacity.

```C#
// Fade an element to 0.5 opacity over 2 seconds
$("#myElement").fadeTo(2000, 0.5);
```

20. .trigger()

Used to trigger an event on an element.

```C#
// Trigger a click event on a button with ID 'btn1'
$("#btn1").trigger("click");
```

21. .serialize()

Serializes form data into a query string, useful for sending data via AJAX.

```C#
// Serialize form data and log it
var formData = $("#myForm").serialize();

console.log(formData);
```

22. .toggleClass()

Toggles a class on or off.

```C#
// Toggle the class 'active' on an element
$("#myElement").toggleClass("active");
```

23. .focusin() and .focusout()

These methods are used to handle when an element gains or loses focus.

```C#
// Trigger when an element gains focus
$("#myInput").focusin(function() {

    console.log("Input field focused");

});

// Trigger when an element loses focus
$("#myInput").focusout(function() {

    console.log("Input field lost focus");

});
```

24. .load()

This method loads data from a server and places it into a specified element.

```C#
// Load data from a URL into an element with ID 'myElement'
$("#myElement").load("/get-data");
```

25. .data()

This method is used to store or retrieve data associated with elements.

```C#
// Store custom data on an element
$("#myElement").data("key", "value");

// Retrieve custom data from an element
var dataValue = $("#myElement").data("key");

console.log(dataValue);
```

1. .filter()

The .filter() method is used to filter a set of elements by a condition or selector.

```C#
// Find all even elements in a list
$("li").filter(":even").css("color", "green");
```

2. .not()

The .not() method excludes elements from a set based on a condition or selector.

```C#
// Exclude all <p> elements and change the background of the rest
$("div").not("p").css("background-color", "yellow");
```

3. .first() and .last()

These methods return the first or last element in a set of matched elements.

```C#
// Change the text color of the first paragraph
$("p").first().css("color", "red");

// Change the background of the last <li> element
$("li").last().css("background-color", "blue");
```

4. .find()

The .find() method is used to find all descendants (child elements) of an element that match the selector.

```C#
// Find all <span> elements within a <div> element with ID 'container'
$("#container").find("span").css("color", "purple");
```

5. .parents()

The .parents() method returns all the ancestors (parent, grandparent, etc.) of an element.

```C#
// Get all ancestors of the <span> element with class 'child'
$(".child").parents().css("border", "2px solid red");
```

6. .closest()

The .closest() method is used to find the nearest ancestor element (including the element itself) that matches a selector.

```C#
// Find the closest <div> to the element with class 'child'
$(".child").closest("div").css("border", "2px solid green");
```

7. .each()

The .each() method iterates over a jQuery object, executing a function for each matched element.

```C#
// Iterate through all <li> elements and change their text color
$("li").each(function() {

    $(this).css("color", "blue");

});
```

8. .map()

The .map() method is similar to .each(), but instead of executing a function for each element, it returns a new array of values.

```C#
// Get an array of the text content of all <p> elements
var texts = $("p").map(function() {

    return $(this).text();

}).get();

console.log(texts);
```

9. .stop()

The .stop() method stops the currently running animations on an element.

```C#
// Stop any current animations on the element
$("#myElement").stop();
```

10. .fadeToggle()

This method is a combination of .fadeIn() and .fadeOut(). It will toggle the visibility of an element by fading it in or out.

```C#
// Toggle fade in or fade out the element with ID 'myElement'
$("#myElement").fadeToggle();
```

11. .slideToggle()

Similar to .fadeToggle(), this method toggles the visibility of an element by sliding it up or down.

```C#
// Toggle slide up or slide down the element with ID 'myElement'
$("#myElement").slideToggle();
```

12. .queue()

The .queue() method lets you retrieve or manipulate the queue of functions for an element.

```C#
// Add a custom function to the queue of animations for #myElement
$("#myElement").queue(function(next) {

    $(this).css("background-color", "red");

    next();  // Move to the next function in the queue

});
```

13. .dequeue()

The .dequeue() method is used to process the next function in the queue.

```C#
// Process the next item in the queue for the element
$("#myElement").dequeue();
```

14. .triggerHandler()

This method triggers an event without the full event propagation (i.e., without the event bubbling or default action).

```C#
// Trigger a custom event on an element
$("#myElement").triggerHandler("click");
```

15. .ajax()

The .ajax() method is used to perform asynchronous HTTP (AJAX) requests.

```C#
// Perform a GET request to a server and log the response
$.ajax({

    url: "/get-data",

    type: "GET",

    success: function(response) {

        console.log("Data received: ", response);

    }

});
```

16. .get()

The .get() method retrieves data from the server using a GET request.

```C#
// Send a GET request and log the result
$.get("/get-data", function(data) {

    console.log(data);

});
```

17. .post()

The .post() method performs a POST request to send data to the server.

```C#
// Send data to the server using a POST request
$.post("/submit-form", { name: "John", age: 30 }, function(response) {

    console.log(response);

});
```

18. .serialize()

The .serialize() method serializes form data into a URL-encoded string.

```C#
// Serialize the form data and log it
var formData = $("form").serialize();

console.log(formData);
```

19. .slideToggle()

This method toggles an element's visibility using a sliding motion (up or down).

```C#
// Slide the element up or down based on its current visibility
$("#myElement").slideToggle();
```

20. .resize()

The .resize() method triggers an event when the window or element is resized.

```C#
// Trigger the resize event on the window
$(window).resize(function() {

    console.log("Window resized");

});
```

21. .scroll()

The .scroll() method is used to execute code when an element or the window is scrolled.

```C#
// Execute code when the user scrolls the window
$(window).scroll(function() {

    console.log("Window is scrolling");

});
```

22. .on() with Multiple Events

You can attach multiple events in a single .on() call.

```C#
// Attach multiple events to an element
$("#myElement").on("click mouseenter", function() {

    $(this).css("color", "green");

});
```

23. .fadeIn() and .fadeOut() with Duration

You can specify the duration for the fade effect.

```C#
// Fade out the element with a 2-second duration
$("#myElement").fadeOut(2000);

// Fade in the element with a 3-second duration
$("#myElement").fadeIn(3000);
```

24. .offset()

The .offset() method gets or sets the current position of an element relative to the document.

```C#
// Get the offset of an element
var offset = $("#myElement").offset();

console.log(offset); // { top: 100, left: 200 }


// Set the offset of an element
$("#myElement").offset({ top: 200, left: 300 });
```

25. .scrollTop() and .scrollLeft()

These methods retrieve or set the scroll position of an element.

```C#
// Get the scroll position of an element
var scrollPos = $("#myElement").scrollTop();

console.log(scrollPos);

// Set the scroll position of an element
$("#myElement").scrollTop(50);
```
