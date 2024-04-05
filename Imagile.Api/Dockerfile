#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY [".certificates/aspnetapp.pfx", "https/aspnetapp.pfx"]
COPY ["Imagile.Api/Imagile.Api.csproj", "Imagile.Api/"]
COPY ["Imagile.Data.Shared/Imagile.Data.Shared.csproj", "Imagile.Data.Shared/"]
COPY ["Imagile.Data.Storage/Imagile.Data.Storage.csproj", "Imagile.Data.Storage/"]
COPY ["Imagile.Data.Company/Imagile.Data.Company.csproj", "Imagile.Data.Company/"]
COPY ["Imagile.Domain/Imagile.Domain.csproj", "Imagile.Domain/"]
RUN dotnet restore "./Imagile.Api/Imagile.Api.csproj"
COPY . .
WORKDIR "/src/Imagile.Api"

RUN dotnet build "./Imagile.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Imagile.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/https/aspnetapp.pfx ./https/aspnetapp.pfx

RUN ls -la
ENTRYPOINT ["dotnet", "Imagile.Api.dll"]