# DOCUMENT CONTAINS INFO ABOUT AZURE PORTAL (App Services, Azure SQL Database ,Key Vault and other)

### To deploy project to App services:

1. bin\Publish folder and select Deploy to Web App
   [Click HERE](https://stackoverflow.com/questions/73800499/how-to-update-changes-to-azure-app-service-after-its-deployed-from-cli)

2. Or we can go to AZURE PORTL -> App Service --> Deployment center (on he right side)

# how to Set server firewall on Azure Portal SQL database (go to database in Azure Portal --> SQL database --> set server firewall

1. Connect your IP firewall rules
2. To avoid HTTP 500 error , It is not allowed to access the server.Go to azure portal address and choose your database.Press"Set server firewall " and Allow Azure services and resources to access this server choose "Yes" save that page and refresh your service.Than you can see your data

#### The launchSettings.json file:

· Is only used on the local development machine.

· Is not deployed.

· Contains profile settings.
