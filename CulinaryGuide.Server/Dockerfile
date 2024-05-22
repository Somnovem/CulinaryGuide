FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /backendApp
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY CulinaryGuide.Server/CulinaryGuide.Server.csproj CulinaryGuide.Server/
RUN dotnet restore CulinaryGuide.Server/CulinaryGuide.Server.csproj
COPY . .
WORKDIR /src/CulinaryGuide.Server

# Install Node.js and npm
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y nodejs

# Navigate to the client directory and clean up node_modules and package-lock.json
WORKDIR /src/culinaryguide.client
RUN rm -rf node_modules package-lock.json

# Install npm dependencies
RUN npm install

# Build the client app
RUN npm run build

# Navigate back to the server directory
WORKDIR /src/CulinaryGuide.Server

RUN dotnet build CulinaryGuide.Server.csproj -c Release -o /backendApp

FROM build AS publish
RUN dotnet publish CulinaryGuide.Server.csproj -c Release -o /backendApp

FROM base AS final
WORKDIR /backendApp
COPY --from=publish /backendApp .
ENTRYPOINT ["dotnet", "CulinaryGuide.Server.dll"]
