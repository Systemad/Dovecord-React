This is a Single Page App (SPA) chat application made with React and ASP.NET Core following the principles of Vertical Slice Architecture with CQRS.

NOTE: The project is in a very (very) early state!

## Technologies

* [React with Typescript](https://reactjs.org/)
* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [Docker](https://www.docker.com/)
* [NUnit](https://nunit.org/)
* [FluentAssertions](https://fluentassertions.com/)

## Getting it running

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Clone the project and cd into it
4. Create EF migrations inside Dovecord project (scroll down)
5. Go through Local Configuration step below
6. Navigate to `src/Dovecord/dovecord-react` and run `yarn install`
7. Navigate to `src/Dovecord/dovecord-react` and run `yarn start` to launch the front end (Angular)
8. Navigate to `src/Dovecord` and run `dotnet run` to launch the backend (ASP.NET Core Web API)

### Local Configuration
The project is configured to use HTTPS and SSL certificate by default. If the certificate does not exist, it will create one automatically upon start.
The path for choosing the certificate must be edited in `.env.development.local`.

To turn this behavior off, remove `.env.development.local`, and remove `prestart` section in package.json.

### Docker Configuration (Not added yet)
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
InMemory Database is used by default in testing/debugging.
For production, the project is configured to use InMemory database for production by default, but alternatively can be switched to PostgreSQL in appsettings.json.

To use `dotnet-ef` for your migrations you need to cd into src/Dovecord project and run the following;
`dotnet ef migrations add "InitialMigration"`
`dotnet ef database update`

## Overview

### Dovecord
Contains the backend written in ASP.NET

### Dovecord-react
Contains React project, seperated from backend 
## License

Coming soon
### TODO:

## Credits
* JasonTaylorDev's [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
* [Discord like design from](https://github.com/gabrielfernans/discord-ui)
* [Typescript generator for Open API](https://github.com/hosseinmd/swagger-typescript)
