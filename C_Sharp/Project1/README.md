# jQuery

to work with jQuery --> The easiest way is to include the jQuery CDN, or local library link in your \_Layout.cshtml or directly in your View file.

The most popular and traditional ways to add client-side functionality using <scripts> to a web page:

is jQuery and JavaScript

1. jQuery
   jQuery remains a solid option for small tasks, quick development, and maintaining legacy applications.

Example of code:

```C#
// First, ensure that jQuery is included in your layout page or directly in the view where you want to use it.
// In your _Layout.cshtml (if you want to include jQuery globally for the project): à in head tag

<head>

<script src=https://code.jquery.com/jquery-3.6.0.min.js></script>

</head>



// Or, if you want to include jQuery directly in your view (e.g., Index.cshtml), you can do the following:
<script src=https://code.jquery.com/jquery-3.6.0.min.js></script>

```

```C#
//<script> block in for example, in your Index.cshtml

@page
@model YourProjectNamespace.Models.YourModel

<h2>My ASP.NET Core MVC Page</h2>

<button id="myButton">Click Me</button>

<div id="result"></div>

<script>

    $(document).ready(function() {

        // jQuery function to handle button click

        $('#myButton').click(function() {

            $('#result').text('Button clicked!');

        });

    });

</script>
```

```C#
//AJAX requests with jQuery: If you want to make AJAX requests to the server, you can use jQuery's $.ajax method. Here's an example of how you can send data from a jQuery function to a controller action

<script>
    $(document).ready(function() {

        $('#myButton').click(function() {

            $.ajax({

                url: '/Home/ActionName', // Your controller action URL

                type: 'POST',

                data: { param: 'some data' },

                success: function(response) {

                    $('#result').text(response); // Process the server response

                },

                error: function(xhr, status, error) {

                    console.log("Error: " + error);

                }

            });

        });

    });

</script>
```

```C#
//In Controller Action

// In your controller (e.g., HomeController.cs), you would handle the AJAX request. Here's an example of a simple controller action to respond to the AJAX call:

public class HomeController : Controller

{

    [HttpPost]

    public IActionResult ActionName(string param)

    {

        // Process the data and return a response

        return Json($"You sent: {param}");

    }

}
```

# Pros of Using jQuery:

- Simplicity: jQuery provides a simple, cross-browser-compatible way to interact with the DOM, handle events, make AJAX calls, and animate elements. For small tasks, jQuery can be very efficient.

- Widely Supported: It has been around for a long time and is supported by most browsers. It's still commonly used in many existing web applications.

- Large Ecosystem: There are plenty of plugins and libraries built on top of jQuery to speed up development.

- Legacy Compatibility: Many older projects still rely on jQuery, so using it may be necessary if you're working with or maintaining older codebases.

# Cons of Using jQuery:

- Large Library: For modern projects, jQuery can be considered too large, especially if you're only using a small subset of its functionality. In fact, many of the features jQuery provides (like fetch, querySelector, addEventListener, etc.) are now available natively in modern browsers.

- Overhead: If you're using only a small part of jQuery, it might be overkill. Modern JavaScript (ES6+) provides similar functionality without needing a large external library.

- Performance: While jQuery is highly optimized, it's still an additional script that the browser needs to load, which can impact performance, especially on mobile devices with slower connections.

- Modern JavaScript Alternatives: Native JavaScript (or modern frameworks) has caught up with a lot of what jQuery does, reducing the need to rely on it for new projects.

2. Vanilla JavaScript (Native JS)

It is ideal for modern projects with minimal dependencies, offering more control with native functionality.

With modern JavaScript (ES6+), you can use native APIs that provide functionality similar to jQuery without needing an external library. These APIs include:

document.querySelector (instead of jQuery's $())
fetch() (instead of $.ajax())
addEventListener() (instead of jQuery's .on())

Example of code:

```C#

<button id="myButton">Click Me</button>

<div id="result"></div>



<script>

    document.addEventListener('DOMContentLoaded', function() {

        document.getElementById('myButton').addEventListener('click', function() {

            document.getElementById('result').textContent = 'Button clicked!';

        });

    });

</script>

```

# Pros of Using JS:

- No need to load an external library (reducing file size and loading time).

- Full control over how you handle interactions with the DOM.

# Cons of Using JS:

You may need to write more code, as some functionality might require a bit more effort than jQuery provides with shorthand methods.
Cross-browser compatibility handling can be more verbose, although modern browsers have standardized many features.

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////// previous code using different options to work

```C#
//_Layout.cshtml file

<!DOCTYPE html>
<html>
<head>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>  //CDN link
    // or
    // <script src="~/js/jquery.min.js"></script>  //local library
</head>
<body>
    @RenderBody()
</body>
</html>
```

- it can be from local library
- or from CDN (content delivery network. It's a group of servers that work together to speed up the delivery of web content to users. )

to make sure jQuery working --> put these code in script tag

```C#
<script>
$(document).ready(function () {
    console.log("jQuery is working!");
});
</script>

//When you open the browser console you should see "jQuery is working!
```

# If statements in Scripts using jQuery

```C#
if (condition) {
    // Code to execute if condition is true
} else {
    // Code to execute if condition is false
}
```

```C#
//Check if an Element is Visible
if ($("#myElement").is(":visible")) {
    console.log("Element is visible!");
} else {
    console.log("Element is hidden!");
}
```

```C#
//Example 2: Check if an Input Field is Empty

if ($("#myInput").val() === "") {
    alert("Input field is empty!");
}
```

```C#
//Example 3: Check if a Button has a Specific Class

if ($("#myButton").hasClass("active")) {
    console.log("Button is active!");
} else {
    console.log("Button is not active!");
}
```

```C#
//Example 4: Use if-else inside a Click Event

$("#toggleButton").click(function() {
    if ($("#box").hasClass("hidden")) {
        $("#box").removeClass("hidden").show();
    } else {
        $("#box").addClass("hidden").hide();
    }
});
```

```C#
//Example 5: Check if a Variable is Defined

let fighterImg = $("#fighterImg").attr("src");

if (fighterImg) {
    console.log("Image source exists: " + fighterImg);
} else {
    console.log("No image source found!");
}
```
