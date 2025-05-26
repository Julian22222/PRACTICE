```C#
//JS logic - No bootstrap implemented

@using Project1.Models

@{
    ViewData["Title"] = "Game";
    @* ViewData["DisableBootstrap"] = true; // Disable Bootstrap for this view *@

      var fighters = new List<Fighter>(){
         new Fighter(){Id = 1, Name = "Sub Zero", Img = "/IMG/game/subzero.jpg", Health = 130, Damage = 25},
         new Fighter(){Id = 2, Name = "Cyrax", Img = "/IMG/game/cybex.jpg", Health = 140, Damage = 25},
         new Fighter(){Id = 3, Name = "Jax", Img = "/IMG/game/jax.jpg", Health = 150, Damage = 25},
         new Fighter(){Id = 4, Name = "Johnny Cage", Img = "/IMG/game/jonny1.jpg", Health = 120, Damage = 23},
         new Fighter(){Id = 5, Name = "Kung Lao", Img = "/IMG/game/lao.jpg", Health = 120, Damage = 24},
         new Fighter(){Id = 6, Name = "Liu Kang", Img = "/IMG/game/liukang.jpg", Health = 120, Damage = 24},
         new Fighter(){Id = 7, Name = "Scorpion", Img = "/IMG/game/scorpion.jpg", Health = 130, Damage = 25},
         new Fighter(){Id = 8, Name = "Reptile", Img = "/IMG/game/reptile.jpg", Health = 130, Damage = 25},
         new Fighter(){Id = 9, Name = "Jade", Img = "/IMG/game/jade.jpg", Health = 110, Damage = 22},
         new Fighter(){Id = 10, Name = "Raiden", Img = "/IMG/game/raiden.jpg", Health = 140, Damage = 26},
         new Fighter(){Id = 11, Name = "Sonya", Img = "/IMG/game/sonya.jpg", Health = 115, Damage = 22},
         new Fighter(){Id = 12, Name = "Baraka", Img = "/IMG/game/baraka.jpg", Health = 120, Damage = 25},
         new Fighter(){Id = 13, Name = "Kitana", Img = "/IMG/game/kitana.jpg", Health = 115, Damage = 21},
         new Fighter(){Id = 14, Name = "Shao Kahn", Img = "/IMG/game/shaukan.jpg", Health = 150, Damage = 30},
         new Fighter(){Id = 15, Name = "Shang Tsung", Img = "/IMG/game/shangsung1.jpg", Health = 130, Damage = 210},
         @* new Fighter(){Id = 1, Name = "Sub Zero", Img = "/IMG/subzero.jpg", Health = 100, Damage = 15, Armor = 5, Move = 1},
         new Fighter(){Id = 2, Name = "Cyrax", Img = "/IMG/cybex.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 3, Name = "Jax", Img = "/IMG/jax.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 4, Name = "Johnny Cage", Img = "/IMG/jonny1.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 5, Name = "Kung Lao", Img = "/IMG/lao.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 6, Name = "Liu Kang", Img = "/IMG/liukang.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 7, Name = "Scorpion", Img = "/IMG/scorpion.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 8, Name = "Reptile", Img = "/IMG/reptile.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 9, Name = "Jade", Img = "/IMG/jade.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 10, Name = "Raiden", Img = "/IMG/raiden.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 11, Name = "Sonya", Img = "/IMG/sonya.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 12, Name = "Baraka", Img = "/IMG/baraka.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 13, Name = "Kitana", Img = "/IMG/kitana.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 14, Name = "Shao Kahn", Img = "/IMG/shaukan.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1},
         new Fighter(){Id = 15, Name = "Shang Tsung", Img = "/IMG/shangsung1.jpg", Health = 120, Damage = 20, Armor = 10, Move = 1}, *@
        };



}

@* Main Header *@
<div class="container text-center my-4 position-relative">

    <h1 class="display-4">Mortal Kombat</h1>
    <hr>
    <h4 class="mb-4">SELECT YOUR FIGHTER</h4>

    @* Empty left and right containers *@
    <div class="empty_left_container"></div>
    <div class="empty_right_container"></div>

    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4 justify-content-center">
        @foreach (var fighter in fighters)
        {
            <div class="col">
                <button class="btn p-0 border-0 fighterbtn w-100 h-100 text-start"
                        data-fighter-name="@fighter.Name"
                        data-fighter-img="@fighter.Img"
                        data-fighter-health="@fighter.Health"
                        data-fighter-damage="@fighter.Damage">
                    <div class="card h-100">
                        <div class="card-body text-center">
                            <h5 class="card-title">@fighter.Name</h5>
                            <img src="@fighter.Img" alt="Fighter logo" class="img-fluid" style="max-height: 150px;">
                        </div>
                    </div>
                </button>
            </div>
        }
    </div>
</div>



@* <script @Url.Action("actionName", "controllerName")> *@
@* </script> *@

<script>

$(document).ready(function() {

     var audioFight = new Audio('/sounds/fight-deep-voice-172194.mp3');
     var audioSwords = new Audio('/sounds/swords-collide-230574.mp3');
     var audioFighterSelection = new Audio('/sounds/fighter-selection.mp3');
     var audioFighterSelection2 = new Audio('/sounds/fighter-selection.mp3');    arguments //the same sound for both fighters,#battle_btn different name top don'text interfare
     //#confirmFighter1 is interfering with #confirmFighter2, it could be preventing the event from properly triggering. if we have the same name of sound .


    console.log("jQuery is working!");

    let fighter_Img1;
    let fighter_Name1;
    let fighter_Health1;
    let fighter_Damage1;

    let fighter_Img2;
    let fighter_Name2;
    let fighter_Health2;
    let fighter_Damage2;





    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////Choose fighter1

        $(".fighterbtn").click(function() { // Use class instead of ID


            console.log("Fighter1 button is clicked!");

            fighter_Img1 = $(this).data("fighter-img"); // Get image URL from data attribute,#battle_btn extracts the data-fighter-Img attribute value.
            fighter_Name1 = $(this).data("fighter-name"); // Get Name from data attribute
            fighter_Health1 = $(this).data("fighter-health"); // Get Health from data attribute
            fighter_Damage1 = $(this).data("fighter-damage"); // Get Damage from data attribute

            // Example: Set the image inside a container when button is clicked
            $("#figterContainer1 img").attr("src", fighter_Img1);  // sets the src of the <img> inside #figterContainer1.


            $("#figterContainer1").show(); // Show the container

            console.log("Fighter1 Name", fighter_Name1);
            console.log("Fighter1 Health", fighter_Health1);
            console.log("Fighter1 Damage", fighter_Damage1);
        });

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////cancelFighter1 function

        $("#cancelFighter1").click(function() {
            $("#figterContainer1").hide(); // Hide figterContainer1 container
        });

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////confirmFighter1 function
        $("#confirmFighter1").click(function() {

            audioFighterSelection.play();

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
                fighter_Health2 = $(this).data("fighter-health"); // Get Health from data attribute
                fighter_Damage2 = $(this).data("fighter-damage"); // Get Damage from data attribute

                console.log("FROM choose fighter 2", fighter_Name2);
                console.log("Fighter2 Health", fighter_Health2);
                console.log("Fighter2 Damage", fighter_Damage2);

                $("#figterContainer2 img").attr("src", fighter_Img2); arguments // sets the src of the <img> inside #figterContainer2.
                $("#figterContainer2").show(); // Show Fighter 2 selection container
            });

        });

         //////////////////////////////////////////////////////////////////////////////////////////////////////////////cancelFighter1 function

        $("#cancelFighter2").click(function() {
        $("#figterContainer2").hide(); // Hide the container
        });


        /////////////////////////////////////////////////////////////////////////////////////////////////////confirm figter 2
        $("#confirmFighter2").click(function() {
            console.log("Confirm Fighter 2 clicked");

            audioFighterSelection2.play();

            $("#figterContainer2").hide(); // Hide the container

            @* let confirmedImg = $("#figterContainer2 img").attr("src"); // Get selected image src *@

            console.log(fighter_Name2);
            $("#confirmFighterContainer2 img").attr("src", fighter_Img2); // Set image in confirmed container
            $("#confirmFighterContainer2").fadeIn(2000).show();  // Show confirmed fighter2 block
            @* $("#confirmFighterContainer1").show();  // Show confirmed fighter1 block *@

            $("#headerofConfirmedFighetr2").text(fighter_Name2);

            $("#confirmFighterContainer2").append("<p>Figter 2 SELECTED!</p>");

            $("#battle_btn").delay(1000).show(500); // Show the battle button

            @* alert("Fighter 1 and Fighter 2 are confirmed!"); *@
        });



   $(".btl_btn").click(function() {

        $('.empty_left_container').hide();
        $('.empty_right_container').hide();
        $('#confirmFighterContainer1 p').hide();
        $('#confirmFighterContainer2 p').hide()
        $('#confirmFighterContainer1 img').animate({'height': '280px','width':'280px', 'position':'absolute', 'left': '10%', 'top':'300px'}, 500);
        $('#confirmFighterContainer2 img').animate({'height': '280px','width':'280px', 'position':'absolute', 'right': '10%', 'top':'300px'}, 500);

        audioSwords.play();

        $(".vs-block").fadeIn(900).fadeOut(500);

        setTimeout(function () {
            $(".image-container").fadeIn();
        }, 2000);


        setTimeout(function () {
            audioFight.play();
        }, 2000);


        $('section, h4, #battle_btn').hide();

        console.log("Battle is started!");

        @* alert("Battle is started!"); *@

        function fight() {
        if (fighter_Health1 > 0 && fighter_Health2 > 0) {
            fighter_Health1 -= fighter_Damage2;
            fighter_Health2 -= fighter_Damage1;

            console.log("Fighter1 Health: ", fighter_Health1);
            console.log("Fighter2 Health: ", fighter_Health2);

            setTimeout(fight, 500); // Continue the battle with a delay
        } else {
            $(".outcome-block").show();

            if (fighter_Health1 <= 0 && fighter_Health2 <= 0) {
                console.log("Both fighters are dead!");
                $("#header_outcome").text("Both fighters are dead!");
            } else if (fighter_Health1 <= 0) {
                console.log("Fighter 1 is dead!");
                $("#header_outcome").text("Fighter 2 WON!");
            } else if (fighter_Health2 <= 0) {
                console.log("Fighter 2 is dead!");
                $("#header_outcome").text("Fighter 1 WON!");
            }
        }
    }

    fight(); // Start the fight
});

$(".play-again").click(function() {
    location.reload(); // Reload the page


});


});

</script>

```

```C#
/* ///////////////////////////////////////// */
/* Game CSS */

.image-container {
  display: absolute;
  margin-left: 45%;
  margin-top: 200px;
  perspective: 1000px; /* Adds 3D effect */
  width: 200px;
  height: 200px;
}

.vs-block {
  display: flex;
  justify-content: center;
  margin-top: 200px;
}

.spin-image {
  width: 100%;
  height: auto;
  display: block;
  transform-origin: center;
  animation: spinX 2s linear infinite; /* Animation */
}

@keyframes spinX {
  from {
    transform: rotateY(0deg);
  }
  to {
    transform: rotateY(360deg);
  }
}

.card-btn {
  margin: 1px;
  margin-bottom: 4px;
}

.header {
  display: flex;
  justify-content: center;
}

.card-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.card-container img {
  border-radius: 5px;
  padding: 10px 10px 25px 10px;
}

.card-container:hover {
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
}

.img-container {
  /* display: flex;
  flex-wrap: wrap; */
  box-shadow: 0 0.2 1px 0 rgba(0, 0, 0, 0.2);
  transition: 0.3s;
  list-style-type: none;
  background: rgb(158, 158, 230);
}

.img-container button {
  border-radius: 10px;
  margin: 10px;
  border: solid #000 2px;
}

.fighter-name {
  position: absolute;
  margin-left: 15px;
  margin-top: 160px;
  color: black;
  font-size: 20px;
}

.figters-container {
  display: flex;
  justify-content: space-between;
}

.fighter1-container {
  margin-top: 20px;
}

.fighter2-container {
  margin-top: 20px;
  margin-right: 500px;
}

.btn-fight {
  margin-top: 170px;
  padding: 15px 30px;
  margin-left: 30%;
  margin-bottom: 50px;
  background-color: rgb(158, 158, 230);
  border-radius: 10px;
}

section {
  width: 70%;
  text-align: center;
  position: absolute;
  left: 0;
  right: 0;
  margin-inline: auto;
  z-index: 1;
}

.empty_left_container {
  position: absolute;
  top: 300px;
  left: 100px;
  border: black 1px solid;
  width: 200px;
  height: 300px;
}

.empty_right_container {
  position: absolute;
  top: 300px;
  right: 100px;
  border: black 1px solid;
  width: 200px;
  height: 300px;
}

#figterContainer1 {
  position: absolute;
  top: 300px;
  left: 115px;
}

#figterContainer2 {
  /* text-align: right; */
  position: absolute;
  top: 300px;
  right: 115px;
}

#confirmFighterContainer1 {
  position: absolute;
  top: 300px;
  left: 110px;
}

#confirmFighterContainer2 {
  position: absolute;
  top: 300px;
  right: 110px;
}

.allfightersblock {
  width: 70%;
  margin-left: auto;
  margin-right: auto;
  /* text-align: center; */
}

#battle_btn {
  background-color: #333;
  position: absolute;
  width: 500px;
  padding: 100px;
  top: 35%;
  left: 0;
  right: 0;
  margin-inline: auto;
  border-radius: 10px;
  z-index: 2;
}

```
