# Mistake

```C#
<img src="@Fighter1Logo" alt="fighter1 logo" width="50" height="50"/>
```

The issue is that Fighter1Logo is assigned a value inside the JavaScript function selectFighter, but this does not update the Razor variable @Fighter1Logo.

Why?

- Razor (@Fighter1Logo) runs on the server before the HTML is sent to the browser.
- JavaScript (Fighter1Logo) runs on the client after the page has already loaded.
- Updating a JavaScript variable does not update the Razor variable on the server.

# Another example

When I have fighter_Name1 why my if statement doesn't work:
if(fighter_Name1 != ""){....}

```C#
@using Project1.Models

@{
    ViewData["Title"] = "Game";

      var fighters = new List<Fighter>(){
         new Fighter(){Id = 1, Name = "Sub Zero", Img = "https://avatarfiles.alphacoders.com/342/342681.png", Health = 100, Damage = 15, Armor = 5, Move = 1},
         new Fighter(){Id = 2, Name = "Cyrax", Img = "https://i.pinimg.com/736x/ae/7f/d9/ae7fd946a9226f41ee51f4406535fca4.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1}
         @* new Fighter(Id = 3, Name = "Jax", Img = "https://wiki.supercombo.gg/images/thumb/9/93/Mk11-jax-splash.png/300px-Mk11-jax-splash.png", 120, 20, 10, 1),
         new Fighter(Id = 4, Name = "Johnny Cage", Img = "https://realleathergarments.co.uk/wp-content/uploads/2022/10/mortal-kombat-11-johnny-cage-leather-jackets.jpg", 120, 20, 10, 1),
         new Fighter(Id = 5, Name = "Kung Lao", Img = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/ea37168f-58bf-43ec-b98d-f15c62f681dd/dejxak9-1ffe21e4-be80-4daf-af83-c79200197322.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcL2VhMzcxNjhmLTU4YmYtNDNlYy1iOThkLWYxNWM2MmY2ODFkZFwvZGVqeGFrOS0xZmZlMjFlNC1iZTgwLTRkYWYtYWY4My1jNzkyMDAxOTczMjIucG5nIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.ASR_eQtVUQxJhi6bbVPiqez-auVFpE0jf6opjjBNzIU", 120, 20, 10, 1),
         new Fighter(Id = 6, "Liu Kang", Img = "https://i2-prod.mirror.co.uk/gaming/article30056626.ece/ALTERNATES/s1200c/0_MK1.jpg", 120, 20, 10, 1),
         new Fighter(Id = 7, "Scorpion", Img = "https://www.sideshow.com/storage/product-images/9029351/scorpion-hellfire-mkx_mortal-kombat_square.jpg", 120, 20, 10, 1),
         new Fighter(Id = 8, "Reptile", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0GSyAA4NBy1Ax2pw6Ji4GmdrY5XF7bI8Wqg&usqp=CAU", 120, 20, 10, 1),
         new Fighter(Id = 9, "Jade", "https://assetsio.reedpopcdn.com/mortal-kombat-11-jade.png?width=1200&height=1200&fit=crop&quality=100&format=png&enable=upscale&auto=webp", 120, 20, 10, 1),
         new Fighter(Id = 10, "Raiden", "https://www.sideshow.com/storage/product-images/907661/raiden_mortal-kombat_square.jpg", 120, 20, 10, 1),
         new Fighter(Id = 11, "Sonya", "https://www.sideshow.com/storage/product-images/911183/sonya-blade_mortal-kombat_square.jpg", 120, 20, 10, 1),
         new Fighter(Id = 12, "Shang Tsung", "https://assetsio.reedpopcdn.com/i-love-mortal-kombat-11-but-it-has-a-serious-custom-variations-problem-1561035900240.jpg?width=1200&height=1200&fit=crop&quality=100&format=png&enable=upscale&auto=webp", 120, 20, 10, 1), *@

        };



}
<h1 class="header">Mortal Combat TEST .....</h1>
<hr>

<h4 >SELECT YOUR FIGHTER</h4>

<section>
@foreach(var fighter in fighters){

<button class="fighterbtn" class="card-btn" data-fighter-name="@fighter.Name" data-fighter-Img ="@fighter.Img" >
    <div class="card-container">

        <ul >
            <li class="img-container">
                <p class="fighter-name"><b>
                @fighter.Name
                </b></p>
              <img src="@fighter.Img" alt="Fighter logo" width="150" height="150"/>
            </li>
        </ul>
    </div>
</button>
    }
    </section>

@* When choose fighter1 --> will add new button to cancel figter  *@
    <div id="figterContainer1" style="display: none; margin-top: 20px;">
    @* <img src="https://www.pngkey.com/png/full/114-1149878_cancel-button-cancel-button-png.png" alt="fighter logo" width="50" height="50"/> *@
        <img alt="fighter1 logo" width="100" height="100"/>
        <br/>
        <button id="cancelFighter1" class="btn btn-primary" >Return</button>
        <br/>
        <button id="confirmFighter1" class="btn btn-primary">confirm Fighter</button>
    </div>

    @* Confirmed fighter1 block *@
    <div id="confirmFighterContainer1" style="display: none; margin-top: 20px;">
        <h2 id= "headerofConfirmedFighetr1"></h2>
        <img alt="fighter1 logo" width="150" height="150"/>
        <hr style="width: 150px; color: red;"/>
    </div>


    @* Fighter2 block *@
    @* When choose fighter1 --> will add new button to cancel figter  *@
    <div id="figterContainer2" style="display: none; margin-top: 20px;">

        @* <img src="https://www.pngkey.com/png/full/114-1149878_cancel-button-cancel-button-png.png" alt="fighter logo" width="50" height="50"/> *@
        <img alt="fighter2 logo" width="100" height="100"/>
        <br/>
        <button id="cancelFighter2" class="btn btn-primary" >Return</button>
        <button id="confirmFighter2" class="btn btn-primary">confirm Fighter</button>
    </div>



    @* Confirmed fighter2 block *@
    <div id="confirmFighterContainer2" style="display: none; margin-top: 20px; margin-left: 100% ;">
        <h2 id= "headerofConfirmedFighetr2"></h2>
        <img alt="fighter2 logo" width="150" height="150"/>
        <hr style="width: 120px; color: red;"/>
     </div>


<script>

$(document).ready(function() {

    console.log("jQuery is working!");

    let fighter_Img1;
    let fighter_Name1 ="";

    let fighter_Img2;
    let fighter_Name2;





    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////Choose fighter1
    if(fighter_Name1 == ""){
        $(".fighterbtn").click(function() { // Use class instead of ID

            console.log("pre- Fighter1 Name", fighter_Name1)
            console.log("Fighter1 button is clicked!");

            fighter_Img1 = $(this).data("fighter-img"); // Get image URL from data attribute
            fighter_Name1 = $(this).data("fighter-name"); // Get Name from data attribute

            // Example: Set the image inside a container when button is clicked
            $("#figterContainer1 img").attr("src", fighter_Img1);


            $("#figterContainer1").show(); // Show the container

            console.log("Fighter1 Name", fighter_Name1)
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////cancelFighter1 function

        $("#cancelFighter1").click(function() {
            $("#figterContainer1").hide(); // Hide the container
        });

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////confirmFighter1 function
        $("#confirmFighter1").click(function() {
            $("#figterContainer1").hide(); // Hide the container

            @* let confirmedImg = $("#figterContainer1 img").attr("src"); // Get selected image src
            let fighterName = $(this).data("fighter-name"); // Get image Name from data attribute *@

            console.log("FROM confirm fighter 1",fighter_Name1);

            $("#confirmFighterContainer1 img").attr("src", fighter_Img1); // Set image in confirmed container
            $("#confirmFighterContainer1").fadeIn(2000).show(); // Show confirmed fighter block

            $("#headerofConfirmedFighetr1").text(fighter_Name1);

            $("#confirmFighterContainer1").append("<p>Figter 1 SELECTED!</p>");

            @* alert("Fighter 1 is confirmed"); *@
        });
    }


    ////////////////////////////////////////////////////////////////////////////////if figterContainer1 confirmed choose fighter2
    if(fighter_Name1 != "") { //<----------------------------------------------------------------------------------------------------------HERE is error

        console.log("Fighter2 button is clicked!");

        $(".fighterbtn").click(function() { // Use class instead of ID

        fighter_Img2 = $(this).data("fighter-img"); // Get image URL from data attribute
        fighter_Name2 = $(this).data("fighter-name"); // Get Name from data attribute
        // Example: Set the image inside a container when button is clicked

        console.log("FROM choose fighter 2",fighter_Name1)
        console.log("FROM choose fighter 2",fighter_Name2);


        $("#figterContainer2 img").attr("src", fighter_Img2);

        $("#figterContainer2").show(); // Show the container

        });

         //////////////////////////////////////////////////////////////////////////////////////////////////////////////cancelFighter1 function

        $("#cancelFighter2").click(function() {
        $("#figterContainer2").hide(); // Hide the container
        });


        /////////////////////////////////////////////////////////////////////////////////////////////////////confirm figter 2
        $("#confirmFighter2").click(function() {
            $("#figterContainer2").hide(); // Hide the container

            @* let confirmedImg = $("#figterContainer2 img").attr("src"); // Get selected image src *@

            console.log(fighter_Name2);
            $("#confirmFighterContainer2 img").attr("src", fighter_Img2); // Set image in confirmed container
            $("#confirmFighterContainer2").fadeIn(2000).show();  // Show confirmed fighter2 block
            $("#confirmFighterContainer1").show();  // Show confirmed fighter1 block

            $("#headerofConfirmedFighetr2").text(fighter_Name2);

            $("#confirmFighterContainer2").append("<p>Figter 2 SELECTED!</p>");

            @* alert("Fighter 1 and Fighter 2 are confirmed"); *@
        });
    }


    if($("#confirmFighterContainer1").is(":visible") && ("#confirmFighterContainer2").is(":visible")){

        alert("Fighter 1 and Fighter 2 are confirmed");

    }



});
</script>

```

Your if (fighter_Name1 != "") condition is outside of any event listener and runs only once when the script is initially executed. At that point, fighter_Name1 is an empty string (""), so the condition evaluates to false, and the fighter selection logic inside the if statement never runs.

Fix:
Instead of checking fighter_Name1 globally, move the logic for choosing fighter 2 inside the confirmFighter1 click event. That way, after confirming Fighter 1, the logic for Fighter 2 becomes active.

Modify your confirmFighter1 click function to enable Fighter 2 selection

```C#
$("#confirmFighter1").click(function() {
    $("#figterContainer1").hide(); // Hide Fighter 1 selection container

    console.log("FROM confirm fighter 1", fighter_Name1);

    $("#confirmFighterContainer1 img").attr("src", fighter_Img1); // Set confirmed image
    $("#confirmFighterContainer1").fadeIn(2000).show(); // Show confirmed fighter 1 block
    $("#headerofConfirmedFighetr1").text(fighter_Name1);
    $("#confirmFighterContainer1").append("<p>Fighter 1 SELECTED!</p>");

    // Now enable Fighter 2 selection
    $(".fighterbtn").off("click").on("click", function() {
        fighter_Img2 = $(this).data("fighter-img"); // Get image URL from data attribute
        fighter_Name2 = $(this).data("fighter-name"); // Get Name from data attribute

        console.log("FROM choose fighter 2", fighter_Name2);

        $("#figterContainer2 img").attr("src", fighter_Img2);
        $("#figterContainer2").show(); // Show Fighter 2 selection container
    });
});

// Cancel Fighter 2 selection
$("#cancelFighter2").click(function() {
    $("#figterContainer2").hide(); // Hide the container
});

// Confirm Fighter 2
$("#confirmFighter2").click(function() {
    $("#figterContainer2").hide(); // Hide the container

    console.log("FROM confirm fighter 2", fighter_Name2);
    $("#confirmFighterContainer2 img").attr("src", fighter_Img2); // Set image in confirmed container
    $("#confirmFighterContainer2").fadeIn(2000).show();  // Show confirmed fighter2 block
    $("#headerofConfirmedFighetr2").text(fighter_Name2);
    $("#confirmFighterContainer2").append("<p>Fighter 2 SELECTED!</p>");

    if ($("#confirmFighterContainer1").is(":visible") && $("#confirmFighterContainer2").is(":visible")) {
        alert("Fighter 1 and Fighter 2 are confirmed!");
    }
});
```

Why this works:

- When the page loads, fighter_Name1 is an empty string, so checking if (fighter_Name1 != "") globally does nothing.
- Instead, we activate the Fighter 2 selection only after Fighter 1 is confirmed by moving the logic inside $("#confirmFighter1").click(...).
- .off("click").on("click", function() { ... }) ensures that clicking a fighter button after confirming Fighter 1 will now select Fighter 2 instead of overwriting Fighter 1.
