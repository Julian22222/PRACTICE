# Notes for AJAX

Ajax Methods to work with GET, POST, PUT, DELETE methods.

```C#
//Method Examples

$.ajaxSetup();  //<--convinient method if we have small application and we always make a request to the same resource. By default it will run asynchronously.

$.ajax();  //<-- universal method, the same as ajaxSetup method but if we have big application and we make requests to different resources. This method has the same parametrs as --> ajaxSetup() method,
//Method ajax by default make GET method request and it is the same as $get() method , with ajax method we can add adjust many things, but with $get() you can't

$get();

$post();

```

```C#
    <div class="content"></div>
    <button class="test">Test</button>


<script>
$(document).ready(function(){

    //ajaxSetup() <--convinient method if we have small application and we always make a request to the same resource. By default it will run asynchronously.
    $.ajaxSetup({
        url: "https://jsonplaceholder.typicode.com/posts",
        context: $(".content"),  //<-- our container <div> for the data
        @* async: false,  //<-- this will make the request synchronous *@
        statusCode: {   //<-- this will handle the status codes of the response
            404: function(){   //<-- if we receive status code 404
                alert("page not found");
            }
        },
        success: function(){  //<-- this will handle the success of the request
                $(this).text("Done");   //<--we are using $(this) to refer to the container <div> -> block .content, and we will add the text from the successful response
        },
        error: function(xhr){  //<-- this will handle the error of the request, parametr --> xhr is the XMLHttpRequest object- where we can get ->error status, error message
                alert(`Error: ${xhr.status}, Error text: ${xhr.statusText}`);
            }
        });

    $(".test").click(function(){        //<-- action when we click the button
        $.ajax();  //<-- it will invoke ajax method, it will use the settings from the ajaxSetup() method
    });
});

</script>
```
