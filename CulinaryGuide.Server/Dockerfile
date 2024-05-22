FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /backendApp
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY CulinaryGuide.Server/CulinaryGuide.Server.csproj CulinaryGuide.Server/
RUN dotnet restore CulinaryGuide.Server/CulinaryGuide.Server.csproj
COPY . .
WORKDIR /src/CulinaryGuide.Server

RUN dotnet build CulinaryGuide.Server.csproj -c Release -o /backendApp

FROM build AS publish
RUN dotnet publish CulinaryGuide.Server.csproj -c Release -o /backendApp

FROM base AS final
WORKDIR /backendApp
COPY --from=publish /backendApp .
ENTRYPOINT ["dotnet", "CulinaryGuide.Server.dll"]
