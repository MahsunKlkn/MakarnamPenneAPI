# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY API/API.sln ./
COPY API/API/API.csproj API/
COPY API/Business/Business.csproj Business/
COPY API/Core/Core.csproj Core/
COPY API/DataAccessLayer/DataAccessLayer.csproj DataAccessLayer/
COPY API/Entities/Entities.csproj Entities/
COPY API/Tool/Tool.csproj Tool/

# Restore dependencies
RUN dotnet restore API.sln

# Copy all source code
COPY API/. .

# Build and publish
WORKDIR /src/API
RUN dotnet publish API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Final runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
