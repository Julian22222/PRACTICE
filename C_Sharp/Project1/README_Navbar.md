```C#
//Insert this NavBar into _Layout.cshtml file inside <body> tag



    <header class="w-100 p-3">
        <span class="logo">ASP.NET CORE Projects </span>

         @* button section for the phone -placing your content of navbar automatically on the phene, to make your app responsive*@
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

@*
        <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
            <ul class="navbar-nav flex-grow-1">
                <li class="nav-item ">
                    <a class="nav-link text-dark fw-bold" href="/">Home</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Game">Game</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Gallery">Gallery</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Ajax">Ajax</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Calculator">Calculator</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Vending">Machine</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/Contacts">Contacts</a>
                </li>

                 <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" href="/home/Test">Test</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" asp-controller="home" asp-action="Form">Form</a>
                </li>

                <li class="nav-item">
                    <a class="nav-link text-dark fw-bold" asp-protocol="http" asp-host="google.com" asp-controller="" asp-action="">Google.com</a>
                </li>
            </ul>
        </div> *@





        @* ///////////////////////////////////////////////////////////////////// *@

        <nav >
            <a href="/">Home</a>
            <a href="/home/Game">Game</a>
            <a href="/home/Gallery">Gallery</a>
            <a href="/home/Ajax">Ajax</a>
            <a href="/home/Calculator">Calculator</a>
            <a href="/home/Vending">Machine</a>
            <a href="/Contacts">Contacts</a>
            <a href="/my-draft">Draft</a>
            <a href="/home/Sketch">Sketch</a>
            <a asp-controller="home" asp-action="Form">Form</a>
            <a asp-protocol="http" asp-host="google.com" asp-controller="" asp-action="">Google.com</a>
        </nav>

    </header>
```
