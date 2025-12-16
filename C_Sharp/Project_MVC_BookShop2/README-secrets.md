# Where to keep secrets, passwards, etc. in ASP.NET Core MVC

1.  Use appsettings.json + Secret Manager (best for development)

For ASP.NET Core apps, the recommended secure method in development is:

User Secrets file (used only for my local setting secrets, works only on my machine, not for a web for eveyone, works only in Development environment) <-- Not encrypted file(working with other people, not save, other people in your team can see the secrets can be shown in secret.json file)

- This file (secrets.json) is not stored with the rest of your project, it is on my machine only, located otside of Project folder

- Connection WebConnection string will not work in Production environment (From my secrets.json file)
- User secret file is located in separate Directory outside of Application Repository, outside of our main Project file, therefore it will never be uploaded to GiHub

To keep sensative data - User Id, Passwords, ConnectionString etc.

- Download nuget package ->

```C#
Microsoft.Extensions.Configuration.UserSecrets
```

- Then right click on (ProjectName).csproj file of your project(<--located in very bottom of the list of all files of our Project)-(step 1 from the pic below) --> then click Manage user secrets (step 2)

![pic10](https://github.com/Julian22222/PRACTICE/blob/main/C_Sharp/Project_MVC_BookShop2/IMG/pic10.jpg)

#### secret.json (file contains)

```C#
//We dont write as usual obects in secret.json file
"ConnectionStrings": {
   "DefaultConnection": "Server=.;Database=BookStore;TrustServerCert..........",
   "WebConnection": "Server=tcp:mybookstoredb.database.windows ...."



   //Insed of example above it should be written the following
   //We should write -->
 "ConnectionStrings:DefaultConnection": "Server=.;Database=BookStore;Tru...",
  "ConnectionStrings:WebConnection": "Server=tcp:mybookstoredb.d...;"

}
```

To have accessto User Secrets in our code --> we use IConfiguration

2. You can use --> .env file

- but you cannot access the values using process.env.VARIABLE_NAME (that’s Node.js syntax). ASP.NET Core has its own configuration system

ASP.NET Core does not load .env files by default. To use .env, install a NuGet package such as:

```C#
NuGet package: DotNetEnv

//install package throug terminal
//dotnet add package DotNetEnv

//or use Nuget terminal section for B=Nuget packages

//Load the .env file in Program.cs
//using DotNetEnv;
```

```C#
var builder = WebApplication.CreateBuilder(args);

Env.Load(); // loads .env from project root

var mySecret = Environment.GetEnvironmentVariable("MySecret"); //get env variable value
Console.WriteLine(mySecret);

//Your .env file
MySecret=superpassword123
ApiKey=abcdefg
```

///////////////////////////////////////////////////////////////////////////

# Azure Portal (CLOUD) provides multiple secure options for storing secrets such as passwords, database connection strings, API keys, etc.

Below are your options, from most secure → secure → less secure.

1. 🛡️ Azure Key Vault (Best & Most Secure)

Use this for:

- Database passwords
- API keys
- Connection strings
- Certificates

Key Vault is built specifically for secrets and uses:

- Hardware security modules (HSM)
- Access control (RBAC)
- Managed identities (no passwords needed)
- Auto-rotation of secrets

How you use it:

- Store the secret in Azure Key Vault
- Give your app’s identity access
- Read the secret from ASP.NET Core:

```C#
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

var connectionString = builder.Configuration["DbConnection"];
```

This is the recommended Azure enterprise method.

2. 🟢 Azure App Service Application Settings (Environment Variables)

If you're hosting on Azure App Service, you can use:

```C#
//Location of the Environment Variables in Azure:

Azure Portal → App Service → Configuration → Application Settings
```

Everything you set here becomes an environment variable.

```C#
//Example: of Azure Application Settings (Environment Variables)
//In Azure Portal → App Service → Configuration → Application Settings

Name                                  |          Value
                                      |
MySecretKey                           |     superpassword123
ConnectionStrings:DefaultConnection   |    Server=.;Database=.....;TrustServerCertificate=true;User ID=sa;Password=123456

Name: ConnectionStrings__DefaultValue
```

- ASP.NET Core automatically loads it via builder.Configuration.

```C#
//Then in code
//Access in C#:

var mySecret = builder.Configuration["MySecretKey"];
var connString = builder.Configuration.GetConnectionString("Default");

///////
//Using dependency injection
//If you want to access config in controllers/services:

public class MyService{

    private readonly IConfiguration _config;

    //Constructor
    public MyService(IConfiguration config)
    {
        _config = config;
    }

    public string GetSecret()
    {
        return _config["MySecretKey"];
    }
}
```

Why this is secure:

- Values are encrypted at rest by Azure
- Never included in source code
- Never stored in .env or appsettings.json`

🔒 Everything you store in Environment Variables:

✔️ Encrypted at rest by Azure

Azure encrypts all App Service settings automatically.

✔️ Not accessible to website visitors

Users cannot view or access these values.
They are internal to your web app’s runtime environment.

✔️ Not included in your source code or deployment

They are stored only on Azure’s secure platform.

✔️ Sent to your app as environment variables

ASP.NET Core automatically reads them into the builder.Configuration system.

3. Azure Container Apps / Azure Kubernetes Service (AKS)

4. Azure App Configuration (with Key Vault integration)
