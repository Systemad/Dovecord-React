This is a Single Page App (SPA) chat application made with Angular and ASP.NET Core following the principles of Vertical Slice Architecture with CQRS.

NOTE: The project is in a very (very) early state!

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [Angular 12](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/)
* [Docker](https://www.docker.com/)

## Getting it running

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
4. Clone the project and cd into it
5. Create EF migrations inside WebUI project (scroll down)
6. Navigate to `src/WebUI/ClientApp` and run `npm install`
7. Navigate to `src/WebUI/ClientApp` and run `npm start` to launch the front end (Angular)
8. Navigate to `src/WebUI` and run `dotnet run` to launch the back end (ASP.NET Core Web API)

### Docker Configuration

In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert.
You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0) that describe the steps required for Windows, macOS, and Linux.

For Windows:
The following will need to be executed from your terminal to create a cert
`dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

NOTE: When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

FOR Linux:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`

In order to build and run the docker containers, execute `docker-compose -f 'docker-compose.yml' up --build` from the root of the solution where you find the docker-compose.yml file.  You can also use "Docker Compose" from Visual Studio for Debugging purposes.
Then open http://localhost:5000 on your browser.

To disable Docker in Visual Studio, right-click on the **docker-compose** file in the **Solution Explorer** and select **Unload Project**.

### Database Migrations

The database is configured to use PostgreSQL in production, and InMemory Database in testing/debugging.
To use `dotnet-ef` for your migrations you need to cd into WebUI project and run the following;

`dotnet ef migrations add "InitialMigration"`
`dotnet ef database update`

## Overview

### TODO: 


### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 6. (IN TODO)

## License

Coming soon

## Credits
JasonTaylorDev for inspiration from https://github.com/jasontaylordev/CleanArchitecture
wrapt for inspiration https://wrapt.dev/
