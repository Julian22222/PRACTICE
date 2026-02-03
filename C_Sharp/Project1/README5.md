# Notes for AJAX

Ajax Methods to work with GET, POST, PUT, DELETE methods.

```C#
//Method Examples

$.ajaxSetup();  //<--convinient method if we have small application and we always make a request to the same resource. By default it will run asynchronously.

$.ajax();  //<-- universal method, the same as ajaxSetup method but if we have big application and we make requests to different resources. This method has the same parametrs as --> ajaxSetup() method,
//Method ajax by default make GET method request and it is the same as $get() method , with ajax method we can add adjust many things, but with $get() you can't


// get and post methods have quick methods -->
$get();  //<- method to get data from the server
$post();   //<--method to post data to the server


// For methods -> PUT and DELETE there is no quick methods such as:
// $put();  <-- there is no such a method
// $delete();  <-- there is no such a method
//  --> Therefore we need to use method ajax(); in these cases

```

```C#
//Example of ajaxSetup method

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

# Event hadling methods

Events that performed successfully, failed etc will invoke a function

```C#
$(document).ajaxSuccess();  //<--this method will run in case of successful execution of any request
$(document).ajaxError();    //<--this method will run in case of failed execution of the request
$(document).ajaxStart();    //<--this method will run during start of execution of the request
$(document).ajaxComplete();  //<--this method will run after finish of execution of the request
$(document).ajaxStop();     //<--this method will run after completion of all request on the Page
```

```C#
//Example of using Event hadling methods

<button class="request">Ajax request</button>
<button class="broken">Broken request</button>
<button class="start">Start loading</button>
<div class="loading" style="display: none">Loading</div>  //<-- this block is hidden


$(document).ready(function(){

    //example successful request
    $(.request).click(function(){
        $.ajax({           //<-- method ajax will be invoked when click on the button with class -> request
            url:"https://jsonplaceholder.typicode.com/users/1",
            success:function(result){
                console.log(result);
            }
        });
    });


    //example of broken request casing error
    $(.broken).click(function(){
        $.ajax({           //<-- method ajax will be invoked when click on the button with class -> request
            url:"ttps://jsonplaceholder.typicode.com/users/1",  //<-- here is the wrong url
            success:function(result){
                console.log(result);
            }
        });
    });


     $(.start).click(function(){
        $.ajax({           //<-- method ajax will be invoked when click on the button with class -> request
            url:"https://jsonplaceholder.typicode.com/users/1",
            success:function(result){
                console.log(result);
            }
        });
    });





    //Event hadling method
    $(document).ajaxSuccess(function(e,xhr, opt){   //<-- here we receive 3 arguments, e -> event, xhr -> all request info and result, opt -> options
        alert("completed");
        console.log(e);
        console.log(xhr);
        console.log(opt);
    });  //<-- this method will be invoked for all successful requests, it allows to process all successful requests without writing additional code



    $(document).ajaxError(function(e,xhr, opt){  //<--this method will be invoked in case of any request fail, error, has the same parametrs as ajaxSuccess method
        alert("failed");
        console.log("failed event", e);
        console.log("failed request info", xhr);
        console.log("failed options", opt);
    });


    //usually this method is used to show of LOADING status and animation, loading some picture, data, sending form etc.
    $(document).ajaxStart(function(){   //<-- when you start any request it will invoke this method
        alert("start");
        $(".loading").show(700);  //<-- showing element loading
    });


    $(document).ajaxStop(function(){   //<-- will run after completion of all requests on the page
        // alert("stop");
        $(".loading").hide(700);    //<-- hide element loading
    });


    $(document).ajaxComplete(function){  //<-- this method run after ajaxSuccess or ajaxError methods, and before ajaxStop method
        alert("completed");
    });
```

# Data handling before sending the Data

```C#
$.param();  //<-- this method creates a string to send the Data from the object, converts the Data type from object to string

//param() method receive Data in object type and convert it to string type

//this method simplifies convertion before sending the Data
```

```C#
//Example

$(document).ready(function(){

    let form = {
        name: "User",
        email: "test@gmail.com",
        password: "123rty"
    }


$(".result").text($param(form))   //<-- we have <div> with class = result. Inside this <div> we will show the data, converting the object to string, using param() method

//$(".result").text($param(form))  <-- this code will give -> name=User&email=test%40gmail.com&password=123rty

//param() method receive Data in object type and convert it to string type
});

```

# Ajax in ASP.NET Core MVC

```C#
// use in View, when send some data in Form

//to use ajax form
// <form method="post" data-ajax="true" data-ajax-complete="myComplete" data-ajax-success="mySuccess" data-ajax-failure="myFailure" data-ajax-loading="#myLoader" asp-controller="Book" asp-action="AddNewBook">
// data-ajax="true"  <-- our form will work with ajax, (this is mandatory text to work with ajax) if in _Layout.cshtml we imported  all needed libraries
// data-ajax-complete="myComplete" data-ajax-success="mySuccess"   <-- 3 different functions, the function logic is in the bottom of the file
// data-ajax-loading="#myLoader" <-- use id="myLoader" in bootstrap piece of code, line 23-29, show loading picture when uploading

<form method="post" data-ajax="true" data-ajax-complete="myComplete" data-ajax-success="mySuccess" data-ajax-loading="#myLoader" enctype="multipart/form-data" asp-controller="Book" asp-action="AddNewBook">
// enctype="multipart/form-data"   <- If you are dealing with the file(if we planning to uploading files in View) in your form then you use this attribute in Form tag
// asp-controller="Book" asp-action="AddNewBook"  <-- we don't need to write this part, if in Controller (-->See BookController.cs) the View page-(form page) name has the same name as [HttpPost] method- (when we send the data) --> to AddNewBook <--is the same name for View page and [HttpPost] method

Some coede here

</form>



@section scripts {
//Here we can insert any scripts

    <script>
        function myComplete(data){ //<--name of the function is myComplete, to receive the data from your function we can pass some attributes--> (data)
        //this code wil execute , doesn't metter if the request is successful or fail.
            alert("I am from complete");
        }

        function mySuccess(data){  //<--name of the function is mySuccess

        //this code wil execute if the request is successful
            alert("I am from Success");
        }

        function myFailure(data){  //<--name of the function is myFailure

        //this code wil execute if the request was failed
            alert("I am from Failure");
        }
    </script>
}

// then we need to put -->   @await RenderSectionAsync("MyScripts", required: false)  in Layout.cshtml file
//This allows to don't use <script> tag in every View page in your Project
```
