#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HO.FamilyTicketTracker.API/HO.FamilyTicketTracker.API.csproj", "HO.FamilyTicketTracker.API/"]
RUN dotnet restore "./HO.FamilyTicketTracker.API/HO.FamilyTicketTracker.API.csproj"
COPY . .
WORKDIR "/src/HO.FamilyTicketTracker.API"
RUN dotnet build "./HO.FamilyTicketTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HO.FamilyTicketTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HO.FamilyTicketTracker.API.dll"]