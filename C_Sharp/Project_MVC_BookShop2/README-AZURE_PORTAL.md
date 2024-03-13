# DOCUMENT CONTAINS INFO ABOUT AZURE PORTAL (App Services, Azure SQL Database ,Key Vault and other)

### To deploy project to App services:

1. bin\Publish folder and select Deploy to Web App
   [Click HERE](https://stackoverflow.com/questions/73800499/how-to-update-changes-to-azure-app-service-after-its-deployed-from-cli)

2. Or we can go to AZURE PORTL -> App Service --> Deployment center (on he right side)

# how to Set server firewall on Azure Portal SQL database (go to database in Azure Portal --> SQL database --> set server firewall)

1. Connect your IP firewall rules
2. To avoid HTTP 500 error , It is not allowed to access the server.Go to azure portal address and choose your database.Press"Set server firewall " and Allow Azure services and resources to access this server choose "Yes" save that page and refresh your service.Than you can see your data

#### The launchSettings.json file:

· Is only used on the local development machine.

· Is not deployed.

· Contains profile settings.

....................................................................................................................

# Clinet side Validation

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
- we can create Ajax-form
- we add jquery.unobtrusive-ajax.js library to \_Layoot.cshtml file, add it in the bottom
- can add some logic with ajax
