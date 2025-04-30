# AJAX

[-->AJAX<--](https://www.w3schools.com/whatis/whatis_ajax.asp)

AJAX (Asynchronous JavaScript and XML), it allows you to communicate with the server without refreshing the web page. This provides a more dynamic and interactive user experience. To use XML is not mandatory and in majority applications it is not used.

Ajax allow to make dynamic operations such as: searching, data loading, sending Form, geting the response from server and receiving responce from server for successful or failed responce. It makes process, searching and processing the data faster.

AJAX is a technique that allows web pages to request small amounts of data from the server asynchronously (in the background), without the need to reload the whole page. This means that you can interact with the server, fetch data, and update parts of the web page without disturbing the user’s current activity.

For example, when a user clicks a button or submits a form, an AJAX request can be triggered to call a specific action method in the server. The server can then send back data (like JSON), which can be used to dynamically update the page content.

Using Ajax we can send:

- JSON
- XML
- HTML
- text

##### There are 2 common methods for making requests to DB in ASP.NET Core MVC:

1. using JavaScript's fetch API
2. jQuery's $.ajax method.

```C#
//JavaScript's fetch API

<button id="invokeButton">Invoke Function</button>

<script>
    document.getElementById("invokeButton").addEventListener("click", function () {
        fetch("/ControllerName/ActionMethod", {
            method: "POST"
        })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Error:', error));
    });

</script>
```

# What is AJAX?

AJAX is a technique that allows web pages to request small amounts of data from the server asynchronously (in the background), without the need to reload the whole page. This means that you can interact with the server, fetch data, and update parts of the web page without disturbing the user’s current activity.

For example, when a user clicks a button or submits a form, an AJAX request can be triggered to call a specific action method in the server. The server can then send back data (like JSON), which can be used to dynamically update the page content.

1. Using the fetch API

The fetch API is a modern JavaScript API that makes it easy to send network requests (like GET or POST requests) to the server and process the response asynchronously. It's built into modern browsers and is much more flexible and easier to use than older methods like XMLHttpRequest.

Here’s how you can use fetch to make an AJAX request in your ASP.NET Core MVC app:

```C#

//Example: Using fetch to make a POST request

<button id="invokeButton">Invoke Function</button>



<script>
    document.getElementById("invokeButton").addEventListener("click", function () {
        // Make the AJAX request using fetch

        fetch("/Home/ActionMethod", {
            method: "POST", // Method type (GET, POST, etc.)
            headers: {
                "Content-Type": "application/json" // Setting content type to JSON
            },
            body: JSON.stringify({ key: "value" }) // Request body, data you want to send
        })
        .then(response => response.json()) // Parse JSON response from the server
        .then(data => {
            // Handle the response data from the server
            console.log("Server Response:", data);
        })
        .catch(error => {
            console.error("Error:", error); // Handle any errors
        });
    });

</script>
```

### Breakdown of the fetch options:

- method: Specifies the type of request, like GET, POST, PUT, DELETE etc.
- headers: Sets the request headers. For a JSON request, you typically set Content-Type: application/json.
- body: The request payload. Here, we use JSON.stringify() to convert a JavaScript object to a JSON string.
- then(response => response.json()): The response.json() method parses the JSON response from the server and returns it as a JavaScript object.
- catch(error): This catches and handles any errors that occur during the request.

2. Using jQuery $.ajax() Method

jQuery provides a simpler and more abstracted way of making AJAX requests using the $.ajax() method. If you are already using jQuery in your project, this might be a more convenient option.

```C#
//Example: Using $.ajax() to make a POST request

<button id="invokeButton">Invoke Function</button>



<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>

    $("#invokeButton").click(function () {

        $.ajax({

            url: "/Home/ActionMethod", // URL to send the request to

            type: "POST", // HTTP method type

            contentType: "application/json", // Data format

            data: JSON.stringify({ key: "value" }), // The data you want to send

            success: function (data) {

                // Handle the successful response from the server

                console.log("Server Response:", data);

            },

            error: function (xhr, status, error) {

                // Handle errors

                console.error("Error:", error);

            }

        });

    });

</script>

```

# Breakdown of the $.ajax() options:

- url: Specifies the URL for the action method on the server you want to call.
- type: Specifies the request method (GET, POST, PUT, DELETE etc.).
- contentType: Defines the type of data you're sending. application/json is common when sending JSON data.
- data: The data sent with the request. It's usually stringified (i.e., converting JavaScript objects to JSON format using JSON.stringify()).
- success: A callback function that’s triggered if the request is successful. The response data is passed to this function.
- error: A callback function that handles any errors that occur during the request.

## Sending and Receiving Data with AJAX

Sending Data (Request Payload):

- POST and PUT requests typically include data (payload) that is sent to the server. This can be in various formats, including JSON, form data, or query parameters.

```C#
//For example, if you’re sending data as JSON, you use JSON.stringify() to convert your JavaScript object into a string

{

    name: "John Doe",

    age: 30

}

//Receiving Data (Response):
// - When the server responds to the AJAX request, it usually sends data back in a format like JSON or XML. You can then handle that data in the JavaScript callback function (then() in fetch, success in $.ajax())


//Example Response (JSON)
{
    "message": "Success",
    "status": "ok"
}

//Example: Controller Action in ASP.NET Core MVC

//On the server side, you need to define an action in your controller that will handle the AJAX request. For example:

public class HomeController : Controller
{
    [HttpPost]
    public IActionResult ActionMethod([FromBody] MyModel model)

    {
        // Process the incoming data (model) from the AJAX request
        // You can access the properties of the model, such as model.key
        var response = new { message = "Data received successfully", status = "ok" };

        return Json(response); // Return a JSON response
    }

}

//- [FromBody]: Indicates that the data will be received as a JSON object in the request body.
//- Json(): Returns the response as JSON.

```

```C#
//Handling Response Data and Updating the View

//Once the AJAX request completes, you can update parts of the page with the data received from the server. For example, you can update the content of a <div>

<div id="message"></div>



<script>

    fetch("/Home/ActionMethod", {

        method: "POST",

        headers: { "Content-Type": "application/json" },

        body: JSON.stringify({ key: "value" })

    })

    .then(response => response.json())

    .then(data => {

        document.getElementById("message").innerHTML = data.message;

    })

    .catch(error => console.error("Error:", error));

</script>

//This will dynamically update the content of the <div id="message"> with the response message.

```

# Advantages of Using AJAX

- Improved User Experience: AJAX allows you to perform actions like data retrieval and submission without reloading the entire page. This keeps the user interface smooth and interactive.
- Asynchronous Processing: The page remains responsive while waiting for the server’s response, allowing users to continue interacting with the page.
- Partial Page Updates: Instead of reloading the entire page, you can update just a part of the page (e.g., a specific <div>, form, or table).

# Summary: fetch vs $.ajax

- fetch API:
  - Built-in JavaScript feature (modern browsers).
  - More flexible and lightweight.
  - Promises-based (clean and modern syntax).
  - Can be a bit verbose for complex requests, especially with older browsers.
- jQuery $.ajax():
  - Part of the jQuery library (requires including jQuery).
  - More concise syntax for older browsers and simpler use cases.
  - Provides built-in methods for error handling and response processing.

# Conclusion

- Use fetch if you prefer modern JavaScript and are okay with handling Promises.
- Use $.ajax() if you're already using jQuery in your project or need a quick and easy way to make AJAX requests.
