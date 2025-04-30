# How to upload files in the app

- Uploadeding Images, image is a havy file and instead of saving everything all details in the database, we can save these type of files in some other place. We can save this files into our application in any of the folders. We use wwwroot folder to use all static files in our app. And we can use the same folder to store uploaded images for this application. But we will store the path to particular file (to correct image) in the database. And when we need to display correct image we can get the URL from the database and then we can disply it in the UI.

### Steps to create and upload files to our app

1. In Models/ Book.cs we create proporty, and this property will hold all ditails about uploaded file -->(Uploaded file,FileName, etc.)

```C#
public IFormFile CoverPhoto { get; set; }
```

- We use special data type-->

```C#
IFormFile //<-- this data type will allow to upload any file to our app, this file contain all details about uploaded particular file, This IFormFile file contains uploaded file (image in this case), File name (image name), etc.

```

- IFormFile is available in the

```C#
Microsoft.AspNetCore.Http
```

2. In AddNewBook.cshtml file we insert uploading template block from Bootstrap [Bootstrap Uploading block](https://getbootstrap.com/docs/4.3/components/forms/#file-browser)

```C#
//we use this code from Bootstrap

<div class="custom-file">
  <input type="file" class="custom-file-input" id="customFile">  //<--type="file"  , to upload some files
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
        <input asp-for="CoverPhoto" class="form-control" id="customFile">  //<--don't need type="file" because we bind to property with IFormFile datatype, it is automaticaly create type="file"
        <label class="custom-file-label" for="customFile">Choose file</label>
        </div>
//////////////////


            //to show an error msg when field in the form is not valid (use only--> span tag )
            <span asp-validation-for="BookPdf" class="text-danger"></span>
        </div>
```

3. If we are planning to Upload some files in our Form using Form tag, then we need to this attribute -->

```C#
//we use this attribute --> enctype="multipart/form-data" in the Form tag

 <form method="post" enctype="multipart/form-data" asp-controller="Book" asp-action="AddNewBook">
```

4. we create folders, where we will store uploaded cover Photos --> wwwroot/books/cover

5. Then in Controller ( --> See BookController.cs, Post method) line 115- 136

- we write the code in Controller to save uploaded files (images) in our app, (In wwwroot/books/cover folder)

```C#
  // to save uploaded cover photo in the wwwroot/books/cover we write this code -->
        if(book.CoverPhoto != null){  //<--if book.Coverphoto is not empty we do this code

            string folder ="books/cover/";  //path to folder where we store uploaded photos
            // if we deploy this app on a server (using the folder path only)then we will get an error (because this folder (path) is not accessable,or this folder (path) is not available because we need a server actual path to store images in this app --> therefore we need to use this parametr --> IWebHostEnvironment webHostEnvironment (it contains all details about environment), in contructor we create it and then assign to _webHostEnvironment) , therefore we use line 87

            //add uploaded img file Name to the path --> book.CoverPhoto.FileName;
            //also we need to avoid errors when upload images with the same name, make the img files name unique -> + Guid.NewGuid().ToString()
            folder += Guid.NewGuid().ToString() + "_" + book.CoverPhoto.FileName;
            //Guid.NewGuid().ToString() <--should be first and then to be file name ,to don't lose -> .jpg in the end of folder URL name

            // assign folder variable to CoverImageUrl property, / <--must be added in front of folder to display an image from database
            book.CoverImageUrl = "/" + folder; //<-saving CoverPhoto path to the variable(we don't use serverFolder variable, to save path in database we use only path from wwwroot folder and we don't need full path using environment variable --> _webHostEnvironment), we use --> "/" picture to be visible in UI

            // To make it work, we need server path to store(Save) uploaded imges in this application When app is deployed in global WEB not localy,(we need to use IWebHostEnvironment dependency injection)
            // Define the path for a server of the actual folder where we keep imgs, add the server path + folder (join server path and folder)
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);  // <-- this function combine 2 variables --> _webHostEnvironment.WebRootPath  +  folder. This serverFolder variable will allow server to save the file using our path to correct folder


            //this line needs to save Image to wwwroot/books/cover, in our Application
            // we save image (this is uploaded image-->book.CoverPhoto) and make a copy of the full img path in wwwroot/books/cover,
            // new FileStream(serverFolder <- the server path)
            await book.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        }

```

### To save uploaded file path in database and display that file in UI

6. we create new property in Database Books table --> in Data / Books.cs model

```C#
// property to store uploaded img file  - path from wwwroot folder, but not full path (serverpath --> in BookController.cs line 133)
public string CoverImageUrl { get; set; }
```

Then we can add changes and update the database -->

```bash
dotnet ef migrations add (AnyMigrationsName) //to add changes to database
```

```bash
dotnet ef database update  //to update database
```

7. Then we add new property to Book Model --> (Model/Book.cs )

```C#
// uploaded image full path, optional property
public string? CoverImageUrl {get; set;}
```

### to save the path to database, we don't need entire path of the file (from serverFolder variable --> line 88), we need only the path that is available in wwwwroot folder (from folder variable --> line 80)

### To store the path in database to correct file we need only the path that is stored in folder variable --> line 80

8. Then in Repository (BookRepository.cs) we write -->

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
    CoverImageUrl = model.CoverImageUrl,  //--> line 82, where we assign BookPdfUrl with correct URL path ,   //<-- we could pass data using parametre / ViewBag and assign to  CoverImageUrl,  or with new property as we done it here in this example
    BookPdfUrl = model.BookPdfUrl
};

// we add newBook to our database -> _context -> in Books2 table
await _context.Books2.AddAsync(newBook);

// then we need save changes, otherwise application won't hit the database ( async method)
await _context.SaveChangesAsync();

return newBook.Id;
    }
```

9. in Repository (BookRepository.cs) we Update GetAllBooks method and GetBookById method

10. in View (--> See GetAllBooks.cshtml)

```C#
 <img src="@book.CoverImageUrl" alt="image logo" class="img-thumbnail" />
```

or in -bookThumbnail.cshtml

```C#
 <img src="@(string.IsNullOrEmpty(Model.CoverImageUrl) ? "/img/340719-200.png" : Model.CoverImageUrl)" alt="image logo" class="img-thumbnail" />
```

...............................................................................................................

# Upload multiple Images

- one book can have many images in their gallery (one to many relationship)

1. Create new folder in --> wwwroot/books/gallery <-- will store all images for particular book in here
2. we cretae a function for all uploaded files in our app --> (See BookController.cs line 184)
   This function receive 2 parametrs --> folder path (where to save file) and file with all info about that uploaded file

```C#
// function, can pass different variable as arguments in this function
private async Task<string> UploadFile(string folderPath, IFormFile file){
    folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

    return "/" + folderPath;
}
```

3. To deal with multiple images in Model/Book.cs we can use --> we add property

```C#
[Required]  //<--mandatory field
public IFormFileCollection GalleryFiles { get; set; }  //<-- this file is inherited from IEnumerable<IFormFile>
```

or

```C#
public List<IFormFile> GalleryFiles { get; set; }
```

or

```C#
public IEnumerable<IFormFile> GalleryFiles { get; set; }
```

4. In View Form file we add input

```C#

<div class="form-group">
//asp-for="Title"  input element for Title property for our Book Model
    <label asp-for="GalleryFiles" class="control-lebel"></label>

        //code block from bootstrap , (4 lines)
        <div class="custom-file">
        <input asp-for="GalleryFiles" class="form-control" id="customFile">
        <label class="custom-file-label" for="customFile">Choose file</label>
        </div>


        //to show an error msg when field in the form is not valid (use only--> span tag ) *@
            <span asp-validation-for="GalleryFiles" class="text-danger"></span>
        </div>
```

5. In Models create new class , each uploaded picture will have Id, Name and URL path, we create new class to store URL of each gallery image of particular book to databse

```C#
public class GalleryModel
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string URL {get;set;}
}
```

6. In Model/Book.cs we add new property, using this proprty --> each Book can have multiple Gallery images, relationship one to many

```C#
public int Id { get; set; }
public string Title { get; set; }
public string Author { get; set; }
public string Description { get; set; }
public string Category { get; set; }
public int LanguageId { get; set; }  //store Id of our language
public string? Language { get; set; }
public int? TotalPages { get; set; } // when you put -> ? -> this field is  - not Required
public IFormFile CoverPhoto { get; set; } // IFormFile allow to upload files
public string? CoverImageUrl {get; set; }  // uploaded image full path

//this new property,  using this proprty --> each Book can have multiple Gallery images
public List<GalleryModel> Gallery {get; set;}
```

7. In BookController.cs file (AddNewBook post method)

```C#
[HttpPost] //this method works by clicking -->add book (posting new book) , POST method (attribute)
public async Task<IActionResult> AddNewBook(Book book){ //book <--is the data coming from AddNewBook.cshtml filled form

if (ModelState.IsValid) //if all fields of form is valid ,it will give = true
    {

if(book.GalleryFiles != null){

string folder = "books/gallery/";

bookModel.Gallery = new List<GalleryModel>();

foreach (var file in book.GalleryFiles){ //<--book.GalleryFiles vontain all uploaded images for gallery for particular book

    var gallery = new GalleryModel(){
        Name = file.FileName,
        URL = await UploadImage(folder, file)
    };
    bookModel.Gallery.Add(gallery);
            }

        }

    }
}
```

8. Create new table in database --> Data/BookGallery

```C#
public int Id {get;set;}  //<--when created ,this field will be filled automatically
public int BookId {get;set;}  //<---when created ,this field will be filled automatically, many Image URL can be linked to one Book, one book cam have multiple images
public string Name {get;set;}
public string URL {get;set;}
public Books Book {get;set;}  //<--connection to Books table, in database
```

9. Use this table in Context class

```C#
public DbSet<BookGallery> BookGallery {get;set;}
```

10. In Data/Books class we add property --> relationship between Books table and BookGallery table, one to many relationship

```C#
public ICollection<BookGallery> bookGallery {get;set;} //<--one book can have multiple images, one to many relationship
```
