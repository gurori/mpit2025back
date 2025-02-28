FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY ["./mpit/mpit.csproj", "./mpit/"]
RUN dotnet restore "./mpit/mpit.csproj"

COPY . .
RUN dotnet publish "./mpit/mpit.csproj" -c Release -o out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "mpit.dll"]