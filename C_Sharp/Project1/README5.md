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
        alert("stop");
        $(".loading").hide(700);    //<-- hide element loading
    });


    $(document).ajaxComplete();   //<-- this method run after ajaxSuccess or ajaxError methods, and before ajaxStop method


});
```
