# Invoking functions in <scripts> separately

```C#
@{

    ViewData["Title"] = "Button Click Example";

}



<h2>@ViewData["Title"]</h2>



<!-- Button 1 -->

<button id="btn1">Button 1</button>



<!-- Button 2 -->

<button id="btn2">Button 2</button>



<!-- Button 3 -->

<button id="btn3">Button 3</button>



<!-- This section contains the script to control button actions -->

@section Scripts {

    <script>

        $(document).ready(function () {

            // When Button 1 is clicked, hide Button 2 and invoke a function

            $("#btn1").click(function () {

                $("#btn2").hide();  // Hide Button 2

                alert("Button 1 clicked, hiding Button 2 and invoking function!");

                invokeFunction();   // Call the custom function

            });



            // When Button 2 is clicked, hide Button 3 and invoke a function

            $("#btn2").click(function () {

                $("#btn3").hide();  // Hide Button 3

                alert("Button 2 clicked, hiding Button 3 and invoking function!");

                invokeAnotherFunction(); // Call another custom function

            });

        });



        // Custom function to be invoked when Button 1 is clicked

        function invokeFunction() {

            console.log("Custom function called by Button 1!");

            // You can add more functionality here, e.g., modify DOM or do AJAX

        }



        // Another custom function to be invoked when Button 2 is clicked

        function invokeAnotherFunction() {

            console.log("Another custom function called by Button 2!");

            // You can add more functionality here as well

        }

    </script>

}
```

# Assign List data to variables

You can absolutely assign the chosen fighter's data from the fighters list (which is a List<Fighter>) to variables. You can access the specific fighter that the user selects and assign their data (like Name, Img, Health, etc.) to variables in your JavaScript.

You already have a data-fighter-name and data-fighter-img attached to the buttons, so you can use those values to identify the selected fighter. You can then loop through the fighters list (on the client-side, you'll likely need to transfer this list to JavaScript), find the matching fighter, and assign their data to variables.

Here’s how you could do this:

1. Transfer the Fighters Data to JavaScript

First, you need to pass the fighters data from C# (Razor) to JavaScript. One way to do this is to use the JSON format. You can serialize the fighters list to JSON and then use it in JavaScript.

You can do that in your Razor view like this:

```C#
@{

    var fighters = new List<Fighter>()

    {

        new Fighter(){Id = 1, Name = "Sub Zero", Img = "https://avatarfiles.alphacoders.com/342/342681.png", Health = 100, Damage = 15, Armor = 5, Move = 1},

        new Fighter(){Id = 2, Name = "Cyrax", Img = "https://i.pinimg.com/736x/ae/7f/d9/ae7fd946a9226f41ee51f4406535fca4.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1}

    };



    string fightersJson = Json.Serialize(fighters);  // Serialize to JSON string

}

<script>

    var fighters = @Html.Raw(fightersJson); // Deserialize JSON string to a JavaScript object

</script>
```

The Json.Serialize(fighters) converts the list of fighters into a JSON string.
@Html.Raw(fightersJson) injects the JSON string into your JavaScript, where it is automatically parsed into a JavaScript array of objects. 2. Assign Chosen Fighter's Data to Variables

Now that you have the fighters array in JavaScript, you can find the selected fighter based on their name (or any other unique identifier) and assign their data to variables.

Here’s an updated version of your script to assign the chosen fighter's data:

```C#
$(document).ready(function() {

    console.log("jQuery is working!");



    let selectedFighter1 = null;  // To store the chosen Fighter 1

    let selectedFighter2 = null;  // To store the chosen Fighter 2



    $(".fighterbtn").click(function() {

        let fighterName = $(this).data("fighter-name");  // Get the name of the selected fighter

        let fighterImg = $(this).data("fighter-img");    // Get the image URL of the selected fighter



        // Find the fighter object in the fighters array by name

        let selectedFighter = fighters.find(f => f.Name === fighterName);



        // Assign the data to the corresponding variable

        if (!selectedFighter1) {

            selectedFighter1 = selectedFighter;

            console.log("Fighter 1 chosen: ", selectedFighter1);

            $("#figterContainer1 img").attr("src", selectedFighter.Img);

            $("#figterContainer1").show();

        } else if (!selectedFighter2) {

            selectedFighter2 = selectedFighter;

            console.log("Fighter 2 chosen: ", selectedFighter2);

            $("#figterContainer2 img").attr("src", selectedFighter.Img);

            $("#figterContainer2").show();

        }

    });



    $("#cancelFighter1").click(function() {

        selectedFighter1 = null;  // Clear Fighter 1's selection

        $("#figterContainer1").hide();

    });



    $("#cancelFighter2").click(function() {

        selectedFighter2 = null;  // Clear Fighter 2's selection

        $("#figterContainer2").hide();

    });



    $("#confirmFighter1").click(function() {

        if (selectedFighter1) {

            $("#confirmFighterContainer1 img").attr("src", selectedFighter1.Img);

            $("#confirmFighterContainer1").show();

            $("#figterContainer1").hide();

        }

    });



    $("#confirmFighter2").click(function() {

        if (selectedFighter2) {

            $("#confirmFighterContainer2 img").attr("src", selectedFighter2.Img);

            $("#confirmFighterContainer2").show();

            $("#figterContainer2").hide();

            alert("Fighter 1 and Fighter 2 are confirmed");

        }

    });

});
```

Explanation:

Fighter Data (selectedFighter1 and selectedFighter2):
When a fighter button is clicked, we extract the fighter-name and fighter-img data attributes.
We use Array.find() to search for the selected fighter in the fighters array (which we got from the Razor code earlier).
The selectedFighter object contains all the data for the selected fighter (like Name, Img, Health, Damage, etc.).
Depending on whether selectedFighter1 or selectedFighter2 is already assigned, we assign the selected fighter's data to the appropriate variable.
UI Changes:
When a fighter is chosen, the corresponding image is shown in the #figterContainer1 or #figterContainer2 div.
When the fighter is confirmed, the image is moved to the confirmation section (e.g., #confirmFighterContainer1).
Canceling the Selection:
The #cancelFighter1 and #cancelFighter2 buttons clear the selected fighters (selectedFighter1 and selectedFighter2), and hide the fighter containers.
Confirming the Fighters:
The #confirmFighter1 and #confirmFighter2 buttons show the confirmed fighters in their respective containers and display a message when both are confirmed.
Customizing the Variables:

You can now access the entire fighter data from selectedFighter1 and selectedFighter2. For example:

console.log(selectedFighter1.Name); // Get the name of Fighter 1

console.log(selectedFighter1.Health); // Get the health of Fighter 1

console.log(selectedFighter2.Damage); // Get the damage of Fighter 2

This will allow you to use the selected fighters' full data in other parts of your code (such as for a battle simulation or to display stats).

Let me know if you need further clarification or adjustments!

# If-else inside a click event

```C#
$("#toggleButton").click(function() {
    if ($("#box").hasClass("hidden")) {
        $("#box").removeClass("hidden").show();
    } else {
        $("#box").addClass("hidden").hide();
    }
});



if ($("#myInput").val() === "") {
    alert("Input field is empty!");
}



if ($("#myElement").is(":visible")) {
    console.log("Element is visible!");
} else {
    console.log("Element is hidden!");
}

```
