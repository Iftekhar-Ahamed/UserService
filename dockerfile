# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file and all referenced projects
COPY . ./

# Restore dependencies
RUN dotnet restore UserService.sln

# Build and publish the WebAPI project
RUN dotnet publish User.WebAPI/User.WebAPI.csproj -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "User.WebAPI.dll"]
