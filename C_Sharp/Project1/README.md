# jQuery

to work with jQuery --> The easiest way is to include the jQuery CDN, or local library link in your \_Layout.cshtml or directly in your View file.

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
