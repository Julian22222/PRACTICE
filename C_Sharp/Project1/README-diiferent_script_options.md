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

    string Fighter1Logo = "";
    string Fighter2Logo = "";
}
<h1 class="header">Mortal Combat TEST .....</h1>
<hr>

<h4 >SELECT YOUR FIGHTER</h4>

<section>
@foreach(var fighter in fighters){

@* assign to --> data-fighter-name="@fighter.Name" *@
@* assign to --> data-fighter-Img ="@fighter.Img" *@
<button class="card-btn" data-fighter-name="@fighter.Name" data-fighter-Img ="@fighter.Img" onclick="selectFighter(this)">
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

@* When choose fighter1 --> will add new button to cancel figter *@
<div id="newButtonContainer1" style="display: none; margin-top: 20px;">
    @* <img src="https://www.pngkey.com/png/full/114-1149878_cancel-button-cancel-button-png.png" alt="fighter logo" width="50" height="50"/> *@
    <img src="@Fighter1Logo" alt="fighter1 logo" width="100" height="100"/>
    <br/>
    <button id="cancelFighter1" class="btn btn-primary" onclick="cancelFighter1(this)">Choose Another Fighter</button>
</div>





@* When choose fighter2 --> will add new button to cancel figter *@
<div id="newButtonContainer2" style="display: none; margin-top: 20px; float: right;">
    @* <img src="https://www.pngkey.com/png/full/114-1149878_cancel-button-cancel-button-png.png" alt="fighter logo" width="50" height="50"/> *@
    <img src="@Fighter2Logo" alt="fighter2 logo" width="100" height="100"/>
    <br/>
    <button id="cancelFighter2" class="btn btn-primary" onclick="cancelFighter2(this)">Choose Another Fighter</button>
</div>






<script>

    function selectFighter(button) {

        // Get the fighter name from the data attribute

        var fighterName = button.getAttribute('data-fighter-name'); //from line 35 we use this assignments
        var fighterLogo = button.getAttribute("data-fighter-Img");  //from line 35 we use this assignments

        console.log("Selected Fighter: " + fighterName);
        console.log("Selected Img: " + fighterLogo);


        // You can assign the fighter name to a variable here if needed
        var selectedFighter = fighterName;


        // Update the image source dynamically from Fighter 1
        var fighterImgElement1 = newButtonContainer1.querySelector("img");
        fighterImgElement1.src = fighterLogo;


        // Show the new button by changing its display style
        newButtonContainer1.style.display = "block"; // Make newButtonContainer1 block visible



        @* if(@Fighter1Logo != ""){

        // Update the image source dynamically from FIGHTER 2
        var fighterImgElement2 = newButtonContainer2.querySelector("img");
        fighterImgElement2.src = fighterLogo;


            // Show the new button by changing its display style
        newButtonContainer1.style.display = "block"; // Make newButtonContainer1 block visible
        } *@


    }



    function cancelFighter1(button) {
        // Hide the new button by changing its display style
        newButtonContainer1.style.display = "none"; // Hide the new button
    }

     function cancelFighter2(button) {
        // Hide the new button by changing its display style
        newButtonContainer2.style.display = "none"; // Hide the new button
    }

</script>
```
