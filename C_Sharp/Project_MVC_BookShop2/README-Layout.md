# \_layout.cshtml file code explanation

```C#
<script src="~/js/site.js" asp-append-version="true"></script>
```

- The file site.js usually contains your own scripts—for example:
  - DOM manipulation
  - Handling user input
  - AJAX calls
  - Event listeners
  - Other UI logic
- Without this line, any JavaScript you wrote in site.js won’t be executed in the browser.
- Use cache busting with asp-append-version="true"
  - Browsers cache static files like JS and CSS.
  - If you update site.js, browsers might still load the old cached version, ignoring your latest changes
  - asp-append-version="true" tells ASP.NET to append a unique version hash based on the file contents.

# GET Bootstrap connected to your ASP.Net Core MVC project

- Go to Getting started section (Left side bar): https://getbootstrap.com/docs/5.3/getting-started/introduction/
- We need to COPY – CSS file link, for a bootstrap:

```C#
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.5/dist/js/bootstrap.min.js" integrity="sha384-VQqxDN0EQCkWoxt/0vsQvZswzTHUVOImccYmSyhJTp7kGtPed0Qcx8rK9h9YEgx+" crossorigin="anonymous"></script>
```
