FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Dovecord.Orleans/Dovecord.Orleans.csproj", "Dovecord.Orleans/"]
RUN dotnet restore "src/Dovecord.Orleans/Dovecord.Orleans.csproj"
COPY . .
WORKDIR "/src/Dovecord.Orleans"
RUN dotnet build "Dovecord.Orleans.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dovecord.Orleans.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dovecord.Orleans.dll"]
