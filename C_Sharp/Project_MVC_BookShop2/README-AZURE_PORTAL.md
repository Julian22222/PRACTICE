# DOCUMENT CONTAINS INFO ABOUT AZURE PORTAL (App Services, Azure SQL Database ,Key Vault and other)

# To Deploy ASP.Net web application in Azure (preparing and creating all resources to deploy)

1. go to --> [Azure Portal](https://portal.azure.com/)
   Create App Services

2. install Azure App Service extension in VS code

3. in correct folder of our app write in terminal -->

```C
   dotnet publish -c Release -o ./bin/Publish
```

4. In VS Code our app, Right click the bin\Publish folder and select Deploy to Web App...

5. Once the deployment is finished, click Browse Website to validate the deployment

..............................................................................................................

### To deploy project to App services in AZURE PORTAL:

1. bin\Publish folder and select Deploy to Web App -->
   [Click HERE](https://stackoverflow.com/questions/73800499/how-to-update-changes-to-azure-app-service-after-its-deployed-from-cli)

2. Or we can go to AZURE PORTL -> App Service --> Deployment center (on he right side) <-- Deploy from GitHub

-

# how to Set server firewall on Azure Portal SQL database (go to database in Azure Portal --> SQL database --> set server firewall)

1. Connect your IP firewall rules
2. To avoid HTTP 500 error , It is not allowed to access the server.Go to azure portal address and choose your database.Press"Set server firewall " and Allow Azure services and resources to access this server choose "Yes" save that page and refresh your service.Than you can see your data

....................................................................................................................

# Azure Services to use to deploy your App.--> [Click Here](https://www.youtube.com/watch?v=EKqXAMLsnKQ&list=PLR-Buy35u4SH0lmQhmlpf2gnX5TxCjUNH&index=13)

1. Subscriptions (Azure subscription)

- What package you use and how mach you pay for using Azure services

2. Resource group

- works as a bag where we put all used, needed services for our app in 1 Resource group. (App Services, SQL databases,SQL server,Key Vault, Log Analytics workspace, App Service Plan) <-- all of them must be in the same Resource group for particular applicatiopn

3. App Services

- allow you build and deploy your app.

4. SQL databases
5. SQL server
6. Key Vault [How to use Key Vault for your secrets](https://www.youtube.com/watch?v=ZXfuxisC0IA&t=1s)

- Creates KeyVaultURL <--copy - paste it to - to use in appsettings.json and in Program.cs for Our secrets /// (Vault URL <- in Key Vault)
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

....................................................................................................................

# Client side Validation

- Without Client side validation we always hit(connect to) our Back-End Server, to get the response (to check that all the data is correctly filled). When we click post button - all the time request comes to the server even when fields are filled incorrectly. Then Validation is taking place on the server and send a response back to the client. in each request we hit the server to validate the data.
- With Client side validation we can validate (check that all the data is correctly filled) all the data without connecting to our server, without sending a request to the server.

- to work with client side validations we need libraries:

1. jQuery.js
2. JQuery.validate.js
3. jQuery.validate.unobtrusive.js

- If we use these 3 libraires we can enable client side validations automatically from the server side validations,
  so you don't need to write your logic again. Whatever code you have written in the Model class as an attribute those attributes will be available as an Client side validation automatically.

- We can use already build in libraries in the project or use CDN

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
