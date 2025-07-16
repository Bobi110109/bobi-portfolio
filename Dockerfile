# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /sr

# This stage is used to publish the service project to be copied to the final stage
COPY ["BobbyPortfolio.csproj", "./"]
RUN dotnet restore "BobbyPortfolio.csproj"

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BobbyPortfolio.dll"]
# Build and publish
RUN dotnet publish "BobbyPortfolio.csproj" -c Release -o /app/publish
