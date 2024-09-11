<div align="center">
  <h1>Cramick Homework</h1>
</div>

## Features
- User registration using email
- Email confirmation
- Configurable user Login based on email confirmation
- Configurable password requirements
- Reset password using email
- Get list of all Users in paged and sortable table
- Create new User
- View Profile of the currently logged in user
- Get list of Contacts in paged and sortable table
- Create new Contact
- Update Contact
- Delete Contact
 
## Dependencies
For Backend only non Microsoft libraries are listed:

- Autofac 10.0.0
- AutoMapper 13.0.1
- Ben.Demystifier 0.4.1
- FluentValidation 11.9.2
- MediatR 12.4.1
- MySql.EntityFrameworkCore 8.0.5
- NSwag.AspNetCore 14.1.0
- SendGrid 9.29.3
- Serilog.AspNetCore 8.0.2
- Swashbuckle.AspNetCore 6.7.3
 
For Frontend only non Angular libraries are listed:

- @ng-bootstrap/ng-bootstrap: "^16.0.0"
- @popperjs/core: "^2.11.8"
- bootstrap: "^5.3.3"
- bootstrap-icons: "^1.11.3"
- jest-editor-support: "*"
- ngx-toastr: "^18.0.0"
- run-script-os: "*"
- rxjs: "~7.8.0"
- tslib: "^2.3.0"
- zone.js: "~0.14.3"

## Install
Install the [ASP.NET Core Runtime 8.0.5](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or if you have SDK of the same version you don't need any additional installations.
You can check which versions you have installed by running the `dotnet --info` command

In order to run the Angular 17 client application you will need minimum version 18.13 of [Node.js](https://nodejs.org/en/download/package-manager/current).

No other installations of libraries are necessary since the startup scripts will do it.


## Setup and Configuration
All configuration is done in CramickHomework.Server **appsettings.json**
First you need to configure the connection string for the MySQL Server database in **DefaultConnection**

```json
"ConnectionStrings": {
  "DefaultConnection": "server=[YOUR-SERVER];uid=[YOUR-USER];pwd=[YOUR_PASSWORD];database=[YOUR_DATABASE]"
}
```

Then you would temporary enter the credentials for the Administrator user that will be created when executing the initial Entity Framework migration that will create CramickHomework database structure and seed initial data. 

```json
"Identity": {
  // This section can be deleted after database has been initialized 
  // (write email and password somewhere else though)
  "Administrator": {
    "Id": "c8d598de-6e02-482f-bea6-c7e0b0c6ea7c",
    "Name": "Administrator",
    "Email": "cramick.homework.admin@cramick-it.com",
    "Password": "$up3RseCr37pwd!"
  }
}
```

Next you need to execute the EF migration using the .NET Core CLI commands.
You can check if you have the neccessary tool by running
```bash
dotnet ef
```

If you don't have the dotnet-ef tool installed you can do it by running: 
```bash
dotnet tool install --global dotnet-ef
```
Go to the **.\src\CramickHomework.Infrastructure** directory and execute:
```bash
dotnet ef database update
```
After that you can delete the **Administrator** section in the **appsettings.json** of the **CramickHomework.Server** project.
You can use Administrator credentials to log in or register a new account using your email.
```json
"Administrator": {
    "Id": "c8d598de-6e02-482f-bea6-c7e0b0c6ea7c",
    "Name": "Administrator",
    "Email": "cramick.homework@cramick-it.com",
    "Password": "$up3RseCr37pwd!"
}
```

You can configure the Microsoft Identity options in the following section of **appsettings.json**:
```json
"Identity": {
  "SignIn": {
    "RequireConfirmedEmail": true
  },
  "Password": {
    "RequireNonAlphanumeric": true,
    "RequireLowercase": true,
    "RequireUppercase": true,
    "RequiredLength": 6
  }
}
```
Configuration of the level of exception details returned by the API by setting the:
```json
"UseDeveloperExceptions": false,
"HideSystemExceptionMessages": false
```
If **UseDeveloperExceptions** is set to true the complete exception with stack trace will be sent in error responses which is good during development, but is not very user friendly. Default settings will be fine for this purpose.


You can completelly customize the Serilog logging configuration, but I suggest you change the `"path": "C:/LogFiles/api/cramick-server-.log"` if this default doesn't suit you.
```json
"Serilog": {
  "MinimumLevel": {
    "Default": "Debug",
    "Override": {
      "Microsoft": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "System": "Warning"
    }
  },
  "WriteTo": [
    {
      "Name": "Console",
      "Args": {
        "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
        "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{User}-{RequestId}-{Address}] [{Level}] [thread:{ThreadId}] [{SourceContext}] {Message}{NewLine}{Exception}"
      }
    },
    {
      "Name": "Logger",
      "Args": {
        "configureLogger": {
          "WriteTo": [
            {
              "Name": "File",
              "Args": {
                "path": "C:/LogFiles/api/cramick-server-.log",
                "rollingInterval": "Day",
                "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{AppVersion}] [{User}-{Address}] [{Level}] [thread:{ThreadId}] [{SourceContext}] {Message}{NewLine}{Exception}"
              }
            }
          ]
        }
      }
    }
  ]
}
```
## Running the application
After everything is installed and configured you need to go to **.\src\CramickHomework.Server** directory and execute:
```bash
dotnet run --lauch-profile Default
```
The application will get and install all necessary libraries and execute build and start of both Server and Client applications.

The default configuration will run the server at [https://localhost:7039](https://localhost:7039) and client at [https://localhost:4200](https://localhost:4200).

Swagger can be accessed at [https://localhost:7039/swagger](https://localhost:7039/swagger)

## Disclaimer
Application does not contain any Unit tests because of the short time which I decided to put in coding additional features not requested in the assignment and writing this nice documentation.