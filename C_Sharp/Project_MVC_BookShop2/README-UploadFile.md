# How to upload files in the app

- Uploadeding Images, image is a havy file and instead of saving everything all details in the database, we can save these type of files in some other place. We can save this files into our application in any of the folders. We use wwwroot folder to use all static files in our app. And we can use the same folder to store uploaded images for this application. But we will store the path to particular file (to correct image) in the database. And when we need to display correct image we can get the URL from the database and then we can disply it in the UI.

### Steps to create and upload files to our app

1. In Models/ Book.cs we create proporty, and this property will hold all ditails about uploaded file -->

```C#
public IFormFile CoverPhoto { get; set; }
```

- We use special data type-->

```C#
IFormFile //<-- this data type will allow to upload any file to our app, this file can contain all details about this uploaded particular file

```

- IFormFile is available in the

```C#
Microsoft.AspNetCore.Http
```

2. In AddNewBook.cshtml file we insert uploading template block from Bootstrap (Bootstrap Uploading block)[https://getbootstrap.com/docs/4.3/components/forms/#file-browser]

```C#
//we use this code from Bootstrap

<div class="custom-file">
  <input type="file" class="custom-file-input" id="customFile">
  <label class="custom-file-label" for="customFile">Choose file</label>
</div>
```

and insert this code in the View ( --> SEE AddNewBook.cshtml file (line 123 - 137))

```C#
    <div class="form-group">
            @* asp-for="Title"  input element for Title property for our Book Model  *@
            <label asp-for="CoverPhoto" class="control-lebel"></label>

//code block from bootstrap
        <div class="custom-file">
        <input asp-for="CoverPhoto" class="form-control" id="customFile">
        <label class="custom-file-label" for="customFile">Choose file</label>
        </div>
//////////////////


            @* to show an error msg when field in the form is not valid (use only--> span tag ) *@
            <span asp-validation-for="BookPdf" class="text-danger"></span>
        </div>
```

3. If we are planning to Upload some files in our Form tag, then we need to this attribute -->

```C#
//we use this attribute --> enctype="multipart/form-data" in the Form tag

 <form method="post" enctype="multipart/form-data" asp-controller="Book" asp-action="AddNewBook">
```

4. Then in Controller ( --> See BookController.cs) line 115- 136

- we create folders --> wwwroot/books/cover
- we write the code in Controller to save uploaded files (images) in our app, (In wwwroot/books/cover folder)

```C#
  // to save uploaded cover photo in the wwwroot/books/cover we write this code -->
        if(book.CoverPhoto != null){  //<--if book.Coverphoto is not empty we do this code

            string folder ="books/cover/";  //path to folder where we store uploaded photos
            // if we deploy this app on a server (using the folder path only)then we will get an error (because this folder (path) is not accessable,or this folder (path) is not available because we need a server actual path to store images in this app --> therefore we need to use this parametr --> IWebHostEnvironment webHostEnvironment (it contains all details about environment), in contructor we create it and then assign to _webHostEnvironment)

            //add uploaded img file Name to the path --> book.CoverPhoto.FileName;
            //also we need to avoid errors when upload images with the same name, make the img files name unique -> + Guid.NewGuid().ToString()
            folder += Guid.NewGuid().ToString() + "_" + book.CoverPhoto.FileName;

            // assign folder variable to CoverImageUrl property, / <--must be added in front of folder to display an image from database
            book.CoverImageUrl = "/" + folder;

            // we need server path to store these imgs in this application(we need to use IWebHostEnvironment dependency injection)
            // Define the path for a server of the actual folder where we keep imgs, add the server path + folder (join server path and folder)
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);  // <-- this function combine 2 variables --> _webHostEnvironment.WebRootPath  +  folder. This serverFolder variable will allow server to save the file using our path to correct folder

            // we need to save the copy of the full img path in wwwroot/books/cover,
            // new FileStream(serverFolder <- the server path)
            await book.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        }

```

### To save uploaded file path in database and display that file in UI

5. we create new property in Database Books table --> in Data / Books.cs model

```C#
// property to store uploaded img file  - full path
public string CoverImageUrl { get; set; }
```

Then we can add changes and update the database -->

```bash
dotnet ef migrations add (AnyMigrationsName) //to add changes to database
```

```bash
dotnet ef database update  //to update database
```

- Then we add new property to Book Model -->

```C#
// uploaded image full path
public string? CoverImageUrl {get; set;}
```

- to save the path to database, we don't need entire path of the file (from serverFolder variable --> line 86), we need only the path that is available in wwwwroot folder (from folder variable --> line 79)
- To store the path in database to correct file we need only the path that is stored in folder variable --> line 79

6. Then in Repository (BookRepository.cs) we write -->

```C#
public async Task<int> AddNewBook(Book model){


// new varible
var newBook = new Books(){
// assign all proporties from received model(data from form) to our proporties in the table
// id -will be assign to it automatically to newBook object
    Title = model.Title,
    Author = model.Author,
    Description = model.Description,
    Category = model.Category,
    LanguageId = model.LanguageId,

    // if model.TotalPages>HasValue(contains some value) return it value, otherwise return 0
    TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,

    //full path to uploaded img folder -->(wwwroot/books/cover)
    CoverImageUrl = model.CoverImageUrl,  //--> line 82, where we assign BookPdfUrl with correct URL path
    BookPdfUrl = model.BookPdfUrl
};

// we add newBook to our database -> _context -> in Books2 table
await _context.Books2.AddAsync(newBook);

// then we need save changes, otherwise application won't hit the database ( async method)
await _context.SaveChangesAsync();

return newBook.Id;
    }
```

7. in View (--> See GetAllBooks.cshtml)

```C#
 <img src="@book.CoverImageUrl" alt="image logo" class="img-thumbnail" />
```

or in -bookThumbnail.cshtml

```C#
 <img src="@(string.IsNullOrEmpty(Model.CoverImageUrl) ? "/img/340719-200.png" : Model.CoverImageUrl)" alt="image logo" class="img-thumbnail" />
```
