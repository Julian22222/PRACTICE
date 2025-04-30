# DOCUMENT CONTAINS INFO ABOUT AZURE PORTAL (App Services, Azure SQL Database ,Key Vault and other)

# To Deploy ASP.Net web application in Azure (preparing and creating all resources to deploy)

### First option how to Deploy your project to Azure

- go to --> [Azure Portal](https://portal.azure.com/) and Create App Services

- install Azure App Service extension and Azure Account extension in VS code

- To deploy new app or deploy new changes, go to correct folder of our app write in terminal-->

```C#
   dotnet publish -c Release -o ./bin/Publish
```

- In VS Code our app, Right click the bin\Publish folder and select Deploy to Web App...

- Then automatically Comand Palette will appear (Terminal Search bar on the top ) --> Choose your Project name there

- first time in Comand Palette --> LogIn through Comand Palette

- if Comand Palette is blank and doesn't show any options --> close VS Code and open again

- Once the deployment is finished, click Browse Website to validate the deployment

### Second option how to Deploy your project to Azure

- go to --> [Azure Portal](https://portal.azure.com/) and Create App Services
- install Azure App Service extension and Azure Account extension in VS code
- From the Left Menu bar find Azure tab in VS Code
- Open correct Subscription (Where is your project)--> Right Click on correct App Services (Name of your project that you are deploying) --> from the menu choose --> Deploy to Web App.
- during first deployment I need select Browse (where is my Application compiles)
  [Check here](https://www.youtube.com/watch?v=4BwyqmRTrx8&t=363s)

............................................................................................................

If we want to added tables or/and new properties to the Database. Doesn't matter Local database and/or to Web Application (to Azure portal) the process is the same:

### Connection string full path allows to connect with that Database which is mentioned in the connection string and allows to add tables and properties that we indicated in Data folder/Context DB connection file and Data/Models --> to that DB that is mentioned in connecion string.

### Key Vault --> doesn't have connection string full path, therefore can't add needed tables and properties to the correct Database.

Process step by step:

- We will update only that database that is mentioned in ConnectionString full path --> can be in User Secrets file /or Variable environment /or in appsettings.json (don't keep web Connection string in the appsettings.json). Example below-->

```C#
//Connection string from User Secrets file / appsettings.json file (For example)
"ConnectionStrings:DefaultConnection": "Server=.;Database=BookStore;TrustServerCertificate=true;User ID=sa;Password=julik3322J!"
```

- Then we use that connection in Data/ Context Database connection file, or in Program.cs file -->

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
```

and

```C#
//For PRODUCTION ENVIRONMENT WE DON'T HAVE ACTUAL CONNECTION STRING HERE WITH KEY VAULT, THEREFORE IT CANNOT UPDATE THE WEB DATABASE IN THE AZURE, WE HAVE TO PUT --> FULL CONNECTION STRING PATH FROM USER SECRETS file or FROM ENVIRONMENT VARIABLES TO CONNECT / UPDATE / ADD ALL THE TABLES AND PROPERTIES (FOR A FIRST TIME WHEN YOU CONNECT TO THE WEB DB or YOU WANT TO ADD SOME PROPERTIES OR/AND UPDATE THE FUNCTIONALITY OF THE APP)


//THIS EXAMPLE WILL NOT WORK, WILL NOT ADD ALL NEEDED TABLES AND PROPERTIES TO CORRECT DATABASE,
//WILL NOT CONNECT / UPDATE / ADD NEW PROPETIES,FUNCTIONS , (WHEM YOU CONNECT FIRST TIME TO WEB DB USE FULL CONNECTION STRING PATH),
//KEY VALT DOESN'T PROVIDE FULL CONNECTION STRING PATH
if(builder.Environment.IsProduction())
{
var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

var credential = new ClientSecretCredential(keyVaultDirectoryID.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());

builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);

builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString()));
}
```

- Instead of KEY VAULT OPTION we need to add -->

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //Where DefaultConnection will be full Connection string path to Web DATABASE FROM USER SECRETS FILE /ENVIRONMENT VARIABLES, TO ADD ALL THE TABLES AND PROPERTIES TO WEB APPLICATION DB
}
```

```C#
//Full Connection String path

"Server= api of your database here;Database=NameOfYourDatabase;TrustServerCertificate=true;User ID=sa(userName);Password=PasswordForDatabase"
```

- Then in terminal we put

```C#
 dotnet ef migrations add (AnyMigrationsName) //to add changes to database + add migrations folder to the project

dotnet ef migrations add init   //<--Example
```

```C#
      dotnet ef database update  //to update database
```

- Once we added all needed tables and properties to the Web DB ,Then we can put KEY VAULT option back for Production environment,as it is
- AND leave Development option as normal , with connection string to local databse

```C#
if(builder.Environment.IsDevelopment())
{
builder.Services.AddDbContext<MyBookStoreWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //change DefaultConnection variable to be a full connection string path to local DB
}
```

- Then -->

# To Deploy ASP.Net web application in Azure (preparing and creating all resources to deploy)

- go to --> [Azure Portal](https://portal.azure.com/)
  Create App Services

- install Azure App Service extension in VS code

- To deploy new app or deploy new changes, go to correct folder of our app write in terminal-->

```C#
   dotnet publish -c Release -o ./bin/Publish
```

- In VS Code our app, Right click the bin\Publish folder and select Deploy to Web App...

- Then automatically Comand Palette will appear (Terminal Search bar on the top ) --> Choose your Project name there

- first time in Comand Palette --> LogIn through Comand Palette

- if Comand Palette is blank and doesn't show any options --> close VS Code and open again

- Once the deployment is finished, click Browse Website to validate the deployment

..............................................................................................................

### To deploy project to App services in AZURE PORTAL:

1. bin\Publish folder and select Deploy to Web App -->
   [Click HERE](https://stackoverflow.com/questions/73800499/how-to-update-changes-to-azure-app-service-after-its-deployed-from-cli)

2. Or we can go to AZURE PORTL -> App Service --> Deployment center (on he right side) <-- Deploy from GitHub

-

# how to Set server firewall on Azure Portal SQL database (go to database in Azure Portal --> SQL database --> set server firewall)

![pic1](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic1.jpg)

- Connect your IP firewall rules
- To avoid HTTP 500 error , It is not allowed to access the server.Go to azure portal address and choose your database.Press"Set server firewall " and Allow Azure services and resources to access this server choose "Yes" save that page and refresh your service.Than you can see your data

Go to SQL sever, to allow connect to our database from our local computer (see picture below) (adding new Firewall rule to allow your IP address to connect Azure). Why it is helpful? -> when you created your DB in Azure Portal you may want to connect from your IP adress (from your local machine) to Azure SQL Database in Azure. It will be blocked if you don't make these 4 steps. will show error 500,

Go to Networking (Step 1)

Fill fields (Step 2 and 3 ), Step 3 - is your IP address,

Tick Step 4 and SAVE

....................................................................................................................

# Azure Services to use to deploy your App.-->

[YouTube How to Deploy to Azure](https://www.youtube.com/watch?v=EKqXAMLsnKQ&list=PLR-Buy35u4SH0lmQhmlpf2gnX5TxCjUNH&index=13)

[Microsoft Azure](https://portal.azure.com/#home)

1. Subscriptions (Azure subscription) // all Projects can go under one subscription (--> Azure subscription 1, in our case)

- What package you use and how mach you pay for using Azure services

2. Resource group (Create new Resource Group for each project)

- works as a bag where we put all used, needed services for our app in 1 Resource group. (App Services, SQL databases,SQL server,Key Vault, Log Analytics workspace, App Service Plan) <-- all of them must be in the same Resource group for particular applicatiopn

3. App Services

- allow you build and deploy/host your app (web apps, mobile back end and RESTful APIs)
- When creating new app services --> create Web App ( Not a Web App + Database)
  [App Services](https://www.youtube.com/watch?v=4BwyqmRTrx8&t=363s)

4. SQL server

- allow to create Server name, Server user ID (In Azure Portal --> Server admin login) and Server Password

5. SQL databases

- allow to get the connection string to DB

6. Key Vault [How to use Key Vault for your secrets](https://www.youtube.com/watch?v=ZXfuxisC0IA&t=1s)

- Creates KeyVaultURL <--copy - paste it to - to use in appsettings.json and in Program.cs for Our secrets /// (Vault URL <- in Key Vault)
- KeyVault Client Secret value expires after some time --> will not connect to your Database , Need to Renew Client Secret for KeyVault in Microsoft Azure/ to Update --> Azure/App registrations/All applications --> choose your project name --> Certificates & secrets --> Create new Client Secret value here
- Located in Azure cloud, this is Azure service where we can store Secrets
- help to keep secrets that are encreapted,
- Here we insert our Secrets (Connection String etc.)
- To work with Key Vault in ASP.NET MVC Core we neeed few nuget packages
  dotnet add package (PackageName)

      1. Azure.Identity
      2. Azure.Security.KeyVault.Secrets
      3. Microsoft.Extensions.Configuration.AzureKeyVault

7. App registrations

- We register our application that Azure Id and Key Vault, all know that we have an application that what to grab these things and they are goood to go and authorised

- Creates ClientId, DirectoryID, ClientSecret(Secret value) <-- copy - paste it to use in appsettings.json and in Program.cs for Our secrets

# Debug or find an Error when your project is hosted on Azure Portal

1. [Azure LogIn](https://azure.microsoft.com/en-gb/get-started/azure-portal)
2. Click on App Services of the project that you want to debug
3. Diagnose and solve problems (Left menu column)
4. Click on --> Application Logs (bottom of the page)

....................................................................................................................

# Client side Validation

- Without Client side validation we always hit(connect to) our Back-End Server, to get the response (to check that all the data is correctly filled). When we click post button - all the time request comes to the server even when fields are filled incorrectly. Then Validation is taking place on the server and send a response back to the client. in each request we hit the server to validate the data.
- With Client side validation we can validate (check that all the data is correctly filled) all the data without connecting to our server, without sending a request to the server.

- to work with client side validations we need libraries:

1. jQuery.js
2. JQuery.validate.js
3. jQuery.validate.unobtrusive.js

- If we use these 3 libraires we can enable client side validations automatically from the server side validation,
  so you don't need to write your logic again. Whatever code you have written in the Model class as an attribute those attributes will be available as an Client side validation automatically.
- To use these libraries we import them in `C# _Layout.cshtml ` in the bottom of the file (line 69 - 72)

```JS
//THE ORDER OF LIBRARIES IS IMPORTANT!!!!!

<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

- We can use already build in libraries in the project or use CDN
- - to use these libraries in our app --> we can use build in libraries from wwwroot/lib/ or we can use CDN
- to disable client-ide validation (if you want to debug your code) --> (--> See Program.cs file, line 47)

# Server side Validation

- imlimenting this by using Model validation attributes in Model classes
- this attributes are available in --> System.ComponentModel.DataAnnotations;

# We need to use both side validations in our projects

# To work with Ajax in ASP.NET Core

- we need 2 libraries: JQuery.js and JQuery.unobtrusive-ajax.js
- we add jquery.unobtrusive-ajax.js library to \_Layoot.cshtml file, add it in the bottom
- can add some logic with ajax
- we can create Ajax-form - we add ajax methods to form tag -->

```C#
 <form method="post" data-ajax="true" data-ajax-complete="myComplete" data-ajax-success="mySuccess" data-ajax-loading="#myLoader" ...>
```

then in View and in the bottom of the same file we write functions -->

```C#
@section scripts {
    <script>
        function myComplete(data){
            alert("I am from complete");
        }

        function mySuccess(data){
            alert("I am from Success");
        }

        function myFailure(data){
            alert("I am from Failure");
        }
    </script>
```

..................................................................................................................
